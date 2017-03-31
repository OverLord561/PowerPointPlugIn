using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ManagerLib;
using Microsoft.Office.Interop.PowerPoint;
using System.Threading;

namespace PowerPointPlugIn
{
    public partial class SidePanel : UserControl
    {
        List<string> listOfImages;
        public IEnumerable<Microsoft.Office.Interop.PowerPoint.Shape> shapes;

        public SidePanel(int height, bool shouldDisplay)
        {
            InitializeComponent();
            listOfImages = new List<string>();
            shapes = new List<Microsoft.Office.Interop.PowerPoint.Shape>();

            //set maximum size for tab panel and listview
            slideListView.Height = height;
            elementsTab.Height = height;
            pptListView.Height = height;

            slideListView.Width = elementsTab.Width;
            pptListView.Width = elementsTab.Width;

            AddSearchIcon();

            ListViewGroup slides_group = new ListViewGroup("Slides");
            ListViewGroup ppt_group = new ListViewGroup("Presentations");

            slideListView.Groups.Add(slides_group);
            pptListView.Groups.Add(ppt_group);


            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(256, 180); // it is possible to set 230px, but 180 is optimal

            String[] paths = { };
            paths = Directory.GetFiles("D:\\PP\\Images");
            foreach (String path in paths)
            {
                listOfImages.Add(Path.GetFileName(path));
                imgs.Images.Add(Image.FromFile(path));
            }
            slideListView.LargeImageList = imgs;
            pptListView.LargeImageList = imgs;

            int count = 0;
            if (shouldDisplay)
            {
                foreach (var image in imgs.Images)
                {
                    slideListView.Items.Add(new ListViewItem(new string[] { listOfImages[count] }, count, slides_group));
                    count++;
                }
                int count1 = 0;
                foreach (var image in imgs.Images)
                {
                    pptListView.Items.Add(new ListViewItem(new string[] { listOfImages[count1] }, count1, ppt_group));
                    count1++;
                }
            }
            


        }
        private void listViewImage_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.PowerPoint.Presentation currentPresentation = Globals.ThisAddIn.Application.ActivePresentation;
            int index = Globals.ThisAddIn.Application.ActiveWindow.View.Slide.SlideIndex;


            Manager.CopyShape(shapes, currentPresentation, index);

            Globals.ThisAddIn.Application.ActiveWindow.View.GotoSlide(index + 1);

        }





        //private void listView1_MouseDown(object sender, MouseEventArgs e)
        //{

        //    Microsoft.Office.Interop.PowerPoint.Presentation currentPresentation = Globals.ThisAddIn.Application.ActivePresentation;
        //    int index = Globals.ThisAddIn.Application.ActiveWindow.View.Slide.SlideIndex;
        //    Manager.CopyShape(shapes, currentPresentation, index);


        //    List<Microsoft.Office.Interop.PowerPoint.Shape> sh = shapes.ToList();

        //    RichTextBox rc = new RichTextBox();
        //    rc.Text = sh[0].ToString();

        //    DataObject data = new DataObject();
        //    data.SetData(DataFormats.Serializable, sh[0]);

        //    (sender as ListView).DoDragDrop(data, DragDropEffects.Copy);
        //}

        private void SearchtTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {




        }

        //create custom searchbox
        private void AddSearchIcon()
        {
            var searchIcon = new PictureBox();
            CreateCustomControl(searchIcon, "search");
        }
        private void RemoveSearchIcon()
        {
            var searchIcon = new Button();
            CreateCustomControl(searchIcon, "delete");
        }

        private void CreateCustomControl(Control searchIcon, string icon)
        {
            foreach (Control control in SearchtTextBox.Controls)
            {
                SearchtTextBox.Controls.Remove(control);
            }

            searchIcon.Size = new Size(24, SearchtTextBox.ClientSize.Height + 2);
            searchIcon.Location = new System.Drawing.Point(SearchtTextBox.ClientSize.Width - searchIcon.Width, -1);
            searchIcon.Cursor = Cursors.Default;

            switch (icon)
            {
                case "delete":
                    (searchIcon as Button).Image = Properties.Resources.delete;
                    (searchIcon as Button).Click += new System.EventHandler(ClearSearchTextBox);
                    break;
                case "search":
                    (searchIcon as PictureBox).Image = Properties.Resources.search;
                    break;
                default: break;
            }

            SearchtTextBox.Controls.Add(searchIcon);
        }

        private void ClearSearchTextBox(object sender, EventArgs e)
        {
            SearchtTextBox.Text = "";
        }

        private void SearchtTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = SearchtTextBox.Text;
            // if (text.Contains("\r\n")) text =  text.Replace("\r\n", ""); // enter was pressed

            if (text == "")
            {
                RemoveSearchIcon();
                AddSearchIcon();

            }
            else
            {
                RemoveSearchIcon();
            }
        }

        private void slideListView_Click(object sender, EventArgs e)
        {

        }
    }
}
