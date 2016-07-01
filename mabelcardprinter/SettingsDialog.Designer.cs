namespace MabelCardPrinter
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.tbMabelUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTestMabelConn = new System.Windows.Forms.Button();
            this.btnGetSettings = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbApiKey = new System.Windows.Forms.TextBox();
            this.tbPrinterId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPrinterLocation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbPrinters = new System.Windows.Forms.ComboBox();
            this.chbMagstripe = new System.Windows.Forms.CheckBox();
            this.chbRFID = new System.Windows.Forms.CheckBox();
            this.chbDebug = new System.Windows.Forms.CheckBox();
            this.rbMagicardType = new System.Windows.Forms.RadioButton();
            this.rbGenericType = new System.Windows.Forms.RadioButton();
            this.rbLandscape = new System.Windows.Forms.RadioButton();
            this.rbPortrait = new System.Windows.Forms.RadioButton();
            this.cbDontPrint = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMabelUrl
            // 
            this.tbMabelUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMabelUrl.Location = new System.Drawing.Point(108, 50);
            this.tbMabelUrl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMabelUrl.Name = "tbMabelUrl";
            this.tbMabelUrl.Size = new System.Drawing.Size(354, 20);
            this.tbMabelUrl.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "MABEL URL";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(108, 98);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(354, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnTestMabelConn
            // 
            this.btnTestMabelConn.Location = new System.Drawing.Point(2, 98);
            this.btnTestMabelConn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTestMabelConn.Name = "btnTestMabelConn";
            this.btnTestMabelConn.Size = new System.Drawing.Size(102, 21);
            this.btnTestMabelConn.TabIndex = 5;
            this.btnTestMabelConn.Text = "Test Connection";
            this.btnTestMabelConn.UseVisualStyleBackColor = true;
            this.btnTestMabelConn.Click += new System.EventHandler(this.btnTestMabelConn_Click);
            // 
            // btnGetSettings
            // 
            this.btnGetSettings.Location = new System.Drawing.Point(2, 123);
            this.btnGetSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGetSettings.Name = "btnGetSettings";
            this.btnGetSettings.Size = new System.Drawing.Size(102, 21);
            this.btnGetSettings.TabIndex = 6;
            this.btnGetSettings.Text = "Get Settings";
            this.btnGetSettings.UseVisualStyleBackColor = true;
            this.btnGetSettings.Click += new System.EventHandler(this.btnGetSettings_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnGetSettings, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.tbApiKey, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnTestMabelConn, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tbPrinterId, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbPrinterLocation, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbMabelUrl, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 1, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(464, 148);
            this.tableLayoutPanel2.TabIndex = 17;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // tbApiKey
            // 
            this.tbApiKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbApiKey.Location = new System.Drawing.Point(108, 74);
            this.tbApiKey.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbApiKey.Name = "tbApiKey";
            this.tbApiKey.Size = new System.Drawing.Size(354, 20);
            this.tbApiKey.TabIndex = 8;
            // 
            // tbPrinterId
            // 
            this.tbPrinterId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPrinterId.Location = new System.Drawing.Point(108, 26);
            this.tbPrinterId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPrinterId.Name = "tbPrinterId";
            this.tbPrinterId.Size = new System.Drawing.Size(354, 20);
            this.tbPrinterId.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "API Key";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 24);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Printer ID";
            // 
            // tbPrinterLocation
            // 
            this.tbPrinterLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPrinterLocation.Location = new System.Drawing.Point(108, 2);
            this.tbPrinterLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPrinterLocation.Name = "tbPrinterLocation";
            this.tbPrinterLocation.Size = new System.Drawing.Size(354, 20);
            this.tbPrinterLocation.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Printer Location";
            // 
            // cbPrinters
            // 
            this.cbPrinters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPrinters.FormattingEnabled = true;
            this.cbPrinters.Location = new System.Drawing.Point(2, 15);
            this.cbPrinters.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPrinters.Name = "cbPrinters";
            this.cbPrinters.Size = new System.Drawing.Size(460, 21);
            this.cbPrinters.TabIndex = 9;
            this.cbPrinters.SelectedIndexChanged += new System.EventHandler(this.cbPrinters_SelectedIndexChanged);
            // 
            // chbMagstripe
            // 
            this.chbMagstripe.AutoSize = true;
            this.chbMagstripe.Location = new System.Drawing.Point(18, 41);
            this.chbMagstripe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chbMagstripe.Name = "chbMagstripe";
            this.chbMagstripe.Size = new System.Drawing.Size(120, 17);
            this.chbMagstripe.TabIndex = 10;
            this.chbMagstripe.Text = "Magstripe Encoding";
            this.chbMagstripe.UseVisualStyleBackColor = true;
            this.chbMagstripe.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chbRFID
            // 
            this.chbRFID.AutoSize = true;
            this.chbRFID.Location = new System.Drawing.Point(18, 60);
            this.chbRFID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chbRFID.Name = "chbRFID";
            this.chbRFID.Size = new System.Drawing.Size(127, 17);
            this.chbRFID.TabIndex = 11;
            this.chbRFID.Text = "Enable RFID Capture";
            this.chbRFID.UseVisualStyleBackColor = true;
            this.chbRFID.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // chbDebug
            // 
            this.chbDebug.AutoSize = true;
            this.chbDebug.Location = new System.Drawing.Point(18, 80);
            this.chbDebug.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chbDebug.Name = "chbDebug";
            this.chbDebug.Size = new System.Drawing.Size(108, 17);
            this.chbDebug.TabIndex = 12;
            this.chbDebug.Text = "Debugging Mode";
            this.chbDebug.UseVisualStyleBackColor = true;
            this.chbDebug.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // rbMagicardType
            // 
            this.rbMagicardType.AutoSize = true;
            this.rbMagicardType.Location = new System.Drawing.Point(15, 16);
            this.rbMagicardType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbMagicardType.Name = "rbMagicardType";
            this.rbMagicardType.Size = new System.Drawing.Size(106, 17);
            this.rbMagicardType.TabIndex = 13;
            this.rbMagicardType.TabStop = true;
            this.rbMagicardType.Text = "Magicard Enduro";
            this.rbMagicardType.UseVisualStyleBackColor = true;
            // 
            // rbGenericType
            // 
            this.rbGenericType.AutoSize = true;
            this.rbGenericType.Location = new System.Drawing.Point(15, 36);
            this.rbGenericType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbGenericType.Name = "rbGenericType";
            this.rbGenericType.Size = new System.Drawing.Size(62, 17);
            this.rbGenericType.TabIndex = 14;
            this.rbGenericType.TabStop = true;
            this.rbGenericType.Text = "Generic";
            this.rbGenericType.UseVisualStyleBackColor = true;
            // 
            // rbLandscape
            // 
            this.rbLandscape.AutoSize = true;
            this.rbLandscape.Location = new System.Drawing.Point(16, 42);
            this.rbLandscape.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbLandscape.Name = "rbLandscape";
            this.rbLandscape.Size = new System.Drawing.Size(78, 17);
            this.rbLandscape.TabIndex = 16;
            this.rbLandscape.TabStop = true;
            this.rbLandscape.Text = "Landscape";
            this.rbLandscape.UseVisualStyleBackColor = true;
            // 
            // rbPortrait
            // 
            this.rbPortrait.AutoSize = true;
            this.rbPortrait.Location = new System.Drawing.Point(16, 22);
            this.rbPortrait.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbPortrait.Name = "rbPortrait";
            this.rbPortrait.Size = new System.Drawing.Size(58, 17);
            this.rbPortrait.TabIndex = 15;
            this.rbPortrait.TabStop = true;
            this.rbPortrait.Text = "Portrait";
            this.rbPortrait.UseVisualStyleBackColor = true;
            // 
            // cbDontPrint
            // 
            this.cbDontPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.cbDontPrint.AutoSize = true;
            this.cbDontPrint.Location = new System.Drawing.Point(18, 21);
            this.cbDontPrint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDontPrint.Name = "cbDontPrint";
            this.cbDontPrint.Size = new System.Drawing.Size(75, 17);
            this.cbDontPrint.TabIndex = 13;
            this.cbDontPrint.Text = "Don\'t Print";
            this.cbDontPrint.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.AccessibleName = "Apply settings";
            this.btnApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(186, 2);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(88, 23);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleName = "Cancel without saving";
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(94, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleName = "Accept settings and close dialogue";
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(2, 2);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 23);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel1, 0, 3);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(468, 398);
            this.tableLayoutPanel7.TabIndex = 20;
            this.tableLayoutPanel7.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel7_Paint);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.groupBox1);
            this.flowLayoutPanel2.Controls.Add(this.groupBox2);
            this.flowLayoutPanel2.Controls.Add(this.groupBox3);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(2, 218);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(464, 139);
            this.flowLayoutPanel2.TabIndex = 22;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleName = "Printer type";
            this.groupBox1.Controls.Add(this.rbMagicardType);
            this.groupBox1.Controls.Add(this.rbGenericType);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(138, 61);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Printer Type";
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleName = "Program Options";
            this.groupBox2.Controls.Add(this.cbAutoStart);
            this.groupBox2.Controls.Add(this.cbDontPrint);
            this.groupBox2.Controls.Add(this.chbMagstripe);
            this.groupBox2.Controls.Add(this.chbRFID);
            this.groupBox2.Controls.Add(this.chbDebug);
            this.groupBox2.Location = new System.Drawing.Point(144, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(158, 119);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleName = "Card orientation";
            this.groupBox3.Controls.Add(this.rbPortrait);
            this.groupBox3.Controls.Add(this.rbLandscape);
            this.groupBox3.Location = new System.Drawing.Point(306, 2);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(116, 82);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Orientation";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbPrinters);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(2, 163);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(464, 51);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Printer";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnOk);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(186, 370);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(280, 26);
            this.flowLayoutPanel1.TabIndex = 21;
            // 
            // cbAutoStart
            // 
            this.cbAutoStart.AutoSize = true;
            this.cbAutoStart.Location = new System.Drawing.Point(18, 102);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(130, 17);
            this.cbAutoStart.TabIndex = 14;
            this.cbAutoStart.Text = "Automatically Register";
            this.cbAutoStart.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(488, 418);
            this.Controls.Add(this.tableLayoutPanel7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.ShowIcon = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tbMabelUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTestMabelConn;
        private System.Windows.Forms.Button btnGetSettings;
        private System.Windows.Forms.ComboBox cbPrinters;
        private System.Windows.Forms.CheckBox chbMagstripe;
        private System.Windows.Forms.CheckBox chbRFID;
        private System.Windows.Forms.CheckBox chbDebug;
        private System.Windows.Forms.RadioButton rbMagicardType;
        private System.Windows.Forms.RadioButton rbGenericType;
        private System.Windows.Forms.RadioButton rbLandscape;
        private System.Windows.Forms.RadioButton rbPortrait;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbApiKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPrinterId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPrinterLocation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbDontPrint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbAutoStart;
    }
}