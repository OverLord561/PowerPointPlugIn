namespace PowerPointPlugIn
{
    partial class SidePanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.slideListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SearchtTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.elementsTab = new System.Windows.Forms.TabControl();
            this.slidePage = new System.Windows.Forms.TabPage();
            this.presentatinPage = new System.Windows.Forms.TabPage();
            this.pptListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.elementsTab.SuspendLayout();
            this.slidePage.SuspendLayout();
            this.presentatinPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // slideListView
            // 
            this.slideListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.slideListView.ForeColor = System.Drawing.SystemColors.Desktop;
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "Test1";
            this.slideListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.slideListView.Location = new System.Drawing.Point(-4, -6);
            this.slideListView.Name = "slideListView";
            this.slideListView.Size = new System.Drawing.Size(315, 616);
            this.slideListView.TabIndex = 0;
            this.slideListView.UseCompatibleStateImageBehavior = false;
            this.slideListView.Click += new System.EventHandler(this.slideListView_Click);
            this.slideListView.DoubleClick += new System.EventHandler(this.listViewImage_Click);
            // 
            // SearchtTextBox
            // 
            this.SearchtTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchtTextBox.Location = new System.Drawing.Point(3, 40);
            this.SearchtTextBox.Name = "SearchtTextBox";
            this.SearchtTextBox.Size = new System.Drawing.Size(311, 22);
            this.SearchtTextBox.TabIndex = 1;
            this.SearchtTextBox.TextChanged += new System.EventHandler(this.SearchtTextBox_TextChanged);
            this.SearchtTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchtTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search slides:";
            // 
            // elementsTab
            // 
            this.elementsTab.Controls.Add(this.slidePage);
            this.elementsTab.Controls.Add(this.presentatinPage);
            this.elementsTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elementsTab.Location = new System.Drawing.Point(6, 85);
            this.elementsTab.Name = "elementsTab";
            this.elementsTab.SelectedIndex = 0;
            this.elementsTab.Size = new System.Drawing.Size(315, 579);
            this.elementsTab.TabIndex = 3;
            // 
            // slidePage
            // 
            this.slidePage.Controls.Add(this.slideListView);
            this.slidePage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slidePage.Location = new System.Drawing.Point(4, 25);
            this.slidePage.Name = "slidePage";
            this.slidePage.Padding = new System.Windows.Forms.Padding(3);
            this.slidePage.Size = new System.Drawing.Size(307, 550);
            this.slidePage.TabIndex = 0;
            this.slidePage.Text = "Slides";
            this.slidePage.UseVisualStyleBackColor = true;
            // 
            // presentatinPage
            // 
            this.presentatinPage.Controls.Add(this.pptListView);
            this.presentatinPage.Location = new System.Drawing.Point(4, 25);
            this.presentatinPage.Name = "presentatinPage";
            this.presentatinPage.Padding = new System.Windows.Forms.Padding(3);
            this.presentatinPage.Size = new System.Drawing.Size(307, 550);
            this.presentatinPage.TabIndex = 1;
            this.presentatinPage.Text = "Presentations";
            this.presentatinPage.UseVisualStyleBackColor = true;
            // 
            // pptListView
            // 
            this.pptListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.pptListView.ForeColor = System.Drawing.SystemColors.Desktop;
            listViewGroup2.Header = "ListViewGroup";
            listViewGroup2.Name = "Test1";
            this.pptListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            this.pptListView.Location = new System.Drawing.Point(-4, -5);
            this.pptListView.Name = "pptListView";
            this.pptListView.Size = new System.Drawing.Size(315, 616);
            this.pptListView.TabIndex = 1;
            this.pptListView.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Please authorize to see slides";
            // 
            // SidePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.elementsTab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchtTextBox);
            this.Name = "SidePanel";
            this.Size = new System.Drawing.Size(324, 667);
            this.elementsTab.ResumeLayout(false);
            this.slidePage.ResumeLayout(false);
            this.presentatinPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView slideListView;
        private System.Windows.Forms.TextBox SearchtTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabControl elementsTab;
        private System.Windows.Forms.TabPage slidePage;
        private System.Windows.Forms.TabPage presentatinPage;
        private System.Windows.Forms.ListView pptListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label2;
    }
}
