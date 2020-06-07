namespace OnBoardPilotSystem
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Предполётный лист проверок", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Полётный лист проверок", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Посадка на базу", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Данные GPS поступают"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Arduino подключена"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Команда включения автопилота получена"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Точка старта/финиша определена"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "План полёта загружен"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "План полёта проверен"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Триммеры осей отрегулированы"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Контрольное висение"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Полёт по маршруту"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Посадка"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightCoral, null);
            this.GPSSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.ArduinoSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.TabControl = new System.Windows.Forms.TabControl();
            this.ControlTabPage = new System.Windows.Forms.TabPage();
            this.PreFlightGroupBox = new System.Windows.Forms.GroupBox();
            this.PreFlightListView = new System.Windows.Forms.ListView();
            this.ArduinoGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ArduinoTextBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.ArduSendTextBox = new System.Windows.Forms.TextBox();
            this.PilotControlGroupBox = new System.Windows.Forms.GroupBox();
            this.TrimmerYawLabel = new System.Windows.Forms.Label();
            this.TrimmerRollLabel = new System.Windows.Forms.Label();
            this.TrimmerPitchLabel = new System.Windows.Forms.Label();
            this.TrimmerThrottleLabel = new System.Windows.Forms.Label();
            this.SatellitesLabel = new System.Windows.Forms.Label();
            this.UTCTimeLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.ThrottleLabelCurr = new System.Windows.Forms.Label();
            this.ThrottleLabel = new System.Windows.Forms.Label();
            this.PitchLabelCurr = new System.Windows.Forms.Label();
            this.PitchLabel = new System.Windows.Forms.Label();
            this.YawLabelCurr = new System.Windows.Forms.Label();
            this.YawLabel = new System.Windows.Forms.Label();
            this.RollLabelCurr = new System.Windows.Forms.Label();
            this.RollLabel = new System.Windows.Forms.Label();
            this.ThrottleTrackBar = new System.Windows.Forms.TrackBar();
            this.PitchTrackBar = new System.Windows.Forms.TrackBar();
            this.YawTrackBar = new System.Windows.Forms.TrackBar();
            this.RollTrackBar = new System.Windows.Forms.TrackBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PilotControlStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.GPSGroupBox = new System.Windows.Forms.GroupBox();
            this.LastGPSListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.ShutDownCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MaxTrimmerTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.waitThreadTextBox = new System.Windows.Forms.TextBox();
            this.correctionAxesTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.minSafeAltTextBox = new System.Windows.Forms.TextBox();
            this.LevelTakeOffTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ABSdopuskRadianTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.ChangePortButton = new System.Windows.Forms.Button();
            this.SetGroupBoxArduino = new System.Windows.Forms.GroupBox();
            this.AutoConnectArduTextBox = new System.Windows.Forms.TextBox();
            this.ArduListView = new System.Windows.Forms.ListView();
            this.PortColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ArduComboBoxBaud = new System.Windows.Forms.ComboBox();
            this.SetGroupBoxGPS = new System.Windows.Forms.GroupBox();
            this.AutoConnectGPSTextBox = new System.Windows.Forms.TextBox();
            this.GPSListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BlackBoxLabelCurr = new System.Windows.Forms.Label();
            this.BlackBoxLabel = new System.Windows.Forms.Label();
            this.BlackBoxTrackBar = new System.Windows.Forms.TrackBar();
            this.GPSComboBoxBaud = new System.Windows.Forms.ComboBox();
            this.FlightControlTabPage = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.allFlightTimeLabel = new System.Windows.Forms.Label();
            this.allDistanceLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.maxDistFlightTextBox = new System.Windows.Forms.TextBox();
            this.maxFlightTimeTextBox = new System.Windows.Forms.TextBox();
            this.NormSpeedTextBox = new System.Windows.Forms.TextBox();
            this.FlightPlanListView = new System.Windows.Forms.ListView();
            this.PointNumberColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LщтColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LatColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HighColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.YawСolumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MeterColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SecColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabControl.SuspendLayout();
            this.ControlTabPage.SuspendLayout();
            this.PreFlightGroupBox.SuspendLayout();
            this.ArduinoGroupBox.SuspendLayout();
            this.PilotControlGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThrottleTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PitchTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YawTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RollTrackBar)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.GPSGroupBox.SuspendLayout();
            this.SettingsTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SetGroupBoxArduino.SuspendLayout();
            this.SetGroupBoxGPS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlackBoxTrackBar)).BeginInit();
            this.FlightControlTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // GPSSerialPort
            // 
            this.GPSSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.GPSSerialPort_DataReceived);
            // 
            // ArduinoSerialPort
            // 
            this.ArduinoSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.ArduinoSerialPort_DataReceived);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.ControlTabPage);
            this.TabControl.Controls.Add(this.SettingsTabPage);
            this.TabControl.Controls.Add(this.FlightControlTabPage);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(798, 445);
            this.TabControl.TabIndex = 0;
            // 
            // ControlTabPage
            // 
            this.ControlTabPage.Controls.Add(this.PreFlightGroupBox);
            this.ControlTabPage.Controls.Add(this.ArduinoGroupBox);
            this.ControlTabPage.Controls.Add(this.PilotControlGroupBox);
            this.ControlTabPage.Controls.Add(this.GPSGroupBox);
            this.ControlTabPage.Location = new System.Drawing.Point(4, 22);
            this.ControlTabPage.Name = "ControlTabPage";
            this.ControlTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ControlTabPage.Size = new System.Drawing.Size(790, 419);
            this.ControlTabPage.TabIndex = 0;
            this.ControlTabPage.Text = "Control";
            this.ControlTabPage.UseVisualStyleBackColor = true;
            // 
            // PreFlightGroupBox
            // 
            this.PreFlightGroupBox.Controls.Add(this.PreFlightListView);
            this.PreFlightGroupBox.Location = new System.Drawing.Point(406, 3);
            this.PreFlightGroupBox.Name = "PreFlightGroupBox";
            this.PreFlightGroupBox.Size = new System.Drawing.Size(379, 218);
            this.PreFlightGroupBox.TabIndex = 5;
            this.PreFlightGroupBox.TabStop = false;
            this.PreFlightGroupBox.Text = "Полётные тесты";
            // 
            // PreFlightListView
            // 
            this.PreFlightListView.BackColor = System.Drawing.SystemColors.Window;
            this.PreFlightListView.CheckBoxes = true;
            this.PreFlightListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreFlightListView.GridLines = true;
            listViewGroup1.Header = "Предполётный лист проверок";
            listViewGroup1.Name = "PreFlightListViewGroup";
            listViewGroup2.Header = "Полётный лист проверок";
            listViewGroup2.Name = "FlightListViewGroup";
            listViewGroup3.Header = "Посадка на базу";
            listViewGroup3.Name = "BaseListViewGroup";
            this.PreFlightListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Group = listViewGroup1;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.Group = listViewGroup1;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.Group = listViewGroup1;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.Group = listViewGroup1;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.Group = listViewGroup1;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.Group = listViewGroup2;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.Group = listViewGroup2;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.Group = listViewGroup2;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.Group = listViewGroup3;
            listViewItem10.StateImageIndex = 0;
            this.PreFlightListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.PreFlightListView.Location = new System.Drawing.Point(3, 16);
            this.PreFlightListView.Name = "PreFlightListView";
            this.PreFlightListView.Size = new System.Drawing.Size(373, 199);
            this.PreFlightListView.TabIndex = 0;
            this.PreFlightListView.UseCompatibleStateImageBehavior = false;
            this.PreFlightListView.View = System.Windows.Forms.View.SmallIcon;
            this.PreFlightListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.PreFlightListView_ItemChecked);
            // 
            // ArduinoGroupBox
            // 
            this.ArduinoGroupBox.Controls.Add(this.button1);
            this.ArduinoGroupBox.Controls.Add(this.ArduinoTextBox);
            this.ArduinoGroupBox.Controls.Add(this.SendButton);
            this.ArduinoGroupBox.Controls.Add(this.ArduSendTextBox);
            this.ArduinoGroupBox.Location = new System.Drawing.Point(406, 224);
            this.ArduinoGroupBox.Name = "ArduinoGroupBox";
            this.ArduinoGroupBox.Size = new System.Drawing.Size(379, 115);
            this.ArduinoGroupBox.TabIndex = 4;
            this.ArduinoGroupBox.TabStop = false;
            this.ArduinoGroupBox.Text = "Данные Arduino";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(208, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Отметить группу тестов";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ArduinoTextBox
            // 
            this.ArduinoTextBox.Location = new System.Drawing.Point(3, 16);
            this.ArduinoTextBox.MaxLength = 3276700;
            this.ArduinoTextBox.Multiline = true;
            this.ArduinoTextBox.Name = "ArduinoTextBox";
            this.ArduinoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ArduinoTextBox.Size = new System.Drawing.Size(373, 53);
            this.ArduinoTextBox.TabIndex = 3;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(3, 69);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(128, 23);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "Отправить";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ArduSendTextBox
            // 
            this.ArduSendTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ArduSendTextBox.Location = new System.Drawing.Point(3, 92);
            this.ArduSendTextBox.Name = "ArduSendTextBox";
            this.ArduSendTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ArduSendTextBox.Size = new System.Drawing.Size(373, 20);
            this.ArduSendTextBox.TabIndex = 1;
            // 
            // PilotControlGroupBox
            // 
            this.PilotControlGroupBox.Controls.Add(this.TrimmerYawLabel);
            this.PilotControlGroupBox.Controls.Add(this.TrimmerRollLabel);
            this.PilotControlGroupBox.Controls.Add(this.TrimmerPitchLabel);
            this.PilotControlGroupBox.Controls.Add(this.TrimmerThrottleLabel);
            this.PilotControlGroupBox.Controls.Add(this.SatellitesLabel);
            this.PilotControlGroupBox.Controls.Add(this.UTCTimeLabel);
            this.PilotControlGroupBox.Controls.Add(this.DateLabel);
            this.PilotControlGroupBox.Controls.Add(this.ThrottleLabelCurr);
            this.PilotControlGroupBox.Controls.Add(this.ThrottleLabel);
            this.PilotControlGroupBox.Controls.Add(this.PitchLabelCurr);
            this.PilotControlGroupBox.Controls.Add(this.PitchLabel);
            this.PilotControlGroupBox.Controls.Add(this.YawLabelCurr);
            this.PilotControlGroupBox.Controls.Add(this.YawLabel);
            this.PilotControlGroupBox.Controls.Add(this.RollLabelCurr);
            this.PilotControlGroupBox.Controls.Add(this.RollLabel);
            this.PilotControlGroupBox.Controls.Add(this.ThrottleTrackBar);
            this.PilotControlGroupBox.Controls.Add(this.PitchTrackBar);
            this.PilotControlGroupBox.Controls.Add(this.YawTrackBar);
            this.PilotControlGroupBox.Controls.Add(this.RollTrackBar);
            this.PilotControlGroupBox.Controls.Add(this.statusStrip1);
            this.PilotControlGroupBox.Enabled = false;
            this.PilotControlGroupBox.Location = new System.Drawing.Point(3, 3);
            this.PilotControlGroupBox.Name = "PilotControlGroupBox";
            this.PilotControlGroupBox.Size = new System.Drawing.Size(403, 336);
            this.PilotControlGroupBox.TabIndex = 3;
            this.PilotControlGroupBox.TabStop = false;
            this.PilotControlGroupBox.Text = "PilotControl";
            // 
            // TrimmerYawLabel
            // 
            this.TrimmerYawLabel.AutoSize = true;
            this.TrimmerYawLabel.Location = new System.Drawing.Point(263, 283);
            this.TrimmerYawLabel.Name = "TrimmerYawLabel";
            this.TrimmerYawLabel.Size = new System.Drawing.Size(86, 13);
            this.TrimmerYawLabel.TabIndex = 23;
            this.TrimmerYawLabel.Text = "Триммер курса";
            // 
            // TrimmerRollLabel
            // 
            this.TrimmerRollLabel.AutoSize = true;
            this.TrimmerRollLabel.Location = new System.Drawing.Point(54, 283);
            this.TrimmerRollLabel.Name = "TrimmerRollLabel";
            this.TrimmerRollLabel.Size = new System.Drawing.Size(87, 13);
            this.TrimmerRollLabel.TabIndex = 22;
            this.TrimmerRollLabel.Text = "Триммер крена";
            // 
            // TrimmerPitchLabel
            // 
            this.TrimmerPitchLabel.AutoSize = true;
            this.TrimmerPitchLabel.Location = new System.Drawing.Point(3, 135);
            this.TrimmerPitchLabel.Name = "TrimmerPitchLabel";
            this.TrimmerPitchLabel.Size = new System.Drawing.Size(82, 13);
            this.TrimmerPitchLabel.TabIndex = 21;
            this.TrimmerPitchLabel.Text = "Трим. тангажа";
            // 
            // TrimmerThrottleLabel
            // 
            this.TrimmerThrottleLabel.AutoSize = true;
            this.TrimmerThrottleLabel.Location = new System.Drawing.Point(207, 135);
            this.TrimmerThrottleLabel.Name = "TrimmerThrottleLabel";
            this.TrimmerThrottleLabel.Size = new System.Drawing.Size(80, 13);
            this.TrimmerThrottleLabel.TabIndex = 20;
            this.TrimmerThrottleLabel.Text = "Триммер газа";
            // 
            // SatellitesLabel
            // 
            this.SatellitesLabel.AutoSize = true;
            this.SatellitesLabel.Location = new System.Drawing.Point(371, 18);
            this.SatellitesLabel.Name = "SatellitesLabel";
            this.SatellitesLabel.Size = new System.Drawing.Size(23, 13);
            this.SatellitesLabel.TabIndex = 19;
            this.SatellitesLabel.Text = "Sat";
            // 
            // UTCTimeLabel
            // 
            this.UTCTimeLabel.AutoSize = true;
            this.UTCTimeLabel.Location = new System.Drawing.Point(189, 18);
            this.UTCTimeLabel.Name = "UTCTimeLabel";
            this.UTCTimeLabel.Size = new System.Drawing.Size(40, 13);
            this.UTCTimeLabel.TabIndex = 18;
            this.UTCTimeLabel.Text = "Время";
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Location = new System.Drawing.Point(3, 18);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(33, 13);
            this.DateLabel.TabIndex = 17;
            this.DateLabel.Text = "Дата";
            // 
            // ThrottleLabelCurr
            // 
            this.ThrottleLabelCurr.AutoSize = true;
            this.ThrottleLabelCurr.Location = new System.Drawing.Point(366, 108);
            this.ThrottleLabelCurr.Name = "ThrottleLabelCurr";
            this.ThrottleLabelCurr.Size = new System.Drawing.Size(31, 13);
            this.ThrottleLabelCurr.TabIndex = 16;
            this.ThrottleLabelCurr.Text = "1000";
            // 
            // ThrottleLabel
            // 
            this.ThrottleLabel.AutoSize = true;
            this.ThrottleLabel.Location = new System.Drawing.Point(207, 108);
            this.ThrottleLabel.Name = "ThrottleLabel";
            this.ThrottleLabel.Size = new System.Drawing.Size(72, 13);
            this.ThrottleLabel.TabIndex = 15;
            this.ThrottleLabel.Text = "Газ / Throttle";
            // 
            // PitchLabelCurr
            // 
            this.PitchLabelCurr.AutoSize = true;
            this.PitchLabelCurr.Location = new System.Drawing.Point(160, 108);
            this.PitchLabelCurr.Name = "PitchLabelCurr";
            this.PitchLabelCurr.Size = new System.Drawing.Size(31, 13);
            this.PitchLabelCurr.TabIndex = 14;
            this.PitchLabelCurr.Text = "1500";
            // 
            // PitchLabel
            // 
            this.PitchLabel.AutoSize = true;
            this.PitchLabel.Location = new System.Drawing.Point(3, 108);
            this.PitchLabel.Name = "PitchLabel";
            this.PitchLabel.Size = new System.Drawing.Size(80, 13);
            this.PitchLabel.TabIndex = 13;
            this.PitchLabel.Text = "Тангаж / Pitch";
            // 
            // YawLabelCurr
            // 
            this.YawLabelCurr.AutoSize = true;
            this.YawLabelCurr.Location = new System.Drawing.Point(366, 261);
            this.YawLabelCurr.Name = "YawLabelCurr";
            this.YawLabelCurr.Size = new System.Drawing.Size(31, 13);
            this.YawLabelCurr.TabIndex = 10;
            this.YawLabelCurr.Text = "1500";
            // 
            // YawLabel
            // 
            this.YawLabel.AutoSize = true;
            this.YawLabel.Location = new System.Drawing.Point(207, 261);
            this.YawLabel.Name = "YawLabel";
            this.YawLabel.Size = new System.Drawing.Size(63, 13);
            this.YawLabel.TabIndex = 9;
            this.YawLabel.Text = "Курс / Yaw";
            // 
            // RollLabelCurr
            // 
            this.RollLabelCurr.AutoSize = true;
            this.RollLabelCurr.Location = new System.Drawing.Point(160, 261);
            this.RollLabelCurr.Name = "RollLabelCurr";
            this.RollLabelCurr.Size = new System.Drawing.Size(31, 13);
            this.RollLabelCurr.TabIndex = 8;
            this.RollLabelCurr.Text = "1500";
            // 
            // RollLabel
            // 
            this.RollLabel.AutoSize = true;
            this.RollLabel.Location = new System.Drawing.Point(3, 261);
            this.RollLabel.Name = "RollLabel";
            this.RollLabel.Size = new System.Drawing.Size(61, 13);
            this.RollLabel.TabIndex = 7;
            this.RollLabel.Text = "Крен / Roll";
            // 
            // ThrottleTrackBar
            // 
            this.ThrottleTrackBar.LargeChange = 100;
            this.ThrottleTrackBar.Location = new System.Drawing.Point(315, 18);
            this.ThrottleTrackBar.Maximum = 2000;
            this.ThrottleTrackBar.Minimum = 1000;
            this.ThrottleTrackBar.Name = "ThrottleTrackBar";
            this.ThrottleTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ThrottleTrackBar.Size = new System.Drawing.Size(45, 190);
            this.ThrottleTrackBar.SmallChange = 30;
            this.ThrottleTrackBar.TabIndex = 4;
            this.ThrottleTrackBar.Value = 1000;
            this.ThrottleTrackBar.Scroll += new System.EventHandler(this.ThrottleTrackBar_Scroll);
            this.ThrottleTrackBar.ValueChanged += new System.EventHandler(this.ThrottleTrackBar_Scroll);
            // 
            // PitchTrackBar
            // 
            this.PitchTrackBar.LargeChange = 100;
            this.PitchTrackBar.Location = new System.Drawing.Point(109, 18);
            this.PitchTrackBar.Maximum = 2000;
            this.PitchTrackBar.Minimum = 1000;
            this.PitchTrackBar.Name = "PitchTrackBar";
            this.PitchTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.PitchTrackBar.Size = new System.Drawing.Size(45, 190);
            this.PitchTrackBar.SmallChange = 30;
            this.PitchTrackBar.TabIndex = 3;
            this.PitchTrackBar.Value = 1500;
            this.PitchTrackBar.Scroll += new System.EventHandler(this.PitchTrackBar_Scroll);
            this.PitchTrackBar.ValueChanged += new System.EventHandler(this.PitchTrackBar_Scroll);
            // 
            // YawTrackBar
            // 
            this.YawTrackBar.LargeChange = 100;
            this.YawTrackBar.Location = new System.Drawing.Point(210, 213);
            this.YawTrackBar.Maximum = 2000;
            this.YawTrackBar.Minimum = 1000;
            this.YawTrackBar.Name = "YawTrackBar";
            this.YawTrackBar.Size = new System.Drawing.Size(190, 45);
            this.YawTrackBar.SmallChange = 30;
            this.YawTrackBar.TabIndex = 2;
            this.YawTrackBar.Value = 1500;
            this.YawTrackBar.Scroll += new System.EventHandler(this.YawTrackBar_Scroll);
            this.YawTrackBar.ValueChanged += new System.EventHandler(this.YawTrackBar_Scroll);
            // 
            // RollTrackBar
            // 
            this.RollTrackBar.LargeChange = 100;
            this.RollTrackBar.Location = new System.Drawing.Point(3, 213);
            this.RollTrackBar.Maximum = 2000;
            this.RollTrackBar.Minimum = 1000;
            this.RollTrackBar.Name = "RollTrackBar";
            this.RollTrackBar.Size = new System.Drawing.Size(190, 45);
            this.RollTrackBar.SmallChange = 30;
            this.RollTrackBar.TabIndex = 1;
            this.RollTrackBar.Value = 1500;
            this.RollTrackBar.Scroll += new System.EventHandler(this.RollTrackBar_Scroll);
            this.RollTrackBar.ValueChanged += new System.EventHandler(this.RollTrackBar_Scroll);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PilotControlStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(3, 311);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(397, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PilotControlStatusLabel
            // 
            this.PilotControlStatusLabel.Name = "PilotControlStatusLabel";
            this.PilotControlStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.PilotControlStatusLabel.Text = "Status";
            // 
            // GPSGroupBox
            // 
            this.GPSGroupBox.BackColor = System.Drawing.Color.LightCoral;
            this.GPSGroupBox.Controls.Add(this.LastGPSListView);
            this.GPSGroupBox.Location = new System.Drawing.Point(3, 339);
            this.GPSGroupBox.Name = "GPSGroupBox";
            this.GPSGroupBox.Size = new System.Drawing.Size(785, 76);
            this.GPSGroupBox.TabIndex = 2;
            this.GPSGroupBox.TabStop = false;
            this.GPSGroupBox.Text = "Данные GPS приёмника";
            // 
            // LastGPSListView
            // 
            this.LastGPSListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.LastGPSListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LastGPSListView.Location = new System.Drawing.Point(3, 16);
            this.LastGPSListView.Name = "LastGPSListView";
            this.LastGPSListView.Size = new System.Drawing.Size(779, 57);
            this.LastGPSListView.TabIndex = 0;
            this.LastGPSListView.UseCompatibleStateImageBehavior = false;
            this.LastGPSListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Время UTC";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Широта";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Север/Юг";
            this.columnHeader5.Width = 62;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Долгота";
            this.columnHeader6.Width = 55;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Запад/Восток";
            this.columnHeader7.Width = 84;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Высота";
            this.columnHeader8.Width = 50;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Ед. изм. высоты";
            this.columnHeader9.Width = 96;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Скорость";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Курс";
            this.columnHeader11.Width = 36;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Вариация";
            this.columnHeader12.Width = 212;
            // 
            // SettingsTabPage
            // 
            this.SettingsTabPage.Controls.Add(this.ShutDownCheckBox);
            this.SettingsTabPage.Controls.Add(this.groupBox2);
            this.SettingsTabPage.Controls.Add(this.groupBox1);
            this.SettingsTabPage.Controls.Add(this.ResetButton);
            this.SettingsTabPage.Controls.Add(this.ChangePortButton);
            this.SettingsTabPage.Controls.Add(this.SetGroupBoxArduino);
            this.SettingsTabPage.Controls.Add(this.SetGroupBoxGPS);
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTabPage.Size = new System.Drawing.Size(790, 419);
            this.SettingsTabPage.TabIndex = 1;
            this.SettingsTabPage.Text = "Settings";
            this.SettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // ShutDownCheckBox
            // 
            this.ShutDownCheckBox.AutoSize = true;
            this.ShutDownCheckBox.Checked = global::OnBoardPilotSystem.Properties.Settings.Default.ShutDuwnAuto;
            this.ShutDownCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::OnBoardPilotSystem.Properties.Settings.Default, "ShutDuwnAuto", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShutDownCheckBox.Location = new System.Drawing.Point(397, 300);
            this.ShutDownCheckBox.Name = "ShutDownCheckBox";
            this.ShutDownCheckBox.Size = new System.Drawing.Size(340, 17);
            this.ShutDownCheckBox.TabIndex = 21;
            this.ShutDownCheckBox.Text = "Автоматическое завершение программы по разряду батареи";
            this.ShutDownCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MaxTrimmerTextBox);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.waitThreadTextBox);
            this.groupBox2.Controls.Add(this.correctionAxesTextBox);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(320, 323);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 90);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Целые";
            // 
            // MaxTrimmerTextBox
            // 
            this.MaxTrimmerTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "MaxTrimmer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MaxTrimmerTextBox.Location = new System.Drawing.Point(7, 64);
            this.MaxTrimmerTextBox.Name = "MaxTrimmerTextBox";
            this.MaxTrimmerTextBox.Size = new System.Drawing.Size(30, 20);
            this.MaxTrimmerTextBox.TabIndex = 19;
            this.MaxTrimmerTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.MaxTrimmer;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(43, 69);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(308, 13);
            this.label17.TabIndex = 20;
            this.label17.Text = "Максимально допустимое значение триммера (по модулю)";
            // 
            // waitThreadTextBox
            // 
            this.waitThreadTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "WaitThread", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.waitThreadTextBox.Location = new System.Drawing.Point(7, 19);
            this.waitThreadTextBox.Name = "waitThreadTextBox";
            this.waitThreadTextBox.Size = new System.Drawing.Size(30, 20);
            this.waitThreadTextBox.TabIndex = 17;
            this.waitThreadTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.WaitThread;
            // 
            // correctionAxesTextBox
            // 
            this.correctionAxesTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "CorrectionAxes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.correctionAxesTextBox.Location = new System.Drawing.Point(7, 41);
            this.correctionAxesTextBox.Name = "correctionAxesTextBox";
            this.correctionAxesTextBox.Size = new System.Drawing.Size(30, 20);
            this.correctionAxesTextBox.TabIndex = 15;
            this.correctionAxesTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.CorrectionAxes;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(43, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(400, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Ждём записи новых данных (можно увеличить, если реакция очень сильная)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(43, 46);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(170, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Шаг изменения данных по осям";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.minSafeAltTextBox);
            this.groupBox1.Controls.Add(this.LevelTakeOffTextBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.ABSdopuskRadianTextBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Location = new System.Drawing.Point(8, 323);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 90);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Двойной точности";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(43, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(204, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Минимально безопасная высота (в м.)";
            // 
            // minSafeAltTextBox
            // 
            this.minSafeAltTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "minSafeAlt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.minSafeAltTextBox.Location = new System.Drawing.Point(7, 19);
            this.minSafeAltTextBox.Name = "minSafeAltTextBox";
            this.minSafeAltTextBox.Size = new System.Drawing.Size(30, 20);
            this.minSafeAltTextBox.TabIndex = 9;
            this.minSafeAltTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.minSafeAlt;
            // 
            // LevelTakeOffTextBox
            // 
            this.LevelTakeOffTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "levelTakeOff", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LevelTakeOffTextBox.Location = new System.Drawing.Point(7, 41);
            this.LevelTakeOffTextBox.Name = "LevelTakeOffTextBox";
            this.LevelTakeOffTextBox.Size = new System.Drawing.Size(30, 20);
            this.LevelTakeOffTextBox.TabIndex = 11;
            this.LevelTakeOffTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.levelTakeOff;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(43, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Уровень взлёта (в м.)";
            // 
            // ABSdopuskRadianTextBox
            // 
            this.ABSdopuskRadianTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "absdopuskRadian", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ABSdopuskRadianTextBox.Location = new System.Drawing.Point(7, 62);
            this.ABSdopuskRadianTextBox.Name = "ABSdopuskRadianTextBox";
            this.ABSdopuskRadianTextBox.Size = new System.Drawing.Size(30, 20);
            this.ABSdopuskRadianTextBox.TabIndex = 13;
            this.ABSdopuskRadianTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.absdopuskRadian;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(43, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(218, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Допуск по крену, тангажу и курсу (в рад.)";
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(396, 270);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(194, 23);
            this.ResetButton.TabIndex = 8;
            this.ResetButton.Text = "Сброс до значений по умолчанию";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // ChangePortButton
            // 
            this.ChangePortButton.Enabled = false;
            this.ChangePortButton.Location = new System.Drawing.Point(664, 270);
            this.ChangePortButton.Name = "ChangePortButton";
            this.ChangePortButton.Size = new System.Drawing.Size(118, 23);
            this.ChangePortButton.TabIndex = 7;
            this.ChangePortButton.Text = "Изменить порты";
            this.ChangePortButton.UseVisualStyleBackColor = true;
            this.ChangePortButton.Click += new System.EventHandler(this.ChangePortButton_Click);
            // 
            // SetGroupBoxArduino
            // 
            this.SetGroupBoxArduino.Controls.Add(this.AutoConnectArduTextBox);
            this.SetGroupBoxArduino.Controls.Add(this.ArduListView);
            this.SetGroupBoxArduino.Controls.Add(this.ArduComboBoxBaud);
            this.SetGroupBoxArduino.Location = new System.Drawing.Point(393, 6);
            this.SetGroupBoxArduino.Name = "SetGroupBoxArduino";
            this.SetGroupBoxArduino.Size = new System.Drawing.Size(394, 242);
            this.SetGroupBoxArduino.TabIndex = 1;
            this.SetGroupBoxArduino.TabStop = false;
            this.SetGroupBoxArduino.Text = "Автоподключение Arduino по указанным параметрам";
            // 
            // AutoConnectArduTextBox
            // 
            this.AutoConnectArduTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "AutoConnectArdu", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AutoConnectArduTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.AutoConnectArduTextBox.Location = new System.Drawing.Point(3, 222);
            this.AutoConnectArduTextBox.Name = "AutoConnectArduTextBox";
            this.AutoConnectArduTextBox.Size = new System.Drawing.Size(388, 20);
            this.AutoConnectArduTextBox.TabIndex = 7;
            this.AutoConnectArduTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.AutoConnectArdu;
            // 
            // ArduListView
            // 
            this.ArduListView.CheckBoxes = true;
            this.ArduListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PortColumnHeader,
            this.NameColumnHeader});
            this.ArduListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.ArduListView.Enabled = false;
            this.ArduListView.Location = new System.Drawing.Point(3, 37);
            this.ArduListView.Name = "ArduListView";
            this.ArduListView.Size = new System.Drawing.Size(388, 185);
            this.ArduListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ArduListView.TabIndex = 8;
            this.ArduListView.UseCompatibleStateImageBehavior = false;
            this.ArduListView.View = System.Windows.Forms.View.Details;
            // 
            // PortColumnHeader
            // 
            this.PortColumnHeader.Text = "Порт";
            // 
            // NameColumnHeader
            // 
            this.NameColumnHeader.Text = "Устройство";
            this.NameColumnHeader.Width = 295;
            // 
            // ArduComboBoxBaud
            // 
            this.ArduComboBoxBaud.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "ArduBaud", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ArduComboBoxBaud.Dock = System.Windows.Forms.DockStyle.Top;
            this.ArduComboBoxBaud.FormattingEnabled = true;
            this.ArduComboBoxBaud.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "9600"});
            this.ArduComboBoxBaud.Location = new System.Drawing.Point(3, 16);
            this.ArduComboBoxBaud.Name = "ArduComboBoxBaud";
            this.ArduComboBoxBaud.Size = new System.Drawing.Size(388, 21);
            this.ArduComboBoxBaud.TabIndex = 2;
            this.ArduComboBoxBaud.Text = global::OnBoardPilotSystem.Properties.Settings.Default.ArduBaud;
            this.ArduComboBoxBaud.SelectedIndexChanged += new System.EventHandler(this.ArduComboBoxBaud_SelectedIndexChanged);
            // 
            // SetGroupBoxGPS
            // 
            this.SetGroupBoxGPS.Controls.Add(this.AutoConnectGPSTextBox);
            this.SetGroupBoxGPS.Controls.Add(this.GPSListView);
            this.SetGroupBoxGPS.Controls.Add(this.BlackBoxLabelCurr);
            this.SetGroupBoxGPS.Controls.Add(this.BlackBoxLabel);
            this.SetGroupBoxGPS.Controls.Add(this.BlackBoxTrackBar);
            this.SetGroupBoxGPS.Controls.Add(this.GPSComboBoxBaud);
            this.SetGroupBoxGPS.Location = new System.Drawing.Point(8, 6);
            this.SetGroupBoxGPS.Name = "SetGroupBoxGPS";
            this.SetGroupBoxGPS.Size = new System.Drawing.Size(383, 316);
            this.SetGroupBoxGPS.TabIndex = 0;
            this.SetGroupBoxGPS.TabStop = false;
            this.SetGroupBoxGPS.Text = "Автоподключение GPS по указанным параметрам";
            // 
            // AutoConnectGPSTextBox
            // 
            this.AutoConnectGPSTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "AutoConnectGPS", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AutoConnectGPSTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.AutoConnectGPSTextBox.Location = new System.Drawing.Point(3, 222);
            this.AutoConnectGPSTextBox.Name = "AutoConnectGPSTextBox";
            this.AutoConnectGPSTextBox.Size = new System.Drawing.Size(377, 20);
            this.AutoConnectGPSTextBox.TabIndex = 8;
            this.AutoConnectGPSTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.AutoConnectGPS;
            // 
            // GPSListView
            // 
            this.GPSListView.CheckBoxes = true;
            this.GPSListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.GPSListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.GPSListView.Enabled = false;
            this.GPSListView.Location = new System.Drawing.Point(3, 37);
            this.GPSListView.Name = "GPSListView";
            this.GPSListView.Size = new System.Drawing.Size(377, 185);
            this.GPSListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.GPSListView.TabIndex = 7;
            this.GPSListView.UseCompatibleStateImageBehavior = false;
            this.GPSListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Порт";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Устройство";
            this.columnHeader2.Width = 290;
            // 
            // BlackBoxLabelCurr
            // 
            this.BlackBoxLabelCurr.AutoSize = true;
            this.BlackBoxLabelCurr.Location = new System.Drawing.Point(354, 248);
            this.BlackBoxLabelCurr.Name = "BlackBoxLabelCurr";
            this.BlackBoxLabelCurr.Size = new System.Drawing.Size(19, 13);
            this.BlackBoxLabelCurr.TabIndex = 5;
            this.BlackBoxLabelCurr.Text = "10";
            // 
            // BlackBoxLabel
            // 
            this.BlackBoxLabel.AutoSize = true;
            this.BlackBoxLabel.Location = new System.Drawing.Point(3, 248);
            this.BlackBoxLabel.Name = "BlackBoxLabel";
            this.BlackBoxLabel.Size = new System.Drawing.Size(151, 13);
            this.BlackBoxLabel.TabIndex = 4;
            this.BlackBoxLabel.Text = "Записать данные GPS (сек.)";
            // 
            // BlackBoxTrackBar
            // 
            this.BlackBoxTrackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::OnBoardPilotSystem.Properties.Settings.Default, "BlackBox", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.BlackBoxTrackBar.Location = new System.Drawing.Point(6, 264);
            this.BlackBoxTrackBar.Maximum = 1000;
            this.BlackBoxTrackBar.Minimum = 10;
            this.BlackBoxTrackBar.Name = "BlackBoxTrackBar";
            this.BlackBoxTrackBar.Size = new System.Drawing.Size(373, 45);
            this.BlackBoxTrackBar.TabIndex = 3;
            this.BlackBoxTrackBar.Value = global::OnBoardPilotSystem.Properties.Settings.Default.BlackBox;
            this.BlackBoxTrackBar.Scroll += new System.EventHandler(this.BlackBoxTrackBar_Scroll);
            this.BlackBoxTrackBar.ValueChanged += new System.EventHandler(this.BlackBoxTrackBar_ValueChanged);
            // 
            // GPSComboBoxBaud
            // 
            this.GPSComboBoxBaud.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "GPSBaud", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.GPSComboBoxBaud.Dock = System.Windows.Forms.DockStyle.Top;
            this.GPSComboBoxBaud.FormattingEnabled = true;
            this.GPSComboBoxBaud.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "9600"});
            this.GPSComboBoxBaud.Location = new System.Drawing.Point(3, 16);
            this.GPSComboBoxBaud.Name = "GPSComboBoxBaud";
            this.GPSComboBoxBaud.Size = new System.Drawing.Size(377, 21);
            this.GPSComboBoxBaud.TabIndex = 1;
            this.GPSComboBoxBaud.Text = global::OnBoardPilotSystem.Properties.Settings.Default.GPSBaud;
            this.GPSComboBoxBaud.SelectedIndexChanged += new System.EventHandler(this.GPSComboBoxBaud_SelectedIndexChanged);
            // 
            // FlightControlTabPage
            // 
            this.FlightControlTabPage.Controls.Add(this.label10);
            this.FlightControlTabPage.Controls.Add(this.label11);
            this.FlightControlTabPage.Controls.Add(this.label8);
            this.FlightControlTabPage.Controls.Add(this.label9);
            this.FlightControlTabPage.Controls.Add(this.allFlightTimeLabel);
            this.FlightControlTabPage.Controls.Add(this.allDistanceLabel);
            this.FlightControlTabPage.Controls.Add(this.label7);
            this.FlightControlTabPage.Controls.Add(this.label6);
            this.FlightControlTabPage.Controls.Add(this.label5);
            this.FlightControlTabPage.Controls.Add(this.label4);
            this.FlightControlTabPage.Controls.Add(this.label3);
            this.FlightControlTabPage.Controls.Add(this.label2);
            this.FlightControlTabPage.Controls.Add(this.label1);
            this.FlightControlTabPage.Controls.Add(this.maxDistFlightTextBox);
            this.FlightControlTabPage.Controls.Add(this.maxFlightTimeTextBox);
            this.FlightControlTabPage.Controls.Add(this.NormSpeedTextBox);
            this.FlightControlTabPage.Controls.Add(this.FlightPlanListView);
            this.FlightControlTabPage.Location = new System.Drawing.Point(4, 22);
            this.FlightControlTabPage.Name = "FlightControlTabPage";
            this.FlightControlTabPage.Size = new System.Drawing.Size(790, 419);
            this.FlightControlTabPage.TabIndex = 2;
            this.FlightControlTabPage.Text = "Flight Control";
            this.FlightControlTabPage.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(760, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "м.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(526, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(178, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Максимальная дальность полёта";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(760, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "сек.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(526, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Максимальное время полёта";
            // 
            // allFlightTimeLabel
            // 
            this.allFlightTimeLabel.AutoSize = true;
            this.allFlightTimeLabel.Location = new System.Drawing.Point(706, 18);
            this.allFlightTimeLabel.Name = "allFlightTimeLabel";
            this.allFlightTimeLabel.Size = new System.Drawing.Size(28, 13);
            this.allFlightTimeLabel.TabIndex = 12;
            this.allFlightTimeLabel.Text = "сек.";
            // 
            // allDistanceLabel
            // 
            this.allDistanceLabel.AutoSize = true;
            this.allDistanceLabel.Location = new System.Drawing.Point(706, 5);
            this.allDistanceLabel.Name = "allDistanceLabel";
            this.allDistanceLabel.Size = new System.Drawing.Size(44, 13);
            this.allDistanceLabel.TabIndex = 11;
            this.allDistanceLabel.Text = "метров";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(526, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Общее время (в сек.)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(526, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Общий путь (в метрах)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(760, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "м/с";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(526, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Крейсерская скорость";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отсчёт высоты";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Описание полётного плана";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Название полётного плана";
            // 
            // maxDistFlightTextBox
            // 
            this.maxDistFlightTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "maxDistFlight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.maxDistFlightTextBox.Location = new System.Drawing.Point(709, 35);
            this.maxDistFlightTextBox.Name = "maxDistFlightTextBox";
            this.maxDistFlightTextBox.Size = new System.Drawing.Size(42, 20);
            this.maxDistFlightTextBox.TabIndex = 16;
            this.maxDistFlightTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.maxDistFlight;
            // 
            // maxFlightTimeTextBox
            // 
            this.maxFlightTimeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "maxFlightTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.maxFlightTimeTextBox.Location = new System.Drawing.Point(709, 55);
            this.maxFlightTimeTextBox.Name = "maxFlightTimeTextBox";
            this.maxFlightTimeTextBox.Size = new System.Drawing.Size(42, 20);
            this.maxFlightTimeTextBox.TabIndex = 13;
            this.maxFlightTimeTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.maxFlightTime;
            // 
            // NormSpeedTextBox
            // 
            this.NormSpeedTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OnBoardPilotSystem.Properties.Settings.Default, "NormSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NormSpeedTextBox.Location = new System.Drawing.Point(709, 75);
            this.NormSpeedTextBox.Name = "NormSpeedTextBox";
            this.NormSpeedTextBox.Size = new System.Drawing.Size(42, 20);
            this.NormSpeedTextBox.TabIndex = 6;
            this.NormSpeedTextBox.Text = global::OnBoardPilotSystem.Properties.Settings.Default.NormSpeed;
            // 
            // FlightPlanListView
            // 
            this.FlightPlanListView.CheckBoxes = true;
            this.FlightPlanListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PointNumberColumnHeader,
            this.LщтColumnHeader,
            this.LatColumnHeader,
            this.HighColumnHeader,
            this.YawСolumnHeader,
            this.MeterColumnHeader,
            this.SecColumnHeader});
            this.FlightPlanListView.Enabled = false;
            this.FlightPlanListView.FullRowSelect = true;
            this.FlightPlanListView.Location = new System.Drawing.Point(3, 29);
            this.FlightPlanListView.Name = "FlightPlanListView";
            this.FlightPlanListView.Size = new System.Drawing.Size(517, 382);
            this.FlightPlanListView.TabIndex = 2;
            this.FlightPlanListView.UseCompatibleStateImageBehavior = false;
            this.FlightPlanListView.View = System.Windows.Forms.View.Details;
            // 
            // PointNumberColumnHeader
            // 
            this.PointNumberColumnHeader.Text = "Номер точки";
            this.PointNumberColumnHeader.Width = 77;
            // 
            // LщтColumnHeader
            // 
            this.LщтColumnHeader.Text = "Долгота";
            this.LщтColumnHeader.Width = 70;
            // 
            // LatColumnHeader
            // 
            this.LatColumnHeader.Text = "Широта";
            this.LatColumnHeader.Width = 55;
            // 
            // HighColumnHeader
            // 
            this.HighColumnHeader.Text = "Высота";
            this.HighColumnHeader.Width = 50;
            // 
            // YawСolumnHeader
            // 
            this.YawСolumnHeader.Text = "ЛПУ";
            this.YawСolumnHeader.Width = 36;
            // 
            // MeterColumnHeader
            // 
            this.MeterColumnHeader.Text = "Метров";
            this.MeterColumnHeader.Width = 50;
            // 
            // SecColumnHeader
            // 
            this.SecColumnHeader.Text = "Секунд";
            this.SecColumnHeader.Width = 175;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 445);
            this.Controls.Add(this.TabControl);
            this.Name = "Form1";
            this.Text = "OnBoardPilotSystem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabControl.ResumeLayout(false);
            this.ControlTabPage.ResumeLayout(false);
            this.PreFlightGroupBox.ResumeLayout(false);
            this.ArduinoGroupBox.ResumeLayout(false);
            this.ArduinoGroupBox.PerformLayout();
            this.PilotControlGroupBox.ResumeLayout(false);
            this.PilotControlGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThrottleTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PitchTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YawTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RollTrackBar)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.GPSGroupBox.ResumeLayout(false);
            this.SettingsTabPage.ResumeLayout(false);
            this.SettingsTabPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.SetGroupBoxArduino.ResumeLayout(false);
            this.SetGroupBoxArduino.PerformLayout();
            this.SetGroupBoxGPS.ResumeLayout(false);
            this.SetGroupBoxGPS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlackBoxTrackBar)).EndInit();
            this.FlightControlTabPage.ResumeLayout(false);
            this.FlightControlTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort GPSSerialPort;
        private System.IO.Ports.SerialPort ArduinoSerialPort;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage ControlTabPage;
        private System.Windows.Forms.TabPage SettingsTabPage;
        private System.Windows.Forms.GroupBox GPSGroupBox;
        private System.Windows.Forms.GroupBox PilotControlGroupBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel PilotControlStatusLabel;
        private System.Windows.Forms.GroupBox ArduinoGroupBox;
        private System.Windows.Forms.Label RollLabelCurr;
        private System.Windows.Forms.Label RollLabel;
        private System.Windows.Forms.TrackBar ThrottleTrackBar;
        private System.Windows.Forms.TrackBar PitchTrackBar;
        private System.Windows.Forms.TrackBar YawTrackBar;
        private System.Windows.Forms.TrackBar RollTrackBar;
        private System.Windows.Forms.Label YawLabelCurr;
        private System.Windows.Forms.Label YawLabel;
        private System.Windows.Forms.Label PitchLabelCurr;
        private System.Windows.Forms.Label PitchLabel;
        private System.Windows.Forms.Label ThrottleLabel;
        private System.Windows.Forms.Label ThrottleLabelCurr;
        private System.Windows.Forms.GroupBox SetGroupBoxArduino;
        private System.Windows.Forms.GroupBox SetGroupBoxGPS;
        private System.Windows.Forms.ComboBox GPSComboBoxBaud;
        private System.Windows.Forms.ComboBox ArduComboBoxBaud;
        private System.Windows.Forms.TextBox ArduinoTextBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox ArduSendTextBox;
        private System.Windows.Forms.Label BlackBoxLabelCurr;
        private System.Windows.Forms.Label BlackBoxLabel;
        private System.Windows.Forms.TrackBar BlackBoxTrackBar;
        private System.Windows.Forms.ListView ArduListView;
        private System.Windows.Forms.ColumnHeader PortColumnHeader;
        private System.Windows.Forms.ColumnHeader NameColumnHeader;
        private System.Windows.Forms.Button ChangePortButton;
        private System.Windows.Forms.TextBox AutoConnectArduTextBox;
        private System.Windows.Forms.TextBox AutoConnectGPSTextBox;
        private System.Windows.Forms.ListView GPSListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.GroupBox PreFlightGroupBox;
        private System.Windows.Forms.ListView PreFlightListView;
        private System.Windows.Forms.TabPage FlightControlTabPage;
        private System.Windows.Forms.ListView FlightPlanListView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader PointNumberColumnHeader;
        private System.Windows.Forms.ColumnHeader LщтColumnHeader;
        private System.Windows.Forms.ColumnHeader LatColumnHeader;
        private System.Windows.Forms.ColumnHeader HighColumnHeader;
        private System.Windows.Forms.TextBox NormSpeedTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader YawСolumnHeader;
        private System.Windows.Forms.ColumnHeader MeterColumnHeader;
        private System.Windows.Forms.ColumnHeader SecColumnHeader;
        private System.Windows.Forms.Label allFlightTimeLabel;
        private System.Windows.Forms.Label allDistanceLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox maxDistFlightTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox maxFlightTimeTextBox;
        private System.Windows.Forms.Label UTCTimeLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Label SatellitesLabel;
        private System.Windows.Forms.Label TrimmerYawLabel;
        private System.Windows.Forms.Label TrimmerRollLabel;
        private System.Windows.Forms.Label TrimmerPitchLabel;
        private System.Windows.Forms.Label TrimmerThrottleLabel;
        private System.Windows.Forms.ListView LastGPSListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox minSafeAltTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox LevelTakeOffTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox correctionAxesTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox ABSdopuskRadianTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox waitThreadTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox MaxTrimmerTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox ShutDownCheckBox;
    }
}

