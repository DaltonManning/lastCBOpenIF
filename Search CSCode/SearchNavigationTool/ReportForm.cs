using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace SearchNavigationTool;

public class ReportForm : Form
{
	private Container components;

	private Button createReportBtn;

	private Label label1;

	private ComboBox reportComboBox;

	private CheckBox openInEditorCB;

	private SaveFileDialog saveFileDialog1;

	private XmlDocument xmlDoc;

	private string[] reports;

	private LocalStringClass resourceStrings;

	private Button CloseBtn;

	private SearchNavigationForm parentDialog;

	public ReportForm(XmlDocument xmlDoc, SearchNavigationForm parentDialog)
	{
		if (xmlDoc == null)
		{
			throw new ArgumentNullException("xmlDoc");
		}
		if (parentDialog == null)
		{
			throw new ArgumentNullException("parentDialog");
		}
		InitializeComponent();
		this.xmlDoc = xmlDoc;
		this.parentDialog = parentDialog;
		string text = parentDialog.GetRegistryValueWrapper("WorkingFolder");
		string text2 = parentDialog.GetRegistryValueWrapper("ReportTemplates");
		if (!text.EndsWith("\\", StringComparison.Ordinal) && !text.EndsWith("/", StringComparison.Ordinal))
		{
			text += "\\";
		}
		string path = text + "ReportTemplates\\SearchNavigation";
		if (!text2.EndsWith("\\", StringComparison.Ordinal) && !text2.EndsWith("/", StringComparison.Ordinal))
		{
			text2 += "\\";
		}
		text2 += "SearchNavigation";
		reportComboBox.Items.Clear();
		string[] array = null;
		try
		{
			array = Directory.GetFiles(text2, "*.xsl*");
		}
		catch (Exception)
		{
		}
		string[] array2 = null;
		try
		{
			array2 = Directory.GetFiles(path, "*.xsl*");
		}
		catch (Exception)
		{
		}
		int num = 0;
		if (array != null)
		{
			num = array.Length;
		}
		int num2 = 0;
		if (array2 != null)
		{
			num2 = array2.Length;
		}
		if (num + num2 == 0)
		{
			createReportBtn.Enabled = false;
			return;
		}
		reports = new string[num + num2];
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				FileInfo fileInfo = new FileInfo(array[i]);
				reportComboBox.Items.Add(fileInfo.Name);
				reports[i] = array[i];
			}
		}
		if (array2 != null)
		{
			for (int j = 0; j < array2.Length; j++)
			{
				FileInfo fileInfo2 = new FileInfo(array2[j]);
				reportComboBox.Items.Add(fileInfo2.Name);
				reports[num + j] = array2[j];
			}
		}
		reportComboBox.SelectedIndex = 0;
		if (xmlDoc == null || xmlDoc.DocumentElement == null)
		{
			createReportBtn.Enabled = false;
		}
		else
		{
			createReportBtn.Enabled = true;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(SearchNavigationTool.ReportForm));
		this.createReportBtn = new System.Windows.Forms.Button();
		this.reportComboBox = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.openInEditorCB = new System.Windows.Forms.CheckBox();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.CloseBtn = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.createReportBtn.AccessibleDescription = resourceManager.GetString("createReportBtn.AccessibleDescription");
		this.createReportBtn.AccessibleName = resourceManager.GetString("createReportBtn.AccessibleName");
		this.createReportBtn.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("createReportBtn.Anchor");
		this.createReportBtn.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("createReportBtn.BackgroundImage");
		this.createReportBtn.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("createReportBtn.Dock");
		this.createReportBtn.Enabled = (bool)resourceManager.GetObject("createReportBtn.Enabled");
		this.createReportBtn.FlatStyle = (System.Windows.Forms.FlatStyle)resourceManager.GetObject("createReportBtn.FlatStyle");
		this.createReportBtn.Font = (System.Drawing.Font)resourceManager.GetObject("createReportBtn.Font");
		this.createReportBtn.Image = (System.Drawing.Image)resourceManager.GetObject("createReportBtn.Image");
		this.createReportBtn.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("createReportBtn.ImageAlign");
		this.createReportBtn.ImageIndex = (int)resourceManager.GetObject("createReportBtn.ImageIndex");
		this.createReportBtn.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("createReportBtn.ImeMode");
		this.createReportBtn.Location = (System.Drawing.Point)resourceManager.GetObject("createReportBtn.Location");
		this.createReportBtn.Name = "createReportBtn";
		this.createReportBtn.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("createReportBtn.RightToLeft");
		this.createReportBtn.Size = (System.Drawing.Size)resourceManager.GetObject("createReportBtn.Size");
		this.createReportBtn.TabIndex = (int)resourceManager.GetObject("createReportBtn.TabIndex");
		this.createReportBtn.Text = resourceManager.GetString("createReportBtn.Text");
		this.createReportBtn.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("createReportBtn.TextAlign");
		this.createReportBtn.Visible = (bool)resourceManager.GetObject("createReportBtn.Visible");
		this.createReportBtn.Click += new System.EventHandler(createReportBtn_Click);
		this.reportComboBox.AccessibleDescription = resourceManager.GetString("reportComboBox.AccessibleDescription");
		this.reportComboBox.AccessibleName = resourceManager.GetString("reportComboBox.AccessibleName");
		this.reportComboBox.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("reportComboBox.Anchor");
		this.reportComboBox.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("reportComboBox.BackgroundImage");
		this.reportComboBox.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("reportComboBox.Dock");
		this.reportComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.reportComboBox.Enabled = (bool)resourceManager.GetObject("reportComboBox.Enabled");
		this.reportComboBox.Font = (System.Drawing.Font)resourceManager.GetObject("reportComboBox.Font");
		this.reportComboBox.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("reportComboBox.ImeMode");
		this.reportComboBox.IntegralHeight = (bool)resourceManager.GetObject("reportComboBox.IntegralHeight");
		this.reportComboBox.ItemHeight = (int)resourceManager.GetObject("reportComboBox.ItemHeight");
		this.reportComboBox.Items.AddRange(new object[1] { resourceManager.GetString("reportComboBox.Items") });
		this.reportComboBox.Location = (System.Drawing.Point)resourceManager.GetObject("reportComboBox.Location");
		this.reportComboBox.MaxDropDownItems = (int)resourceManager.GetObject("reportComboBox.MaxDropDownItems");
		this.reportComboBox.MaxLength = (int)resourceManager.GetObject("reportComboBox.MaxLength");
		this.reportComboBox.Name = "reportComboBox";
		this.reportComboBox.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("reportComboBox.RightToLeft");
		this.reportComboBox.Size = (System.Drawing.Size)resourceManager.GetObject("reportComboBox.Size");
		this.reportComboBox.TabIndex = (int)resourceManager.GetObject("reportComboBox.TabIndex");
		this.reportComboBox.Text = resourceManager.GetString("reportComboBox.Text");
		this.reportComboBox.Visible = (bool)resourceManager.GetObject("reportComboBox.Visible");
		this.label1.AccessibleDescription = resourceManager.GetString("label1.AccessibleDescription");
		this.label1.AccessibleName = resourceManager.GetString("label1.AccessibleName");
		this.label1.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("label1.Anchor");
		this.label1.AutoSize = (bool)resourceManager.GetObject("label1.AutoSize");
		this.label1.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("label1.Dock");
		this.label1.Enabled = (bool)resourceManager.GetObject("label1.Enabled");
		this.label1.Font = (System.Drawing.Font)resourceManager.GetObject("label1.Font");
		this.label1.Image = (System.Drawing.Image)resourceManager.GetObject("label1.Image");
		this.label1.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("label1.ImageAlign");
		this.label1.ImageIndex = (int)resourceManager.GetObject("label1.ImageIndex");
		this.label1.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("label1.ImeMode");
		this.label1.Location = (System.Drawing.Point)resourceManager.GetObject("label1.Location");
		this.label1.Name = "label1";
		this.label1.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("label1.RightToLeft");
		this.label1.Size = (System.Drawing.Size)resourceManager.GetObject("label1.Size");
		this.label1.TabIndex = (int)resourceManager.GetObject("label1.TabIndex");
		this.label1.Text = resourceManager.GetString("label1.Text");
		this.label1.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("label1.TextAlign");
		this.label1.Visible = (bool)resourceManager.GetObject("label1.Visible");
		this.openInEditorCB.AccessibleDescription = resourceManager.GetString("openInEditorCB.AccessibleDescription");
		this.openInEditorCB.AccessibleName = resourceManager.GetString("openInEditorCB.AccessibleName");
		this.openInEditorCB.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("openInEditorCB.Anchor");
		this.openInEditorCB.Appearance = (System.Windows.Forms.Appearance)resourceManager.GetObject("openInEditorCB.Appearance");
		this.openInEditorCB.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("openInEditorCB.BackgroundImage");
		this.openInEditorCB.CheckAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("openInEditorCB.CheckAlign");
		this.openInEditorCB.Checked = true;
		this.openInEditorCB.CheckState = System.Windows.Forms.CheckState.Checked;
		this.openInEditorCB.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("openInEditorCB.Dock");
		this.openInEditorCB.Enabled = (bool)resourceManager.GetObject("openInEditorCB.Enabled");
		this.openInEditorCB.FlatStyle = (System.Windows.Forms.FlatStyle)resourceManager.GetObject("openInEditorCB.FlatStyle");
		this.openInEditorCB.Font = (System.Drawing.Font)resourceManager.GetObject("openInEditorCB.Font");
		this.openInEditorCB.Image = (System.Drawing.Image)resourceManager.GetObject("openInEditorCB.Image");
		this.openInEditorCB.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("openInEditorCB.ImageAlign");
		this.openInEditorCB.ImageIndex = (int)resourceManager.GetObject("openInEditorCB.ImageIndex");
		this.openInEditorCB.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("openInEditorCB.ImeMode");
		this.openInEditorCB.Location = (System.Drawing.Point)resourceManager.GetObject("openInEditorCB.Location");
		this.openInEditorCB.Name = "openInEditorCB";
		this.openInEditorCB.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("openInEditorCB.RightToLeft");
		this.openInEditorCB.Size = (System.Drawing.Size)resourceManager.GetObject("openInEditorCB.Size");
		this.openInEditorCB.TabIndex = (int)resourceManager.GetObject("openInEditorCB.TabIndex");
		this.openInEditorCB.Text = resourceManager.GetString("openInEditorCB.Text");
		this.openInEditorCB.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("openInEditorCB.TextAlign");
		this.openInEditorCB.Visible = (bool)resourceManager.GetObject("openInEditorCB.Visible");
		this.saveFileDialog1.FileName = "sn-report.html";
		this.saveFileDialog1.Filter = resourceManager.GetString("saveFileDialog1.Filter");
		this.saveFileDialog1.Title = resourceManager.GetString("saveFileDialog1.Title");
		this.CloseBtn.AccessibleDescription = resourceManager.GetString("CloseBtn.AccessibleDescription");
		this.CloseBtn.AccessibleName = resourceManager.GetString("CloseBtn.AccessibleName");
		this.CloseBtn.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("CloseBtn.Anchor");
		this.CloseBtn.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("CloseBtn.BackgroundImage");
		this.CloseBtn.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("CloseBtn.Dock");
		this.CloseBtn.Enabled = (bool)resourceManager.GetObject("CloseBtn.Enabled");
		this.CloseBtn.FlatStyle = (System.Windows.Forms.FlatStyle)resourceManager.GetObject("CloseBtn.FlatStyle");
		this.CloseBtn.Font = (System.Drawing.Font)resourceManager.GetObject("CloseBtn.Font");
		this.CloseBtn.Image = (System.Drawing.Image)resourceManager.GetObject("CloseBtn.Image");
		this.CloseBtn.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("CloseBtn.ImageAlign");
		this.CloseBtn.ImageIndex = (int)resourceManager.GetObject("CloseBtn.ImageIndex");
		this.CloseBtn.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("CloseBtn.ImeMode");
		this.CloseBtn.Location = (System.Drawing.Point)resourceManager.GetObject("CloseBtn.Location");
		this.CloseBtn.Name = "CloseBtn";
		this.CloseBtn.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("CloseBtn.RightToLeft");
		this.CloseBtn.Size = (System.Drawing.Size)resourceManager.GetObject("CloseBtn.Size");
		this.CloseBtn.TabIndex = (int)resourceManager.GetObject("CloseBtn.TabIndex");
		this.CloseBtn.Text = resourceManager.GetString("CloseBtn.Text");
		this.CloseBtn.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("CloseBtn.TextAlign");
		this.CloseBtn.Visible = (bool)resourceManager.GetObject("CloseBtn.Visible");
		this.CloseBtn.Click += new System.EventHandler(CloseBtn_Click);
		base.AccessibleDescription = resourceManager.GetString("$this.AccessibleDescription");
		base.AccessibleName = resourceManager.GetString("$this.AccessibleName");
		this.AutoScaleBaseSize = (System.Drawing.Size)resourceManager.GetObject("$this.AutoScaleBaseSize");
		this.AutoScroll = (bool)resourceManager.GetObject("$this.AutoScroll");
		base.AutoScrollMargin = (System.Drawing.Size)resourceManager.GetObject("$this.AutoScrollMargin");
		base.AutoScrollMinSize = (System.Drawing.Size)resourceManager.GetObject("$this.AutoScrollMinSize");
		this.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("$this.BackgroundImage");
		base.ClientSize = (System.Drawing.Size)resourceManager.GetObject("$this.ClientSize");
		base.Controls.Add(this.CloseBtn);
		base.Controls.Add(this.openInEditorCB);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.reportComboBox);
		base.Controls.Add(this.createReportBtn);
		base.Enabled = (bool)resourceManager.GetObject("$this.Enabled");
		this.Font = (System.Drawing.Font)resourceManager.GetObject("$this.Font");
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
		base.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("$this.ImeMode");
		base.Location = (System.Drawing.Point)resourceManager.GetObject("$this.Location");
		base.MaximizeBox = false;
		this.MaximumSize = (System.Drawing.Size)resourceManager.GetObject("$this.MaximumSize");
		base.MinimizeBox = false;
		this.MinimumSize = (System.Drawing.Size)resourceManager.GetObject("$this.MinimumSize");
		base.Name = "ReportForm";
		this.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("$this.RightToLeft");
		base.StartPosition = (System.Windows.Forms.FormStartPosition)resourceManager.GetObject("$this.StartPosition");
		this.Text = resourceManager.GetString("$this.Text");
		base.ResumeLayout(false);
	}

	private void createReportBtn_Click(object sender, EventArgs e)
	{
		if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
		{
			return;
		}
		Refresh();
		Cursor = Cursors.WaitCursor;
		XmlElement documentElement = xmlDoc.DocumentElement;
		DateTime now = DateTime.Now;
		XmlAttribute xmlAttribute = documentElement.Attributes["Date"];
		if (xmlAttribute == null)
		{
			xmlAttribute = xmlDoc.CreateAttribute("Date");
			documentElement.Attributes.Append(xmlAttribute);
		}
		xmlAttribute.Value = now.ToShortDateString();
		XmlAttribute xmlAttribute2 = documentElement.Attributes["Time"];
		if (xmlAttribute2 == null)
		{
			xmlAttribute2 = xmlDoc.CreateAttribute("Time");
			documentElement.Attributes.Append(xmlAttribute2);
		}
		xmlAttribute2.Value = now.ToShortTimeString();
		XmlAttribute xmlAttribute3 = documentElement.Attributes["Filter"];
		if (xmlAttribute3 == null)
		{
			xmlAttribute3 = xmlDoc.CreateAttribute("Filter");
			documentElement.Attributes.Append(xmlAttribute3);
		}
		xmlAttribute3.Value = parentDialog.GetFilter;
		XPathNavigator input = documentElement.CreateNavigator();
		XmlUrlResolver xmlUrlResolver = new XmlUrlResolver();
		xmlUrlResolver.Credentials = CredentialCache.DefaultCredentials;
		XsltSettings settings = new XsltSettings(enableDocumentFunction: true, enableScript: true);
		try
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(reports[reportComboBox.SelectedIndex], settings, xmlUrlResolver);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(saveFileDialog1.FileName, null);
			xslCompiledTransform.Transform(input, null, xmlTextWriter);
			xmlTextWriter.Close();
			if (openInEditorCB.Checked)
			{
				try
				{
					Process.Start(saveFileDialog1.FileName);
				}
				catch (Exception)
				{
					if (parentDialog != null)
					{
						resourceStrings = parentDialog.Strings;
						parentDialog.ErrorMsgBox(resourceStrings.ReportMsgBoxText);
					}
				}
			}
		}
		catch (Exception)
		{
			parentDialog.ErrorMsgBox("Invalid Transformation");
		}
		Cursor = Cursors.Default;
	}

	private void CloseBtn_Click(object sender, EventArgs e)
	{
		Close();
	}
}
