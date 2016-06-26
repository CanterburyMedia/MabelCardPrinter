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
            this.label1 = new System.Windows.Forms.Label();
            this.tbPrinterName = new System.Windows.Forms.TextBox();
            this.tbMabelUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTestMabelConn = new System.Windows.Forms.Button();
            this.btnGetSettings = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPrinters = new System.Windows.Forms.ComboBox();
            this.chbMagstripe = new System.Windows.Forms.CheckBox();
            this.chbRFID = new System.Windows.Forms.CheckBox();
            this.chbDebug = new System.Windows.Forms.CheckBox();
            this.rbMagicardType = new System.Windows.Forms.RadioButton();
            this.rbGenericType = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.gbPrinterType = new System.Windows.Forms.GroupBox();
            this.gbCardOrientation = new System.Windows.Forms.GroupBox();
            this.rbLandscape = new System.Windows.Forms.RadioButton();
            this.rbPortrait = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbPrinterLocation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPrinterId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbPrinterType.SuspendLayout();
            this.gbCardOrientation.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer Name";
            // 
            // tbPrinterName
            // 
            this.tbPrinterName.Location = new System.Drawing.Point(92, 11);
            this.tbPrinterName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPrinterName.Name = "tbPrinterName";
            this.tbPrinterName.Size = new System.Drawing.Size(269, 20);
            this.tbPrinterName.TabIndex = 1;
            // 
            // tbMabelUrl
            // 
            this.tbMabelUrl.Location = new System.Drawing.Point(92, 87);
            this.tbMabelUrl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMabelUrl.Name = "tbMabelUrl";
            this.tbMabelUrl.Size = new System.Drawing.Size(269, 20);
            this.tbMabelUrl.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "MABEL URL";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(214, 161);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(146, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnTestMabelConn
            // 
            this.btnTestMabelConn.Location = new System.Drawing.Point(92, 134);
            this.btnTestMabelConn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTestMabelConn.Name = "btnTestMabelConn";
            this.btnTestMabelConn.Size = new System.Drawing.Size(115, 21);
            this.btnTestMabelConn.TabIndex = 5;
            this.btnTestMabelConn.Text = "Test Connection";
            this.btnTestMabelConn.UseVisualStyleBackColor = true;
            this.btnTestMabelConn.Click += new System.EventHandler(this.btnTestMabelConn_Click);
            // 
            // btnGetSettings
            // 
            this.btnGetSettings.Location = new System.Drawing.Point(92, 159);
            this.btnGetSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGetSettings.Name = "btnGetSettings";
            this.btnGetSettings.Size = new System.Drawing.Size(115, 21);
            this.btnGetSettings.TabIndex = 6;
            this.btnGetSettings.Text = "Get Settings";
            this.btnGetSettings.UseVisualStyleBackColor = true;
            this.btnGetSettings.Click += new System.EventHandler(this.btnGetSettings_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Printer";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbPrinterId);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tbPrinterLocation);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbApiKey);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnGetSettings);
            this.panel1.Controls.Add(this.btnTestMabelConn);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.tbMabelUrl);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbPrinterName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(9, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 183);
            this.panel1.TabIndex = 8;
            // 
            // tbApiKey
            // 
            this.tbApiKey.Location = new System.Drawing.Point(92, 106);
            this.tbApiKey.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbApiKey.Name = "tbApiKey";
            this.tbApiKey.Size = new System.Drawing.Size(269, 20);
            this.tbApiKey.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 108);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "API Key";
            // 
            // cbPrinters
            // 
            this.cbPrinters.FormattingEnabled = true;
            this.cbPrinters.Location = new System.Drawing.Point(82, 7);
            this.cbPrinters.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPrinters.Name = "cbPrinters";
            this.cbPrinters.Size = new System.Drawing.Size(256, 21);
            this.cbPrinters.TabIndex = 9;
            this.cbPrinters.SelectedIndexChanged += new System.EventHandler(this.cbPrinters_SelectedIndexChanged);
            // 
            // chbMagstripe
            // 
            this.chbMagstripe.AutoSize = true;
            this.chbMagstripe.Location = new System.Drawing.Point(18, 20);
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
            this.chbRFID.Location = new System.Drawing.Point(18, 44);
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
            this.chbDebug.Location = new System.Drawing.Point(18, 68);
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
            this.rbMagicardType.Location = new System.Drawing.Point(22, 23);
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
            this.rbGenericType.Location = new System.Drawing.Point(22, 42);
            this.rbGenericType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbGenericType.Name = "rbGenericType";
            this.rbGenericType.Size = new System.Drawing.Size(62, 17);
            this.rbGenericType.TabIndex = 14;
            this.rbGenericType.TabStop = true;
            this.rbGenericType.Text = "Generic";
            this.rbGenericType.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 15;
            // 
            // gbPrinterType
            // 
            this.gbPrinterType.Controls.Add(this.rbMagicardType);
            this.gbPrinterType.Controls.Add(this.rbGenericType);
            this.gbPrinterType.Location = new System.Drawing.Point(10, 35);
            this.gbPrinterType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbPrinterType.Name = "gbPrinterType";
            this.gbPrinterType.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbPrinterType.Size = new System.Drawing.Size(196, 70);
            this.gbPrinterType.TabIndex = 16;
            this.gbPrinterType.TabStop = false;
            this.gbPrinterType.Text = "Printer Type";
            // 
            // gbCardOrientation
            // 
            this.gbCardOrientation.Controls.Add(this.rbLandscape);
            this.gbCardOrientation.Controls.Add(this.rbPortrait);
            this.gbCardOrientation.Location = new System.Drawing.Point(10, 108);
            this.gbCardOrientation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbCardOrientation.Name = "gbCardOrientation";
            this.gbCardOrientation.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbCardOrientation.Size = new System.Drawing.Size(196, 62);
            this.gbCardOrientation.TabIndex = 17;
            this.gbCardOrientation.TabStop = false;
            this.gbCardOrientation.Text = "Card Orientation";
            // 
            // rbLandscape
            // 
            this.rbLandscape.AutoSize = true;
            this.rbLandscape.Location = new System.Drawing.Point(22, 40);
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
            this.rbPortrait.Location = new System.Drawing.Point(22, 21);
            this.rbPortrait.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbPortrait.Name = "rbPortrait";
            this.rbPortrait.Size = new System.Drawing.Size(58, 17);
            this.rbPortrait.TabIndex = 15;
            this.rbPortrait.TabStop = true;
            this.rbPortrait.Text = "Portrait";
            this.rbPortrait.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chbMagstripe);
            this.groupBox3.Controls.Add(this.chbRFID);
            this.groupBox3.Controls.Add(this.chbDebug);
            this.groupBox3.Location = new System.Drawing.Point(214, 43);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(152, 127);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbPrinters);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.gbCardOrientation);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.gbPrinterType);
            this.panel2.Location = new System.Drawing.Point(10, 197);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(368, 180);
            this.panel2.TabIndex = 13;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(300, 386);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(79, 28);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(217, 386);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 28);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(135, 386);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(79, 28);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbPrinterLocation
            // 
            this.tbPrinterLocation.Location = new System.Drawing.Point(92, 35);
            this.tbPrinterLocation.Margin = new System.Windows.Forms.Padding(2);
            this.tbPrinterLocation.Name = "tbPrinterLocation";
            this.tbPrinterLocation.Size = new System.Drawing.Size(269, 20);
            this.tbPrinterLocation.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 37);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Printer Location";
            // 
            // tbPrinterId
            // 
            this.tbPrinterId.Location = new System.Drawing.Point(92, 59);
            this.tbPrinterId.Margin = new System.Windows.Forms.Padding(2);
            this.tbPrinterId.Name = "tbPrinterId";
            this.tbPrinterId.Size = new System.Drawing.Size(269, 20);
            this.tbPrinterId.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 61);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Printer ID";
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 416);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SettingsDialog";
            this.Text = "SettingsDialog";
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbPrinterType.ResumeLayout(false);
            this.gbPrinterType.PerformLayout();
            this.gbCardOrientation.ResumeLayout(false);
            this.gbCardOrientation.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPrinterName;
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbPrinterType;
        private System.Windows.Forms.GroupBox gbCardOrientation;
        private System.Windows.Forms.RadioButton rbLandscape;
        private System.Windows.Forms.RadioButton rbPortrait;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbApiKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPrinterId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPrinterLocation;
        private System.Windows.Forms.Label label6;
    }
}