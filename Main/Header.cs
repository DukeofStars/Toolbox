using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ToolBox2.Pages.InstalledPage;
using ToolBox2.Pages.UnInstalledPage;

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
        public static void SetPage(Page page)
        {
            if (page != Page.NULL)
            {
                Header.prevPage = Header.currentPage;
                Header.currentPage = page;
            }
            else
            {
                Header.prevPage = Page.NULL;
                Header.currentPage = Page.NULL;
            }
        }
    }

    public enum Page
    {
        INSTALLED,
        UNINSTALLED,
        NULL
    }
}