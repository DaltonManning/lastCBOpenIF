namespace CBOpenInterfaceHelperApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtObjectName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnSaveToExcel = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnListApps = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeViewHardware = new System.Windows.Forms.TreeView();
            this.btnGetHardware = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtObjectName);
            this.tabPage1.Controls.Add(this.txtDescription);
            this.tabPage1.Controls.Add(this.btnRead);
            this.tabPage1.Controls.Add(this.btnCreate);
            this.tabPage1.Controls.Add(this.btnModify);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.btnSaveToFile);
            this.tabPage1.Controls.Add(this.btnSaveToExcel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CB Open Interface";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtObjectName
            // 
            this.txtObjectName.Location = new System.Drawing.Point(6, 6);
            this.txtObjectName.Name = "txtObjectName";
            this.txtObjectName.Size = new System.Drawing.Size(200, 20);
            this.txtObjectName.TabIndex = 0;
            this.txtObjectName.Text = "Object Name";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(6, 32);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 20);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.Text = "Description";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(6, 58);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(87, 58);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(168, 58);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 4;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(249, 58);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 87);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 283);
            this.dataGridView1.TabIndex = 6;
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(6, 376);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveToFile.TabIndex = 7;
            this.btnSaveToFile.Text = "Save to File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnSaveToExcel
            // 
            this.btnSaveToExcel.Location = new System.Drawing.Point(87, 376);
            this.btnSaveToExcel.Name = "btnSaveToExcel";
            this.btnSaveToExcel.Size = new System.Drawing.Size(75, 23);
            this.btnSaveToExcel.TabIndex = 8;
            this.btnSaveToExcel.Text = "Save to Excel";
            this.btnSaveToExcel.UseVisualStyleBackColor = true;
            this.btnSaveToExcel.Click += new System.EventHandler(this.btnSaveToExcel_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.treeView1);
            this.tabPage2.Controls.Add(this.btnListApps);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Applications";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(6, 35);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(780, 383);
            this.treeView1.TabIndex = 1;
            // 
            // btnListApps
            // 
            this.btnListApps.Location = new System.Drawing.Point(6, 6);
            this.btnListApps.Name = "btnListApps";
            this.btnListApps.Size = new System.Drawing.Size(75, 23);
            this.btnListApps.TabIndex = 0;
            this.btnListApps.Text = "List Apps";
            this.btnListApps.UseVisualStyleBackColor = true;
            this.btnListApps.Click += new System.EventHandler(this.btnListApps_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.treeViewHardware);
            this.tabPage3.Controls.Add(this.btnGetHardware);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 424);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Hardware";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // treeViewHardware
            // 
            this.treeViewHardware.Location = new System.Drawing.Point(6, 35);
            this.treeViewHardware.Name = "treeViewHardware";
            this.treeViewHardware.Size = new System.Drawing.Size(780, 383);
            this.treeViewHardware.TabIndex = 1;
            // 
            // btnGetHardware
            // 
            this.btnGetHardware.Location = new System.Drawing.Point(6, 6);
            this.btnGetHardware.Name = "btnGetHardware";
            this.btnGetHardware.Size = new System.Drawing.Size(75, 23);
            this.btnGetHardware.TabIndex = 0;
            this.btnGetHardware.Text = "Get Hardware";
            this.btnGetHardware.UseVisualStyleBackColor = true;
            this.btnGetHardware.Click += new System.EventHandler(this.btnGetHardware_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(824, 474);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "CB Open Interface Helper";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtObjectName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnSaveToExcel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnListApps;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView treeViewHardware;
        private System.Windows.Forms.Button btnGetHardware;
    }
}
