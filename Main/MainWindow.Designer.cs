namespace ToolBox.Main
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menupanel = new System.Windows.Forms.Panel();
            this.line_panel = new System.Windows.Forms.Panel();
            this.exitBTN = new System.Windows.Forms.Button();
            this.menupanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menupanel
            // 
            this.menupanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.menupanel.Controls.Add(this.line_panel);
            this.menupanel.Location = new System.Drawing.Point(0, 0);
            this.menupanel.Name = "menupanel";
            this.menupanel.Size = new System.Drawing.Size(200, 680);
            this.menupanel.TabIndex = 0;
            // 
            // line_panel
            // 
            this.line_panel.BackColor = System.Drawing.Color.Teal;
            this.line_panel.Location = new System.Drawing.Point(0, 0);
            this.line_panel.Name = "line_panel";
            this.line_panel.Size = new System.Drawing.Size(200, 4);
            this.line_panel.TabIndex = 0;
            this.line_panel.Visible = false;
            // 
            // exitBTN
            // 
            this.exitBTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.exitBTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.exitBTN.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.exitBTN.FlatAppearance.BorderSize = 0;
            this.exitBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBTN.Location = new System.Drawing.Point(1140, 0);
            this.exitBTN.Name = "exitBTN";
            this.exitBTN.Size = new System.Drawing.Size(20, 20);
            this.exitBTN.TabIndex = 1;
            this.exitBTN.Text = "X";
            this.exitBTN.UseVisualStyleBackColor = false;
            this.exitBTN.Click += new System.EventHandler(this.exitBTN_Click);
            this.exitBTN.MouseEnter += new System.EventHandler(this.exitBTN_MouseEnter);
            this.exitBTN.MouseLeave += new System.EventHandler(this.exitBTN_MouseLeave);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 680);
            this.Controls.Add(this.exitBTN);
            this.Controls.Add(this.menupanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Save);
            this.menupanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menupanel;
        private System.Windows.Forms.Panel line_panel;
        private System.Windows.Forms.Button exitBTN;
    }
}

