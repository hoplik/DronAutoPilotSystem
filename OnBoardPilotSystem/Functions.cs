using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;

namespace OnBoardPilotSystem
{
    public class Functions
    {
        /// <summary>
        /// Чёрный ящик для данных GPS приёмника
        /// </summary>
        public ListView GPSData = new ListView();

        /// <summary>
        /// Хранение массива данных, полученных от квадрокоптера по трём осям
        /// </summary>
        public List<string> SQuadroData = new List<string>();

        /// <summary>
        /// Хранение массива данных, полученных от квадрокоптера по высоте
        /// </summary>
        public ListView zQuadroData = new ListView();

        /// <summary>
        /// Параметры ветра
        /// </summary>
        public Dictionary<string, double> wind = new Dictionary<string, double> { { "rumb", 0 }, { "speed", 0 } };

        internal Dictionary<string, string> allUSBDev = new Dictionary<string, string>();

        // Триммеры коррекции
        public int TrimmerThrottel { get; set; }
        public int TrimmerYaw { get; set; }
        public int TrimmerRoll { get; set; }
        public int TrimmerPitch { get; set; }

        public FlightStage currentFlightStage = FlightStage.PREFLIGHT;  // Текущий статус состояния полёта

        internal int correctionAxes = Convert.ToInt32(Properties.Settings.Default.CorrectionAxes); // Шаг изменения данных по осям (1000 - изменение значений для каждой шкалы)
        internal int waitThread = Convert.ToInt32(Properties.Settings.Default.WaitThread);    //Ждём записи новых данных (можно увеличить, если реакция очень сильная)
        internal int maxTrimmer = Convert.ToInt32(Properties.Settings.Default.MaxTrimmer);    // Максимально допустимое значение триммера (по модулю)
        internal double ABSdopuskRadian = Convert.ToDouble(Properties.Settings.Default.absdopuskRadian, CultureInfo.GetCultureInfo("en-US")); // Допуск по модулю в радианах по крену, тангажу и курсу (экспериментально!)
        internal double LevelTakeOff = Convert.ToDouble(Properties.Settings.Default.levelTakeOff, CultureInfo.GetCultureInfo("en-US"));   // Уровень в метрах, при котором считается, что зафиксирован отрыв от земли
        internal double minSafeAlt = Convert.ToDouble(Properties.Settings.Default.minSafeAlt, CultureInfo.GetCultureInfo("en-US"));  // Минимально безопасная высота в метрах (пока поставим 1.2м. для тестов)

        internal bool correctAxes = false;   // Когда все оси корректны можем взлетать.
        internal bool throttelCorrectionFixed = false; // Признак коррктной установки триммера газа
        internal bool haveNewSData = false;   // Получены новые данные по S запросу
        double lastDeltaLevel = 0;  // Последнее изменение высоты
        double lastDeltaYaw = 0;    // Последнее изменение курса

        /// <summary>
        /// Ограничение на колличество записей
        /// </summary>
        private int bbr = Properties.Settings.Default.BlackBox;
        public int bbRecords
        {
            get { return bbr; }
            set { bbr = value; }
        }

        /// <summary>
        /// Режимы полёта.
        /// </summary>
        [Flags]
        public enum FlightStage
        {
            PREFLIGHT = 0,          // Предвзлётная подготовка
            TAKEOFF = 1,            // Взлёт
            CONTROLHOVERING = 2,    // Контрольное висение
            ROUTEFLIGHT = 3,        // Полёт по маршруту
            TOUCHDOWN = 4           // Посадка
        }

        [Flags]
        public enum Axes
        {
            PITCH = 0,
            ROLL = 1,
            YAW = 2,
            ALT = 3
        }

        /// <summary>
        /// Список команд, которые понимает приложение на входе в порт Ардуины
        /// </summary>
        [Flags]
        public enum serialInputCommands
        {
            SOBPS_OFF = 0,  // Автопилот выключен
            SOBPS_ON = 1,   // Автопилот включён
            S = 2,          // Запросили крен, тангаж, курс
            z = 3,          // Запросили высоту
            SHUTDOWN = 4    // Выключили компьютер
        }

        /// <summary>
        /// Список полётных проверок
        /// </summary>
        [Flags]
        public enum CHECKLIST
        {
            GPSENABLE = 0,          // Данные GPS поступают
            ARDUINOENABLE = 1,      // Ардуина подключена
            AUTOPILOTISON = 2,      // Автопилот включен
            BASEFIXED = 3,          // Точка старта/финиша определена
            FLIGHTPLANLOAD = 4,     // План полёта загружен
            FLIGHTPLANCHECKED = 5,  // План полёта проверен
            TAKEOFFCOMPLITED = 6,   // Триммеры осей отрегулированы (начали взлёт до контрольного висения)
            HOVERING = 7,           // Контрольное висение (начали полёт по маршруту)
            ENROUTEFLIGHT = 8,      // Полёт по маршруту
            LANDING = 9             // Посадка
        }

        /// <summary>
        /// Перечислитель позиций данных GPS. Строка Recommended Minimum Specific GNSS Data
        /// </summary>
        [Flags]
        public enum RMCNAME : byte
        {
            MESSAGEID = 0,          // Заголовок сообщения RMC
            UTCTIME = 1,            // Время UTC. Формат hhmmss.sss
            STATUS = 2,             // Валидность данных. A - валидны, V - не валидны
            LATITUDE = 3,           // Широта. Формат ddmm.mmmm
            NSINDICATOR = 4,        // Индикатор широты. Формат N - северная, S - южная
            LONGITUDE = 5,          // Долгота. Формат dddmm.mmmm
            EWINDICATOR = 6,        // Индикатор долготы. Формат E - восточная, W - западная
            SPEEDOVERGROUND = 7,    // Измеренная горизонтальная скорость в узлах. 1 узел = 1.852 км/ч, 0.5144 м/с, 30.86667 м/мин
            COURSEOVERGROUND = 8,   // Курс относительно истинного севера в градусах
            DATE = 9,               // Дата. Формат ddmmyy
            MAGNETICVARIATION = 10, // Магнитная вариация в градусах. E - восточная, W - западная
            CHECKSUM = 11           // Контрольная сумма. Начинается с *
        }

