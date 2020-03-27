namespace UserDriverStaticImage
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbDevices = new System.Windows.Forms.ComboBox();
            this.groupDevice = new System.Windows.Forms.GroupBox();
            this.btnDeviceRefresh = new System.Windows.Forms.Button();
            this.groupPicture = new System.Windows.Forms.GroupBox();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.picView = new System.Windows.Forms.PictureBox();
            this.groupDevice.SuspendLayout();
            this.groupPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path:";
            // 
            // cbDevices
            // 
            this.cbDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevices.FormattingEnabled = true;
            this.cbDevices.Location = new System.Drawing.Point(69, 34);
            this.cbDevices.Name = "cbDevices";
            this.cbDevices.Size = new System.Drawing.Size(523, 26);
            this.cbDevices.TabIndex = 1;
            this.cbDevices.SelectedIndexChanged += new System.EventHandler(this.cbDevices_SelectedIndexChanged);
            // 
            // groupDevice
            // 
            this.groupDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDevice.Controls.Add(this.btnDeviceRefresh);
            this.groupDevice.Controls.Add(this.cbDevices);
            this.groupDevice.Controls.Add(this.label1);
            this.groupDevice.Location = new System.Drawing.Point(12, 12);
            this.groupDevice.Name = "groupDevice";
            this.groupDevice.Size = new System.Drawing.Size(736, 88);
            this.groupDevice.TabIndex = 2;
            this.groupDevice.TabStop = false;
            this.groupDevice.Text = "Device";
            // 
            // btnDeviceRefresh
            // 
            this.btnDeviceRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeviceRefresh.Location = new System.Drawing.Point(598, 34);
            this.btnDeviceRefresh.Name = "btnDeviceRefresh";
            this.btnDeviceRefresh.Size = new System.Drawing.Size(112, 26);
            this.btnDeviceRefresh.TabIndex = 2;
            this.btnDeviceRefresh.Text = "Refresh";
            this.btnDeviceRefresh.UseVisualStyleBackColor = true;
            this.btnDeviceRefresh.Click += new System.EventHandler(this.btnDeviceRefresh_Click);
            // 
            // groupPicture
            // 
            this.groupPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPicture.Controls.Add(this.btnLoadImage);
            this.groupPicture.Controls.Add(this.picView);
            this.groupPicture.Enabled = false;
            this.groupPicture.Location = new System.Drawing.Point(12, 106);
            this.groupPicture.Name = "groupPicture";
            this.groupPicture.Size = new System.Drawing.Size(736, 412);
            this.groupPicture.TabIndex = 3;
            this.groupPicture.TabStop = false;
            this.groupPicture.Text = "Image";
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadImage.Location = new System.Drawing.Point(618, 380);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(112, 26);
            this.btnLoadImage.TabIndex = 3;
            this.btnLoadImage.Text = "Load";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // picView
            // 
            this.picView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picView.Location = new System.Drawing.Point(6, 23);
            this.picView.Name = "picView";
            this.picView.Size = new System.Drawing.Size(724, 351);
            this.picView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picView.TabIndex = 0;
            this.picView.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(760, 530);
            this.Controls.Add(this.groupPicture);
            this.Controls.Add(this.groupDevice);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserDriverStaticImage";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupDevice.ResumeLayout(false);
            this.groupDevice.PerformLayout();
            this.groupPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.GroupBox groupDevice;
        private System.Windows.Forms.Button btnDeviceRefresh;
        private System.Windows.Forms.GroupBox groupPicture;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.PictureBox picView;
    }
}

