namespace WindowsFormsApp3
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.objectBrowser = new System.Windows.Forms.TreeView();
            this.searchGroupbox = new System.Windows.Forms.GroupBox();
            this.searchTextbox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.SearchCasesensitive = new System.Windows.Forms.CheckBox();
            this.resultsGroupbox = new System.Windows.Forms.GroupBox();
            this.resultsListview = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusObjectcount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.searchGroupbox.SuspendLayout();
            this.resultsGroupbox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openXMLToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openXMLToolStripMenuItem
            // 
            this.openXMLToolStripMenuItem.Name = "openXMLToolStripMenuItem";
            this.openXMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openXMLToolStripMenuItem.Text = "Open XML";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.objectBrowser);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resultsGroupbox);
            this.splitContainer1.Panel2.Controls.Add(this.searchGroupbox);
            this.splitContainer1.Size = new System.Drawing.Size(800, 537);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 1;
            // 
            // objectBrowser
            // 
            this.objectBrowser.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.objectBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectBrowser.Location = new System.Drawing.Point(0, 0);
            this.objectBrowser.Name = "objectBrowser";
            this.objectBrowser.Size = new System.Drawing.Size(266, 537);
            this.objectBrowser.TabIndex = 0;
            // 
            // searchGroupbox
            // 
            this.searchGroupbox.Controls.Add(this.SearchCasesensitive);
            this.searchGroupbox.Controls.Add(this.searchButton);
            this.searchGroupbox.Controls.Add(this.searchTextbox);
            this.searchGroupbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchGroupbox.Location = new System.Drawing.Point(0, 0);
            this.searchGroupbox.Name = "searchGroupbox";
            this.searchGroupbox.Size = new System.Drawing.Size(530, 100);
            this.searchGroupbox.TabIndex = 2;
            this.searchGroupbox.TabStop = false;
            this.searchGroupbox.Text = "Search";
            // 
            // searchTextbox
            // 
            this.searchTextbox.Location = new System.Drawing.Point(3, 16);
            this.searchTextbox.Name = "searchTextbox";
            this.searchTextbox.Size = new System.Drawing.Size(100, 20);
            this.searchTextbox.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(138, 28);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            // 
            // SearchCasesensitive
            // 
            this.SearchCasesensitive.AutoSize = true;
            this.SearchCasesensitive.Location = new System.Drawing.Point(7, 53);
            this.SearchCasesensitive.Name = "SearchCasesensitive";
            this.SearchCasesensitive.Size = new System.Drawing.Size(96, 17);
            this.SearchCasesensitive.TabIndex = 2;
            this.SearchCasesensitive.Text = "Case Sensitive";
            this.SearchCasesensitive.UseVisualStyleBackColor = true;
            // 
            // resultsGroupbox
            // 
            this.resultsGroupbox.Controls.Add(this.resultsListview);
            this.resultsGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsGroupbox.Location = new System.Drawing.Point(0, 100);
            this.resultsGroupbox.Name = "resultsGroupbox";
            this.resultsGroupbox.Size = new System.Drawing.Size(530, 437);
            this.resultsGroupbox.TabIndex = 3;
            this.resultsGroupbox.TabStop = false;
            this.resultsGroupbox.Text = "Results";
            // 
            // resultsListview
            // 
            this.resultsListview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsListview.HideSelection = false;
            this.resultsListview.Location = new System.Drawing.Point(3, 16);
            this.resultsListview.Name = "resultsListview";
            this.resultsListview.Size = new System.Drawing.Size(524, 418);
            this.resultsListview.TabIndex = 0;
            this.resultsListview.UseCompatibleStateImageBehavior = false;
            this.resultsListview.View = System.Windows.Forms.View.Details;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusObjectcount,
            this.statusProgressbar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusObjectcount
            // 
            this.statusObjectcount.Name = "statusObjectcount";
            this.statusObjectcount.Size = new System.Drawing.Size(104, 17);
            this.statusObjectcount.Text = "statusObjectcount";
            // 
            // statusProgressbar
            // 
            this.statusProgressbar.Name = "statusProgressbar";
            this.statusProgressbar.Size = new System.Drawing.Size(100, 16);
            this.statusProgressbar.Tag = "statusProgressbar";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "XML Search Tool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.searchGroupbox.ResumeLayout(false);
            this.searchGroupbox.PerformLayout();
            this.resultsGroupbox.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView objectBrowser;
        private System.Windows.Forms.GroupBox searchGroupbox;
        private System.Windows.Forms.CheckBox SearchCasesensitive;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextbox;
        private System.Windows.Forms.GroupBox resultsGroupbox;
        private System.Windows.Forms.ListView resultsListview;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusObjectcount;
        private System.Windows.Forms.ToolStripProgressBar statusProgressbar;
    }
}

