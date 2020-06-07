using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Text;

namespace OnBoardPilotSystem
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Новый экземпляр класса "Функции"
        /// </summary>
        Functions fn = new Functions();

        public Object takeofflock = new Object();   // Блокировка потока для однократного исполнения

        internal bool gpsportclosed = true;    // Статус порта GPS

        /// <summary>
        /// Инициализация компонентов формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отлавливаем подключение/отключение USB устройств. Перезапускаем программу
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (System.Reflection.TargetInvocationException)
            {

            }

            switch (m.WParam.ToInt32())
            {
                case 0x8000://новое usb подключено
                    Application.Restart();
                    break;
                case 0x8004: // usb отключено
                    Application.Restart();
                    break;
                case 0x0007: // Любое изменение конфигурации оборудования
                    Application.Restart();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Выполняется однократно, перед загрузкой основной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, System.EventArgs e)
        {
            // Выставили все триггеры в 0
            fn.TrimmerThrottel = 0;
            fn.TrimmerYaw = 0;
            fn.TrimmerRoll = 0;
            fn.TrimmerPitch = 0;
            fn.ListingUSBDic(); // Однократный запрос всех USB устройств для записи в глобальную переменную
            GPSSerialPort.BaudRate = Convert.ToInt32(GPSComboBoxBaud.Text);
            GPSDevices(AutoConnectGPSTextBox.Text); //  Автоподключение портов и проверка всех USB устройств
            ArduinoSerialPort.BaudRate = Convert.ToInt32(ArduComboBoxBaud.Text);
            ArduDevDic(AutoConnectArduTextBox.Text);
            enablePorts();  // Если после автоподключения в листвью есть доступные устройства, то пробуем открыть порты
            BlackBoxLabelCurr.Text = fn.bbRecords.ToString();
            LoadFlightPlan();   // Загружаем план полёта
        }

        /// <summary>
        /// Открываем для чтения отмеченные доступные порты
        /// </summary>
        private void enablePorts()
        {
            if (ArduListView.Items.Count != 0)
            {
                for (int Item = 0; Item < GPSListView.Items.Count; Item++)
                {
                    if (GPSListView.Items[Item].Checked)
                    {
                        try
                        {
                            if (GPSSerialPort.IsOpen)
                            {
                                gpsportclosed = true;
                                GPSSerialPort.Close();
                                GPSSerialPort.Dispose();
                            }
                            GPSSerialPort.PortName = GPSListView.Items[Item].Text;
                            GPSSerialPort.Open();
                            gpsportclosed = false;
                            GPSSerialPort.DiscardInBuffer();
                        }
                        catch (Exception)
                        {
                        }

                    }
                    if (ArduListView.Items[Item].Checked)
                    {
                        if (ArduinoSerialPort.IsOpen)
                        {
                            ArduinoSerialPort.Close();
                            ArduinoSerialPort.Dispose();
                            if (PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].Checked)
                            {
                                PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].Checked = false;
                            }
                            PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].BackColor = Color.LightCoral;
                        }
                        ArduinoSerialPort.PortName = ArduListView.Items[Item].Text;
                        ArduinoSerialPort.Open();
                        if (!PreFlightListView.Items[(int)Functions.CHECKLIST.ARDUINOENABLE].Checked)
                        {
                            PreFlightListView.Items[(int)Functions.CHECKLIST.ARDUINOENABLE].Checked = true;
                        }
                        PreFlightListView.Items[(int)Functions.CHECKLIST.ARDUINOENABLE].BackColor = Color.Lime;
                    }
                }
            }
        }

        private void YawTrackBar_Scroll(object sender, System.EventArgs e)
        {
            YawLabelCurr.Text = YawTrackBar.Value.ToString();
            PilotControlStatusLabel.Text = "Изменили курс";
            PilotControlStatusLabel.BackColor = Color.Transparent;
            sendAxes();
        }

        private void RollTrackBar_Scroll(object sender, System.EventArgs e)
        {
            RollLabelCurr.Text = RollTrackBar.Value.ToString();
            PilotControlStatusLabel.Text = "Изменили крен";
            PilotControlStatusLabel.BackColor = Color.Transparent;
            sendAxes();
        }

        private void PitchTrackBar_Scroll(object sender, System.EventArgs e)
        {
            PitchLabelCurr.Text = PitchTrackBar.Value.ToString();
            PilotControlStatusLabel.Text = "Изменили тангаж";
            PilotControlStatusLabel.BackColor = Color.Transparent;
            sendAxes();
        }

        private void ThrottleTrackBar_Scroll(object sender, System.EventArgs e)
        {
            ThrottleLabelCurr.Text = ThrottleTrackBar.Value.ToString();
            PilotControlStatusLabel.Text = "Изменили газ";
            PilotControlStatusLabel.BackColor = Color.Yellow;
            sendAxes();
        }

        private void SendButton_Click(object sender, System.EventArgs e)
        {
            if (ArduSendTextBox.Text == string.Empty)
            {
                PilotControlStatusLabel.Text = "Нечего отправлять! Поле пустое!";
                PilotControlStatusLabel.BackColor = Color.LightCoral;

            }
            else
            {
                PilotControlStatusLabel.Text = "Отправили данные в порт Arduino";
                PilotControlStatusLabel.BackColor = Color.Transparent;
                SendPortCommand(ArduSendTextBox.Text);
            }
        }

        /// <summary>
        /// При получении данных в порт Ардуины начинаем парсить строчку и в зависимости от начала выполняем
        /// определённый набор функций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArduinoSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);   // Сделаем небольшую задержку, чтоб всё успело в порт свалиться.
            try
            {
                if (ArduinoSerialPort.BytesToRead > 0)
                {
                    string incomingString = ArduinoSerialPort.ReadLine();
                    this.BeginInvoke(new SetDelegateGroupBox(GroupBoxStatus), new object[] { fn.parsingString(incomingString) });
                    this.BeginInvoke(new SetDelegateText(afterReadArduPort), new object[] { incomingString });
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// В текущем потоке записываем строковые данные на контрол отображения текста Ардуины
        /// </summary>
        /// <param name="inputString">Строка, полученная из порта ардуины</param>
        public void afterReadArduPort(string inputString)
        {
            ArduinoTextBox.Text = "Получили:" + inputString;
        }

        /// <summary>
        /// Исполняет команды после парсинга
        /// </summary>
        /// <param name="status">При получении номера команды - передаётся на исполнение</param>
        public void GroupBoxStatus(int status)
        {
            switch (status)
            {
                case -1:
                    PilotControlStatusLabel.Text = "Команда не распознана";
                    PilotControlStatusLabel.BackColor = Color.LightCoral;
                    break;
                case (int)Functions.serialInputCommands.SOBPS_OFF:
                    PilotControlGroupBox.Enabled = false;
                    PilotControlStatusLabel.Text = "Автопилот отключён";
                    PilotControlStatusLabel.BackColor = Color.Transparent;
                    if (PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].Checked)
                    {
                        PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].Checked = false;
                        PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].BackColor = Color.LightCoral;
                    }
                    break;
                case (int)Functions.serialInputCommands.SOBPS_ON:
                    PilotControlGroupBox.Enabled = true;
                    PilotControlStatusLabel.Text = "Автопилот включён";
                    PilotControlStatusLabel.BackColor = Color.Transparent;
                    if (!PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].Checked)
                    {
                        PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].Checked = true;
                        PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].BackColor = Color.Lime;
                    }
                    break;
                case (int)Functions.serialInputCommands.S:
                    PilotControlStatusLabel.Text = "Записаны данные крена, тангажа, курса";
                    PilotControlStatusLabel.BackColor = Color.Transparent;
                    break;
                case (int)Functions.serialInputCommands.z:
                    PilotControlStatusLabel.Text = "Записаны данные высоты";
                    PilotControlStatusLabel.BackColor = Color.Transparent;
                    break;
                case (int)Functions.serialInputCommands.SHUTDOWN:
                    PilotControlStatusLabel.Text = "Разряд батареи! Компьютер должен быть выключен через несколько секунд!";
                    PilotControlStatusLabel.BackColor = Color.LightCoral;
                    if (ShutDownCheckBox.Checked)
                    {
                        Process.Start("cmd", "/c shutdown /s /c \"Батарея сильно разряжена!\""); //  Отключение компьютера по разряду батареи
                        this.Close();
                    }
                    break;
                default:
                    PilotControlStatusLabel.Text = "Команда не распознана";
                    PilotControlStatusLabel.BackColor = Color.LightCoral;
                    break;
            }
        }

        // Делегат для записи текста из одного потока в другой
        private delegate void SetDelegateText(string text);
        // Делегат изменения статуса группы
        private delegate void SetDelegateGroupBox(int status);

        /// <summary>
        /// Отправляем указанную строку в порт.
        /// </summary>
        /// <param name="commandToSend">Команда для отправки. Конец и перевод строки добавляются автоматически.</param>
        private void SendPortCommand(string commandToSend)
        {
            if (ArduinoSerialPort.IsOpen)
            {
                ArduinoSerialPort.WriteLine(commandToSend);
                //ArduinoTextBox.Text += "Отправили:" + commandToSend + Environment.NewLine;
            }
            else
            {
                PilotControlStatusLabel.Text = "Проверьте доступность порта в настройках!";
                PilotControlStatusLabel.BackColor = Color.LightCoral;
            }
        }
        /// <summary>
        /// Данные получили в порт GPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!gpsportclosed)
            {
                StringBuilder GPSPortString = new StringBuilder();
                Thread.Sleep(200);   // Сделаем небольшую задержку, чтоб всё успело в порт свалиться.
                while (GPSSerialPort.BytesToRead > 0)
                {
                    int current = GPSSerialPort.ReadChar();
                    GPSPortString.Append((char)current);
                }
                try
                {
                    this.BeginInvoke(new SetLastGPS(showLastGPS), new object[] { fn.GPSParsingString(GPSPortString.ToString()) });
                }
                catch (Exception)
                {
                }
            }
        }

        // Делегат исполнения команд при поступлении данных в GPS порт
        private delegate void SetLastGPS(int item);

        /// <summary>
        /// Подтверждение обработки строк GPS (получаем каждую секунду!)
        /// </summary>
        private void showLastGPS(int copyComplited)
        {
            if (copyComplited > 0 && copyComplited <= fn.bbRecords)
            {
                if (!PreFlightListView.Items[(int)Functions.CHECKLIST.GPSENABLE].Checked)
                {
                    PreFlightListView.Items[(int)Functions.CHECKLIST.GPSENABLE].Checked = true;
                    PreFlightListView.Items[(int)Functions.CHECKLIST.GPSENABLE].BackColor = Color.Lime;
                }
                PilotControlStatusLabel.Text = string.Empty;
                PilotControlStatusLabel.BackColor = Color.Transparent;
                GPSGroupBox.Text = "Данные GPS приёмника (" + copyComplited.ToString() + ").";
                LoadFormGPSData(copyComplited - 1);
            }
            else
            {
                if (PreFlightListView.Items[(int)Functions.CHECKLIST.GPSENABLE].Checked)
                {
                    PreFlightListView.Items[(int)Functions.CHECKLIST.GPSENABLE].Checked = false;  // Чек-лист 1. Данные GPS  не поступают.
                    PreFlightListView.Items[(int)Functions.CHECKLIST.GPSENABLE].BackColor = Color.LightCoral;
                }
                PilotControlStatusLabel.Text = "Некорректно прошло копирование данных GPS";
                PilotControlStatusLabel.BackColor = Color.LightCoral;
            }
        }

        // Последняя запись чёрного ящика
        private void LoadFormGPSData(int lastGPSItem)
        {
            try
            {
                string convTime = fn.GPSData.Items[lastGPSItem].Text;
                if (convTime.Length > 5)
                {
                    UTCTimeLabel.Text = convTime[0].ToString() + convTime[1].ToString() + ":" + convTime[2].ToString() + convTime[3].ToString() + ":" + convTime[4].ToString() + convTime[5].ToString();
                }
                string convDate = fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.DATE].Text;
                if (convDate.Length > 5)
                {
                    DateLabel.Text = convDate[0].ToString() + convDate[1].ToString() + "." + convDate[2].ToString() + convDate[3].ToString() + "." + convDate[4].ToString() + convDate[5].ToString();
                }
                SatellitesLabel.Text = fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.SATELLITESUSED].Text;
                ListViewItem LastGPSData = new ListViewItem(fn.GPSData.Items[lastGPSItem].Text);  // Создали новый экземпляр итема
                while (LastGPSListView.Items.Count >= 1)    // Убрали все лишние записи из контрола
                {
                    LastGPSListView.Items[0].Remove();
                }
                string[] lastData = { 
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.LATITUDE].Text,           // Широта. Формат ddmm.mmmm
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.NSINDICATOR].Text,        // Индикатор широты. Формат N - северная, S - южная
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.LONGITUDE].Text,          // Долгота. Формат dddmm.mmmm
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.EWINDICATOR].Text,        // Индикатор долготы. Формат E - восточная, W - западная
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.MSLALTITUDE].Text,        // Высота от среднего уровня моря (MeanSeaLevel) в метрах
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.MSLAUNITS].Text,          // Единицы измерения MSLA. М - метры
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.SPEEDOVERGROUND].Text,    // Измеренная горизонтальная скорость в узлах. 1 узел = 1.852 км/ч, 0.5144 м/с, 30.86667 м/мин
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.COURSEOVERGROUND].Text,   // Курс относительно истинного севера в градусах
                                    fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.MAGNETICVARIATION].Text     // Магнитная вариация в градусах. E - восточная, W - западная
                                };
                //// Добавили все подитемы к итему
                LastGPSData.SubItems.AddRange(lastData);
                LastGPSListView.Items.Add(LastGPSData);
                LastGPSListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                if (fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.STATUS].Text == "A")   // Если данные валидны
                {
                    // Если точка старта не определена - определяем, если определена - проскакиваем
                    if (!PreFlightListView.Items[(int)Functions.CHECKLIST.BASEFIXED].Checked &&
                        FlightPlanListView.Items.Count > 0 && FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].Text == "BasePoint")
                    {
                        // Пересчитать ddmm.mmmm в dd.dddddd
                        string[] newgeo = fn.convNMEAtoGEO(fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.LATITUDE].Text, fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.LONGITUDE].Text);
                        FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[2].Text = newgeo[0]; //lat;
                        FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[1].Text = newgeo[1]; //lon;
                        PreFlightListView.Items[(int)Functions.CHECKLIST.BASEFIXED].Checked = true;
                        PreFlightListView.Items[(int)Functions.CHECKLIST.BASEFIXED].BackColor = Color.Lime;
                    }
                    GPSGroupBox.BackColor = Color.Lime;
                }
                else
                {
                    GPSGroupBox.BackColor = Color.LightCoral;
                }
                // Если план полёта загружен и определена точка старта и финиша
                if (PreFlightListView.Items[(int)Functions.CHECKLIST.BASEFIXED].Checked && PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANLOAD].Checked && !PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANCHECKED].Checked)
                {
                    fullCount();
                }
                if (fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.POSITIONFIXINDICATOR].Text != string.Empty)
                {
                    if (Convert.ToInt32(fn.GPSData.Items[lastGPSItem].SubItems[(int)Functions.GPSDataNAME.POSITIONFIXINDICATOR].Text) != 0)
                    {
                        GPSGroupBox.Text += " Позиция определена.";
                    }
                    else
                    {
                        GPSGroupBox.Text += " Позиция не определена.";
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// Ограничение на колличество записей данных GPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlackBoxTrackBar_Scroll(object sender, EventArgs e)
        {
            fn.bbRecords = BlackBoxTrackBar.Value;
            BlackBoxLabelCurr.Text = fn.bbRecords.ToString();
        }
        private void BlackBoxTrackBar_ValueChanged(object sender, EventArgs e)
        {
            fn.bbRecords = BlackBoxTrackBar.Value;
            BlackBoxLabelCurr.Text = fn.bbRecords.ToString();
        }

        /// <summary>
        /// Заполняем GPS листвью доступными портами и устройствами
        /// </summary>
        internal void GPSDevices(string autoConnectString)
        {
            bool deviceDetected = false;
            if (fn.allUSBDev != null)
            {
                foreach (KeyValuePair<string, string> FrNmDvPr in fn.allUSBDev)
                {
                    if (FrNmDvPr.Value != null && FrNmDvPr.Value.StartsWith("COM")) // Если не пустое значение и начинается с ком - наш клиент!
                    {
                        ListViewItem GPSItem = new ListViewItem(FrNmDvPr.Value);
                        GPSItem.SubItems.Add(FrNmDvPr.Key);
                        if (fn.availablePort(FrNmDvPr.Value))   // Если этот порт присутствует в списке доступных портов
                        {
                            GPSItem.BackColor = Color.Lime;
                            string[] valCollection = autoConnectString.Split(',');
                            foreach (string val in valCollection)
                            {
                                if (FrNmDvPr.Key.Contains(val.Trim()) && !deviceDetected)
                                {
                                    GPSItem.Checked = true;
                                    GPSItem.EnsureVisible();
                                    deviceDetected = true;  // Только одно устройство отмечаем галкой
                                }
                            }
                        }
                        else
                        {
                            GPSItem.BackColor = Color.LightCoral;
                        }
                        GPSListView.Items.Add(GPSItem);
                    }
                }
            }
        }

        /// <summary>
        /// Заполняем листвью Arduino доступными портами и устройствами
        /// </summary>
        internal void ArduDevDic(string autoConnectString)
        {
            bool deviceDetected = false;
            if (fn.allUSBDev != null)
            {
                foreach (KeyValuePair<string, string> FrNmDvPr in fn.allUSBDev)
                {
                    if (FrNmDvPr.Value != null && FrNmDvPr.Value.StartsWith("COM")) // Если не пустое значение и начинается с ком - наш клиент!
                    {
                        ListViewItem arduItem = new ListViewItem(FrNmDvPr.Value);
                        arduItem.SubItems.Add(FrNmDvPr.Key);
                        if (fn.availablePort(FrNmDvPr.Value))   // Если этот порт присутствует в списке доступных портов
                        {
                            arduItem.BackColor = Color.Lime;
                            string[] valCollection = autoConnectString.Split(',');
                            foreach (string val in valCollection)
                            {
                                if (FrNmDvPr.Key.Contains(val.Trim()) && !deviceDetected)
                                {
                                    arduItem.Checked = true;
                                    arduItem.EnsureVisible();
                                    deviceDetected = true;  // Отмечаем только одно устройство
                                }
                            }
                        }
                        else
                        {
                            arduItem.BackColor = Color.LightCoral;
                        }
                        ArduListView.Items.Add(arduItem);
                    }
                }
            }
        }

        /// <summary>
        /// Кнопка "Изменить порты" пока заблокирована.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePortButton_Click(object sender, EventArgs e)
        {
            ArduListView.Enabled = true;
            GPSListView.Enabled = true;
        }

        /// <summary>
        /// Выполняем при закрытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save(); // Сохранение изменённых параметров при выходе из приложения
            gpsportclosed = true;
            try
            {
                if (GPSSerialPort.IsOpen)
                {
                    GPSSerialPort.Close();
                    GPSSerialPort.Dispose();
                }
                if (ArduinoSerialPort.IsOpen)
                {
                    ArduinoSerialPort.Close();
                    ArduinoSerialPort.Dispose();
                }
            }
            catch (IOException) { }

            try
            {
                Directory.Delete(Environment.CurrentDirectory + "\\fltpln", true);
            }
            catch (DirectoryNotFoundException) { }
        }

        /// <summary>   
        /// Отправляем данные передатчика в порт
        ///    XAXIS 0	    // Для MODE3 Крен/Roll
        ///    YAXIS 1	    // Для MODE3 Тангаж/Pitch
        ///    ZAXIS 2	    // Для MODE3 Курс/Yaw
        ///    THROTTLE 3   // Газ
        ///    MODE 4	    // Rate<->Attitude Mode
        ///    AUX1 5	    // AltitudeHold<->PositionHold
        /// </summary>
        private void sendAxes()
        {
            int roll = RollTrackBar.Value + fn.TrimmerRoll;
            if (roll > 2000) roll = 2000;
            if (roll < 1000) roll = 1000;
            int pitch = PitchTrackBar.Value + fn.TrimmerPitch;
            if (pitch > 2000) pitch = 2000;
            if (pitch < 1000) pitch = 1000;
            int yaw = YawTrackBar.Value + fn.TrimmerYaw;
            if (yaw > 2000) yaw = 2000;
            if (yaw < 1000) yaw = 1000;
            int throttle = ThrottleTrackBar.Value + fn.TrimmerThrottel;
            if (throttle > 2000) throttle = 2000;
            if (throttle < 1000) throttle = 1000;
            string axes = "T" + roll.ToString() + ";" + pitch.ToString() + yaw.ToString() + ";" + throttle.ToString() + ";2000;2000";   // Отпраляем Т и позиции четырёх осей + два мода
            SendPortCommand(axes);
        }

        /// <summary>
        /// Сброс всех параметров до значений по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
        }

        /// <summary>
        /// Меняем скорость чтения порта GPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSComboBoxBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GPSSerialPort.IsOpen)
            {
                gpsportclosed = true;
                GPSSerialPort.Close();
                GPSSerialPort.Dispose();
            }
            GPSSerialPort.BaudRate = Convert.ToInt32(GPSComboBoxBaud.Text);
            enablePorts();
        }

        /// <summary>
        /// Меняем скорость чтения порта Arduino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArduComboBoxBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ArduinoSerialPort.IsOpen)
            {
                ArduinoSerialPort.Close();
                ArduinoSerialPort.Dispose();
                PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].Checked = false;
                PreFlightListView.Items[(int)Functions.CHECKLIST.AUTOPILOTISON].BackColor = Color.LightCoral;
            }
            ArduinoSerialPort.BaudRate = Convert.ToInt32(ArduComboBoxBaud.Text);
            enablePorts();
        }

        #region Flight Control  // Третья закладка на форме

        /// <summary>
        /// Загрузка полётного плана
        /// </summary>
        private void LoadFlightPlan()
        {
            string[] kmzFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.kmz");
            if (kmzFiles.Length == 0)
            {
                return; // Если ни одного файла не найдено, то выходим
            }
            // Создаём временную директорию для полётного плана
            Directory.CreateDirectory(Environment.CurrentDirectory + "\\fltpln");
            // Разархивируем первый попавшийся kmz файл
            using (ZipFile zip = ZipFile.Read(kmzFiles[0]))
                foreach (ZipEntry kmlFile in zip)
                {
                    try
                    {
                        kmlFile.Extract(Environment.CurrentDirectory + "\\fltpln");
                    }
                    catch (Exception)
                    {
                    }
                }
            // Проверяем есть ли в архиве файл с полётным планом
            if (File.Exists(".\\fltpln\\doc.kml"))
            {
                XmlDocument kmlFile = new XmlDocument();
                kmlFile.Load(".\\fltpln\\doc.kml");
                XmlNodeList labelPlacemark = kmlFile.GetElementsByTagName("Placemark");
                for (int i = 0; i < labelPlacemark[0].ChildNodes.Count; i++)
                {
                    if (labelPlacemark[0].ChildNodes.Item(i).Name == "name")    // Название полётного плана
                    {
                        label1.Text = labelPlacemark[0].ChildNodes.Item(i).InnerText.Trim();
                    }
                    if (labelPlacemark[0].ChildNodes.Item(i).Name == "description")    // Описание полётного плана
                    {
                        label2.Text = labelPlacemark[0].ChildNodes.Item(i).InnerText.Trim();
                    }
                }
                XmlNodeList labelLineString = kmlFile.GetElementsByTagName("LineString");
                for (int i = 0; i < labelLineString[0].ChildNodes.Count; i++)
                {
                    if (labelLineString[0].ChildNodes.Item(i).Name == "altitudeMode")   // Указание отношения высоты (от моря, от земли)
                    {
                        label3.Text = labelLineString[0].ChildNodes.Item(i).InnerText.Trim();
                    }
                    if (labelLineString[0].ChildNodes.Item(i).Name == "coordinates")
                    {
                        string[] points = labelLineString[0].ChildNodes.Item(i).InnerText.Trim().Split(' ');  // Значения координат и высоты
                        for (int n = 0; n < points.Length; n++)
                        {
                            ListViewItem fltplnItem = new ListViewItem("CheckPoint-" + n.ToString());
                            fltplnItem.SubItems.AddRange(points[n].Trim().Split(','));
                            FlightPlanListView.Items.Add(fltplnItem);
                        }
                        // К списку контрольных точек добавляем в конец координаты базы
                        FlightPlanListView.Items.Add("BasePoint");
                        string[] baseCollection = { string.Empty, string.Empty, "0", "0", "0", "0" };
                        FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems.AddRange(baseCollection);
                        FlightPlanListView.Items[0].SubItems.Add("0");
                    }
                }
                // Добавляем курс, метры и секунды.
                for (int i = 1; i < FlightPlanListView.Items.Count - 1; i++)
                {
                    string geoidString = fn.geoidDistanceAzimut(FlightPlanListView.Items[i - 1].SubItems[2].Text, FlightPlanListView.Items[i - 1].SubItems[1].Text,
                         FlightPlanListView.Items[i].SubItems[2].Text, FlightPlanListView.Items[i].SubItems[1].Text);
                    foreach (string newgeo in geoidString.Trim().Split(','))
                    {
                        FlightPlanListView.Items[i].SubItems.Add(newgeo);
                    }
                    double seconds = Convert.ToDouble(FlightPlanListView.Items[i].SubItems[5].Text) / Convert.ToDouble(NormSpeedTextBox.Text);// +"\u2033";      // distance/NormSpeedTextBox
                    FlightPlanListView.Items[i].SubItems.Add(seconds.ToString("0"));
                }
                FlightPlanListView.AutoResizeColumns(headerAutoResize: ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            if (!PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANLOAD].Checked)
            {
                PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANLOAD].Checked = true;
                PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANLOAD].BackColor = Color.Lime;
            }
        }

        /// <summary>
        /// Досчитываем полные расстояния, курсы и время при определении координат базы
        /// </summary>
        private void fullCount()
        {
            double alldist = 0;
            double allFT = 0;
            string geoidString1 = fn.geoidDistanceAzimut(FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[2].Text,
                FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[1].Text,
                FlightPlanListView.Items[0].SubItems[2].Text, FlightPlanListView.Items[0].SubItems[1].Text);
            string[] newgeoid1 = geoidString1.Trim().Split(',');
            // ДОбиваем первый чекпоинт реальными данными 
            if (FlightPlanListView.Items[0].SubItems.Count == 5)
            {
                FlightPlanListView.Items[0].SubItems[4].Text = newgeoid1[0];
                FlightPlanListView.Items[0].SubItems.Add(newgeoid1[1]);
                FlightPlanListView.Items[0].SubItems.Add((Convert.ToDouble(newgeoid1[1]) / Convert.ToDouble(NormSpeedTextBox.Text)).ToString("0"));
            }
            else
            {
                for (int i = 4; i < 6; i++)
                {
                    FlightPlanListView.Items[0].SubItems[i].Text = newgeoid1[i - 4];
                }
                FlightPlanListView.Items[0].SubItems[6].Text = (Convert.ToDouble(newgeoid1[1]) / Convert.ToDouble(NormSpeedTextBox.Text)).ToString("0");
            }
            // Заменяем в базе нули на реальные данные
            string geoidString2 = fn.geoidDistanceAzimut(FlightPlanListView.Items[FlightPlanListView.Items.Count - 2].SubItems[2].Text,
                FlightPlanListView.Items[FlightPlanListView.Items.Count - 2].SubItems[1].Text,
                FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[2].Text,
                FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[1].Text);
            string[] newgeoid2 = geoidString2.Trim().Split(',');
            for (int i = 4; i < 6; i++)
            {
                FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[i].Text = newgeoid2[i - 4];
            }
            FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[6].Text = (Convert.ToDouble(newgeoid2[1]) / Convert.ToDouble(NormSpeedTextBox.Text)).ToString("0");

            foreach (ListViewItem item in FlightPlanListView.Items)
            {
                alldist += Convert.ToDouble(item.SubItems[5].Text);
                allFT += Convert.ToDouble(item.SubItems[6].Text);
            }
            allDistanceLabel.Text = alldist.ToString("0");
            allDistanceLabel.BackColor = Color.Transparent;
            allFlightTimeLabel.Text = allFT.ToString("0");
            allFlightTimeLabel.BackColor = Color.Transparent;

            // Проверка на общее расстояние и общее время в полёте
            if (alldist > Convert.ToDouble(maxDistFlightTextBox.Text) || allFT > Convert.ToDouble(maxFlightTimeTextBox.Text))
            {
                if (PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANCHECKED].Checked)
                {
                    PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANCHECKED].Checked = false;
                    PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANCHECKED].BackColor = Color.LightCoral;
                }
                if (alldist > Convert.ToDouble(maxDistFlightTextBox.Text))
                {
                    allDistanceLabel.BackColor = Color.LightCoral;
                }
                if (allFT > Convert.ToDouble(maxFlightTimeTextBox.Text))
                {
                    allFlightTimeLabel.BackColor = Color.LightCoral;
                }
            }
            else
            {
                if (!PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANCHECKED].Checked)
                {
                    PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANCHECKED].Checked = true;
                    PreFlightListView.Items[(int)Functions.CHECKLIST.FLIGHTPLANCHECKED].BackColor = Color.Lime;
                }
            }
        }

        /// <summary>
        /// Выполняем при каждой отметке проверки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreFlightListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // При успешном прохождении всех тестов начинаем корректировку осей и взлёт с контрольным висением
            if (fn.currentFlightStage == Functions.FlightStage.PREFLIGHT)
            {
                int count = 0;  // Подсчёт пройденных в группе тестов
                foreach (ListViewItem item in PreFlightListView.Groups[0].Items)
                {
                    if (item.Checked)
                    {
                        count++;
                    }
                }
                if (count == PreFlightListView.Groups[0].Items.Count)   // Проверяем все ли предполётные проверки прошли
                {
                    // Меняем режим полёта и выполняем соответствующие функции
                    fn.currentFlightStage = Functions.FlightStage.TAKEOFF;
                    // Запросили данные по осям
                    byte cont = 0;
                    while (fn.SQuadroData.Count == 0 & cont++ <= 20)
                    {
                        SendPortCommand("S");   // Запросили данные по осям
                        Thread.Sleep(fn.waitThread);
                        if (cont == 20)
                        {
                            MessageBox.Show("В массив данные так и не добавились по S команде :(");
                            break;
                        }
                    }
                    // Определили базовые параметры ддля взлёта
                    string[] lastaxes = fn.SQuadroData[fn.SQuadroData.Count - 1].Trim().Split(';');
                    double baseLevel = Convert.ToDouble(lastaxes[(int)Functions.Axes.ALT], CultureInfo.GetCultureInfo("en-US"));   // Базовый уровень высоты (~4см.). 
                    double baseYaw = Convert.ToDouble(lastaxes[(int)Functions.Axes.YAW], CultureInfo.GetCultureInfo("en-US"));    // Базовый курс
                    double basePitch = Convert.ToDouble(lastaxes[(int)Functions.Axes.PITCH], CultureInfo.GetCultureInfo("en-US"));    // Базовый тангаж
                    double baseRoll = Convert.ToDouble(lastaxes[(int)Functions.Axes.ROLL], CultureInfo.GetCultureInfo("en-US"));    // Базовый крен

                    // Инициировали булево и функцию коррекции осей триммерами. Выполняем пока оси не будут ровными и газа достаточно для взлёта
                    while (!fn.correctAxes)
                    {
                        if (ThrottleTrackBar.Value <= ThrottleTrackBar.Minimum + (2 * fn.correctionAxes))
                        {
                            ThrottleTrackBar.Value = ThrottleTrackBar.Minimum + (2 * fn.correctionAxes);
                        }
                        if (ThrottleTrackBar.Value >= 1500)
                        {
                            ThrottleTrackBar.Value = 1500;
                            PilotControlStatusLabel.Text = "Максимальный газ для корректировки осей. Необходима дополнительная настройка.";
                            PilotControlStatusLabel.BackColor = Color.LightCoral;
                            SystemSounds.Beep.Play();    // Блямкнули при ошибке
                            break;
                        }
                        if (fn.TrimmerThrottel >= fn.maxTrimmer - fn.correctionAxes || Math.Abs(fn.TrimmerRoll) >= fn.maxTrimmer - fn.correctionAxes || Math.Abs(fn.TrimmerPitch) >= fn.maxTrimmer - fn.correctionAxes || Math.Abs(fn.TrimmerYaw) >= fn.maxTrimmer - fn.correctionAxes)
                        {
                            PilotControlStatusLabel.Text = "Максимальная корректировка. Необходима дополнительная настройка.";
                            PilotControlStatusLabel.BackColor = Color.LightCoral;
                            SystemSounds.Beep.Play();    // Блямкнули при ошибке
                            break;
                        }
                        ThrottleTrackBar.Value += fn.TrimmerCorrectionFunction(baseLevel, baseYaw, basePitch, baseRoll);
                        sendAxes(); // Отправили новые данные "как с пульта"
                        // Печатаем изменённые значения триггеров
                        TrimmerThrottleLabel.Text = fn.TrimmerThrottel.ToString();
                        TrimmerRollLabel.Text = fn.TrimmerRoll.ToString();
                        TrimmerPitchLabel.Text = fn.TrimmerPitch.ToString();
                        TrimmerYawLabel.Text = fn.TrimmerYaw.ToString();
                        // Запросили данные по осям
                        Thread snd = new Thread(new ThreadStart(spc));
                        snd.Start();
                        snd.Join();
                    }
                    PreFlightListView.Items[(int)Functions.CHECKLIST.TAKEOFFCOMPLITED].Checked = true;
                    PreFlightListView.Items[(int)Functions.CHECKLIST.TAKEOFFCOMPLITED].BackColor = Color.Lime;
                }
            }



            // Контрольное висение нужно для определения скорости и направления ветра.
            // Сохраняем данные для использования при построении маршрута.
            // Выполняем функцию взлёта от 2 см. до минимальной безопасной уже без корректировок осей (ТОЛЬКО КОНТРОЛЬ ВЫСОТЫ!!!)
            // Плавно увеличиваем газ и при приближении плавно уменьшаем, чтоб дельта стремилась к 0 (по синусоиде)

            if (fn.currentFlightStage == Functions.FlightStage.TAKEOFF && PreFlightListView.Items[(int)Functions.CHECKLIST.TAKEOFFCOMPLITED].Checked)
            {
                fn.currentFlightStage = Functions.FlightStage.CONTROLHOVERING;
                Stopwatch tickWind;
                tickWind = Stopwatch.StartNew();    // Начали замер времени на смещение взлёта по ветру
                double correctYaw = Convert.ToDouble(FlightPlanListView.Items[0].SubItems[4].Text) * Math.PI / 180; // Переводим в радианы
                while (fn.ControlHoveringFunction(fn.minSafeAlt, correctYaw)[2] != 1)
                {// Как только пришла 1 из функции, значит достигли минимальной безопасной и развернулись на курс
                    if (ThrottleTrackBar.Value <= ThrottleTrackBar.Minimum + (2 * fn.correctionAxes))
                    {
                        ThrottleTrackBar.Value = ThrottleTrackBar.Minimum + (2 * fn.correctionAxes);
                    }
                    if (ThrottleTrackBar.Value >= 1900)
                    {
                        ThrottleTrackBar.Value = 1900;
                    }
                    if (YawTrackBar.Value <= YawTrackBar.Minimum + (2 * fn.correctionAxes))
                    {
                        YawTrackBar.Value = YawTrackBar.Minimum + (2 * fn.correctionAxes);
                    }
                    if (YawTrackBar.Value >= YawTrackBar.Maximum - (2 * fn.correctionAxes))
                    {
                        YawTrackBar.Value = YawTrackBar.Maximum - (2 * fn.correctionAxes);
                    }
                    ThrottleTrackBar.Value += fn.ControlHoveringFunction(fn.minSafeAlt, correctYaw)[0];   // Поправили газ
                    YawTrackBar.Value += fn.ControlHoveringFunction(fn.minSafeAlt, correctYaw)[1];   // Поправили курс
                    sendAxes(); // Отправили новые данные "как с пульта"
                    // Запросили данные по осям
                    Thread snd = new Thread(new ThreadStart(spc));
                    snd.Start();
                    snd.Join();
                }
                // Анализ скорости и направления ветра
                // Посмотрели за сколько снесло на какое расстояние и градус - считаем за ветер.
                /*
                string[] convLatLon = fn.convNMEAtoGEO(fn.GPSData.Items[fn.GPSData.Items.Count - 1].SubItems[(int)Functions.GPSDataNAME.LATITUDE].Text, fn.GPSData.Items[fn.GPSData.Items.Count - 1].SubItems[(int)Functions.GPSDataNAME.LONGITUDE].Text);
                double[] realWind = fn.geoidDistAzDouble(FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[2].Text, FlightPlanListView.Items[FlightPlanListView.Items.Count - 1].SubItems[1].Text, convLatLon[0], convLatLon[1]);
                tickWind.Stop();
                double speedWind = tickWind.ElapsedMilliseconds / 1000; // Время движения по ветру в секундах
                fn.wind["speed"] = realWind[1] / speedWind; // Посчитали и записали ветер в метрах в секунду
                // Направление ветра - 180гр.
                if (realWind[0] > Math.PI)
                {
                    fn.wind["rumb"] = realWind[0] - Math.Round(Math.PI, 2);
                }
                else
                {
                    fn.wind["rumb"] = realWind[0] + Math.Round(Math.PI, 2);
                }
                // Возвращаем на исходную точку и начинаем полёт по маршруту
                // ЕШЁ ОДНА ФУНКЦИЯ, НО С КРЕНОМ ПО ПАРАМЕТРАМ ВЕТРА
                */
                // После всех изменений меняем лист проверки и выходим из функции
                PreFlightListView.Items[(int)Functions.CHECKLIST.HOVERING].Checked = true;
                PreFlightListView.Items[(int)Functions.CHECKLIST.HOVERING].BackColor = Color.Lime;
            }
            // Выполняем полёт по маршруту. Тут анализируем чекитемс на маршрутном листе!!!
            if (fn.currentFlightStage == Functions.FlightStage.CONTROLHOVERING && PreFlightListView.Items[(int)Functions.CHECKLIST.HOVERING].Checked)
            {
                fn.currentFlightStage = Functions.FlightStage.ROUTEFLIGHT;

                // После всех чеков на маршрутном листе меняем тут лист проверки и выходим из функции
                PreFlightListView.Items[(int)Functions.CHECKLIST.ENROUTEFLIGHT].Checked = true;
                PreFlightListView.Items[(int)Functions.CHECKLIST.ENROUTEFLIGHT].BackColor = Color.Lime;
            }
            // Посадка. Тут проверяем температуру и зарядку аккумулятора и при удачном заряде начинаем новый полёт.
            if (fn.currentFlightStage == Functions.FlightStage.ROUTEFLIGHT && PreFlightListView.Items[(int)Functions.CHECKLIST.ENROUTEFLIGHT].Checked)
            {
                fn.currentFlightStage = Functions.FlightStage.TOUCHDOWN;
                //DialogResult res = MessageBox.Show("Запускаем функции посадки и зарядки для следующего полёта", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                //if (res == DialogResult.No)
                //{

                // ПОКА!!! ДЛЯ ТЕСТА ТУТ ПРОСТО САЖАЕМ НА МЕСТО ВЗЛЁТА
                while (ThrottleTrackBar.Value > 1000 + fn.correctionAxes)
                {
                    ThrottleTrackBar.Value -= fn.correctionAxes;
                    sendAxes();
                    SystemSounds.Beep.Play();
                    Thread.Sleep(500);
                }
                return;
                //}
                // ЖДЁМ ЗАРЯДКИ АККУМУЛЯТОРА И ТОЛЬКО ПОСЛЕ ЭТОГО ИДЁМ ДАЛЬШЕ!!!!
                // После всех изменений меняем лист проверки и выходим из функции
                //PreFlightListView.Items[(int)Functions.CHECKLIST.LANDING].Checked = true;
                //PreFlightListView.Items[(int)Functions.CHECKLIST.LANDING].BackColor = Color.Lime;
            }
            // Последующий взлёт без проверки. Сразу на маршрут.
            if (fn.currentFlightStage == Functions.FlightStage.TOUCHDOWN && PreFlightListView.Items[(int)Functions.CHECKLIST.LANDING].Checked)
            {
                fn.currentFlightStage = Functions.FlightStage.CONTROLHOVERING;
                DialogResult res = MessageBox.Show("Поднимаемся на безопасную и летим по маршруту", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.No)
                {
                    return;
                }
                // После всех изменений меняем лист проверки и выходим из функции
                PreFlightListView.Items[(int)Functions.CHECKLIST.HOVERING].Checked = true;
                PreFlightListView.Items[(int)Functions.CHECKLIST.HOVERING].BackColor = Color.Lime;
                PreFlightListView.Items[(int)Functions.CHECKLIST.ENROUTEFLIGHT].Checked = false;
                PreFlightListView.Items[(int)Functions.CHECKLIST.ENROUTEFLIGHT].BackColor = Color.LightCoral;
                PreFlightListView.Items[(int)Functions.CHECKLIST.LANDING].Checked = false;
                PreFlightListView.Items[(int)Functions.CHECKLIST.LANDING].BackColor = Color.LightCoral;
            }
        }
        /// <summary>
        /// Запросили данные по осям в отдельном потоке
        /// </summary>
        private void spc()
        {
            lock (takeofflock)
            {
                byte f = 0;
                while (!fn.haveNewSData & f++ <= 50)
                {
                    SendPortCommand("S");
                    Thread.Sleep(fn.waitThread);  // Ждём записи новых данных в массив
                    if (f == 50)
                    {
                        SystemSounds.Beep.Play();   // Не дождались :(
                        break;
                    }
                }
            }
        }

        #endregion
        // Типа прошли все тесты
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in PreFlightListView.Groups[0].Items)
            {
                item.Checked = true;
            }
        }
    }
}
