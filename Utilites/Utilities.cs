using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System;

using ToolBox2.Main;

namespace ToolBox2.Util
{
    class Utilities
    {
        public static void RoundBorderForm(Form frm)
        {

            Rectangle Bounds = new Rectangle(0, 0, frm.Width, frm.Height);
            int CornerRadius = 20;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
            path.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
            path.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            path.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            path.CloseAllFigures();

            frm.Region = new Region(path);
            frm.Show();
        }
        public static void RoundBorderControl(Control control)
        {
            Rectangle Bounds = new Rectangle(0, 0, control.Width, control.Height);
            int CornerRadius = 20;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
            path.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
            path.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            path.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            path.CloseAllFigures();

            control.Region = new Region(path);
            control.Show();
        }
        public static void RoundBorderPanel(Panel panel)
        {
            Rectangle Bounds = new Rectangle(0, 0, panel.Width, panel.Height);
            int CornerRadius = 20;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
            path.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
            path.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            path.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            path.CloseAllFigures();

            panel.Region = new Region(path);
            panel.Show();
        }
        public static void RestartWithAdmin()
        {
            var result = MessageBox.Show(
                    "Toolbox requires higher permissions to perform this task",
                    "Unauthorized Access Exception",
                    MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                ProcessStartInfo restart = new ProcessStartInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
                restart.UseShellExecute = true;
                restart.Verb = "runas";
                Process.Start(restart);
                MainWindow.self.Save(null, null);
                Environment.Exit(0);
            }
            return;
        }
        public static string ToVersionString(int version)
        {
            string versionStr = Convert.ToString(version);
            string returnStr = "";
            if (versionStr.Length != 6)
                return "failed";
            for (int i = 0; i < 6; i++)
            {
                returnStr += i % 2 == 0 ? versionStr[i] + '.' : versionStr[i];
            }
            return returnStr;
        }
    }
}
