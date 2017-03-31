using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using ManagerLib;

namespace PowerPointPlugIn
{
    public partial class Ribbon
    {
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            Manager.ChangeStatus("Online");
        }
              
        private void LinkButton_Click(object sender, RibbonControlEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com.ua/");
        }

        private void LogInButton_Click(object sender, RibbonControlEventArgs e)
        {
           
            LogInForm logForm = new LogInForm();
            logForm.StartPosition = FormStartPosition.CenterScreen;
            logForm.Show();
        }

        
    }
}
