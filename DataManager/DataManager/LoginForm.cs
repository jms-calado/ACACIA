using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;
using static DataManager.JsonRESTObjects;

namespace DataManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalVars.username = usernameTextBox.Text;
            GlobalVars.password = passwordTextBox.Text;
            Close();
        }
    }
}
