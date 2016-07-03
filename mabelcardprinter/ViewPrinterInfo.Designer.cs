namespace MabelCardPrinter
{
    partial class ViewPrinterInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewPrinterInfo));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lvParamVal = new System.Windows.Forms.ListView();
            this.colParam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MabelCardPrinter.Properties.Resources.ENDURO_Land;
            this.pictureBox1.Location = new System.Drawing.Point(12, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(231, 201);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Get Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(780, 725);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(154, 57);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lvParamVal
            // 
            this.lvParamVal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colParam,
            this.colValue});
            this.lvParamVal.Location = new System.Drawing.Point(287, 22);
            this.lvParamVal.Name = "lvParamVal";
            this.lvParamVal.Size = new System.Drawing.Size(647, 697);
            this.lvParamVal.TabIndex = 4;
            this.lvParamVal.UseCompatibleStateImageBehavior = false;
            this.lvParamVal.View = System.Windows.Forms.View.Details;
            // 
            // colParam
            // 
            this.colParam.Text = "Parameter";
            this.colParam.Width = 157;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 401;
            // 
            // ViewPrinterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 794);
            this.Controls.Add(this.lvParamVal);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewPrinterInfo";
            this.Text = "View Printer Info";
            this.Load += new System.EventHandler(this.ViewPrinterInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListView lvParamVal;
        private System.Windows.Forms.ColumnHeader colParam;
        private System.Windows.Forms.ColumnHeader colValue;
    }
}