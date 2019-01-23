using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HmiConfig
{
    public partial class PasswordForm : Form
    {
        string m_password;
        public PasswordForm(string password)
        {
            m_password = password;
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (tbPassword.TextLength > 0 && tbPassword.TextLength <= 6
                && string.Compare(tbPassword.Text, m_password) ==0)
            {
                DialogResult = DialogResult.OK;
            }
        }

    }
}
