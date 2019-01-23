using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Config
{
    public partial class WaitingForm : Form
    {
        public WaitingForm()
        {
            InitializeComponent();
        }
        public WaitingForm(Form Parent)
        {
            Point localtion = new Point();
            InitializeComponent();
            localtion.X = Parent.Location.X + (Parent.Size.Width - this.Size.Width) / 2;
            localtion.Y = Parent.Location.Y + (Parent.Size.Height - this.Size.Height) / 2;
            this.Location = localtion;
        }
    }
}
