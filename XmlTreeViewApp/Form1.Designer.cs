namespace XmlTreeViewApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TreeView xmlTreeView;
        private System.Windows.Forms.ComboBox attributeComboBox;
        private System.Windows.Forms.CheckBox filterCheckBox;
        private System.Windows.Forms.Button loadXmlButton;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xmlTreeView = new System.Windows.Forms.TreeView();
            this.attributeComboBox = new System.Windows.Forms.ComboBox();
            this.filterCheckBox = new System.Windows.Forms.CheckBox();
            this.loadXmlButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // xmlTreeView
            // 
            this.xmlTreeView.Location = new System.Drawing.Point(12, 12);
            this.xmlTreeView.Name = "xmlTreeView";
            this.xmlTreeView.Size = new System.Drawing.Size(260, 400);
            this.xmlTreeView.TabIndex = 0;

            // 
            // attributeComboBox
            // 
            this.attributeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attributeComboBox.Location = new System.Drawing.Point(280, 12);
            this.attributeComboBox.Name = "attributeComboBox";
            this.attributeComboBox.Size = new System.Drawing.Size(150, 21);
            this.attributeComboBox.TabIndex = 1;

            // 
            // filterCheckBox
            // 
            this.filterCheckBox.AutoSize = true;
            this.filterCheckBox.Location = new System.Drawing.Point(280, 40);
            this.filterCheckBox.Name = "filterCheckBox";
            this.filterCheckBox.Size = new System.Drawing.Size(95, 17);
            this.filterCheckBox.TabIndex = 2;
            this.filterCheckBox.Text = "Filter by Attribute";
            this.filterCheckBox.UseVisualStyleBackColor = true;

            // 
            // loadXmlButton
            // 
            this.loadXmlButton.Location = new System.Drawing.Point(280, 70);
            this.loadXmlButton.Name = "loadXmlButton";
            this.loadXmlButton.Size = new System.Drawing.Size(150, 23);
            this.loadXmlButton.TabIndex = 3;
            this.loadXmlButton.Text = "Load XML";
            this.loadXmlButton.UseVisualStyleBackColor = true;
            this.loadXmlButton.Click += new System.EventHandler(this.loadXmlButton_Click);

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.loadXmlButton);
            this.Controls.Add(this.filterCheckBox);
            this.Controls.Add(this.attributeComboBox);
            this.Controls.Add(this.xmlTreeView);
            this.Name = "Form1";
            this.Text = "XML Tree View App";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
