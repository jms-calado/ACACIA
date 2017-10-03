using RestEase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Interface.JsonSocketObjects;
using static Interface.JsonRESTObjects;
using static Interface.GlobalVars;
using System.Net;

namespace Interface
{
    public partial class Interface : Form
    {
        public static IAcaciaApi api;
        private SessionObject session;
        public Interface()
        {
            // Creates an implementation of the Rest interface
            api = RestClient.For<IAcaciaApi>(restURL);

            InitializeComponent();
            
            panelSelectSession.Enabled = false;
            panelSelectSession.EnabledChanged += new EventHandler(panelSelectSession_EnabledChanged);
            panelWorkSession.Hide();
            tabControl1.Hide();
        }
        private void listBoxSelectSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            string session = listBoxSelectSession.SelectedItem.ToString();
            panelSelectSession.Enabled = false;
            panelSelectSession.Hide();
            panelWorkSession.Enabled = true;
            panelWorkSession.Show();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            //verify credentials//
            panelLogin.Hide();
            tabControl1.Show();
            panelSelectSession.Enabled = true;
        }

        private void panelSelectSession_EnabledChanged(object sender, EventArgs e)
        {
            if(panelSelectSession.Enabled == true)
            {
                var response = api.GetListSession().Result;
                if (response.ResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var responseObject = response.GetContent();
                }
                else 
                {
                    //deal with http status error
                }
            }
        }
    }
}
