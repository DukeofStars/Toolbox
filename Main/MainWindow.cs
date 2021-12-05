using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

using ToolBox.Pages.InstalledPage;
using ToolBox.Pages.UnInstalledPage;
using Toolbox.Update;

namespace ToolBox.Main
{
    public partial class MainWindow : Form
    {
        // Main
        public MainWindow()
        {
            self = this;
            Visible = false;
            InitializeComponent();
        }
        
        // Init

        public MainWindow Initialize()
        {
            Console.WriteLine("ToolBox started at {0:dd/MM/yyyy H:mm:ss}", DateTime.Now);

            Task.Run(() =>
            {
                InstalledPanel.Apps = Data.FetchInstalled();
                Data.AllApps = Data.FetchAll();
            });
            Updater updater = new Updater();
            Task.Run(() => updater.UpdateAll());

            Utilities.Utilities.RoundBorderForm(this);

            this.MouseDown += this.Draggable;
            this.menupanel.MouseDown += this.Draggable;

            this.exitBTN.BringToFront();

            this.InitButtons();
            this.InitPages();
            this.ClearPage();

            Header.SetPage(Page.NULL);
            Header.SetPage(Page.INSTALLED);

            this.Invalidate();
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
            btn_installed.Cursor = Cursors.Hand;

            var btn_uninstalled = new Button();
            btn_uninstalled.Parent = this.menupanel;
            btn_uninstalled.Text = "Browse";
            btn_uninstalled.AutoSize = false;
            btn_uninstalled.Size = new Size(width, height);
            btn_uninstalled.FlatStyle = FlatStyle.Flat;
            btn_uninstalled.FlatAppearance.BorderSize = 0;
            btn_uninstalled.Location = new Point(x, y += 30);
            btn_uninstalled.MouseClick += this.menupanel_UnInstalledBtn_Click;
            btn_uninstalled.Cursor = Cursors.Hand;

            this.menuButtons.Add(btn_installed);
            this.menuButtons.Add(btn_uninstalled);
        }

        public void InitPages()
        {
            // InstalledPage
            this.InstalledPage = new InstalledPanel();
            this.InstalledPage.Parent = this;
            this.InstalledPage.Location = new Point(200, 0);
            this.InstalledPage.Size = new Size(960, 680);
            this.InstalledPage.BackColor = Header.darkish;
            this.InstalledPage.MouseDown += this.Draggable;

            // UnInstalled Page
            this.UnInstalledPage = new UnInstalledPanel();
            this.UnInstalledPage.Parent = this;
            this.UnInstalledPage.Location = new Point(200, 0);
            this.UnInstalledPage.Size = new Size(960, 680);
            this.UnInstalledPage.BackColor = Header.darkish;
            this.UnInstalledPage.Visible = false;
            this.UnInstalledPage.Enabled = false;
            this.UnInstalledPage.MouseDown += this.Draggable;

            this.InstalledPage.Refresh();
            this.UnInstalledPage.Refresh();
        }

        // Managing

        public void menupanel_InstalledBtn_Click(object sender, MouseEventArgs e)
        {
            Header.SetPage(Page.INSTALLED);
            this.Invalidate();
        }

        public void menupanel_UnInstalledBtn_Click(object sender, MouseEventArgs e)
        {
            Header.SetPage(Page.UNINSTALLED);
            this.Invalidate();
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
            else if (Header.currentPage == Page.APPDESC)
            {
                this.line_panel.Visible = false;
            }
            else
            {
                this.ClearPage();
                this.line_panel.Visible = false;
            }
        }

        public void ClearPage()
        {
            this.line_panel.Visible = false;
            this.line_panel.Enabled = false;
            this.InstalledPage.Visible = false;
            this.UnInstalledPage.Visible = false;

            this.InstalledPage.Enabled = false;
            this.UnInstalledPage.Enabled = false;

            if (Header.prevPage == Page.APPDESC)
            {
                Header.appDescPanel.Visible = false;
                Header.appDescPanel.Enabled = false;
                Header.appDescPanel.Dispose();
            }
        }

        private void exitBTN_MouseEnter(object sender, EventArgs e)
        {
            exitBTN.BackColor = Color.Red;
        }

        private void exitBTN_MouseLeave(object sender, EventArgs e)
        {
            exitBTN.BackColor = Header.darkish;
        }
    }
}
