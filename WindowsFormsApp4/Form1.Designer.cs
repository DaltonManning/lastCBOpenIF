using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XMLSearchTool
{
    partial class XMLSearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabBasicSearch;
        private System.Windows.Forms.TabPage tabAdvancedSearch;
        private System.Windows.Forms.ComboBox cboSearchField;
        private System.Windows.Forms.ComboBox cboSearchCondition;
        private System.Windows.Forms.TextBox txtSearchValue;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstCommonValues;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblCommonValues;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.GroupBox grpSearchOptions;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.DataGridView gridResults;

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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBasicSearch = new System.Windows.Forms.TabPage();
            this.grpSearchOptions = new System.Windows.Forms.GroupBox();
            this.lstCommonValues = new System.Windows.Forms.ListBox();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.lblField = new System.Windows.Forms.Label();
            this.cboSearchField = new System.Windows.Forms.ComboBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.cboSearchCondition = new System.Windows.Forms.ComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCommonValues = new System.Windows.Forms.Label();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.tabAdvancedSearch = new System.Windows.Forms.TabPage();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gridResults = new System.Windows.Forms.DataGridView();
            this.tabControl.SuspendLayout();
            this.tabBasicSearch.SuspendLayout();
            this.grpSearchOptions.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabBasicSearch);
            this.tabControl.Controls.Add(this.tabAdvancedSearch);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1024, 746);
            this.tabControl.TabIndex = 1;
            // 
            // tabBasicSearch
            // 
            this.tabBasicSearch.Controls.Add(this.grpSearchOptions);
            this.tabBasicSearch.Controls.Add(this.grpResults);
            this.tabBasicSearch.Location = new System.Drawing.Point(4, 22);
            this.tabBasicSearch.Name = "tabBasicSearch";
            this.tabBasicSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabBasicSearch.Size = new System.Drawing.Size(1016, 720);
            this.tabBasicSearch.TabIndex = 0;
            this.tabBasicSearch.Text = "Simple Search";
            // 
            // grpSearchOptions
            // 
            this.grpSearchOptions.Controls.Add(this.lstCommonValues);
            this.grpSearchOptions.Controls.Add(this.btnLoadXML);
            this.grpSearchOptions.Controls.Add(this.lblField);
            this.grpSearchOptions.Controls.Add(this.cboSearchField);
            this.grpSearchOptions.Controls.Add(this.lblCondition);
            this.grpSearchOptions.Controls.Add(this.cboSearchCondition);
            this.grpSearchOptions.Controls.Add(this.lblValue);
            this.grpSearchOptions.Controls.Add(this.txtSearchValue);
            this.grpSearchOptions.Controls.Add(this.btnSearch);
            this.grpSearchOptions.Controls.Add(this.lblCommonValues);
            this.grpSearchOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSearchOptions.Location = new System.Drawing.Point(3, 3);
            this.grpSearchOptions.Name = "grpSearchOptions";
            this.grpSearchOptions.Size = new System.Drawing.Size(1010, 200);
            this.grpSearchOptions.TabIndex = 0;
            this.grpSearchOptions.TabStop = false;
            this.grpSearchOptions.Text = "Search Options";
            // 
            // lstCommonValues
            // 
            this.lstCommonValues.Location = new System.Drawing.Point(350, 50);
            this.lstCommonValues.Name = "lstCommonValues";
            this.lstCommonValues.Size = new System.Drawing.Size(200, 134);
            this.lstCommonValues.TabIndex = 8;
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Location = new System.Drawing.Point(20, 148);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(100, 23);
            this.btnLoadXML.TabIndex = 0;
            this.btnLoadXML.Text = "Load XML File";
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // lblField
            // 
            this.lblField.Location = new System.Drawing.Point(20, 30);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(100, 23);
            this.lblField.TabIndex = 0;
            this.lblField.Text = "What to Search:";
            // 
            // cboSearchField
            // 
            this.cboSearchField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchField.Location = new System.Drawing.Point(120, 27);
            this.cboSearchField.Name = "cboSearchField";
            this.cboSearchField.Size = new System.Drawing.Size(200, 21);
            this.cboSearchField.TabIndex = 1;
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(20, 60);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(100, 23);
            this.lblCondition.TabIndex = 2;
            this.lblCondition.Text = "How to Match:";
            // 
            // cboSearchCondition
            // 
            this.cboSearchCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchCondition.Location = new System.Drawing.Point(120, 57);
            this.cboSearchCondition.Name = "cboSearchCondition";
            this.cboSearchCondition.Size = new System.Drawing.Size(200, 21);
            this.cboSearchCondition.TabIndex = 3;
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(20, 90);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(100, 23);
            this.lblValue.TabIndex = 4;
            this.lblValue.Text = "Search For:";
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.Location = new System.Drawing.Point(120, 87);
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(200, 20);
            this.txtSearchValue.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(120, 120);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            // 
            // lblCommonValues
            // 
            this.lblCommonValues.Location = new System.Drawing.Point(350, 30);
            this.lblCommonValues.Name = "lblCommonValues";
            this.lblCommonValues.Size = new System.Drawing.Size(100, 17);
            this.lblCommonValues.TabIndex = 7;
            this.lblCommonValues.Text = "Common Values:";
            // 
            // grpResults
            // 
            this.grpResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResults.Location = new System.Drawing.Point(3, 3);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(1010, 714);
            this.grpResults.TabIndex = 1;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Search Results";
            // 
            // tabAdvancedSearch
            // 
            this.tabAdvancedSearch.Location = new System.Drawing.Point(4, 22);
            this.tabAdvancedSearch.Name = "tabAdvancedSearch";
            this.tabAdvancedSearch.Size = new System.Drawing.Size(1016, 720);
            this.tabAdvancedSearch.TabIndex = 1;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 746);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1024, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // gridResults
            // 
            this.gridResults.AllowUserToAddRows = false;
            this.gridResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResults.Location = new System.Drawing.Point(0, 0);
            this.gridResults.Name = "gridResults";
            this.gridResults.ReadOnly = true;
            this.gridResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridResults.Size = new System.Drawing.Size(240, 150);
            this.gridResults.TabIndex = 0;
            // 
            // XMLSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "XMLSearchForm";
            this.Text = "XML Search Tool";
            this.tabControl.ResumeLayout(false);
            this.tabBasicSearch.ResumeLayout(false);
            this.grpSearchOptions.ResumeLayout(false);
            this.grpSearchOptions.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}