using System;
using System.Drawing;
using System.Windows.Forms;

using ToolBox2.Pages.InstalledPage;
using ToolBox2.Pages.UnInstalledPage;
using ToolBox2.Util;

namespace ToolBox2.Main
{
    public partial class MainWindow : Form
    {
        // Main
        public MainWindow()
        {
            MainWindow.self = this;
            this.Visible = false;
            InitializeComponent();
        }
        
        // Init

        public MainWindow Initialize()
        {
            Console.WriteLine("ToolBox 2.0 started at {0:dd/MM/yyyy H:mm:ss}", DateTime.Now);
            Data.InitializeData();
            Util.Utilities.RoundBorderForm(this);
            this.MouseDown += this.Draggable;
            this.menupanel.MouseDown += this.Draggable;
            this.exitBTN.BringToFront();
            this.InitButtons();
            this.InitPages();
            this.ClearPage();
            this.Visible = true;
            return this;
        }

        public void InitButtons()
        {
            int x = 0; 
            int y = 50;
            int width = 200;
            int height = 30;

            var btn_installed = new Button();
            btn_installed.Parent = this.menupanel;
            btn_installed.Text = "Installed";
            btn_installed.AutoSize = false;
            btn_installed.Size = new Size(width, height);
            btn_installed.FlatStyle = FlatStyle.Flat;
            btn_installed.FlatAppearance.BorderSize = 0;
            btn_installed.Location = new Point(x, y += 30);
            btn_installed.MouseClick += this.menupanel_InstalledBtn_Click;

            var btn_uninstalled = new Button();
            btn_uninstalled.Parent = this.menupanel;
            btn_uninstalled.Text = "Browse";
            btn_uninstalled.AutoSize = false;
            btn_uninstalled.Size = new Size(width, height);
            btn_uninstalled.FlatStyle = FlatStyle.Flat;
            btn_uninstalled.FlatAppearance.BorderSize = 0;
            btn_uninstalled.Location = new Point(x, y += 30);
            btn_uninstalled.MouseClick += this.menupanel_UnInstalledBtn_Click;

            this.menuButtons.Add(btn_installed);
            this.menuButtons.Add(btn_uninstalled);
        }

        public void InitPages()
        {
            Color darkish = Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            /*InstalledPanel.Apps.Add(new App
            {
                Name = "test",
                Installed = true
            });*/
            // InstalledPage
            this.InstalledPage = new InstalledPanel();
            this.InstalledPage.Parent = this;
            this.InstalledPage.Location = new Point(200, 0);
            this.InstalledPage.Size = new Size(960, 680);
            this.InstalledPage.BackColor = darkish;
            this.InstalledPage.MouseDown += this.Draggable;

            // UnInstalled Page
            this.UnInstalledPage = new UnInstalledPanel();
            this.UnInstalledPage.Parent = this;
            this.UnInstalledPage.Location = new Point(200, 0);
            this.UnInstalledPage.Size = new Size(960, 680);
            this.UnInstalledPage.BackColor = darkish;
            this.UnInstalledPage.Visible = false;
            this.UnInstalledPage.Enabled = false;
            this.UnInstalledPage.MouseDown += this.Draggable;

            this.InstalledPage.Initialize();
        }

        // Managing

        public void ClearPage()
        {
            this.line_panel.Visible = false;
            this.line_panel.Enabled = false;
            this.InstalledPage.Visible = false;
            this.UnInstalledPage.Visible = false;

            this.InstalledPage.Enabled = false;
            this.UnInstalledPage.Enabled = false;
        }

        public void menupanel_InstalledBtn_Click(object sender, MouseEventArgs e)
        {
            if (Header.currentPage != Page.INSTALLED)
            {
                Header.SetPage(Page.INSTALLED);
                this.Invalidate();
            }
        }

        public void menupanel_UnInstalledBtn_Click(object sender, MouseEventArgs e)
        {
            if (Header.currentPage != Page.UNINSTALLED)
            {
                Header.SetPage(Page.UNINSTALLED);
                this.Invalidate();
            }
        }

        public void Save(object sender, FormClosingEventArgs e)
        {
            this.InstalledPage.Save();
            this.UnInstalledPage.Save();
            Data.Save();
        }

        private void exitBTN_Click(object sender, EventArgs e)
        {
            this.Save(null, null);
            Environment.Exit(Environment.ExitCode);
        }

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

        // Pages

        public void PageInstalled()
        {
            Header.SetPage(Page.INSTALLED);
            this.InstalledPage.Visible = true;
            this.InstalledPage.Enabled = true;
        }

        public void PageUnInstalled()
        {
            Header.SetPage(Page.UNINSTALLED);
            this.UnInstalledPage.Visible = true;
            this.UnInstalledPage.Enabled = true;
        }

        // Rendering

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Header.currentPage == Page.INSTALLED)
            {
                if (Header.prevPage != Page.INSTALLED) 
                {
                    this.ClearPage();
                    this.line_panel.Location = new Point(0, 105);
                    this.line_panel.Visible = true;
                    this.line_panel.BringToFront();
                    this.PageInstalled();
                }
            }
            else if (Header.currentPage == Page.UNINSTALLED)
            {
                if (Header.prevPage != Page.UNINSTALLED)
                {
                    this.ClearPage();
                    this.line_panel.Location = new Point(0, 135);
                    this.line_panel.Visible = true;
                    this.line_panel.BringToFront();
                    this.PageUnInstalled();
                }
            }
            else
            {
                this.ClearPage();
                this.line_panel.Visible = false;
            }
        }
    }
}
