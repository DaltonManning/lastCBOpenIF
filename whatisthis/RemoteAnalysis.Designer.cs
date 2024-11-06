namespace RemoteAnalysis
{
    partial class RemoteAnalysisView
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
            this.btnCreateObjects = new System.Windows.Forms.Button();
            this.btnAddVariable = new System.Windows.Forms.Button();
            this.btnCopyFBType = new System.Windows.Forms.Button();
            this.btnListLibs = new System.Windows.Forms.Button();
            this.btnListDatatypes = new System.Windows.Forms.Button();
            this.btnFindDatatype = new System.Windows.Forms.Button();
            this.btnGetHwUnit = new System.Windows.Forms.Button();
            this.btnSetHwUnit = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxSignalRange = new System.Windows.Forms.TextBox();
            this.textBoxHWUnit = new System.Windows.Forms.TextBox();
            this.textBoxHWConn = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCreateObjects
            // 
            this.btnCreateObjects.Location = new System.Drawing.Point(10, 10);
            this.btnCreateObjects.Name = "btnCreateObjects";
            this.btnCreateObjects.Size = new System.Drawing.Size(100, 23);
            this.btnCreateObjects.TabIndex = 0;
            this.btnCreateObjects.Text = "Create Objects";
            this.btnCreateObjects.UseVisualStyleBackColor = true;
            this.btnCreateObjects.Click += new System.EventHandler(this.OnCreateObjects_Click);
            // 
            // btnAddVariable
            // 
            this.btnAddVariable.Location = new System.Drawing.Point(10, 50);
            this.btnAddVariable.Name = "btnAddVariable";
            this.btnAddVariable.Size = new System.Drawing.Size(100, 23);
            this.btnAddVariable.TabIndex = 1;
            this.btnAddVariable.Text = "Add Variable";
            this.btnAddVariable.UseVisualStyleBackColor = true;
            this.btnAddVariable.Click += new System.EventHandler(this.OnAddVariable_Click);
            // 
            // btnCopyFBType
            // 
            this.btnCopyFBType.Location = new System.Drawing.Point(10, 90);
            this.btnCopyFBType.Name = "btnCopyFBType";
            this.btnCopyFBType.Size = new System.Drawing.Size(100, 23);
            this.btnCopyFBType.TabIndex = 2;
            this.btnCopyFBType.Text = "Copy FBType";
            this.btnCopyFBType.UseVisualStyleBackColor = true;
            this.btnCopyFBType.Click += new System.EventHandler(this.OnCopyFBType_Click);
            // 
            // btnListLibs
            // 
            this.btnListLibs.Location = new System.Drawing.Point(10, 130);
            this.btnListLibs.Name = "btnListLibs";
            this.btnListLibs.Size = new System.Drawing.Size(100, 23);
            this.btnListLibs.TabIndex = 3;
            this.btnListLibs.Text = "List Libraries";
            this.btnListLibs.UseVisualStyleBackColor = true;
            this.btnListLibs.Click += new System.EventHandler(this.OnListLibs_Click);
            // 
            // btnListDatatypes
            // 
            this.btnListDatatypes.Location = new System.Drawing.Point(10, 170);
            this.btnListDatatypes.Name = "btnListDatatypes";
            this.btnListDatatypes.Size = new System.Drawing.Size(100, 23);
            this.btnListDatatypes.TabIndex = 4;
            this.btnListDatatypes.Text = "List Datatypes";
            this.btnListDatatypes.UseVisualStyleBackColor = true;
            this.btnListDatatypes.Click += new System.EventHandler(this.OnListDatatypes_Click);
            // 
            // btnFindDatatype
            // 
            this.btnFindDatatype.Location = new System.Drawing.Point(10, 210);
            this.btnFindDatatype.Name = "btnFindDatatype";
            this.btnFindDatatype.Size = new System.Drawing.Size(100, 23);
            this.btnFindDatatype.TabIndex = 5;
            this.btnFindDatatype.Text = "Find Datatype";
            this.btnFindDatatype.UseVisualStyleBackColor = true;
            this.btnFindDatatype.Click += new System.EventHandler(this.OnFindDatatype_Click);
            // 
            // btnGetHwUnit
            // 
            this.btnGetHwUnit.Location = new System.Drawing.Point(10, 250);
            this.btnGetHwUnit.Name = "btnGetHwUnit";
            this.btnGetHwUnit.Size = new System.Drawing.Size(100, 23);
            this.btnGetHwUnit.TabIndex = 6;
            this.btnGetHwUnit.Text = "Get HW Unit";
            this.btnGetHwUnit.UseVisualStyleBackColor = true;
            this.btnGetHwUnit.Click += new System.EventHandler(this.OnGetHwUnit_Click);
            // 
            // btnSetHwUnit
            // 
            this.btnSetHwUnit.Location = new System.Drawing.Point(10, 290);
            this.btnSetHwUnit.Name = "btnSetHwUnit";
            this.btnSetHwUnit.Size = new System.Drawing.Size(100, 23);
            this.btnSetHwUnit.TabIndex = 7;
            this.btnSetHwUnit.Text = "Set HW Unit";
            this.btnSetHwUnit.UseVisualStyleBackColor = true;
            this.btnSetHwUnit.Click += new System.EventHandler(this.OnSetHwUnit_Click);
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(150, 10);
            this.listBox1.Size = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.listBox1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(470, 10);
            this.textBox1.Size = new System.Drawing.Size(200, 23);
            this.Controls.Add(this.textBox1);
            // 
            // textBoxSignalRange
            // 
            this.textBoxSignalRange.Location = new System.Drawing.Point(470, 50);
            this.textBoxSignalRange.Size = new System.Drawing.Size(200, 23);
            this.Controls.Add(this.textBoxSignalRange);
            // 
            // textBoxHWUnit
            // 
            this.textBoxHWUnit.Location = new System.Drawing.Point(470, 90);
            this.textBoxHWUnit.Size = new System.Drawing.Size(200, 23);
            this.Controls.Add(this.textBoxHWUnit);
            // 
            // textBoxHWConn
            // 
            this.textBoxHWConn.Location = new System.Drawing.Point(470, 130);
            this.textBoxHWConn.Size = new System.Drawing.Size(200, 23);
            this.Controls.Add(this.textBoxHWConn);
            // 
            // RemoteAnalysisView
            // 
            this.Name = "RemoteAnalysisView";
            this.Size = new System.Drawing.Size(800, 450);
        }
    }
}
