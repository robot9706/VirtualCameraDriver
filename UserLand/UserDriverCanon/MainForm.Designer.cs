namespace UserDriverCanon
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDevices = new System.Windows.Forms.ComboBox();
            this.groupDevice = new System.Windows.Forms.GroupBox();
            this.btnDeviceRefresh = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefreshCameras = new System.Windows.Forms.Button();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cameraKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.groupDevice.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.cbDevices.Size = new System.Drawing.Size(435, 26);
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
            this.groupDevice.Size = new System.Drawing.Size(648, 88);
            this.groupDevice.TabIndex = 2;
            this.groupDevice.TabStop = false;
            this.groupDevice.Text = "Device";
            // 
            // btnDeviceRefresh
            // 
            this.btnDeviceRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeviceRefresh.Location = new System.Drawing.Point(510, 34);
            this.btnDeviceRefresh.Name = "btnDeviceRefresh";
            this.btnDeviceRefresh.Size = new System.Drawing.Size(112, 26);
            this.btnDeviceRefresh.TabIndex = 2;
            this.btnDeviceRefresh.Text = "Refresh";
            this.btnDeviceRefresh.UseVisualStyleBackColor = true;
            this.btnDeviceRefresh.Click += new System.EventHandler(this.btnDeviceRefresh_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnRefreshCameras);
            this.groupBox1.Controls.Add(this.cbCameras);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 88);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // btnRefreshCameras
            // 
            this.btnRefreshCameras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshCameras.Location = new System.Drawing.Point(510, 34);
            this.btnRefreshCameras.Name = "btnRefreshCameras";
            this.btnRefreshCameras.Size = new System.Drawing.Size(112, 26);
            this.btnRefreshCameras.TabIndex = 2;
            this.btnRefreshCameras.Text = "Refresh";
            this.btnRefreshCameras.UseVisualStyleBackColor = true;
            this.btnRefreshCameras.Click += new System.EventHandler(this.btnRefreshCameras_Click);
            // 
            // cbCameras
            // 
            this.cbCameras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(69, 34);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(435, 26);
            this.cbCameras.TabIndex = 1;
            this.cbCameras.SelectedIndexChanged += new System.EventHandler(this.cbCameras_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Path:";
            // 
            // cameraKeepAlive
            // 
            this.cameraKeepAlive.Enabled = true;
            this.cameraKeepAlive.Interval = 2000;
            this.cameraKeepAlive.Tick += new System.EventHandler(this.cameraKeepAlive_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(672, 207);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupDevice);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserDriverStaticImage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupDevice.ResumeLayout(false);
            this.groupDevice.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.GroupBox groupDevice;
        private System.Windows.Forms.Button btnDeviceRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRefreshCameras;
        private System.Windows.Forms.ComboBox cbCameras;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer cameraKeepAlive;
    }
}

