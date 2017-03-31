using ManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PowerPointPlugIn
{
   

    public partial class LogInForm : Form
    {
        public delegate void DelCloser();
        DelCloser delCloser;
        public delegate void DelError();
        DelError delError;
      

        // private static readonly HttpClient client = new HttpClient();
        public LogInForm()
        {
            InitializeComponent();
            delCloser = new DelCloser(CloseForm);
            delError = new DelError(ShowError); 

        }
       

        private void ShowError()
        {
            ErrorLabel.Text = "Invalid Email Address or Password entered!";
        }

        public void CloseForm()
        {
            
            this.Close();
            var addIn =  Globals.ThisAddIn;           
            addIn.OpenTaskPane(true,true);
         
        }
        private async void SendButton_Click(object sender, EventArgs e)
        {

            string LogIn = LogInTextBox.Text;
            string Password = PasswordTextBox.Text;

            if (LogIn == "" || Password == "")
            {
                MessageBox.Show("All fields are required!");
                return;
            }

            //LogIn = "obiivan@mail.ru";
            //Password = "89Zxcv";
            var data = new Dictionary<string, string>
            {
                {"ihcaction" , "login"},
                {"log" , LogIn },
                {"pwd" , Password }
            };

            bool res = await Manager.CheckIfUserExists(data, "http://site2max.pro/iump-login/");


            if (!res)
            {
                this.BeginInvoke(delError);
            }
            else
            {
                if (InvokeRequired)
                {

                    Manager.SetDataToRegistry("LogIn", LogIn);
                    Manager.SetDataToRegistry("Password", Password);
                    Manager.SetDataToRegistry("LastRun", DateTime.Now.ToString());
                    Manager.SetDataToRegistry("InitialRun", DateTime.Now.ToString());
               
                    this.BeginInvoke(delCloser);
                }
                else
                {
                    this.Close();
                }
            }
            
        }

        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendButton_Click(sender, (e as EventArgs));
            }

        }
    }
}
