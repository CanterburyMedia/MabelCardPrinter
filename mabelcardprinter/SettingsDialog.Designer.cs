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
            this.tbApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer Name";
            // 
            // tbPrinterName
            // 
            this.tbPrinterName.Location = new System.Drawing.Point(165, 22);
            this.tbPrinterName.Name = "tbPrinterName";
            this.tbPrinterName.Size = new System.Drawing.Size(508, 31);
            this.tbPrinterName.TabIndex = 1;
            // 
            // tbMabelUrl
            // 
            this.tbMabelUrl.Location = new System.Drawing.Point(165, 115);
            this.tbMabelUrl.Name = "tbMabelUrl";
            this.tbMabelUrl.Size = new System.Drawing.Size(508, 31);
            this.tbMabelUrl.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "MABEL URL";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(427, 258);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(243, 31);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnTestMabelConn
            // 
            this.btnTestMabelConn.Location = new System.Drawing.Point(165, 206);
            this.btnTestMabelConn.Name = "btnTestMabelConn";
            this.btnTestMabelConn.Size = new System.Drawing.Size(250, 41);
            this.btnTestMabelConn.TabIndex = 5;
            this.btnTestMabelConn.Text = "Test Connection";
            this.btnTestMabelConn.UseVisualStyleBackColor = true;
            this.btnTestMabelConn.Click += new System.EventHandler(this.btnTestMabelConn_Click);
            // 
            // btnGetSettings
            // 
            this.btnGetSettings.Location = new System.Drawing.Point(165, 253);
            this.btnGetSettings.Name = "btnGetSettings";
            this.btnGetSettings.Size = new System.Drawing.Size(250, 41);
            this.btnGetSettings.TabIndex = 6;
            this.btnGetSettings.Text = "Get Settings";
            this.btnGetSettings.UseVisualStyleBackColor = true;
            this.btnGetSettings.Click += new System.EventHandler(this.btnGetSettings_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Printer";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbApiKey);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnGetSettings);
            this.panel1.Controls.Add(this.btnTestMabelConn);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.tbMabelUrl);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbPrinterName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 304);
            this.panel1.TabIndex = 8;
            // 
            // cbPrinters
            // 
            this.cbPrinters.FormattingEnabled = true;
            this.cbPrinters.Location = new System.Drawing.Point(165, 13);
            this.cbPrinters.Name = "cbPrinters";
            this.cbPrinters.Size = new System.Drawing.Size(508, 33);
            this.cbPrinters.TabIndex = 9;
            this.cbPrinters.SelectedIndexChanged += new System.EventHandler(this.cbPrinters_SelectedIndexChanged);
            // 
            // chbMagstripe
            // 
            this.chbMagstripe.AutoSize = true;
            this.chbMagstripe.Location = new System.Drawing.Point(36, 39);
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
            this.chbRFID.Location = new System.Drawing.Point(36, 84);
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
            this.chbDebug.Location = new System.Drawing.Point(36, 131);
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
            this.rbMagicardType.Location = new System.Drawing.Point(43, 44);
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
            this.rbGenericType.Location = new System.Drawing.Point(43, 80);
            this.rbGenericType.Name = "rbGenericType";
            this.rbGenericType.Size = new System.Drawing.Size(113, 30);
            this.rbGenericType.TabIndex = 14;
            this.rbGenericType.TabStop = true;
            this.rbGenericType.Text = "Generic";
            this.rbGenericType.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 26);
            this.label4.TabIndex = 15;
            // 
            // gbPrinterType
            // 
            this.gbPrinterType.Controls.Add(this.rbMagicardType);
            this.gbPrinterType.Controls.Add(this.rbGenericType);
            this.gbPrinterType.Location = new System.Drawing.Point(19, 68);
            this.gbPrinterType.Name = "gbPrinterType";
            this.gbPrinterType.Size = new System.Drawing.Size(392, 134);
            this.gbPrinterType.TabIndex = 16;
            this.gbPrinterType.TabStop = false;
            this.gbPrinterType.Text = "Printer Type";
            // 
            // gbCardOrientation
            // 
            this.gbCardOrientation.Controls.Add(this.rbLandscape);
            this.gbCardOrientation.Controls.Add(this.rbPortrait);
            this.gbCardOrientation.Location = new System.Drawing.Point(19, 208);
            this.gbCardOrientation.Name = "gbCardOrientation";
            this.gbCardOrientation.Size = new System.Drawing.Size(392, 120);
            this.gbCardOrientation.TabIndex = 17;
            this.gbCardOrientation.TabStop = false;
            this.gbCardOrientation.Text = "Card Orientation";
            // 
            // rbLandscape
            // 
            this.rbLandscape.AutoSize = true;
            this.rbLandscape.Location = new System.Drawing.Point(43, 77);
            this.rbLandscape.Name = "rbLandscape";
            this.rbLandscape.Size = new System.Drawing.Size(143, 30);
            this.rbLandscape.TabIndex = 16;
            this.rbLandscape.TabStop = true;
            this.rbLandscape.Text = "Landscape";
            this.rbLandscape.UseVisualStyleBackColor = true;
            // 
            // rbPortrait
            // 
            this.rbPortrait.AutoSize = true;
            this.rbPortrait.Location = new System.Drawing.Point(43, 41);
            this.rbPortrait.Name = "rbPortrait";
            this.rbPortrait.Size = new System.Drawing.Size(107, 30);
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
            this.groupBox3.Location = new System.Drawing.Point(427, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(305, 245);
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
            this.panel2.Location = new System.Drawing.Point(14, 322);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(736, 347);
            this.panel2.TabIndex = 13;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(594, 685);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(158, 54);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(428, 685);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(158, 54);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(264, 685);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(158, 54);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbApiKey
            // 
            this.tbApiKey.Location = new System.Drawing.Point(165, 152);
            this.tbApiKey.Name = "tbApiKey";
            this.tbApiKey.Size = new System.Drawing.Size(508, 31);
            this.tbApiKey.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 26);
            this.label5.TabIndex = 7;
            this.label5.Text = "API Key";
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 745);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}