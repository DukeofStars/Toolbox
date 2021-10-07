
namespace ToolBox2.Pages
{
    partial class AppDescPanel
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
            this.lbl_desc = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_desc
            // 
            this.lbl_desc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_desc.Location = new System.Drawing.Point(50, 100);
            this.lbl_desc.Margin = new System.Windows.Forms.Padding(50, 200, 50, 50);
            this.lbl_desc.Name = "lbl_desc";
            this.lbl_desc.Size = new System.Drawing.Size(860, 530);
            this.lbl_desc.TabIndex = 0;
            this.lbl_desc.Text = "App Description Here";
            this.lbl_desc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_name
            // 
            this.lbl_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(50, 0);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(860, 100);
            this.lbl_name.TabIndex = 1;
            this.lbl_name.Text = "App";
            this.lbl_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AppDescPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.lbl_desc);
            this.Name = "AppDescPanel";
            this.Size = new System.Drawing.Size(960, 680);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_desc;
        private System.Windows.Forms.Label lbl_name;
    }
}