        /// <summary>
        /// Флаг фиксации позиции
        /// </summary>
        [Flags]
        public enum POSFIXINDICATOR : byte
        {
            NOTFIX = 0,
            GPSSPS = 1,
            DGPSSPS = 2,
            GPSPPS = 3
        }

        /// <summary>
        /// Перечислитель позиций данных GPS. Строка Global Positioning System Fixed Data
        /// </summary>
        [Flags]
        public enum GGANAME : byte  // Счётчик показывает 15 записей! Надо проверить!
        {
            MESSAGEID = 0,          // Заголовок сообщения GGA
            UTCTIME = 1,            // Время UTC. Формат hhmmss.sss
            LATITUDE = 2,           // Широта. Формат ddmm.mmmm
            NSINDICATOR = 3,        // Индикатор широты. Формат N - северная, S - южная
            LONGITUDE = 4,          // Долгота. Формат dddmm.mmmm
            EWINDICATOR = 5,        // Индикатор долготы. Формат E - восточная, W - западная
            POSITIONFIXINDICATOR = 6,// Флаг фиксации позиции. 0 - позиция не определена, 1 - GPS SPS, 2 - DGPS SPS, 3 - GPS PPS
            SATELLITESUSED = 7,     // Колличество спутников, от которых идёт обработка данных (значение от 0 до 12)
            HDOP = 8,               // Horizontal Dilution of Precision - Горизонтальное разведение точности
            MSLALTITUDE = 9,        // Высота от среднего уровня моря (MeanSeaLevel) в метрах
            MSLAUNITS = 10,         // Единицы измерения MSLA. М - метры
            GEOIDSEPARATION = 11,   // В метрах
            GSUNITS = 12,           // Единицы измерения GS. М - метры
            AGEOFDIFFCORR = 13,     // В секундах. Пусто, если DGPS не используется
            //DIFFREFSTATIONID = 14,  // 0000
            CHECKSUM = 14           // Контрольная сумма. Начинается с *
        }

        /// <summary>
        /// Перечислитель позиций данных GPS. Объединённый массив данных
        /// </summary>
        [Flags]
        public enum GPSDataNAME : byte
        {
            UTCTIMEGGA = 1,            // GGA Время UTC. Формат hhmmss.sss
            LATITUDE = 2,           // Широта. Формат ddmm.mmmm
            NSINDICATOR = 3,        // Индикатор широты. Формат N - северная, S - южная
            LONGITUDE = 4,          // Долгота. Формат dddmm.mmmm
            EWINDICATOR = 5,        // Индикатор долготы. Формат E - восточная, W - западная
            MSLALTITUDE = 6,        // Высота от среднего уровня моря (MeanSeaLevel) в метрах
            MSLAUNITS = 7,          // Единицы измерения MSLA. М - метры
            POSITIONFIXINDICATOR = 8,// Флаг фиксации позиции. 0 - позиция не определена, 1 - GPS SPS, 2 - DGPS SPS, 3 - GPS PPS
            SATELLITESUSED = 9,     // Колличество спутников, от которых идёт обработка данных (значение от 0 до 12)
            UTCTIMERMC = 10,            // RMC Время UTC. Формат hhmmss.sss
            STATUS = 11,            // Валидность данных. A - валидны, V - не валидны, W - валидны, но из другой посылки
            DATE = 12,               // Дата. Формат ddmmyy
            SPEEDOVERGROUND = 13,    // Измеренная горизонтальная скорость в узлах. 1 узел = 1.852 км/ч, 0.5144 м/с, 30.86667 м/мин
            COURSEOVERGROUND = 14,   // Курс относительно истинного севера в градусах
            MAGNETICVARIATION = 15 // Магнитная вариация в градусах. E - восточная, W - западная
        }

        /// <summary>
        /// Список папок реестра, которые необходимо проверить на подключаемые в параллельные порты устройства
        /// </summary>
        [Flags]
        public enum RegistryDevices
        {
            USB,
            FTDIBUS
        }

        /// <summary>
        /// Разбираем входящую строку на команды.
        /// </summary>
        /// <param name="incomeString">Строковое представление команды, полученной в порт</param>
        /// <returns>При успешном распознавании команды возвращается её номер на исполнение.</returns>
        internal int parsingString(string incomeString)
        {
            string[] arduCommands = incomeString.Split(',');

            try
            {
                serialInputCommands realCommand = (serialInputCommands)Enum.Parse(typeof(serialInputCommands), arduCommands[arduCommands.Length - 1]);
                switch (realCommand)
                {
                    case serialInputCommands.SOBPS_OFF:    // Автопилот выключен
                        return 0;
                    case serialInputCommands.SOBPS_ON:    // Включен автопилот
                        return 1;
                    case serialInputCommands.S:    // Запросили крен, тангаж, курс
                        string lineCommand = string.Empty;
                        for (int i = 0; i < arduCommands.Length - 1; i++)
                        {
                            lineCommand += arduCommands[i] + ";";
                        }
                        lineCommand += arduCommands[arduCommands.Length - 1];
                        SQuadroData.Add(lineCommand);
                        haveNewSData = true;    // Записали новые данные в массив
                        return 2;
                    case serialInputCommands.z:    // Запросили высоту
                        return 3;
                    case serialInputCommands.SHUTDOWN:    // Запросили высоту
                        return 4;
                    default:
                        return -1;
                }
            }
            catch (ArgumentException)
            {
                return -1;
            }
        }

