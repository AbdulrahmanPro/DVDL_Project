namespace WindowsFormsApp4.Test
{
    partial class frmTakeTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTakeTest));
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2GradientPanel3 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.btnSave = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2ImageButton();
            this.lblUserMassage = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtNotes = new System.Windows.Forms.RichTextBox();
            this.rbFaild = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbPassed = new Guna.UI2.WinForms.Guna2RadioButton();
            this.guna2GradientPanel2 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.ctrScheduledTest1 = new WindowsFormsApp4.Test.Controls.ctrScheduledTest();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2GradientPanel3.SuspendLayout();
            this.guna2GradientPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.Controls.Add(this.guna2GradientPanel3);
            this.guna2GradientPanel1.Controls.Add(this.guna2GradientPanel2);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(53)))), ((int)(((byte)(107)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(63)))), ((int)(((byte)(50)))));
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(536, 737);
            this.guna2GradientPanel1.TabIndex = 1;
            // 
            // guna2GradientPanel3
            // 
            this.guna2GradientPanel3.Controls.Add(this.btnSave);
            this.guna2GradientPanel3.Controls.Add(this.btnClose);
            this.guna2GradientPanel3.Controls.Add(this.lblUserMassage);
            this.guna2GradientPanel3.Controls.Add(this.guna2HtmlLabel2);
            this.guna2GradientPanel3.Controls.Add(this.guna2HtmlLabel1);
            this.guna2GradientPanel3.Controls.Add(this.txtNotes);
            this.guna2GradientPanel3.Controls.Add(this.rbFaild);
            this.guna2GradientPanel3.Controls.Add(this.rbPassed);
            this.guna2GradientPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2GradientPanel3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(53)))), ((int)(((byte)(107)))));
            this.guna2GradientPanel3.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(63)))), ((int)(((byte)(50)))));
            this.guna2GradientPanel3.Location = new System.Drawing.Point(0, 554);
            this.guna2GradientPanel3.Name = "guna2GradientPanel3";
            this.guna2GradientPanel3.Size = new System.Drawing.Size(536, 183);
            this.guna2GradientPanel3.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSave.HoverState.ImageSize = new System.Drawing.Size(70, 60);
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSave.ImageRotate = 0F;
            this.btnSave.ImageSize = new System.Drawing.Size(80, 64);
            this.btnSave.Location = new System.Drawing.Point(431, 124);
            this.btnSave.Name = "btnSave";
            this.btnSave.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSave.Size = new System.Drawing.Size(73, 59);
            this.btnSave.TabIndex = 7;
            this.btnSave.UseTransparentBackground = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnClose.HoverState.ImageSize = new System.Drawing.Size(70, 60);
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnClose.ImageRotate = 0F;
            this.btnClose.ImageSize = new System.Drawing.Size(80, 64);
            this.btnClose.Location = new System.Drawing.Point(364, 124);
            this.btnClose.Name = "btnClose";
            this.btnClose.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnClose.Size = new System.Drawing.Size(72, 54);
            this.btnClose.TabIndex = 6;
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblUserMassage
            // 
            this.lblUserMassage.BackColor = System.Drawing.Color.Transparent;
            this.lblUserMassage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserMassage.Location = new System.Drawing.Point(232, 8);
            this.lblUserMassage.Name = "lblUserMassage";
            this.lblUserMassage.Size = new System.Drawing.Size(114, 18);
            this.lblUserMassage.TabIndex = 5;
            this.lblUserMassage.Text = "guna2HtmlLabel3";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(24, 5);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(55, 21);
            this.guna2HtmlLabel2.TabIndex = 4;
            this.guna2HtmlLabel2.Text = "Result";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(29, 71);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(50, 21);
            this.guna2HtmlLabel1.TabIndex = 3;
            this.guna2HtmlLabel1.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(98, 41);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(406, 77);
            this.txtNotes.TabIndex = 2;
            this.txtNotes.Text = "";
            // 
            // rbFaild
            // 
            this.rbFaild.AutoSize = true;
            this.rbFaild.BackColor = System.Drawing.Color.Transparent;
            this.rbFaild.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbFaild.CheckedState.BorderThickness = 0;
            this.rbFaild.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.rbFaild.CheckedState.InnerColor = System.Drawing.Color.Red;
            this.rbFaild.CheckedState.InnerOffset = -4;
            this.rbFaild.Location = new System.Drawing.Point(163, 9);
            this.rbFaild.Name = "rbFaild";
            this.rbFaild.Size = new System.Drawing.Size(47, 17);
            this.rbFaild.TabIndex = 1;
            this.rbFaild.Text = "Faild";
            this.rbFaild.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rbFaild.UncheckedState.BorderThickness = 2;
            this.rbFaild.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rbFaild.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rbFaild.UseVisualStyleBackColor = false;
            // 
            // rbPassed
            // 
            this.rbPassed.AutoSize = true;
            this.rbPassed.BackColor = System.Drawing.Color.Transparent;
            this.rbPassed.Checked = true;
            this.rbPassed.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbPassed.CheckedState.BorderThickness = 0;
            this.rbPassed.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.rbPassed.CheckedState.InnerColor = System.Drawing.Color.Lime;
            this.rbPassed.CheckedState.InnerOffset = -4;
            this.rbPassed.Location = new System.Drawing.Point(98, 9);
            this.rbPassed.Name = "rbPassed";
            this.rbPassed.Size = new System.Drawing.Size(59, 17);
            this.rbPassed.TabIndex = 0;
            this.rbPassed.TabStop = true;
            this.rbPassed.Text = "Passed";
            this.rbPassed.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rbPassed.UncheckedState.BorderThickness = 2;
            this.rbPassed.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rbPassed.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rbPassed.UseVisualStyleBackColor = false;
            // 
            // guna2GradientPanel2
            // 
            this.guna2GradientPanel2.Controls.Add(this.ctrScheduledTest1);
            this.guna2GradientPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel2.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel2.Name = "guna2GradientPanel2";
            this.guna2GradientPanel2.Size = new System.Drawing.Size(536, 737);
            this.guna2GradientPanel2.TabIndex = 1;
            // 
            // ctrScheduledTest1
            // 
            this.ctrScheduledTest1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrScheduledTest1.Location = new System.Drawing.Point(0, 0);
            this.ctrScheduledTest1.Name = "ctrScheduledTest1";
            this.ctrScheduledTest1.Size = new System.Drawing.Size(536, 737);
            this.ctrScheduledTest1.TabIndex = 1;
            this.ctrScheduledTest1.TestTypeID = DVDLBusiness.clsTestTypeBusiness.enTestType.VisionTest;
            // 
            // frmTakeTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 737);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Name = "frmTakeTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmTakeTest";
            this.Load += new System.EventHandler(this.frmTakeTest_Load);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel3.ResumeLayout(false);
            this.guna2GradientPanel3.PerformLayout();
            this.guna2GradientPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUserMassage;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private System.Windows.Forms.RichTextBox txtNotes;
        private Guna.UI2.WinForms.Guna2RadioButton rbFaild;
        private Guna.UI2.WinForms.Guna2RadioButton rbPassed;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel2;
        private Controls.ctrScheduledTest ctrScheduledTest1;
        private Guna.UI2.WinForms.Guna2ImageButton btnSave;
        private Guna.UI2.WinForms.Guna2ImageButton btnClose;
    }
}