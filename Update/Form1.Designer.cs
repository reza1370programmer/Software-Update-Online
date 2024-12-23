namespace Update
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblNewVer = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurentVer = new System.Windows.Forms.Label();
            this.lblCheck = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(298, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "ذخیره";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblNewVer
            // 
            this.lblNewVer.Location = new System.Drawing.Point(253, 34);
            this.lblNewVer.Name = "lblNewVer";
            this.lblNewVer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblNewVer.Size = new System.Drawing.Size(74, 21);
            this.lblNewVer.TabIndex = 17;
            this.lblNewVer.Text = "|";
            this.lblNewVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(5, 95);
            this.lblCount.Name = "lblCount";
            this.lblCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCount.Size = new System.Drawing.Size(64, 20);
            this.lblCount.TabIndex = 16;
            this.lblCount.Text = "|";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "نسخه جدید:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "نسخه جاری:";
            // 
            // lblCurentVer
            // 
            this.lblCurentVer.Location = new System.Drawing.Point(253, 5);
            this.lblCurentVer.Name = "lblCurentVer";
            this.lblCurentVer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCurentVer.Size = new System.Drawing.Size(74, 21);
            this.lblCurentVer.TabIndex = 13;
            this.lblCurentVer.Text = "|";
            this.lblCurentVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCheck
            // 
            this.lblCheck.Location = new System.Drawing.Point(56, 95);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(338, 45);
            this.lblCheck.TabIndex = 12;
            this.lblCheck.Text = "|";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(187, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(207, 26);
            this.button2.TabIndex = 11;
            this.button2.Text = "بروزرسانی";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 228);
            this.Controls.Add(this.lblNewVer);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurentVer);
            this.Controls.Add(this.lblCheck);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblNewVer;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurentVer;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.Button button2;
    }
}

