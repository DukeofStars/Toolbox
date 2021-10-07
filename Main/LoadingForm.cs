using System.Windows.Forms;
using System;

using ToolBox2.Util;

namespace ToolBox2.Main
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            Utilities.RoundBorderForm(this);
        }
        public void Step() => this.progressBar.PerformStep();
        public string Status
        {
            get
            {
                return this.lbl_status.Text.TrimEnd('.');
            }
            set
            {
                this.lbl_status.Text = value + "...";
            }
        }
        public int MaxLength
        {
            get
            {
                return this.progressBar.Maximum;
            }
            set
            {
                this.progressBar.Maximum = value;
            }
        }
        public int MinLength
        {
            get
            {
                return this.progressBar.Minimum;
            }
            set
            {
                this.progressBar.Minimum = value;
            }
        }
        public int StepLength
        {
            get
            {
                return this.progressBar.Step;
            }
            set
            {
                this.progressBar.Step = value;
            }
        }
        public int Progress
        {
            get
            {
                return this.progressBar.Value;
            }
            set
            {
                this.progressBar.Value = value;
            }
        }

        // Managing
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Draggable(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
