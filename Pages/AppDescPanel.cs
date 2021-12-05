using System;
using System.Drawing;
using System.Windows.Forms;

using ToolBox2.Main;

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

        public void Slide(Direction direction)
        {
            if (direction == Direction.LEFT)
            {
                this.Enabled = false;
                while (this.Location.X > 200)
                {
                    this.BringToFront();
                    this.Location = new Point(this.Location.X - 2, 0);
                }
                this.Enabled = true;
            }
            else if (direction == Direction.RIGHT)
            {
                this.Enabled = false;
                while (this.Location.X < 1160)
                {
                    this.BringToFront();
                    this.Location = new Point(this.Location.X + 2, 0);
                }
                this.Enabled = true;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
    public enum Direction 
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
