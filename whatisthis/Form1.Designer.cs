namespace RemoteAnalysis
{
    partial class Form1
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSampleView = new System.Windows.Forms.TabPage();
            this.remoteAnalysisView = new RemoteAnalysis.RemoteAnalysisView();
            this.tabControl.SuspendLayout();
            this.tabPageSampleView.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageSampleView);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageSampleView
            // 
            this.tabPageSampleView.Controls.Add(this.remoteAnalysisView);
            this.tabPageSampleView.Location = new System.Drawing.Point(4, 24);
            this.tabPageSampleView.Name = "tabPageSampleView";
            this.tabPageSampleView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSampleView.Size = new System.Drawing.Size(792, 422);
            this.tabPageSampleView.TabIndex = 0;
            this.tabPageSampleView.Text = "Sample View";
            this.tabPageSampleView.UseVisualStyleBackColor = true;
            // 
            // remoteAnalysisView
            // 
            this.remoteAnalysisView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteAnalysisView.Location = new System.Drawing.Point(3, 3);
            this.remoteAnalysisView.Name = "remoteAnalysisView";
            this.remoteAnalysisView.Size = new System.Drawing.Size(786, 416);
            this.remoteAnalysisView.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "RemoteAnalysis";
            this.tabControl.ResumeLayout(false);
            this.tabPageSampleView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSampleView;
        private RemoteAnalysis.RemoteAnalysisView remoteAnalysisView;
    }
}
