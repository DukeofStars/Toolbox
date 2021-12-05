
namespace ToolBox.Pages
{
    partial class AppButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AppNameLBL = new System.Windows.Forms.Label();
            this.btn_install = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AppNameLBL
            // 
            this.AppNameLBL.Font = new System.Drawing.Font("Calibri", 20F);
            this.AppNameLBL.ForeColor = System.Drawing.Color.Gainsboro;
            this.AppNameLBL.Location = new System.Drawing.Point(0, 0);
            this.AppNameLBL.Name = "AppNameLBL";
            this.AppNameLBL.Size = new System.Drawing.Size(150, 80);
            this.AppNameLBL.TabIndex = 0;
            this.AppNameLBL.Text = "NULL";
            this.AppNameLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AppNameLBL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Execute);
            // 
            // btn_install
            // 
            this.btn_install.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_install.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_install.FlatAppearance.BorderSize = 0;
            this.btn_install.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_install.Image = global::ToolBox.Properties.Resources.install_icon_20x20_;
            this.btn_install.Location = new System.Drawing.Point(0, 80);
            this.btn_install.Name = "btn_install";
            this.btn_install.Size = new System.Drawing.Size(25, 25);
            this.btn_install.TabIndex = 1;
            this.btn_install.UseVisualStyleBackColor = true;
            // 
            // AppButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Controls.Add(this.btn_install);
            this.Controls.Add(this.AppNameLBL);
            this.Name = "AppButton";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Execute);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label AppNameLBL;
        private System.Windows.Forms.Button btn_install;
    }
}
