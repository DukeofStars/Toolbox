using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolBox2.Pages
{
    public partial class AppDescPanel : UserControl
    {
        public AppDescPanel()
        {
            InitializeComponent();
        }

        public string Description
        {
            get
            {
                return this.lbl_desc.Text;
            }
            set
            {
                this.lbl_desc.Text = value;
            }
        }

        public new string Name
        {
            get
            {
                return this.lbl_name.Text;
            }
            set
            {
                this.lbl_name.Text = value;
            }
        }
    }
}
