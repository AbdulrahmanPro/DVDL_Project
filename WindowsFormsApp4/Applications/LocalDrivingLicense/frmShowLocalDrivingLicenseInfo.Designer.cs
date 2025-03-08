namespace WindowsFormsApp4.Applications.LocalDrivingLicense
{
    partial class frmShowLocalDrivingLicenseInfo
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
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ctrLocalDrivingInfo1 = new WindowsFormsApp4.Applications.LocalDrivingLicense.Controls.ctrLocalDrivingInfo();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.Controls.Add(this.guna2ImageButton1);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(53)))), ((int)(((byte)(107)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(63)))), ((int)(((byte)(50)))));
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 335);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(828, 77);
            this.guna2GradientPanel1.TabIndex = 1;
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(80, 60);
            this.guna2ImageButton1.Image = global::WindowsFormsApp4.Properties.Resources._1716833984295;
            this.guna2ImageButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton1.ImageRotate = 0F;
            this.guna2ImageButton1.ImageSize = new System.Drawing.Size(90, 70);
            this.guna2ImageButton1.Location = new System.Drawing.Point(731, 0);
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Size = new System.Drawing.Size(97, 77);
            this.guna2ImageButton1.TabIndex = 0;
            this.guna2ImageButton1.UseTransparentBackground = true;
            this.guna2ImageButton1.Click += new System.EventHandler(this.guna2ImageButton1_Click);
            // 
            // ctrLocalDrivingInfo1
            // 
            this.ctrLocalDrivingInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrLocalDrivingInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrLocalDrivingInfo1.Name = "ctrLocalDrivingInfo1";
            this.ctrLocalDrivingInfo1.Size = new System.Drawing.Size(828, 412);
            this.ctrLocalDrivingInfo1.TabIndex = 0;
            // 
            // frmShowLocalDrivingLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 412);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.ctrLocalDrivingInfo1);
            this.Name = "frmShowLocalDrivingLicenseInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmShowLocalDrivingLicenseInfo";
            this.Load += new System.EventHandler(this.frmShowLocalDrivingLicenseInfo_Load);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrLocalDrivingInfo ctrLocalDrivingInfo1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
    }
}