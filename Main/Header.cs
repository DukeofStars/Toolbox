using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

using ToolBox2.Pages.InstalledPage;
using ToolBox2.Pages.UnInstalledPage;
using ToolBox2.Pages;

namespace ToolBox2.Main
{
    public partial class MainWindow : Form
    {
        public InstalledPanel InstalledPage;
        public UnInstalledPanel UnInstalledPage;
        public List<Button> menuButtons = new List<Button>();
        public static MainWindow self;
        public static InstalledPanel InstalledPanel
        {
            get
            {
                return self.InstalledPage;
            }
        }
        public static UnInstalledPanel UnInstalledPanel
        {
            get
            {
                return self.UnInstalledPage;
            }
        }
    }

    public static class Header
    {
        public static Page currentPage = Page.INSTALLED;
        public static Page prevPage = Page.NULL;
        public static AppDescPanel appDescPanel;

        public static Color darkish = Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));

        public static void SetPage(Page page, AppDescPanel panel = null)
        {
            //MessageBox.Show("Setting page to " + page.ToString());
            if (panel != null && page == Page.APPDESC) Header.appDescPanel = panel;
            if (page is Page.NULL)
            {
                Header.prevPage = Page.NULL;
                Header.currentPage = Page.NULL;
            }
            else
            {
                Header.prevPage = Header.currentPage;
                Header.currentPage = page;
            }
        }
    }

    public enum Page
    {
        INSTALLED,
        UNINSTALLED,
        NULL,
        APPDESC
    }
}