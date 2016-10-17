namespace ReflowController
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.btnGetTcStatus = new System.Windows.Forms.Button();
            this.btnRelayOn = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRelayOff = new System.Windows.Forms.Button();
            this.btnPing = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnGetProfileStage = new System.Windows.Forms.Button();
            this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cboPorts = new System.Windows.Forms.ComboBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnGetReflowProfile = new System.Windows.Forms.Button();
            this.btnSetProfile = new System.Windows.Forms.Button();
            this.lblStartRate = new System.Windows.Forms.Label();
            this.lblSoakTemp1 = new System.Windows.Forms.Label();
            this.lblSoakTemp2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudStartRate = new System.Windows.Forms.NumericUpDown();
            this.nudSoakTemp1 = new System.Windows.Forms.NumericUpDown();
            this.nudSoakTemp2 = new System.Windows.Forms.NumericUpDown();
            this.nudSoakLength = new System.Windows.Forms.NumericUpDown();
            this.nudPeakTemp = new System.Windows.Forms.NumericUpDown();
            this.nudTimeToPeak = new System.Windows.Forms.NumericUpDown();
            this.nudCoolRate = new System.Windows.Forms.NumericUpDown();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.lblPreheat = new System.Windows.Forms.Label();
            this.nudPreheat = new System.Windows.Forms.NumericUpDown();
            this.btnGetPid = new System.Windows.Forms.Button();
            this.btnSetPid = new System.Windows.Forms.Button();
            this.nudKp = new System.Windows.Forms.NumericUpDown();
            this.lblK = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudKi = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudKd = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoakTemp1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoakTemp2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoakLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeakTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeToPeak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCoolRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreheat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKd)).BeginInit();
            this.SuspendLayout();
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(12, 430);
            this.tbStatus.Multiline = true;
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbStatus.Size = new System.Drawing.Size(680, 159);
            this.tbStatus.TabIndex = 1;
            // 
            // btnGetTcStatus
            // 
            this.btnGetTcStatus.Enabled = false;
            this.btnGetTcStatus.Location = new System.Drawing.Point(91, 373);
            this.btnGetTcStatus.Name = "btnGetTcStatus";
            this.btnGetTcStatus.Size = new System.Drawing.Size(98, 23);
            this.btnGetTcStatus.TabIndex = 3;
            this.btnGetTcStatus.Text = "Get Tc Status";
            this.btnGetTcStatus.UseVisualStyleBackColor = true;
            this.btnGetTcStatus.Click += new System.EventHandler(this.btnGetTcStatus_Click);
            // 
            // btnRelayOn
            // 
            this.btnRelayOn.Enabled = false;
            this.btnRelayOn.Location = new System.Drawing.Point(297, 373);
            this.btnRelayOn.Name = "btnRelayOn";
            this.btnRelayOn.Size = new System.Drawing.Size(98, 23);
            this.btnRelayOn.TabIndex = 4;
            this.btnRelayOn.Text = "Relay On";
            this.btnRelayOn.UseVisualStyleBackColor = true;
            this.btnRelayOn.Click += new System.EventHandler(this.btnRelayOn_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(10, 373);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(10, 402);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRelayOff
            // 
            this.btnRelayOff.Enabled = false;
            this.btnRelayOff.Location = new System.Drawing.Point(297, 402);
            this.btnRelayOff.Name = "btnRelayOff";
            this.btnRelayOff.Size = new System.Drawing.Size(98, 23);
            this.btnRelayOff.TabIndex = 8;
            this.btnRelayOff.Text = "Relay Off";
            this.btnRelayOff.UseVisualStyleBackColor = true;
            this.btnRelayOff.Click += new System.EventHandler(this.btnRelayOff_Click);
            // 
            // btnPing
            // 
            this.btnPing.Enabled = false;
            this.btnPing.Location = new System.Drawing.Point(195, 373);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(96, 23);
            this.btnPing.TabIndex = 9;
            this.btnPing.Text = "Ping";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Enabled = false;
            this.btnClearLog.Location = new System.Drawing.Point(401, 373);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 10;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnGetProfileStage
            // 
            this.btnGetProfileStage.Enabled = false;
            this.btnGetProfileStage.Location = new System.Drawing.Point(91, 402);
            this.btnGetProfileStage.Name = "btnGetProfileStage";
            this.btnGetProfileStage.Size = new System.Drawing.Size(96, 23);
            this.btnGetProfileStage.TabIndex = 11;
            this.btnGetProfileStage.Text = "Get Profile Stage";
            this.btnGetProfileStage.UseVisualStyleBackColor = true;
            this.btnGetProfileStage.Click += new System.EventHandler(this.btnGetProfileStage_Click);
            // 
            // chartMain
            // 
            this.chartMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chartMain.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.AxisX.IsMarginVisible = false;
            chartArea2.AxisX.MinorTickMark.Interval = 1D;
            chartArea2.AxisX.Title = "Time";
            chartArea2.AxisY.Title = "°C";
            chartArea2.BackColor = System.Drawing.Color.Gainsboro;
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea";
            this.chartMain.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend";
            this.chartMain.Legends.Add(legend2);
            this.chartMain.Location = new System.Drawing.Point(12, 12);
            this.chartMain.Name = "chartMain";
            series4.ChartArea = "ChartArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend";
            series4.LegendText = "Profile";
            series4.Name = "Profile";
            series5.ChartArea = "ChartArea";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend";
            series5.Name = "Actual";
            series6.ChartArea = "ChartArea";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend";
            series6.Name = "Target";
            this.chartMain.Series.Add(series4);
            this.chartMain.Series.Add(series5);
            this.chartMain.Series.Add(series6);
            this.chartMain.Size = new System.Drawing.Size(680, 355);
            this.chartMain.TabIndex = 12;
            this.chartMain.Text = "chart";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Title";
            title2.Text = "Reflow Controller Profile";
            this.chartMain.Titles.Add(title2);
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(482, 373);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cboPorts
            // 
            this.cboPorts.FormattingEnabled = true;
            this.cboPorts.Location = new System.Drawing.Point(563, 375);
            this.cboPorts.Name = "cboPorts";
            this.cboPorts.Size = new System.Drawing.Size(101, 21);
            this.cboPorts.TabIndex = 14;
            this.cboPorts.SelectionChangeCommitted += new System.EventHandler(this.cboPorts_SelectionChangeCommitted);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(482, 402);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 15;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnGetReflowProfile
            // 
            this.btnGetReflowProfile.Enabled = false;
            this.btnGetReflowProfile.Location = new System.Drawing.Point(701, 13);
            this.btnGetReflowProfile.Name = "btnGetReflowProfile";
            this.btnGetReflowProfile.Size = new System.Drawing.Size(71, 23);
            this.btnGetReflowProfile.TabIndex = 16;
            this.btnGetReflowProfile.Text = "Get Profile";
            this.btnGetReflowProfile.UseVisualStyleBackColor = true;
            this.btnGetReflowProfile.Click += new System.EventHandler(this.btnGetReflowProfile_Click);
            // 
            // btnSetProfile
            // 
            this.btnSetProfile.Enabled = false;
            this.btnSetProfile.Location = new System.Drawing.Point(701, 41);
            this.btnSetProfile.Name = "btnSetProfile";
            this.btnSetProfile.Size = new System.Drawing.Size(71, 23);
            this.btnSetProfile.TabIndex = 17;
            this.btnSetProfile.Text = "Set Profile";
            this.btnSetProfile.UseVisualStyleBackColor = true;
            this.btnSetProfile.Click += new System.EventHandler(this.btnSetProfile_Click);
            // 
            // lblStartRate
            // 
            this.lblStartRate.AutoSize = true;
            this.lblStartRate.Location = new System.Drawing.Point(698, 106);
            this.lblStartRate.Name = "lblStartRate";
            this.lblStartRate.Size = new System.Drawing.Size(55, 13);
            this.lblStartRate.TabIndex = 18;
            this.lblStartRate.Text = "Start Rate";
            // 
            // lblSoakTemp1
            // 
            this.lblSoakTemp1.AutoSize = true;
            this.lblSoakTemp1.Location = new System.Drawing.Point(698, 145);
            this.lblSoakTemp1.Name = "lblSoakTemp1";
            this.lblSoakTemp1.Size = new System.Drawing.Size(71, 13);
            this.lblSoakTemp1.TabIndex = 20;
            this.lblSoakTemp1.Text = "Soak Temp 1";
            // 
            // lblSoakTemp2
            // 
            this.lblSoakTemp2.AutoSize = true;
            this.lblSoakTemp2.Location = new System.Drawing.Point(698, 184);
            this.lblSoakTemp2.Name = "lblSoakTemp2";
            this.lblSoakTemp2.Size = new System.Drawing.Size(71, 13);
            this.lblSoakTemp2.TabIndex = 22;
            this.lblSoakTemp2.Text = "Soak Temp 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(698, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Soak Length";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(698, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Peak Temp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(698, 301);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Time To Peak";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(698, 340);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Cool Rate";
            // 
            // nudStartRate
            // 
            this.nudStartRate.Enabled = false;
            this.nudStartRate.Location = new System.Drawing.Point(701, 122);
            this.nudStartRate.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudStartRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStartRate.Name = "nudStartRate";
            this.nudStartRate.Size = new System.Drawing.Size(71, 20);
            this.nudStartRate.TabIndex = 31;
            this.nudStartRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStartRate.ValueChanged += new System.EventHandler(this.Profile_ValueChanged);
            // 
            // nudSoakTemp1
            // 
            this.nudSoakTemp1.Enabled = false;
            this.nudSoakTemp1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSoakTemp1.Location = new System.Drawing.Point(701, 161);
            this.nudSoakTemp1.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudSoakTemp1.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.nudSoakTemp1.Name = "nudSoakTemp1";
            this.nudSoakTemp1.Size = new System.Drawing.Size(71, 20);
            this.nudSoakTemp1.TabIndex = 32;
            this.nudSoakTemp1.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.nudSoakTemp1.ValueChanged += new System.EventHandler(this.Profile_ValueChanged);
            // 
            // nudSoakTemp2
            // 
            this.nudSoakTemp2.Enabled = false;
            this.nudSoakTemp2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSoakTemp2.Location = new System.Drawing.Point(701, 200);
            this.nudSoakTemp2.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudSoakTemp2.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.nudSoakTemp2.Name = "nudSoakTemp2";
            this.nudSoakTemp2.Size = new System.Drawing.Size(71, 20);
            this.nudSoakTemp2.TabIndex = 33;
            this.nudSoakTemp2.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.nudSoakTemp2.ValueChanged += new System.EventHandler(this.Profile_ValueChanged);
            // 
            // nudSoakLength
            // 
            this.nudSoakLength.Enabled = false;
            this.nudSoakLength.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSoakLength.Location = new System.Drawing.Point(701, 239);
            this.nudSoakLength.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudSoakLength.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudSoakLength.Name = "nudSoakLength";
            this.nudSoakLength.Size = new System.Drawing.Size(71, 20);
            this.nudSoakLength.TabIndex = 34;
            this.nudSoakLength.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudSoakLength.ValueChanged += new System.EventHandler(this.Profile_ValueChanged);
            // 
            // nudPeakTemp
            // 
            this.nudPeakTemp.Enabled = false;
            this.nudPeakTemp.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPeakTemp.Location = new System.Drawing.Point(701, 278);
            this.nudPeakTemp.Maximum = new decimal(new int[] {
            220,
            0,
            0,
            0});
            this.nudPeakTemp.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudPeakTemp.Name = "nudPeakTemp";
            this.nudPeakTemp.Size = new System.Drawing.Size(71, 20);
            this.nudPeakTemp.TabIndex = 35;
            this.nudPeakTemp.Value = new decimal(new int[] {
            220,
            0,
            0,
            0});
            this.nudPeakTemp.ValueChanged += new System.EventHandler(this.Profile_ValueChanged);
            // 
            // nudTimeToPeak
            // 
            this.nudTimeToPeak.Enabled = false;
            this.nudTimeToPeak.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTimeToPeak.Location = new System.Drawing.Point(701, 317);
            this.nudTimeToPeak.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudTimeToPeak.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudTimeToPeak.Name = "nudTimeToPeak";
            this.nudTimeToPeak.Size = new System.Drawing.Size(71, 20);
            this.nudTimeToPeak.TabIndex = 36;
            this.nudTimeToPeak.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudTimeToPeak.ValueChanged += new System.EventHandler(this.Profile_ValueChanged);
            // 
            // nudCoolRate
            // 
            this.nudCoolRate.Enabled = false;
            this.nudCoolRate.Location = new System.Drawing.Point(701, 356);
            this.nudCoolRate.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudCoolRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCoolRate.Name = "nudCoolRate";
            this.nudCoolRate.Size = new System.Drawing.Size(71, 20);
            this.nudCoolRate.TabIndex = 37;
            this.nudCoolRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCoolRate.ValueChanged += new System.EventHandler(this.Profile_ValueChanged);
            // 
            // tmrMain
            // 
            this.tmrMain.Interval = 1000;
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // lblPreheat
            // 
            this.lblPreheat.AutoSize = true;
            this.lblPreheat.Location = new System.Drawing.Point(698, 67);
            this.lblPreheat.Name = "lblPreheat";
            this.lblPreheat.Size = new System.Drawing.Size(44, 13);
            this.lblPreheat.TabIndex = 38;
            this.lblPreheat.Text = "Preheat";
            // 
            // nudPreheat
            // 
            this.nudPreheat.Enabled = false;
            this.nudPreheat.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPreheat.Location = new System.Drawing.Point(701, 83);
            this.nudPreheat.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudPreheat.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPreheat.Name = "nudPreheat";
            this.nudPreheat.Size = new System.Drawing.Size(71, 20);
            this.nudPreheat.TabIndex = 39;
            this.nudPreheat.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPreheat.ValueChanged += new System.EventHandler(this.Profile_ValueChanged);
            // 
            // btnGetPid
            // 
            this.btnGetPid.Location = new System.Drawing.Point(701, 430);
            this.btnGetPid.Name = "btnGetPid";
            this.btnGetPid.Size = new System.Drawing.Size(71, 23);
            this.btnGetPid.TabIndex = 40;
            this.btnGetPid.Text = "Get Pid";
            this.btnGetPid.UseVisualStyleBackColor = true;
            this.btnGetPid.Click += new System.EventHandler(this.btnGetPid_Click);
            // 
            // btnSetPid
            // 
            this.btnSetPid.Location = new System.Drawing.Point(701, 460);
            this.btnSetPid.Name = "btnSetPid";
            this.btnSetPid.Size = new System.Drawing.Size(71, 23);
            this.btnSetPid.TabIndex = 41;
            this.btnSetPid.Text = "Set Pid";
            this.btnSetPid.UseVisualStyleBackColor = true;
            this.btnSetPid.Click += new System.EventHandler(this.btnSetPid_Click);
            // 
            // nudKp
            // 
            this.nudKp.Location = new System.Drawing.Point(728, 490);
            this.nudKp.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudKp.Name = "nudKp";
            this.nudKp.Size = new System.Drawing.Size(44, 20);
            this.nudKp.TabIndex = 42;
            // 
            // lblK
            // 
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(702, 494);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(20, 13);
            this.lblK.TabIndex = 45;
            this.lblK.Text = "Kp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(702, 521);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Ki";
            // 
            // nudKi
            // 
            this.nudKi.Location = new System.Drawing.Point(728, 517);
            this.nudKi.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudKi.Name = "nudKi";
            this.nudKi.Size = new System.Drawing.Size(44, 20);
            this.nudKi.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(702, 548);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Kd";
            // 
            // nudKd
            // 
            this.nudKd.Location = new System.Drawing.Point(728, 544);
            this.nudKd.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudKd.Name = "nudKd";
            this.nudKd.Size = new System.Drawing.Size(44, 20);
            this.nudKd.TabIndex = 48;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 601);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudKd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudKi);
            this.Controls.Add(this.lblK);
            this.Controls.Add(this.nudKp);
            this.Controls.Add(this.btnSetPid);
            this.Controls.Add(this.btnGetPid);
            this.Controls.Add(this.nudPreheat);
            this.Controls.Add(this.lblPreheat);
            this.Controls.Add(this.nudCoolRate);
            this.Controls.Add(this.nudTimeToPeak);
            this.Controls.Add(this.nudPeakTemp);
            this.Controls.Add(this.nudSoakLength);
            this.Controls.Add(this.nudSoakTemp2);
            this.Controls.Add(this.nudSoakTemp1);
            this.Controls.Add(this.nudStartRate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSoakTemp2);
            this.Controls.Add(this.lblSoakTemp1);
            this.Controls.Add(this.lblStartRate);
            this.Controls.Add(this.btnSetProfile);
            this.Controls.Add(this.btnGetReflowProfile);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.cboPorts);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.chartMain);
            this.Controls.Add(this.btnGetProfileStage);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnPing);
            this.Controls.Add(this.btnRelayOff);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnRelayOn);
            this.Controls.Add(this.btnGetTcStatus);
            this.Controls.Add(this.tbStatus);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Reflow Controller";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoakTemp1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoakTemp2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoakLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeakTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeToPeak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCoolRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreheat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button btnGetTcStatus;
        private System.Windows.Forms.Button btnRelayOn;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRelayOff;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnGetProfileStage;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cboPorts;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnGetReflowProfile;
        private System.Windows.Forms.Button btnSetProfile;
        private System.Windows.Forms.Label lblStartRate;
        private System.Windows.Forms.Label lblSoakTemp1;
        private System.Windows.Forms.Label lblSoakTemp2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudStartRate;
        private System.Windows.Forms.NumericUpDown nudSoakTemp1;
        private System.Windows.Forms.NumericUpDown nudSoakTemp2;
        private System.Windows.Forms.NumericUpDown nudSoakLength;
        private System.Windows.Forms.NumericUpDown nudPeakTemp;
        private System.Windows.Forms.NumericUpDown nudTimeToPeak;
        private System.Windows.Forms.NumericUpDown nudCoolRate;
        private System.Windows.Forms.Timer tmrMain;
        private System.Windows.Forms.Label lblPreheat;
        private System.Windows.Forms.NumericUpDown nudPreheat;
        private System.Windows.Forms.Button btnGetPid;
        private System.Windows.Forms.Button btnSetPid;
        private System.Windows.Forms.NumericUpDown nudKp;
        private System.Windows.Forms.Label lblK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudKi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudKd;
    }
}

