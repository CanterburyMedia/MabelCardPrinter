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
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbPrinterId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPrinterLocation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPrinters = new System.Windows.Forms.ComboBox();
            this.chbMagstripe = new System.Windows.Forms.CheckBox();
            this.chbRFID = new System.Windows.Forms.CheckBox();
            this.chbDebug = new System.Windows.Forms.CheckBox();
            this.rbMagicardType = new System.Windows.Forms.RadioButton();
            this.rbGenericType = new System.Windows.Forms.RadioButton();
            this.gbPrinterType = new System.Windows.Forms.GroupBox();
            this.gbCardOrientation = new System.Windows.Forms.GroupBox();
            this.rbLandscape = new System.Windows.Forms.RadioButton();
            this.rbPortrait = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbDontPrint = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.gbPrinterType.SuspendLayout();
            this.gbCardOrientation.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMabelUrl
            // 
            this.tbMabelUrl.Location = new System.Drawing.Point(244, 82);
            this.tbMabelUrl.Margin = new System.Windows.Forms.Padding(4);
            this.tbMabelUrl.Name = "tbMabelUrl";
            this.tbMabelUrl.Size = new System.Drawing.Size(488, 31);
            this.tbMabelUrl.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "MABEL URL";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(244, 208);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(288, 31);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnTestMabelConn
            // 
            this.btnTestMabelConn.Location = new System.Drawing.Point(4, 160);
            this.btnTestMabelConn.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestMabelConn.Name = "btnTestMabelConn";
            this.btnTestMabelConn.Size = new System.Drawing.Size(230, 40);
            this.btnTestMabelConn.TabIndex = 5;
            this.btnTestMabelConn.Text = "Test Connection";
            this.btnTestMabelConn.UseVisualStyleBackColor = true;
            this.btnTestMabelConn.Click += new System.EventHandler(this.btnTestMabelConn_Click);
            // 
            // btnGetSettings
            // 
            this.btnGetSettings.Location = new System.Drawing.Point(4, 208);
            this.btnGetSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetSettings.Name = "btnGetSettings";
            this.btnGetSettings.Size = new System.Drawing.Size(230, 40);
            this.btnGetSettings.TabIndex = 6;
            this.btnGetSettings.Text = "Get Settings";
            this.btnGetSettings.UseVisualStyleBackColor = true;
            this.btnGetSettings.Click += new System.EventHandler(this.btnGetSettings_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Printer";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(18, 21);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 352);
            this.panel1.TabIndex = 8;
            // 
            // tbPrinterId
            // 
            this.tbPrinterId.Location = new System.Drawing.Point(244, 43);
            this.tbPrinterId.Margin = new System.Windows.Forms.Padding(4);
            this.tbPrinterId.Name = "tbPrinterId";
            this.tbPrinterId.Size = new System.Drawing.Size(488, 31);
            this.tbPrinterId.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 39);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 26);
            this.label7.TabIndex = 11;
            this.label7.Text = "Printer ID";
            // 
            // tbPrinterLocation
            // 
            this.tbPrinterLocation.Location = new System.Drawing.Point(244, 4);
            this.tbPrinterLocation.Margin = new System.Windows.Forms.Padding(4);
            this.tbPrinterLocation.Name = "tbPrinterLocation";
            this.tbPrinterLocation.Size = new System.Drawing.Size(488, 31);
            this.tbPrinterLocation.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 26);
            this.label6.TabIndex = 9;
            this.label6.Text = "Printer Location";
            // 
            // tbApiKey
            // 
            this.tbApiKey.Location = new System.Drawing.Point(244, 121);
            this.tbApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.tbApiKey.Name = "tbApiKey";
            this.tbApiKey.Size = new System.Drawing.Size(488, 31);
            this.tbApiKey.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 117);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 26);
            this.label5.TabIndex = 7;
            this.label5.Text = "API Key";
            // 
            // cbPrinters
            // 
            this.cbPrinters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPrinters.FormattingEnabled = true;
            this.cbPrinters.Location = new System.Drawing.Point(147, 4);
            this.cbPrinters.Margin = new System.Windows.Forms.Padding(4);
            this.cbPrinters.Name = "cbPrinters";
            this.cbPrinters.Size = new System.Drawing.Size(585, 33);
            this.cbPrinters.TabIndex = 9;
            this.cbPrinters.SelectedIndexChanged += new System.EventHandler(this.cbPrinters_SelectedIndexChanged);
            // 
            // chbMagstripe
            // 
            this.chbMagstripe.AutoSize = true;
            this.chbMagstripe.Location = new System.Drawing.Point(4, 40);
            this.chbMagstripe.Margin = new System.Windows.Forms.Padding(4);
            this.chbMagstripe.Name = "chbMagstripe";
            this.chbMagstripe.Size = new System.Drawing.Size(230, 30);
            this.chbMagstripe.TabIndex = 10;
            this.chbMagstripe.Text = "Magstripe Encoding";
            this.chbMagstripe.UseVisualStyleBackColor = true;
            this.chbMagstripe.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chbRFID
            // 
            this.chbRFID.AutoSize = true;
            this.chbRFID.Location = new System.Drawing.Point(4, 78);
            this.chbRFID.Margin = new System.Windows.Forms.Padding(4);
            this.chbRFID.Name = "chbRFID";
            this.chbRFID.Size = new System.Drawing.Size(246, 30);
            this.chbRFID.TabIndex = 11;
            this.chbRFID.Text = "Enable RFID Capture";
            this.chbRFID.UseVisualStyleBackColor = true;
            this.chbRFID.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // chbDebug
            // 
            this.chbDebug.AutoSize = true;
            this.chbDebug.Location = new System.Drawing.Point(4, 116);
            this.chbDebug.Margin = new System.Windows.Forms.Padding(4);
            this.chbDebug.Name = "chbDebug";
            this.chbDebug.Size = new System.Drawing.Size(203, 30);
            this.chbDebug.TabIndex = 12;
            this.chbDebug.Text = "Debugging Mode";
            this.chbDebug.UseVisualStyleBackColor = true;
            this.chbDebug.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // rbMagicardType
            // 
            this.rbMagicardType.AutoSize = true;
            this.rbMagicardType.Location = new System.Drawing.Point(4, 4);
            this.rbMagicardType.Margin = new System.Windows.Forms.Padding(4);
            this.rbMagicardType.Name = "rbMagicardType";
            this.rbMagicardType.Size = new System.Drawing.Size(202, 30);
            this.rbMagicardType.TabIndex = 13;
            this.rbMagicardType.TabStop = true;
            this.rbMagicardType.Text = "Magicard Enduro";
            this.rbMagicardType.UseVisualStyleBackColor = true;
            // 
            // rbGenericType
            // 
            this.rbGenericType.AutoSize = true;
            this.rbGenericType.Location = new System.Drawing.Point(4, 55);
            this.rbGenericType.Margin = new System.Windows.Forms.Padding(4);
            this.rbGenericType.Name = "rbGenericType";
            this.rbGenericType.Size = new System.Drawing.Size(113, 30);
            this.rbGenericType.TabIndex = 14;
            this.rbGenericType.TabStop = true;
            this.rbGenericType.Text = "Generic";
            this.rbGenericType.UseVisualStyleBackColor = true;
            // 
            // gbPrinterType
            // 
            this.gbPrinterType.Controls.Add(this.tableLayoutPanel3);
            this.gbPrinterType.Location = new System.Drawing.Point(4, 4);
            this.gbPrinterType.Margin = new System.Windows.Forms.Padding(4);
            this.gbPrinterType.Name = "gbPrinterType";
            this.gbPrinterType.Padding = new System.Windows.Forms.Padding(4);
            this.gbPrinterType.Size = new System.Drawing.Size(360, 135);
            this.gbPrinterType.TabIndex = 16;
            this.gbPrinterType.TabStop = false;
            this.gbPrinterType.Text = "Printer Type";
            // 
            // gbCardOrientation
            // 
            this.gbCardOrientation.Controls.Add(this.tableLayoutPanel4);
            this.gbCardOrientation.Location = new System.Drawing.Point(4, 209);
            this.gbCardOrientation.Margin = new System.Windows.Forms.Padding(4);
            this.gbCardOrientation.Name = "gbCardOrientation";
            this.gbCardOrientation.Padding = new System.Windows.Forms.Padding(4);
            this.gbCardOrientation.Size = new System.Drawing.Size(360, 105);
            this.gbCardOrientation.TabIndex = 17;
            this.gbCardOrientation.TabStop = false;
            this.gbCardOrientation.Text = "Card Orientation";
            // 
            // rbLandscape
            // 
            this.rbLandscape.AutoSize = true;
            this.rbLandscape.Location = new System.Drawing.Point(4, 40);
            this.rbLandscape.Margin = new System.Windows.Forms.Padding(4);
            this.rbLandscape.Name = "rbLandscape";
            this.rbLandscape.Size = new System.Drawing.Size(143, 29);
            this.rbLandscape.TabIndex = 16;
            this.rbLandscape.TabStop = true;
            this.rbLandscape.Text = "Landscape";
            this.rbLandscape.UseVisualStyleBackColor = true;
            // 
            // rbPortrait
            // 
            this.rbPortrait.AutoSize = true;
            this.rbPortrait.Location = new System.Drawing.Point(4, 4);
            this.rbPortrait.Margin = new System.Windows.Forms.Padding(4);
            this.rbPortrait.Name = "rbPortrait";
            this.rbPortrait.Size = new System.Drawing.Size(107, 28);
            this.rbPortrait.TabIndex = 15;
            this.rbPortrait.TabStop = true;
            this.rbPortrait.Text = "Portrait";
            this.rbPortrait.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(372, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(304, 197);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // cbDontPrint
            // 
            this.cbDontPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.cbDontPrint.AutoSize = true;
            this.cbDontPrint.Location = new System.Drawing.Point(3, 3);
            this.cbDontPrint.Name = "cbDontPrint";
            this.cbDontPrint.Size = new System.Drawing.Size(139, 30);
            this.cbDontPrint.TabIndex = 13;
            this.cbDontPrint.Text = "Don\'t Print";
            this.cbDontPrint.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(596, 794);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(158, 54);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(430, 794);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(158, 54);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(264, 794);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(158, 54);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chbMagstripe, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbDontPrint, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chbRFID, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chbDebug, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(296, 165);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.6087F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.3913F));
            this.tableLayoutPanel2.Controls.Add(this.btnGetSettings, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.tbApiKey, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnTestMabelConn, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tbPrinterId, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbPrinterLocation, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbMabelUrl, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(736, 352);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.rbGenericType, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.rbMagicardType, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 28);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(352, 103);
            this.tableLayoutPanel3.TabIndex = 17;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.rbLandscape, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.rbPortrait, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 28);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(352, 73);
            this.tableLayoutPanel4.TabIndex = 17;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.4617F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.5383F));
            this.tableLayoutPanel5.Controls.Add(this.cbPrinters, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(18, 380);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(736, 83);
            this.tableLayoutPanel5.TabIndex = 17;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.gbPrinterType, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.gbCardOrientation, 0, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(18, 469);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.5614F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.43859F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(736, 318);
            this.tableLayoutPanel6.TabIndex = 19;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 861);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(797, 930);
            this.Name = "SettingsDialog";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.panel1.ResumeLayout(false);
            this.gbPrinterType.ResumeLayout(false);
            this.gbCardOrientation.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tbMabelUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTestMabelConn;
        private System.Windows.Forms.Button btnGetSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbPrinters;
        private System.Windows.Forms.CheckBox chbMagstripe;
        private System.Windows.Forms.CheckBox chbRFID;
        private System.Windows.Forms.CheckBox chbDebug;
        private System.Windows.Forms.RadioButton rbMagicardType;
        private System.Windows.Forms.RadioButton rbGenericType;
        private System.Windows.Forms.GroupBox gbPrinterType;
        private System.Windows.Forms.GroupBox gbCardOrientation;
        private System.Windows.Forms.RadioButton rbLandscape;
        private System.Windows.Forms.RadioButton rbPortrait;
        private System.Windows.Forms.GroupBox groupBox3;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    }
}