namespace COMPort_Full_duplex_Data_Transmitter
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cBoxParitybit = new System.Windows.Forms.ComboBox();
            this.cBoxStopbit = new System.Windows.Forms.ComboBox();
            this.cBoxDatabit = new System.Windows.Forms.ComboBox();
            this.cBoxBaudrate = new System.Windows.Forms.ComboBox();
            this.cBoxComPort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.cBoxChunkSize = new System.Windows.Forms.ComboBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.btnSendData = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.btnReceiveData = new System.Windows.Forms.Button();
            this.btnSaveHere = new System.Windows.Forms.Button();
            this.ReceivedFilePathTextBox = new System.Windows.Forms.TextBox();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label11 = new System.Windows.Forms.Label();
            this.InputHere = new System.Windows.Forms.TextBox();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox1.Location = new System.Drawing.Point(35, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 317);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COMport Config Panel";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.progressBar1);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnOpen);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("Bahnschrift", 7.8F);
            this.groupBox3.Location = new System.Drawing.Point(326, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(327, 151);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.progressBar1.Location = new System.Drawing.Point(88, 66);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(217, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.btnClose.Font = new System.Drawing.Font("Bahnschrift SemiLight", 7.8F);
            this.btnClose.Location = new System.Drawing.Point(222, 96);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.btnOpen.Font = new System.Drawing.Font("Bahnschrift SemiLight", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(87, 27);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(85, 32);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Connect";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.cBoxParitybit);
            this.groupBox2.Controls.Add(this.cBoxStopbit);
            this.groupBox2.Controls.Add(this.cBoxDatabit);
            this.groupBox2.Controls.Add(this.cBoxBaudrate);
            this.groupBox2.Controls.Add(this.cBoxComPort);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Bahnschrift", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(33, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 255);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // cBoxParitybit
            // 
            this.cBoxParitybit.BackColor = System.Drawing.Color.Snow;
            this.cBoxParitybit.Font = new System.Drawing.Font("Arial", 7.8F);
            this.cBoxParitybit.FormattingEnabled = true;
            this.cBoxParitybit.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cBoxParitybit.Location = new System.Drawing.Point(108, 209);
            this.cBoxParitybit.Name = "cBoxParitybit";
            this.cBoxParitybit.Size = new System.Drawing.Size(121, 24);
            this.cBoxParitybit.TabIndex = 9;
            this.cBoxParitybit.Text = "    -- select --";
            // 
            // cBoxStopbit
            // 
            this.cBoxStopbit.BackColor = System.Drawing.Color.Snow;
            this.cBoxStopbit.Font = new System.Drawing.Font("Arial", 7.8F);
            this.cBoxStopbit.FormattingEnabled = true;
            this.cBoxStopbit.Items.AddRange(new object[] {
            "One",
            "Two"});
            this.cBoxStopbit.Location = new System.Drawing.Point(108, 162);
            this.cBoxStopbit.Name = "cBoxStopbit";
            this.cBoxStopbit.Size = new System.Drawing.Size(121, 24);
            this.cBoxStopbit.TabIndex = 8;
            this.cBoxStopbit.Text = "    -- select --";
            // 
            // cBoxDatabit
            // 
            this.cBoxDatabit.BackColor = System.Drawing.Color.Snow;
            this.cBoxDatabit.Font = new System.Drawing.Font("Arial", 7.8F);
            this.cBoxDatabit.FormattingEnabled = true;
            this.cBoxDatabit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cBoxDatabit.Location = new System.Drawing.Point(107, 117);
            this.cBoxDatabit.Name = "cBoxDatabit";
            this.cBoxDatabit.Size = new System.Drawing.Size(121, 24);
            this.cBoxDatabit.TabIndex = 7;
            this.cBoxDatabit.Text = "    -- select --";
            // 
            // cBoxBaudrate
            // 
            this.cBoxBaudrate.BackColor = System.Drawing.Color.Snow;
            this.cBoxBaudrate.Font = new System.Drawing.Font("Arial", 7.8F);
            this.cBoxBaudrate.FormattingEnabled = true;
            this.cBoxBaudrate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "115200",
            "230400"});
            this.cBoxBaudrate.Location = new System.Drawing.Point(107, 72);
            this.cBoxBaudrate.Name = "cBoxBaudrate";
            this.cBoxBaudrate.Size = new System.Drawing.Size(121, 24);
            this.cBoxBaudrate.TabIndex = 6;
            this.cBoxBaudrate.Text = "    -- select --";
            // 
            // cBoxComPort
            // 
            this.cBoxComPort.BackColor = System.Drawing.Color.Snow;
            this.cBoxComPort.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxComPort.FormattingEnabled = true;
            this.cBoxComPort.Location = new System.Drawing.Point(106, 27);
            this.cBoxComPort.Name = "cBoxComPort";
            this.cBoxComPort.Size = new System.Drawing.Size(121, 24);
            this.cBoxComPort.TabIndex = 5;
            this.cBoxComPort.Text = "    -- select --";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift", 7.8F);
            this.label5.Location = new System.Drawing.Point(18, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "ParityBits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift", 7.8F);
            this.label4.Location = new System.Drawing.Point(18, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "StopBit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "DataBits";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift", 7.8F);
            this.label2.Location = new System.Drawing.Point(18, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "BaudRate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 7.8F);
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "COMPorts";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox4.Controls.Add(this.groupBox9);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox4.Location = new System.Drawing.Point(35, 402);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(624, 284);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sender Panel";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label7);
            this.groupBox9.Controls.Add(this.label8);
            this.groupBox9.Controls.Add(this.label9);
            this.groupBox9.Controls.Add(this.tbxFileName);
            this.groupBox9.Controls.Add(this.cBoxChunkSize);
            this.groupBox9.Controls.Add(this.btnBrowse);
            this.groupBox9.Font = new System.Drawing.Font("Bahnschrift", 7.8F);
            this.groupBox9.Location = new System.Drawing.Point(20, 22);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(583, 136);
            this.groupBox9.TabIndex = 8;
            this.groupBox9.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "ChunkSize";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "SelectFile";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 16);
            this.label9.TabIndex = 2;
            this.label9.Text = "FilePath";
            // 
            // tbxFileName
            // 
            this.tbxFileName.BackColor = System.Drawing.Color.Snow;
            this.tbxFileName.Location = new System.Drawing.Point(113, 97);
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.Size = new System.Drawing.Size(451, 23);
            this.tbxFileName.TabIndex = 5;
            // 
            // cBoxChunkSize
            // 
            this.cBoxChunkSize.BackColor = System.Drawing.Color.Snow;
            this.cBoxChunkSize.Font = new System.Drawing.Font("Arial", 7.8F);
            this.cBoxChunkSize.FormattingEnabled = true;
            this.cBoxChunkSize.Items.AddRange(new object[] {
            "128",
            "256",
            "512",
            "1024",
            "2048"});
            this.cBoxChunkSize.Location = new System.Drawing.Point(113, 20);
            this.cBoxChunkSize.Name = "cBoxChunkSize";
            this.cBoxChunkSize.Size = new System.Drawing.Size(131, 24);
            this.cBoxChunkSize.TabIndex = 3;
            this.cBoxChunkSize.Text = "    -- select --";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.btnBrowse.Font = new System.Drawing.Font("Bahnschrift SemiLight", 7.8F);
            this.btnBrowse.Location = new System.Drawing.Point(112, 56);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(88, 30);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox5.Controls.Add(this.progressBar2);
            this.groupBox5.Controls.Add(this.btnSendData);
            this.groupBox5.Font = new System.Drawing.Font("Bahnschrift", 7.8F);
            this.groupBox5.Location = new System.Drawing.Point(140, 174);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(370, 85);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            // 
            // progressBar2
            // 
            this.progressBar2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.progressBar2.Location = new System.Drawing.Point(145, 26);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(205, 35);
            this.progressBar2.TabIndex = 1;
            // 
            // btnSendData
            // 
            this.btnSendData.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.btnSendData.Font = new System.Drawing.Font("Bahnschrift SemiLight", 7.8F);
            this.btnSendData.Location = new System.Drawing.Point(17, 13);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(118, 59);
            this.btnSendData.TabIndex = 0;
            this.btnSendData.Text = "SEND DATA";
            this.btnSendData.UseVisualStyleBackColor = false;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.Snow;
            this.textBox.Location = new System.Drawing.Point(89, 43);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(483, 233);
            this.textBox.TabIndex = 7;
            this.textBox.Text = " ";
            // 
            // btnReceiveData
            // 
            this.btnReceiveData.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.btnReceiveData.Font = new System.Drawing.Font("Bahnschrift", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceiveData.Location = new System.Drawing.Point(16, 25);
            this.btnReceiveData.Name = "btnReceiveData";
            this.btnReceiveData.Size = new System.Drawing.Size(144, 52);
            this.btnReceiveData.TabIndex = 4;
            this.btnReceiveData.Text = "RECEIVE DATA";
            this.btnReceiveData.UseVisualStyleBackColor = false;
            this.btnReceiveData.Click += new System.EventHandler(this.btnReceiveData_Click);
            // 
            // btnSaveHere
            // 
            this.btnSaveHere.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.btnSaveHere.Location = new System.Drawing.Point(19, 30);
            this.btnSaveHere.Name = "btnSaveHere";
            this.btnSaveHere.Size = new System.Drawing.Size(98, 34);
            this.btnSaveHere.TabIndex = 6;
            this.btnSaveHere.Text = "Browse";
            this.btnSaveHere.UseVisualStyleBackColor = false;
            this.btnSaveHere.Click += new System.EventHandler(this.btnSaveHere_Click);
            // 
            // ReceivedFilePathTextBox
            // 
            this.ReceivedFilePathTextBox.BackColor = System.Drawing.Color.Snow;
            this.ReceivedFilePathTextBox.Location = new System.Drawing.Point(20, 74);
            this.ReceivedFilePathTextBox.Name = "ReceivedFilePathTextBox";
            this.ReceivedFilePathTextBox.Size = new System.Drawing.Size(396, 23);
            this.ReceivedFilePathTextBox.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Bahnschrift", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(17, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 16);
            this.label11.TabIndex = 8;
            this.label11.Text = "Input FileName";
            // 
            // InputHere
            // 
            this.InputHere.BackColor = System.Drawing.Color.Snow;
            this.InputHere.Font = new System.Drawing.Font("Bahnschrift", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputHere.Location = new System.Drawing.Point(128, 24);
            this.InputHere.Name = "InputHere";
            this.InputHere.Size = new System.Drawing.Size(187, 23);
            this.InputHere.TabIndex = 9;
            // 
            // cmbFileType
            // 
            this.cmbFileType.BackColor = System.Drawing.Color.Snow;
            this.cmbFileType.Font = new System.Drawing.Font("Bahnschrift", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Items.AddRange(new object[] {
            "Image",
            "Text",
            "Audio",
            "Video"});
            this.cmbFileType.Location = new System.Drawing.Point(128, 60);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(128, 24);
            this.cmbFileType.TabIndex = 10;
            this.cmbFileType.Text = "       -- select --";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox6.Controls.Add(this.groupBox11);
            this.groupBox6.Controls.Add(this.groupBox10);
            this.groupBox6.Controls.Add(this.groupBox8);
            this.groupBox6.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(691, 402);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(677, 284);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Receiver Panel";
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox11.Controls.Add(this.btnReceiveData);
            this.groupBox11.Controls.Add(this.progressBar3);
            this.groupBox11.Location = new System.Drawing.Point(474, 122);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(175, 137);
            this.groupBox11.TabIndex = 12;
            this.groupBox11.TabStop = false;
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(16, 87);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(144, 27);
            this.progressBar3.TabIndex = 12;
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox10.Controls.Add(this.cmbFileType);
            this.groupBox10.Controls.Add(this.label11);
            this.groupBox10.Controls.Add(this.label12);
            this.groupBox10.Controls.Add(this.InputHere);
            this.groupBox10.Location = new System.Drawing.Point(29, 22);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(352, 100);
            this.groupBox10.TabIndex = 14;
            this.groupBox10.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Bahnschrift", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(18, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "Data Type";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnSaveHere);
            this.groupBox8.Controls.Add(this.ReceivedFilePathTextBox);
            this.groupBox8.Font = new System.Drawing.Font("Bahnschrift", 7.8F);
            this.groupBox8.Location = new System.Drawing.Point(29, 139);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(430, 120);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Save Here";
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox12.Controls.Add(this.label10);
            this.groupBox12.Controls.Add(this.textBox);
            this.groupBox12.Font = new System.Drawing.Font("Bahnschrift", 9F);
            this.groupBox12.Location = new System.Drawing.Point(752, 44);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(616, 317);
            this.groupBox12.TabIndex = 12;
            this.groupBox12.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 18);
            this.label10.TabIndex = 8;
            this.label10.Text = "Output : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1405, 712);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cBoxParitybit;
        private System.Windows.Forms.ComboBox cBoxStopbit;
        private System.Windows.Forms.ComboBox cBoxDatabit;
        private System.Windows.Forms.ComboBox cBoxBaudrate;
        private System.Windows.Forms.ComboBox cBoxComPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label6;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ComboBox cBoxChunkSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.TextBox tbxFileName;
        private System.Windows.Forms.Button btnReceiveData;
        private System.Windows.Forms.Button btnSaveHere;
        private System.Windows.Forms.TextBox ReceivedFilePathTextBox;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox InputHere;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label10;
    }
}