        /// <summary>
        /// Парсим данные GPS. Раскладываем данные спутника в значения массива.
        /// </summary>
        /// <param name="fullGPSString">Строковая переменная. Все данные, которые были доступны в GPS-порту.</param>
        /// <returns>Возвращается колличество записей. -1 если ошибка.</returns>
        internal int GPSParsingString(string fullGPSString)
        {
            string[] gpsArraySubitems = new string[16]; // Сводный массив данных GPS
            // Сначала бьём строку по символам окончания строки
            string[] NMEAStrings = fullGPSString.Trim().Split(new char[] { '\r', '\n' });
            // Получили много цельных строк и плохие начало и конец, которые не надо учитывать
            for (int i = 0; i < NMEAStrings.Length; i++)
            {
                if (NMEAStrings[i] != string.Empty) // Пустые строки игнорируем, оставляем только с записями
                {
                    //if (NMEAStrings[i].IndexOfAny(new char[] { '&', 'G', 'P' }, 0, 3) != -1 && NMEAStrings[i].Contains("*"))   // Есть нормальное начало и контрольная сумма строки!
                    if (NMEAStrings[i].IndexOf("GP", 0, 3) != -1 && NMEAStrings[i].Contains("*"))   // Есть нормальное начало и контрольная сумма строки!
                    {
                        string[] nmeaArray = NMEAStrings[i].Split(','); // Строки корректны и разбиты на массив
                        if (nmeaArray[0].Contains("GGA")) // Если нашли нужные нам строки
                        {
                            gpsArraySubitems[(int)GPSDataNAME.LATITUDE] = nmeaArray[(int)GGANAME.LATITUDE];            // Широта. Формат ddmm.mmmm
                            gpsArraySubitems[(int)GPSDataNAME.NSINDICATOR] = nmeaArray[(int)GGANAME.NSINDICATOR];          // Индикатор широты. Формат N - северная, S - южная
                            gpsArraySubitems[(int)GPSDataNAME.LONGITUDE] = nmeaArray[(int)GGANAME.LONGITUDE];            // Долгота. Формат dddmm.mmmm
                            gpsArraySubitems[(int)GPSDataNAME.EWINDICATOR] = nmeaArray[(int)GGANAME.EWINDICATOR];          // Индикатор долготы. Формат E - восточная, W - западная
                            gpsArraySubitems[(int)GPSDataNAME.MSLALTITUDE] = nmeaArray[(int)GGANAME.MSLALTITUDE];          // Высота от среднего уровня моря (MeanSeaLevel) в метрах
                            gpsArraySubitems[(int)GPSDataNAME.MSLAUNITS] = nmeaArray[(int)GGANAME.MSLAUNITS];            // Единицы измерения MSLA. М - метры
                            gpsArraySubitems[(int)GPSDataNAME.POSITIONFIXINDICATOR] = nmeaArray[(int)GGANAME.POSITIONFIXINDICATOR]; // Флаг фиксации позиции. 0 - позиция не определена, 1 - GPS SPS, 2 - DGPS SPS, 3 - GPS PPS
                            gpsArraySubitems[(int)GPSDataNAME.SATELLITESUSED] = nmeaArray[(int)GGANAME.SATELLITESUSED];
                            gpsArraySubitems[(int)GPSDataNAME.UTCTIMEGGA] = nmeaArray[(int)GGANAME.UTCTIME];
                        }
                        if (nmeaArray[0].Contains("RMC"))
                        {
                            gpsArraySubitems[(int)GPSDataNAME.STATUS] = nmeaArray[(int)RMCNAME.STATUS];               // Валидность данных. A - валидны, V - не валидны, W - данные верны, но из другой посылки
                            gpsArraySubitems[(int)GPSDataNAME.DATE] = nmeaArray[(int)RMCNAME.DATE];                 // Дата. Формат ddmmyy
                            gpsArraySubitems[(int)GPSDataNAME.SPEEDOVERGROUND] = nmeaArray[(int)RMCNAME.SPEEDOVERGROUND];      // Измеренная горизонтальная скорость в узлах. 1 узел = 1.852 км/ч, 0.5144 м/с, 30.86667 м/мин
                            gpsArraySubitems[(int)GPSDataNAME.COURSEOVERGROUND] = nmeaArray[(int)RMCNAME.COURSEOVERGROUND];     // Курс относительно истинного севера в градусах
                            gpsArraySubitems[(int)GPSDataNAME.MAGNETICVARIATION] = nmeaArray[(int)RMCNAME.MAGNETICVARIATION];
                            gpsArraySubitems[(int)GPSDataNAME.UTCTIMERMC] = nmeaArray[(int)RMCNAME.UTCTIME];
                        }
                    }
                }
            }
            // В GGA и RMC лежат проверенные строки, но надо убедиться, что они из одной посылки!
            if (gpsArraySubitems[(int)GPSDataNAME.UTCTIMEGGA] != gpsArraySubitems[(int)GPSDataNAME.UTCTIMERMC] && gpsArraySubitems[(int)GPSDataNAME.STATUS] == "A")
            {
                gpsArraySubitems[(int)GPSDataNAME.STATUS] = "W";
            }
            ListViewItem gpsdata = new ListViewItem(gpsArraySubitems[(int)GPSDataNAME.UTCTIMEGGA]);
            try
            {
                while (GPSData.Items.Count > bbRecords - 1)   // Удаляем лишние старые записи
                {
                    GPSData.Items[0].Remove();
                }
                gpsdata.SubItems.AddRange(gpsArraySubitems);
                GPSData.Items.Add(gpsdata);
                if (GPSData.Items[GPSData.Items.Count - 1].SubItems[(int)GPSDataNAME.LATITUDE].Text == string.Empty)
                {
                    GPSData.Items[GPSData.Items.Count - 1].Remove();
                }
                return GPSData.Items.Count;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Готовим листинг всех устройств, прописанных в реестре на USB порту и отмечаем доступные
        /// </summary>
        /// <returns>Возвращает массив устройств (название, порт), которые были подключены к системе.</returns>
        internal void ListingUSBDic()
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine; // Зашли в локал машин
                for (int i = 0; i < Enum.GetValues(typeof(RegistryDevices)).Length; i++)
                {
                    string currentFolder = Enum.GetName(typeof(RegistryDevices), i);
                    RegistryKey openRK = rk.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum\\" + currentFolder); // Открыли на чтение папку USB устройств
                    string[] USBDevices = openRK.GetSubKeyNames();  // Получили имена всех, когда-либо подключаемых устройств
                    foreach (string stepOne in USBDevices)  // Для каждого производителя устройства проверяем подпапки, т.к. бывает несколько устройств на одном ВИД/ПИД
                    {
                        RegistryKey stepOneReg = openRK.OpenSubKey(stepOne);    // Открываем каждого производителя на чтение
                        string[] stepTwo = stepOneReg.GetSubKeyNames(); // Получили список всех устройств для каждого производителя
                        foreach (string friendName in stepTwo)
                        {
                            RegistryKey friendRegName = stepOneReg.OpenSubKey(friendName);
                            string[] fn = friendRegName.GetValueNames();
                            foreach (string currentName in fn)
                            {
                                if (currentName == "FriendlyName")
                                {
                                    object frn = friendRegName.GetValue("FriendlyName");
                                    RegistryKey devPar = friendRegName.OpenSubKey("Device Parameters");
                                    object dp = devPar.GetValue("PortName");
                                    allUSBDev.Add((string)frn, (string)dp);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                allUSBDev.Add("Внимание! Ошибка!", (string)err);
            }
        }

        /// <summary>
        /// Проверка доступности указанного порта
        /// </summary>
        /// <param name="checkPort">Строковая переменная. Имя порта для проверки.</param>
        /// <returns>Если указанный порт присутвует в системе - true, иначе - false.</returns>
        internal bool availablePort(string checkPort)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                if (checkPort == port)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Рассчитываем курс и метры
        /// </summary>
        /// <param name="_strtLat">Строковое значение широты начальной точки. Формат dd.dddddd</param>
        /// <param name="_strtLon">Строковое значение долготы начальной точки. Формат dd.dddddd</param>
        /// <param name="_fnshLat">Строковое значение широты конечной точки. Формат dd.dddddd</param>
        /// <param name="_fnshLon">Строковое значение долготы конечной точки. Формат dd.dddddd</param>
        /// <returns>В одной строке, через ',' результат расчёта азимута и расстояния между координатами двух точек.</returns>
        internal string geoidDistanceAzimut(string _strtLat, string _strtLon, string _fnshLat, string _fnshLon)
        {
            string distance = string.Empty;
            string azimut = string.Empty;
            const uint radiusEarth = 6372795;
            // Преобразовали строковые переменные в число, для удобства расчётов
            double strtLat = Convert.ToDouble(_strtLat, CultureInfo.GetCultureInfo("en-US"));
            double strtLon = Convert.ToDouble(_strtLon, CultureInfo.GetCultureInfo("en-US"));
            double fnshLat = Convert.ToDouble(_fnshLat, CultureInfo.GetCultureInfo("en-US"));
            double fnshLon = Convert.ToDouble(_fnshLon, CultureInfo.GetCultureInfo("en-US"));
            // Переводим градусы в радианы
            double radstrtLat = strtLat * Math.PI / 180;
            double radstrtLon = strtLon * Math.PI / 180;
            double radfnshLat = fnshLat * Math.PI / 180;
            double radfnshLon = fnshLon * Math.PI / 180;
            // Косинусы и синусы широт и разницы долгот
            double cl1 = Math.Cos(radstrtLat);
            double cl2 = Math.Cos(radfnshLat);
            double sl1 = Math.Sin(radstrtLat);
            double sl2 = Math.Sin(radfnshLat);
            double delta = radfnshLon - radstrtLon;
            double cdelta = Math.Cos(delta);
            double sdelta = Math.Sin(delta);
            // Вычисления длины большого круга - ортодромия
            double y = Math.Sqrt(Math.Pow(sdelta * cl2, 2) + Math.Pow((cl1 * sl2) - (sl1 * cl2 * cdelta), 2));
            double x = sl1 * sl2 + cl1 * cl2 * cdelta;
            double ad = Math.Atan2(y, x);
            double dist = ad * radiusEarth;
            // Вычисление азимута - локсодромия
            double pi2 = Math.PI / 2;
            double long1 = -1 * radstrtLon;
            double long2 = -1 * radfnshLon;
            double dlon_E = (long2 - long1) - (2 * Math.PI * (Math.Floor((long2 - long1) / (2 * Math.PI))));
            double dlon_W = (long1 - long2) - (2 * Math.PI * (Math.Floor((long1 - long2) / (2 * Math.PI))));
            double dphi = Math.Log((Math.Tan((radfnshLat / 2) + (Math.PI / 4))) / (Math.Tan((radstrtLat / 2) + (Math.PI / 4))));
            sbyte sign = 0;
            double Atn2 = 0;
            if (dlon_W < dlon_E) // 0-180
            {
                if (dlon_W >= 0)
                {
                    sign = 1;
                }
                else
                {
                    sign = -1;
                }
                if (Math.Abs(dlon_W) >= Math.Abs(dphi))
                {
                    Atn2 = (sign * pi2) - Math.Atan2(dphi, dlon_W);
                }
                else
                {
                    if (dphi > 0)
                    {
                        Atn2 = Math.Atan2(dlon_W, dphi);
                    }
                    else
                    {
                        if ((-1 * dlon_W) >= 0)
                        {
                            Atn2 = Math.Atan2(dlon_W, dphi) + Math.PI;
                        }
                        else
                        {
                            Atn2 = Math.Atan2(dlon_W, dphi);
                        }
                    }
                }
            }
            else
            {   // 180 - 360
                if (dlon_E >= 0)
                {
                    sign = 1;
                }
                else
                {
                    sign = -1;
                }
                dlon_E = -1 * dlon_E;
                if (Math.Abs(dlon_E) >= Math.Abs(dphi))
                {
                    Atn2 = sign * pi2 - Math.Atan2(dphi, dlon_E);
                }
                else
                {
                    if (dphi > 0)
                    {
                        Atn2 = Math.Atan2(dlon_E, dphi);
                    }
                    else
                    {
                        if ((dlon_E) >= 0)
                        {
                            Atn2 = Math.Atan2(dlon_E, dphi) + Math.PI;
                        }
                        else
                        {
                            Atn2 = Math.Atan2(dlon_E, dphi);
                        }
                    }
                }
            }
            double tc = Atn2 - (2 * Math.PI * (Math.Floor((Atn2) / (2 * Math.PI))));
            double tcdeg = (tc * 180) / Math.PI;    //результат - угол в градусах
            azimut = tcdeg.ToString("000");// +"\u00B0";
            distance = dist.ToString("0");// +"м";
            return azimut + "," + distance;
        }

        /// <summary>
        /// Рассчитываем курс и метры, но сразу в десятых, а не в строке
        /// </summary>
        /// <param name="_strtLat">Строковое значение широты начальной точки. Формат dd.dddddd</param>
        /// <param name="_strtLon">Строковое значение долготы начальной точки. Формат dd.dddddd</param>
        /// <param name="_fnshLat">Строковое значение широты конечной точки. Формат dd.dddddd</param>
        /// <param name="_fnshLon">Строковое значение долготы конечной точки. Формат dd.dddddd</param>
        /// <returns>В одной строке, через ',' результат расчёта азимута и расстояния между координатами двух точек.</returns>
        internal double[] geoidDistAzDouble(string _strtLat, string _strtLon, string _fnshLat, string _fnshLon)
        {
            string distance = string.Empty;
            string azimut = string.Empty;
            const uint radiusEarth = 6372795;
            // Преобразовали строковые переменные в число, для удобства расчётов
            double strtLat = Convert.ToDouble(_strtLat, CultureInfo.GetCultureInfo("en-US"));
            double strtLon = Convert.ToDouble(_strtLon, CultureInfo.GetCultureInfo("en-US"));
            double fnshLat = Convert.ToDouble(_fnshLat, CultureInfo.GetCultureInfo("en-US"));
            double fnshLon = Convert.ToDouble(_fnshLon, CultureInfo.GetCultureInfo("en-US"));
            // Переводим градусы в радианы
            double radstrtLat = strtLat * Math.PI / 180;
            double radstrtLon = strtLon * Math.PI / 180;
            double radfnshLat = fnshLat * Math.PI / 180;
            double radfnshLon = fnshLon * Math.PI / 180;
            // Косинусы и синусы широт и разницы долгот
            double cl1 = Math.Cos(radstrtLat);
            double cl2 = Math.Cos(radfnshLat);
            double sl1 = Math.Sin(radstrtLat);
            double sl2 = Math.Sin(radfnshLat);
            double delta = radfnshLon - radstrtLon;
            double cdelta = Math.Cos(delta);
            double sdelta = Math.Sin(delta);
            // Вычисления длины большого круга - ортодромия
            double y = Math.Sqrt(Math.Pow(sdelta * cl2, 2) + Math.Pow((cl1 * sl2) - (sl1 * cl2 * cdelta), 2));
            double x = sl1 * sl2 + cl1 * cl2 * cdelta;
            double ad = Math.Atan2(y, x);
            double dist = ad * radiusEarth;
            // Вычисление азимута - локсодромия
            double pi2 = Math.PI / 2;
            double long1 = -1 * radstrtLon;
            double long2 = -1 * radfnshLon;
            double dlon_E = (long2 - long1) - (2 * Math.PI * (Math.Floor((long2 - long1) / (2 * Math.PI))));
            double dlon_W = (long1 - long2) - (2 * Math.PI * (Math.Floor((long1 - long2) / (2 * Math.PI))));
            double dphi = Math.Log((Math.Tan((radfnshLat / 2) + (Math.PI / 4))) / (Math.Tan((radstrtLat / 2) + (Math.PI / 4))));
            sbyte sign = 0;
            double Atn2 = 0;
            if (dlon_W < dlon_E) // 0-180
            {
                if (dlon_W >= 0)
                {
                    sign = 1;
                }
                else
                {
                    sign = -1;
                }
                if (Math.Abs(dlon_W) >= Math.Abs(dphi))
                {
                    Atn2 = (sign * pi2) - Math.Atan2(dphi, dlon_W);
                }
                else
                {
                    if (dphi > 0)
                    {
                        Atn2 = Math.Atan2(dlon_W, dphi);
                    }
                    else
                    {
                        if ((-1 * dlon_W) >= 0)
                        {
                            Atn2 = Math.Atan2(dlon_W, dphi) + Math.PI;
                        }
                        else
                        {
                            Atn2 = Math.Atan2(dlon_W, dphi);
                        }
                    }
                }
            }
            else
            {   // 180 - 360
                if (dlon_E >= 0)
                {
                    sign = 1;
                }
                else
                {
                    sign = -1;
                }
                dlon_E = -1 * dlon_E;
                if (Math.Abs(dlon_E) >= Math.Abs(dphi))
                {
                    Atn2 = sign * pi2 - Math.Atan2(dphi, dlon_E);
                }
                else
                {
                    if (dphi > 0)
                    {
                        Atn2 = Math.Atan2(dlon_E, dphi);
                    }
                    else
                    {
                        if ((dlon_E) >= 0)
                        {
                            Atn2 = Math.Atan2(dlon_E, dphi) + Math.PI;
                        }
                        else
                        {
                            Atn2 = Math.Atan2(dlon_E, dphi);
                        }
                    }
                }
            }
            double tc = Atn2 - (2 * Math.PI * (Math.Floor((Atn2) / (2 * Math.PI))));
            double tcdeg = (tc * 180) / Math.PI;    //результат - угол в градусах
            double[] azdi = { tcdeg, dist };
            return azdi;
        }

        /// <summary>
        /// Функция взлёта. Выводим триммерами нулевые положения для отрыва от земли на 2 см.
        /// </summary>
        internal int TrimmerCorrectionFunction(double baseLevel, double baseYaw, double basePitch, double baseRoll)
        {
            string[] lastaxes = SQuadroData[SQuadroData.Count - 1].Trim().Split(';');
            haveNewSData = false;   // Обработали строку с последними данными
            double currentPitch = Convert.ToDouble(lastaxes[(int)Axes.PITCH], CultureInfo.GetCultureInfo("en-US"));
            double currentRoll = Convert.ToDouble(lastaxes[(int)Axes.ROLL], CultureInfo.GetCultureInfo("en-US"));
            double currentYaw = Convert.ToDouble(lastaxes[(int)Axes.YAW], CultureInfo.GetCultureInfo("en-US"));
            double currentLevel = Convert.ToDouble(lastaxes[(int)Axes.ALT], CultureInfo.GetCultureInfo("en-US"));  // Определяем текущий уровень высоты.

            double deltaLevel = currentLevel - baseLevel; // Изменение высоты.
            double deltaPitch = currentPitch - basePitch; // Изменение тангажа.
            double deltaRoll = currentRoll - baseRoll;      // Изменение крена

            // Первый шаг - добавили газа - проверили высоту - если меньше минимума - проверили оси - если изменились - убрали чуть газа - поправили оси
            // ТУТ НАДО ДОБАВЛЯТЬ ГАЗ ПОКА НЕ НАЧНУТСЯ ИЗМЕНЕНИЯ ПО ОСЯМ ИЛИ ВЗЛЁТ!!!
            if (!throttelCorrectionFixed & deltaLevel < LevelTakeOff & Math.Abs(deltaPitch) <= ABSdopuskRadian & Math.Abs(deltaRoll) <= ABSdopuskRadian & Math.Abs(deltaYaw(baseYaw, currentYaw)) <= ABSdopuskRadian)
            {
                if (TrimmerThrottel < maxTrimmer - correctionAxes) // Ограничим в 500 триммер газа
                {
                    TrimmerThrottel += correctionAxes;  // Добавили газа триммером
                }
                return 0;
            }
            else
            {
                if (!throttelCorrectionFixed & deltaLevel >= LevelTakeOff) // Газа достаточно для отрыва
                {
                    throttelCorrectionFixed = true;
                    TrimmerThrottel -= 2 * correctionAxes; // Выставили тримммер газа!!!
                    return correctionAxes;
                }
                // Считаем, что тут дребезг контактов, т.к. газа нет, а уровни "гуляют"
            }


            // Сравниваем данные по трём осям - если есть отличия - убрали газ, внесли корректировки, иначе - коррект
            if (deltaLevel > LevelTakeOff & Math.Abs(currentPitch) <= ABSdopuskRadian & Math.Abs(currentRoll) <= ABSdopuskRadian & Math.Abs(deltaYaw(baseYaw, currentYaw)) <= ABSdopuskRadian)  // Вывели все уровни в висении
            {
                correctAxes = true;
                return correctionAxes;
            }
            else// Корректируем все уровни
            {
                correctAxes = false;
                // Анализируем были ли изменения по осям из-за увеличения газа. Если были - газ надо убрать, иначе перевернёмся.
                if (deltaLevel <= LevelTakeOff & Math.Abs(deltaPitch) <= ABSdopuskRadian & Math.Abs(deltaRoll) <= ABSdopuskRadian & Math.Abs(deltaYaw(baseYaw, currentYaw)) <= ABSdopuskRadian)
                {
                    // Изменений по осям не было. Можно вносить корректировки и добавлять газ
                    corrAxes(currentPitch, currentRoll, deltaYaw(baseYaw, currentYaw), ABSdopuskRadian);
                    return correctionAxes;
                }
                else
                {
                    // Так как изменения по осям были, а триммер газа уже выставлен, то отправляем -20!!
                    // Корректируем триммеры с уменьшением газа!!!
                    corrAxes(currentPitch, currentRoll, deltaYaw(baseYaw, currentYaw), ABSdopuskRadian);
                    return -1 * correctionAxes;
                }
            }
        }

        /// <summary>
        /// Корректируем оси по моментальным параметрам
        /// </summary>
        /// <param name="currentPitch">Текущий тангаж</param>
        /// <param name="currentRoll">Текущий крен</param>
        /// <param name="deltaYaw">Изменение курса</param>
        /// <param name="ABSdopuskRadian">Допуск в радианах</param>
        private void corrAxes(double currentPitch, double currentRoll, double deltaYaw, double ABSdopuskRadian)
        {
            if (Math.Abs(currentPitch) > ABSdopuskRadian)   // Корректируем тангаж
            {
                if (currentPitch > ABSdopuskRadian)
                {
                    if (TrimmerPitch > correctionAxes - maxTrimmer)    // Чтоб не выходили за уровни
                    {
                        TrimmerPitch -= correctionAxes;
                    }
                }
                else
                {
                    if (TrimmerPitch < maxTrimmer - correctionAxes)    // Чтоб не выходили за уровни
                    {
                        TrimmerPitch += correctionAxes;
                    }
                }
                // С тангажом порядок, проверяем крен и курс
                if (Math.Abs(currentRoll) > ABSdopuskRadian)// Корректируем крен при нормальном тангаже
                {
                    if (currentRoll > ABSdopuskRadian)
                    {
                        if (TrimmerRoll > correctionAxes - maxTrimmer)    // Чтоб не выходили за уровни
                        {
                            TrimmerRoll -= correctionAxes;
                        }

                    }
                    else
                    {
                        if (TrimmerRoll < maxTrimmer - correctionAxes)    // Чтоб не выходили за уровни
                        {
                            TrimmerRoll += correctionAxes;
                        }

                    }
                    // С креном тоже порядок. Остался курс...
                    if (Math.Abs(deltaYaw) > ABSdopuskRadian)   // Курс корректируем только при необходимости
                    {
                        if (deltaYaw > ABSdopuskRadian)
                        {
                            if (TrimmerYaw > correctionAxes - maxTrimmer)
                            {
                                TrimmerYaw -= correctionAxes;
                            }

                        }
                        else
                        {
                            if (TrimmerYaw < maxTrimmer - correctionAxes)
                            {
                                TrimmerYaw += correctionAxes;
                            }
                        }
                    }
                }
                else// С креном тоже порядок. Остался курс...
                {
                    if (Math.Abs(deltaYaw) > ABSdopuskRadian)   // Курс корректируем только при необходимости
                    {
                        if (deltaYaw > ABSdopuskRadian)
                        {
                            if (TrimmerYaw > correctionAxes - maxTrimmer)
                            {
                                TrimmerYaw -= correctionAxes;
                            }
                        }
                        else
                        {
                            if (TrimmerYaw < maxTrimmer - correctionAxes)
                            {
                                TrimmerYaw += correctionAxes;
                            }
                        }
                    }
                }
            }
            else// С тангажом порядок, проверяем крен и курс
            {
                if (Math.Abs(currentRoll) > ABSdopuskRadian)// Корректируем крен при нормальном тангаже
                {
                    if (currentRoll > ABSdopuskRadian)
                    {
                        if (TrimmerRoll > correctionAxes - maxTrimmer)
                        {
                            TrimmerRoll -= correctionAxes;
                        }
                    }
                    else
                    {
                        if (TrimmerRoll < maxTrimmer - correctionAxes)
                        {
                            TrimmerRoll += correctionAxes;
                        }
                    }
                    // С креном тоже порядок. Остался курс...
                    if (Math.Abs(deltaYaw) > ABSdopuskRadian)   // Курс корректируем только при необходимости
                    {
                        if (deltaYaw > ABSdopuskRadian)
                        {
                            if (TrimmerYaw > correctionAxes - maxTrimmer)
                            {
                                TrimmerYaw -= correctionAxes;
                            }
                        }
                        else
                        {
                            if (TrimmerYaw < maxTrimmer - correctionAxes)
                            {
                                TrimmerYaw += correctionAxes;
                            }
                        }
                    }
                }
                else// С креном тоже порядок. Остался курс...
                {
                    if (Math.Abs(deltaYaw) > ABSdopuskRadian)   // Курс корректируем только при необходимости
                    {
                        if (deltaYaw > ABSdopuskRadian)
                        {
                            if (TrimmerYaw > correctionAxes - maxTrimmer)
                            {
                                TrimmerYaw -= correctionAxes;
                            }
                        }
                        else
                        {
                            if (TrimmerYaw < maxTrimmer - correctionAxes)
                            {
                                TrimmerYaw += correctionAxes;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Функция контрольного висения. Поднимаемся на минимальную безопасную и разворачиваемся на курс.
        /// </summary>
        internal int[] ControlHoveringFunction(double targetLevel, double targetYaw)
        {
            int rezultFunction = 0; // Результат работы функции (1 - если успешно завершено)
            int throttleLevel = 0;  // Текущий уровень изменения оси газа для вывода уровня высоты
            int yawLevel = 0;   // Текущий уровень изменения оси курса для вывода установки на заданный курс

            string[] lastaxes = SQuadroData[SQuadroData.Count - 1].Trim().Split(';');
            haveNewSData = false;   // Обработали строку с последними данными
            double currentYaw = Convert.ToDouble(lastaxes[(int)Axes.YAW], CultureInfo.GetCultureInfo("en-US"));
            double currentLevel = Convert.ToDouble(lastaxes[(int)Axes.ALT], CultureInfo.GetCultureInfo("en-US"));  // Определяем текущий уровень высоты.
            double deltaLevel = targetLevel - currentLevel; // Оставшийся уровень высоты до мин.без.
            /* Первое значение массива должно возвращать либо -20 для уменьшения газа, но не более одного раза при начале снижения
             * либо 0, если достигли дельты с минимальной безопасной 0 или идёт снижение при перелёте
             * либо +20 если надо набрать высоту, но не менее трёх периодов в запасе для первого значения массива.
             */
            //  Сначала смотрим надо ли нам взлетать или опускаться или зависнуть на этом уровне
            if (deltaLevel > 0)
            {// Надо взлетать
                // укладываемся ли в три периода за подъём
                if ((lastDeltaLevel - deltaLevel) * 3 > deltaLevel)
                {//Достаточная скороподъёмность. Надо плавно снижать
                    throttleLevel -= correctionAxes;
                }
                else
                {// Скороподъёмность низкая. Надо увеличить
                    throttleLevel += correctionAxes;
                }
            }
            else
            {// Надо зависнуть или плавно опускаться
                // Если пролетели уровень и продолжаем взлетать или висеть, то надо убрать газ
                // Если пролетели и опускаемся, то ничего не меняем
                if (lastDeltaLevel >= deltaLevel)
                {
                    throttleLevel -= correctionAxes;
                }
                else
                {
                    throttleLevel = 0;
                }
            }
            // Для второго значения массива надо давать аналогичные данные для поворота влево или вправо
            // Сначала смотрим надо ли нам поворачивать вообще
            if (Math.Abs(deltaYaw(targetYaw, currentYaw)) > ABSdopuskRadian)
            {// Поворачивать надо. Смотрим куда, влево или вправо
                if (deltaYaw(targetYaw, currentYaw) < 0)
                {//Поворот влево 
                    if (lastDeltaYaw < deltaYaw(targetYaw, currentYaw))
                    {//Поворот уже происходит
                        yawLevel = 0;
                    }
                    else
                    {
                        yawLevel += correctionAxes;
                    }
                }
                else
                {// Поворот вправо
                    if (lastDeltaYaw > deltaYaw(targetYaw, currentYaw))
                    {//Поворот уже происходит
                        yawLevel = 0;
                    }
                    else
                    {
                        yawLevel -= correctionAxes;
                    }
                }
            }

            // По завершению цикла перезаписываем новые значения глобальных переменных
            lastDeltaLevel = deltaLevel;
            lastDeltaYaw = deltaYaw(targetYaw, currentYaw);
            if (Math.Abs(deltaLevel) <= LevelTakeOff && Math.Abs(deltaYaw(targetYaw, currentYaw)) <= ABSdopuskRadian)
            {//Подпрограмма считается выполненой когда дельта между текущей и минимальной равно 0 (с допуском) и легли на курс (3-й параметр массива)
                rezultFunction = 1;
            }
            int[] CHF = { throttleLevel, yawLevel, rezultFunction };
            return CHF;
        }

        /// <summary>
        /// Конвертация координат из формата NMEA в формат координат десятичных градусов (для расчёта расстояний и курсов)
        /// </summary>
        /// <param name="latNMEA">Строка широты в формате NMEA ddmm.mmmm</param>
        /// <param name="lonNMEA">Строка долготы в формате NMEA dddmm.mmmm</param>
        /// <returns>Возврат координат в виде массива в формате dd.dddd</returns>
        internal string[] convNMEAtoGEO(string latNMEA, string lonNMEA)
        {
            string[] lat = latNMEA.Split('.'); // Разделили на ddmm и десятые mmmm
            string[] lon = lonNMEA.Split('.'); // Разделили на dddmm и десятые mmmm
            string grdLat = lat[0].Remove(2);
            if (grdLat.StartsWith("0"))
            {
                grdLat = grdLat.Remove(0, 1);
            }
            string grdLon = lon[0].Remove(3);
            for (int i = 0; i < 2; i++)
            {
                if (grdLon.StartsWith("0"))
                {
                    grdLon = grdLon.Remove(0, 1);
                }
            }
            string latGEO = grdLat + "." + (Convert.ToDouble(lat[0].Remove(0, 2) + "," + lat[1]) / 60).ToString().Remove(0, 2);
            string lonGEO = grdLon + "." + (Convert.ToDouble(lon[0].Remove(0, 3) + "," + lon[1]) / 60).ToString().Remove(0, 2);
            string[] coordGEO = { latGEO, lonGEO };
            return coordGEO;
        }

        /// <summary>
        /// Функция расчёта изменения угла от текущего показателя до необходимого.
        /// </summary>
        /// <param name="targetYaw">Курс, на который надо выйти</param>
        /// <param name="currentYaw">Текущее значение курса</param>
        /// <returns>Возвращает наименьший угол разворота на заданный курс. + вправо, - влево</returns>
        internal double deltaYaw(double targetYaw, double currentYaw)
        {
            double delta = 0;
            if (targetYaw < Math.Round(Math.PI, 2))
            {   // Первая и вторая четверти
                if (currentYaw >= targetYaw & currentYaw <= targetYaw + Math.Round(Math.PI, 2))
                {   // Ушли вправо, показываем +
                    delta = currentYaw - targetYaw;
                }
                else
                {   // Ушли влево, показываем -
                    if (currentYaw < targetYaw)
                    {   // Текущие данные от базы до 0
                        delta = currentYaw - targetYaw;
                    }
                    else
                    {   // Текущие данные до 360
                        delta = currentYaw - (targetYaw + Math.Round(Math.PI * 2, 2));
                    }
                }
            }
            else
            {   // Третья и четвёртая четверти
                if (currentYaw <= targetYaw & currentYaw >= targetYaw - Math.Round(Math.PI, 2))
                {   // Ушли влево, показываем -
                    delta = currentYaw - targetYaw;
                }
                else
                {   // Ушли вправо, показываем +
                    if (currentYaw > targetYaw)
                    {   // До 360
                        delta = currentYaw - targetYaw;
                    }
                    else
                    {   // От 0
                        delta = (currentYaw + Math.Round(Math.PI * 2, 2)) - targetYaw;
                    }
                }
            }
            return delta;
        }
    }
}
