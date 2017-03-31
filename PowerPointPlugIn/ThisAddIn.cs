using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using ManagerLib;
using System.Windows.Forms;
using System.Threading;
using PowerPointPlugIn;

namespace PowerPointPlugIn
{
    public partial class ThisAddIn
    {
    
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // If 20 days left, show Log In form
            if (Manager.CheckIfRefreshData())
            {

                LogInForm logForm = new LogInForm();
                logForm.StartPosition = FormStartPosition.CenterScreen;
                logForm.Show();

                OpenTaskPane(false,false);
            }
            else
            {               
                OpenTaskPane(true,false);
            }

        }

        public void OpenTaskPane(bool show, bool emptySidePanelIsShowed)
        {

            
            if (show)
            {
                if (emptySidePanelIsShowed)
                {
                    this.CustomTaskPanes.Remove(myCustomTaskPane);
                }
                userControl = new SidePanel(900, true);
            }
            else
            {
                userControl = new SidePanel(900, false);
                
            }
                var width = userControl.Width;
                myCustomTaskPane = this.CustomTaskPanes.Add(userControl, "Slides");
                myCustomTaskPane.Visible = true;
                myCustomTaskPane.Width = width;


            
            Manager.ChangeStatus("Online");
            Manager.SetDataToRegistry("LastRun", DateTime.Now.ToString());
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {

            Manager.ChangeStatus("Offline");
        }



        //added this part for custom user controll
        private  SidePanel userControl;
        private  Microsoft.Office.Tools.CustomTaskPane myCustomTaskPane;


        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
