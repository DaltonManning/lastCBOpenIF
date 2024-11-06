using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CONTROLBUILDERLib;

namespace SearchNavigationTool;

[ComVisible(false)]
public class SearchNavigationForm : Form
{
	[ComVisible(false)]
	internal class QueryEventArgs : EventArgs
	{
		public string symbol;

		public string path;

		public SNQueryMatchType match;

		public int maxNoHits;

		public CBModeType cbMode;

		public QueryEventArgs(string path, string symbol, SNQueryMatchType match, int maxNoHits, CBModeType cbMode)
		{
			this.path = path;
			this.symbol = symbol;
			this.match = match;
			this.maxNoHits = maxNoHits;
			this.cbMode = cbMode;
		}
	}

	[ComVisible(false)]
	internal class GoToPOUEditorEventArgs : EventArgs
	{
		public string path;

		public string specification;

		public string tab;

		public string row;

		public string position;

		public string element;

		public string guiType;

		public GoToPOUEditorEventArgs(string path, string specification, string tab, string row, string position, string element, string guiType)
		{
			this.path = path;
			this.specification = specification;
			this.tab = tab;
			this.row = row;
			this.position = position;
			this.element = element;
			this.guiType = guiType;
		}
	}

	[ComVisible(false)]
	internal class GetRegistryValueEventArgs : EventArgs
	{
		public string keyName;

		public GetRegistryValueEventArgs(string keyName)
		{
			this.keyName = keyName;
		}
	}

	[ComVisible(false)]
	internal class StoreComboBoxListsAndFilterInComFacadeEventArgs : EventArgs
	{
		public string[] searchInStrings;

		public string[] searchForStrings;

		public StoreComboBoxListsAndFilterInComFacadeEventArgs(ref string[] searchInStrings, ref string[] searchForStrings)
		{
			this.searchInStrings = new string[21];
			this.searchForStrings = new string[21];
			searchInStrings.CopyTo(this.searchInStrings, 0);
			searchForStrings.CopyTo(this.searchForStrings, 0);
		}
	}

	[ComVisible(false)]
	internal delegate void RebuildScbDBEventHandler(object sender);

	[ComVisible(false)]
	internal delegate string QueryEventHandler(object sender, QueryEventArgs e);

	[ComVisible(false)]
	internal delegate void CloseSNFormEventHandler();

	[ComVisible(false)]
	internal delegate void GoToPOUEditorEventHandler(object sender, GoToPOUEditorEventArgs e);

	[ComVisible(false)]
	internal delegate void GoToProjectExplorerEventHandler(object sender, GoToPOUEditorEventArgs e);

	[ComVisible(false)]
	internal delegate string GetRegistryValueEventHandler(object sender, GetRegistryValueEventArgs e);

	[ComVisible(false)]
	internal delegate bool ExecuteSearchInstantlyEventHandler();

	[ComVisible(false)]
	internal delegate void OpenHelpEventHandler();

	internal delegate void StoreComboBoxListsAndFilterInComFacadeEventHandler(object sender, StoreComboBoxListsAndFilterInComFacadeEventArgs e);

	private bool closingBool;

	private string queryResultString;

	internal string queryDSResultString;

	internal string queryDSSymbolString;

	private bool startDebugMode;

	private bool startExtendedMode;

	private bool startExtendedMode2;

	private bool ignoreKeyClickBool;

	internal LocalStringClass Strings;

	private static CBModeType CBMode;

	private static CBModeType newCBMode;

	private StringCollection tmpStringColl;

	private Label searchForLabel;

	private Label searchInLabel;

	private ComboBox searchForComboBox;

	private ComboBox searchInComboBox;

	private Button closeButton;

	private Button helpButton;

	private StatusBar SNStatusBar;

	private ViewCtrl SNViewCtrl;

	private BrowserDataClass browser;

	private Button rebuildButton;

	private Button searchButton;

	private GroupBox searchOptGroupBox;

	private Label maxHitsLabel;

	private TextBox maxHitsTextBox;

	private CheckBox alwaysOnTopCheckBox;

	internal RichTextBox debugRichTextBox;

	private RadioButton matchPrefixRadioButton;

	private RadioButton matchWholeWordRadioButton;

	private Button debugModeButton;

	private RadioButton matchSubStringRadioButton;

	private GroupBox searchGroupBox;

	private GroupBox resultFilterGroupBox;

	private Label filterByLabel;

	private StatusBarPanel pathStatusBarPanel;

	private StatusBarPanel maxHitsStatusBarPanel;

	private ComboBox resultFilterComboBox;

	private bool iterativeSearch;

	public string GetSearchForString
	{
		get
		{
			if (searchForComboBox.Text != "*" && searchForComboBox.Text.Length > 0)
			{
				return searchForComboBox.Text;
			}
			return SNViewCtrl.SelectedSymbolString;
		}
	}

	public string GetFilter => resultFilterComboBox.Text;

	internal XmlDocument XmlDoc => browser.XmlDoc;

	public bool GetExecuteSearchInstantly => this.ExecuteSearchInstantly();

	public BrowserDataClass GetBrowserData => browser;

	internal static CBModeType Mode => CBMode;

	internal event RebuildScbDBEventHandler RebuildScbDB;

	internal event QueryEventHandler Query;

	internal event CloseSNFormEventHandler CloseSNForm;

	internal event GoToPOUEditorEventHandler GotoPouEditor;

	internal event GoToProjectExplorerEventHandler GoToProjectExplorer;

	internal event GetRegistryValueEventHandler GetRegistryValue;

	internal event ExecuteSearchInstantlyEventHandler ExecuteSearchInstantly;

	internal event OpenHelpEventHandler OpenHelp;

	internal event StoreComboBoxListsAndFilterInComFacadeEventHandler StoreComboBoxListsAndFilterInComFacade;

	public SearchNavigationForm()
	{
		try
		{
			InitializeComponent();
			Control.CheckForIllegalCrossThreadCalls = false;
			Strings = new LocalStringClass();
			SNViewCtrl.InitViewCtrl();
			browser = new BrowserDataClass(Strings);
			resultFilterComboBox.SelectedItem = resultFilterComboBox.Items[0];
			debugModeButton.Visible = false;
			debugRichTextBox.Visible = false;
			SNViewCtrl.SetBounds(SNViewCtrl.Location.X, SNViewCtrl.Location.Y, SNViewCtrl.Size.Width, SNViewCtrl.Size.Height + debugRichTextBox.Size.Height);
			tmpStringColl = new StringCollection();
		}
		catch (Exception)
		{
		}
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchNavigationTool.SearchNavigationForm));
		this.searchForLabel = new System.Windows.Forms.Label();
		this.searchInLabel = new System.Windows.Forms.Label();
		this.searchForComboBox = new System.Windows.Forms.ComboBox();
		this.searchInComboBox = new System.Windows.Forms.ComboBox();
		this.closeButton = new System.Windows.Forms.Button();
		this.rebuildButton = new System.Windows.Forms.Button();
		this.helpButton = new System.Windows.Forms.Button();
		this.SNStatusBar = new System.Windows.Forms.StatusBar();
		this.pathStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
		this.maxHitsStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
		this.searchButton = new System.Windows.Forms.Button();
		this.debugModeButton = new System.Windows.Forms.Button();
		this.searchOptGroupBox = new System.Windows.Forms.GroupBox();
		this.matchPrefixRadioButton = new System.Windows.Forms.RadioButton();
		this.matchWholeWordRadioButton = new System.Windows.Forms.RadioButton();
		this.maxHitsTextBox = new System.Windows.Forms.TextBox();
		this.maxHitsLabel = new System.Windows.Forms.Label();
		this.matchSubStringRadioButton = new System.Windows.Forms.RadioButton();
		this.alwaysOnTopCheckBox = new System.Windows.Forms.CheckBox();
		this.debugRichTextBox = new System.Windows.Forms.RichTextBox();
		this.searchGroupBox = new System.Windows.Forms.GroupBox();
		this.resultFilterGroupBox = new System.Windows.Forms.GroupBox();
		this.resultFilterComboBox = new System.Windows.Forms.ComboBox();
		this.filterByLabel = new System.Windows.Forms.Label();
		this.SNViewCtrl = new SearchNavigationTool.ViewCtrl();
		((System.ComponentModel.ISupportInitialize)this.pathStatusBarPanel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.maxHitsStatusBarPanel).BeginInit();
		this.searchOptGroupBox.SuspendLayout();
		this.searchGroupBox.SuspendLayout();
		this.resultFilterGroupBox.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.searchForLabel, "searchForLabel");
		this.searchForLabel.Name = "searchForLabel";
		resources.ApplyResources(this.searchInLabel, "searchInLabel");
		this.searchInLabel.Name = "searchInLabel";
		resources.ApplyResources(this.searchForComboBox, "searchForComboBox");
		this.searchForComboBox.Items.AddRange(new object[1] { resources.GetString("searchForComboBox.Items") });
		this.searchForComboBox.Name = "searchForComboBox";
		this.searchForComboBox.TextChanged += new System.EventHandler(searchForComboBoxTextChanged);
		this.searchForComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(searchForComboBox_KeyPress);
		resources.ApplyResources(this.searchInComboBox, "searchInComboBox");
		this.searchInComboBox.Items.AddRange(new object[4]
		{
			resources.GetString("searchInComboBox.Items"),
			resources.GetString("searchInComboBox.Items1"),
			resources.GetString("searchInComboBox.Items2"),
			resources.GetString("searchInComboBox.Items3")
		});
		this.searchInComboBox.Name = "searchInComboBox";
		this.searchInComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(searchForComboBox_KeyPress);
		resources.ApplyResources(this.closeButton, "closeButton");
		this.closeButton.Name = "closeButton";
		this.closeButton.Click += new System.EventHandler(closeButton_Click);
		resources.ApplyResources(this.rebuildButton, "rebuildButton");
		this.rebuildButton.Name = "rebuildButton";
		this.rebuildButton.Click += new System.EventHandler(rebuildButton_Click);
		resources.ApplyResources(this.helpButton, "helpButton");
		this.helpButton.Name = "helpButton";
		this.helpButton.Click += new System.EventHandler(helpButton_Click);
		resources.ApplyResources(this.SNStatusBar, "SNStatusBar");
		this.SNStatusBar.Name = "SNStatusBar";
		this.SNStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[2] { this.pathStatusBarPanel, this.maxHitsStatusBarPanel });
		this.SNStatusBar.ShowPanels = true;
		this.SNStatusBar.SizingGrip = false;
		this.SNStatusBar.DrawItem += new System.Windows.Forms.StatusBarDrawItemEventHandler(SNStatusBar_DrawItem);
		resources.ApplyResources(this.pathStatusBarPanel, "pathStatusBarPanel");
		resources.ApplyResources(this.maxHitsStatusBarPanel, "maxHitsStatusBarPanel");
		resources.ApplyResources(this.searchButton, "searchButton");
		this.searchButton.Name = "searchButton";
		this.searchButton.Click += new System.EventHandler(searchButton_Click);
		this.searchButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(searchButton_KeyPress);
		resources.ApplyResources(this.debugModeButton, "debugModeButton");
		this.debugModeButton.Name = "debugModeButton";
		this.debugModeButton.TabStop = false;
		this.debugModeButton.Click += new System.EventHandler(debugModeButton_Click);
		resources.ApplyResources(this.searchOptGroupBox, "searchOptGroupBox");
		this.searchOptGroupBox.Controls.Add(this.debugModeButton);
		this.searchOptGroupBox.Controls.Add(this.matchPrefixRadioButton);
		this.searchOptGroupBox.Controls.Add(this.matchWholeWordRadioButton);
		this.searchOptGroupBox.Controls.Add(this.maxHitsTextBox);
		this.searchOptGroupBox.Controls.Add(this.maxHitsLabel);
		this.searchOptGroupBox.Controls.Add(this.matchSubStringRadioButton);
		this.searchOptGroupBox.Name = "searchOptGroupBox";
		this.searchOptGroupBox.TabStop = false;
		resources.ApplyResources(this.matchPrefixRadioButton, "matchPrefixRadioButton");
		this.matchPrefixRadioButton.Name = "matchPrefixRadioButton";
		this.matchWholeWordRadioButton.Checked = true;
		resources.ApplyResources(this.matchWholeWordRadioButton, "matchWholeWordRadioButton");
		this.matchWholeWordRadioButton.Name = "matchWholeWordRadioButton";
		this.matchWholeWordRadioButton.TabStop = true;
		resources.ApplyResources(this.maxHitsTextBox, "maxHitsTextBox");
		this.maxHitsTextBox.Name = "maxHitsTextBox";
		this.maxHitsTextBox.TextChanged += new System.EventHandler(maxHitsTextBox_TextChanged);
		this.maxHitsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(maxHitsTextBox_KeyDown);
		this.maxHitsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(maxHitsTextBox_KeyPress);
		this.maxHitsTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(maxHitsTextBox_KeyUp);
		resources.ApplyResources(this.maxHitsLabel, "maxHitsLabel");
		this.maxHitsLabel.Name = "maxHitsLabel";
		resources.ApplyResources(this.matchSubStringRadioButton, "matchSubStringRadioButton");
		this.matchSubStringRadioButton.Name = "matchSubStringRadioButton";
		resources.ApplyResources(this.alwaysOnTopCheckBox, "alwaysOnTopCheckBox");
		this.alwaysOnTopCheckBox.Name = "alwaysOnTopCheckBox";
		this.alwaysOnTopCheckBox.CheckedChanged += new System.EventHandler(alwaysOnTopCheckBox_CheckedChanged);
		resources.ApplyResources(this.debugRichTextBox, "debugRichTextBox");
		this.debugRichTextBox.DetectUrls = false;
		this.debugRichTextBox.HideSelection = false;
		this.debugRichTextBox.Name = "debugRichTextBox";
		this.debugRichTextBox.TabStop = false;
		resources.ApplyResources(this.searchGroupBox, "searchGroupBox");
		this.searchGroupBox.Controls.Add(this.searchForComboBox);
		this.searchGroupBox.Controls.Add(this.searchInComboBox);
		this.searchGroupBox.Controls.Add(this.searchForLabel);
		this.searchGroupBox.Controls.Add(this.searchInLabel);
		this.searchGroupBox.Name = "searchGroupBox";
		this.searchGroupBox.TabStop = false;
		resources.ApplyResources(this.resultFilterGroupBox, "resultFilterGroupBox");
		this.resultFilterGroupBox.Controls.Add(this.resultFilterComboBox);
		this.resultFilterGroupBox.Controls.Add(this.filterByLabel);
		this.resultFilterGroupBox.Name = "resultFilterGroupBox";
		this.resultFilterGroupBox.TabStop = false;
		resources.ApplyResources(this.resultFilterComboBox, "resultFilterComboBox");
		this.resultFilterComboBox.BackColor = System.Drawing.SystemColors.Window;
		this.resultFilterComboBox.Items.AddRange(new object[5]
		{
			resources.GetString("resultFilterComboBox.Items"),
			resources.GetString("resultFilterComboBox.Items1"),
			resources.GetString("resultFilterComboBox.Items2"),
			resources.GetString("resultFilterComboBox.Items3"),
			resources.GetString("resultFilterComboBox.Items4")
		});
		this.resultFilterComboBox.Name = "resultFilterComboBox";
		this.resultFilterComboBox.DropDown += new System.EventHandler(resultFilterComboBox_DropDown);
		this.resultFilterComboBox.SelectedIndexChanged += new System.EventHandler(resultFilterComboBox_SelectedIndexChanged);
		this.resultFilterComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(searchForComboBox_KeyPress);
		resources.ApplyResources(this.filterByLabel, "filterByLabel");
		this.filterByLabel.Name = "filterByLabel";
		resources.ApplyResources(this.SNViewCtrl, "SNViewCtrl");
		this.SNViewCtrl.Name = "SNViewCtrl";
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.SNStatusBar);
		base.Controls.Add(this.searchGroupBox);
		base.Controls.Add(this.debugRichTextBox);
		base.Controls.Add(this.alwaysOnTopCheckBox);
		base.Controls.Add(this.searchOptGroupBox);
		base.Controls.Add(this.searchButton);
		base.Controls.Add(this.SNViewCtrl);
		base.Controls.Add(this.helpButton);
		base.Controls.Add(this.rebuildButton);
		base.Controls.Add(this.closeButton);
		base.Controls.Add(this.resultFilterGroupBox);
		base.Name = "SearchNavigationForm";
		base.Closed += new System.EventHandler(closeButton_Click);
		base.SizeChanged += new System.EventHandler(SearchNavigationForm_SizeChanged);
		((System.ComponentModel.ISupportInitialize)this.pathStatusBarPanel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.maxHitsStatusBarPanel).EndInit();
		this.searchOptGroupBox.ResumeLayout(false);
		this.searchOptGroupBox.PerformLayout();
		this.searchGroupBox.ResumeLayout(false);
		this.resultFilterGroupBox.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	[STAThread]
	private static void Main()
	{
		Application.Run(new SearchNavigationForm());
	}

	private void closeButton_Click(object sender, EventArgs e)
	{
		if (!closingBool)
		{
			closingBool = true;
			string[] searchInStrings = new string[21];
			string[] searchForStrings = new string[21];
			for (int i = 0; i < searchInComboBox.Items.Count; i++)
			{
				searchInStrings[i] = searchInComboBox.Items[i].ToString();
			}
			for (int j = 0; j < searchForComboBox.Items.Count; j++)
			{
				searchForStrings[j] = searchForComboBox.Items[j].ToString();
			}
			this.StoreComboBoxListsAndFilterInComFacade(this, new StoreComboBoxListsAndFilterInComFacadeEventArgs(ref searchInStrings, ref searchForStrings));
			this.CloseSNForm();
		}
	}

	private void rebuildButton_Click(object sender, EventArgs e)
	{
		SetStatusBarText(Strings.RebuildingSearchData + "...", clearOld: true);
		Cursor = Cursors.WaitCursor;
		rebuildButton.Enabled = false;
		searchButton.Enabled = false;
		try
		{
			this.RebuildScbDB(this);
		}
		catch (Exception ex)
		{
			ErrorMsgBox(ex.ToString());
		}
		Cursor = Cursors.Default;
		rebuildButton.Enabled = true;
		EnableSearchButton();
		SetStatusBarText(" " + Strings.Done + "!!!", clearOld: false);
	}

	public void SetReferencesData(string symbol, string path, string specification, string guiType)
	{
		ReferencesDataCollection referenceCollection = new ReferencesDataCollection();
		bool flag = false;
		browser.GetReferencesCollection(symbol, path, guiType, referenceCollection, ref flag);
		SNViewCtrl.SetReferencesData(referenceCollection, symbol, path, specification, guiType, flag);
	}

	public void GoToCBGui(NavigationDataClass navigationData)
	{
		if (navigationData == null)
		{
			throw new ArgumentNullException("navigationData");
		}
		if (navigationData.tab == Strings.StructComponent)
		{
			navigationData.position = "1";
		}
		if (navigationData.tab == Strings.Parameter)
		{
			navigationData.tab = Strings.Parameters;
			navigationData.position = "1";
		}
		if (navigationData.tab == Strings.Variable)
		{
			navigationData.tab = Strings.Variables;
			navigationData.position = "1";
		}
		if (navigationData.tab == Strings.FunctionBlock)
		{
			navigationData.tab = Strings.FunctionBlocks;
			navigationData.position = "1";
		}
		if (navigationData.tab == Strings.CMConnections && CBMode == CBModeType.Offline)
		{
			string[] array = navigationData.path.Split('.');
			if (array[array.Length - 1] != navigationData.element)
			{
				navigationData.path = navigationData.path + "." + navigationData.element;
				navigationData.specification = navigationData.specification + "." + Strings.CMConnections;
			}
		}
		if (navigationData.tab == Strings.CMGraphConn && CBMode == CBModeType.Offline)
		{
			string[] array2 = navigationData.element.Split('.');
			if (array2[array2.Length - 1] != navigationData.search)
			{
				navigationData.element = navigationData.element + "." + navigationData.search;
			}
		}
		if (navigationData.tab == Strings.CMConnections && CBMode != 0 && navigationData.element != navigationData.search)
		{
			string[] array3 = navigationData.path.Split('.');
			if (array3[array3.Length - 1] != navigationData.element)
			{
				navigationData.path = navigationData.path + "." + navigationData.element;
			}
			navigationData.tab = Strings.Parameters;
			navigationData.element = navigationData.search;
		}
		if (navigationData.path.Contains("."))
		{
			string[] stringArray = navigationData.path.Split('.');
			StringArrayVersionFix(ref stringArray);
			string[] array4 = navigationData.specification.Split('.');
			if (stringArray.Length != array4.Length)
			{
				navigationData.specification = navigationData.specification + "." + navigationData.guiType;
			}
		}
		if (navigationData.codeBlockType == "FBD" || navigationData.codeBlockType == "LD")
		{
			string[] array5 = navigationData.refPath.Split('.');
			string[] array6 = navigationData.path.Split('.');
			StringBuilder stringBuilder = new StringBuilder("");
			bool flag = false;
			int num = 0;
			string[] array7 = array5;
			foreach (string value in array7)
			{
				flag = false;
				if (num < array6.Length)
				{
					if (array5[num] != array6[num])
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					stringBuilder.Append(value);
					if (num < array5.Length - 1)
					{
						stringBuilder.Append(".");
					}
				}
				num++;
			}
			string element = stringBuilder.ToString();
			this.GotoPouEditor(this, new GoToPOUEditorEventArgs(navigationData.path, navigationData.specification, navigationData.tab, navigationData.row, navigationData.position, element, navigationData.guiType));
		}
		else
		{
			this.GotoPouEditor(this, new GoToPOUEditorEventArgs(navigationData.path, navigationData.specification, navigationData.tab, navigationData.row, navigationData.position, navigationData.element, navigationData.guiType));
		}
		if (alwaysOnTopCheckBox.Checked)
		{
			Activate();
			Focus();
		}
	}

	public void GoToCBProjectExplorer(NavigationDataClass navigationData)
	{
		if (navigationData == null)
		{
			throw new ArgumentNullException("navigationData");
		}
		this.GoToProjectExplorer(this, new GoToPOUEditorEventArgs(navigationData.path, navigationData.specification, navigationData.tab, navigationData.row, navigationData.position, navigationData.element, navigationData.guiType));
		if (alwaysOnTopCheckBox.Checked)
		{
			Activate();
			Focus();
		}
	}

	public void SetSearchStrings(string symbol, string path)
	{
		if (symbol == null)
		{
			throw new ArgumentNullException("symbol");
		}
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		SetSearchFor(symbol);
		SetSearchIn(path);
		if (symbol.Length < 1)
		{
			searchForComboBox.Select();
		}
		else
		{
			searchButton.Select();
		}
		if (base.WindowState == FormWindowState.Minimized)
		{
			base.WindowState = FormWindowState.Normal;
			SetMode(newCBMode);
		}
		Activate();
	}

	public void SetSearchFor(string symbol)
	{
		searchForComboBox.Text = symbol;
	}

	private string GetSearchInString()
	{
		if (searchInComboBox.Text == Strings.All)
		{
			return "";
		}
		if (searchInComboBox.Text == Strings.Applications2)
		{
			return Strings.Applications;
		}
		if (searchInComboBox.Text == Strings.Controllers2)
		{
			return Strings.Controllers;
		}
		if (searchInComboBox.Text == Strings.Libraries2)
		{
			return Strings.Libraries;
		}
		string text = searchInComboBox.Text;
		if (text.Length > Strings.Applications2.Length && text.Substring(0, Strings.Applications2.Length + 1).ToLower(CultureInfo.CurrentCulture) == (Strings.Applications + ".").ToLower(CultureInfo.CurrentCulture))
		{
			text = text.Substring(Strings.Applications2.Length + 1);
		}
		else if (text.Length > Strings.Controllers2.Length && text.Substring(0, Strings.Controllers2.Length).ToLower(CultureInfo.CurrentCulture) == Strings.Controllers.ToLower(CultureInfo.CurrentCulture))
		{
			text = text.Substring(Strings.Controllers2.Length + 1);
		}
		else if (text.Length > Strings.Libraries2.Length && text.Substring(0, Strings.Libraries2.Length).ToLower(CultureInfo.CurrentCulture) == Strings.Libraries.ToLower(CultureInfo.CurrentCulture))
		{
			text = text.Substring(Strings.Libraries2.Length + 1);
		}
		return text;
	}

	public void SetSearchIn(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.Length == 0)
		{
			path = Strings.All;
		}
		searchInComboBox.Text = path;
	}

	public void SetMatchType(SNQueryMatchType matchType)
	{
		switch (matchType)
		{
		case SNQueryMatchType.SN_QWholeWord:
			matchWholeWordRadioButton.Checked = true;
			break;
		case SNQueryMatchType.SN_QSubString:
			matchSubStringRadioButton.Checked = true;
			break;
		default:
			matchPrefixRadioButton.Checked = true;
			break;
		}
	}

	private SNQueryMatchType GetMatchType()
	{
		if (matchWholeWordRadioButton.Checked)
		{
			return SNQueryMatchType.SN_QWholeWord;
		}
		if (matchPrefixRadioButton.Checked)
		{
			return SNQueryMatchType.SN_QPrefix;
		}
		return SNQueryMatchType.SN_QSubString;
	}

	public void SetMaxHits(int maxHits)
	{
		maxHitsTextBox.Text = maxHits.ToString(CultureInfo.CurrentCulture);
	}

	public void SetSearchForInStrings(string[] searchFor, string[] searchIn)
	{
		if (searchFor == null)
		{
			throw new ArgumentNullException("searchFor");
		}
		if (searchIn == null)
		{
			throw new ArgumentNullException("searchIn");
		}
		int i;
		for (i = 0; searchFor[i] != null; i++)
		{
			if (i >= searchForComboBox.Items.Count)
			{
				searchForComboBox.Items.Add(searchFor[i].ToString());
			}
			else
			{
				searchForComboBox.Items[i] = searchFor[i];
			}
		}
		for (; searchIn[i] != null; i++)
		{
			if (i >= searchInComboBox.Items.Count || i < 5)
			{
				if (!searchInComboBox.Items.Contains(searchIn[i].ToString()))
				{
					searchInComboBox.Items.Add(searchIn[i].ToString());
				}
			}
			else
			{
				searchInComboBox.Items[i] = searchIn[i];
			}
		}
		i = 0;
	}

	public void SearchButtonFocus()
	{
		searchButton.Focus();
		searchButton.Select();
	}

	public void PerformSearch()
	{
		if (searchInComboBox.Text.Length > 0 && searchForComboBox.Text.Length > 0)
		{
			searchButton.PerformClick();
		}
	}

	public void SetResult(string result)
	{
		if (result == null)
		{
			throw new ArgumentNullException(result);
		}
		CBMode = newCBMode;
		SNViewCtrl.Clear();
		queryResultString = result;
		if (queryResultString.Length > 39 && CBMode == CBModeType.Offline)
		{
			debugRichTextBox.Text += queryResultString;
			browser.SetXml(queryResultString);
			browser.AddProjectToPaths();
		}
		else
		{
			queryResultString = "";
		}
		UpdateSymbolCtrl();
		UpdateComboBoxes();
	}

	private void searchButton_Click(object sender, EventArgs e)
	{
		DoTheSearch();
	}

	public void DoTheSearch()
	{
		CBMode = newCBMode;
		if (CBMode != 0 && (searchForComboBox.Text.Length == 0 || searchForComboBox.Text == "*"))
		{
			EnableSearchButton();
			return;
		}
		Cursor = Cursors.WaitCursor;
		searchButton.Enabled = false;
		rebuildButton.Enabled = false;
		SetStatusBarText(Strings.Searching + "...", clearOld: true);
		resultFilterComboBox.SelectedItem = resultFilterComboBox.Items[0];
		SNViewCtrl.Clear();
		if (debugModeButton.Visible)
		{
			debugRichTextBox.Text = "Query(\"";
			debugRichTextBox.Text += searchInComboBox.Text;
			debugRichTextBox.Text += "\", \"";
			debugRichTextBox.Text += searchForComboBox.Text;
			debugRichTextBox.Text += "\", \"";
			debugRichTextBox.Text += GetMatchType();
			debugRichTextBox.Text += "\", \"";
			debugRichTextBox.Text += maxHitsTextBox.Text;
			debugRichTextBox.Text += "\")\n";
		}
		int num = 0;
		try
		{
			num = Convert.ToInt32(maxHitsTextBox.Text, CultureInfo.CurrentCulture);
		}
		catch
		{
			num = int.MaxValue;
		}
		queryResultString = this.Query(this, new QueryEventArgs(GetSearchInString(), searchForComboBox.Text, GetMatchType(), num, CBMode));
		if (debugModeButton.Visible)
		{
			debugRichTextBox.Text += queryResultString;
		}
		SetStatusBarText(" " + Strings.Done, clearOld: false);
		if (queryResultString.Length > 39)
		{
			browser.SetXml(queryResultString);
		}
		EmptyReferences();
		browser.AddProjectToPaths();
		UpdateSymbolCtrl();
		UpdateComboBoxes();
		Cursor = Cursors.Default;
		EnableSearchButton();
		rebuildButton.Enabled = true;
	}

	private void UpdateComboBoxes()
	{
		if (CBMode == CBModeType.OnlineSearchFieldsDisabled || CBMode == CBModeType.TestModeSearchFieldsDisabled || CBMode == CBModeType.OnlineIterativeSearchFieldsDisabled || CBMode == CBModeType.TestModeIterativeSearchFieldsDisabled)
		{
			return;
		}
		string text = "";
		if (searchForComboBox.Text.Length > 0 && searchForComboBox.Text != "*" && !searchForComboBox.Items.Contains(searchForComboBox.Text))
		{
			searchForComboBox.Items.Insert(1, searchForComboBox.Text);
		}
		if (searchInComboBox.Text.Length > 0 && searchInComboBox.Text != Strings.All && searchInComboBox.Text != Strings.Applications2 && searchInComboBox.Text != Strings.Controllers2 && searchInComboBox.Text != Strings.Libraries2)
		{
			if (searchInComboBox.Items.Contains(searchInComboBox.Text))
			{
				text = searchInComboBox.Text;
				searchInComboBox.Items.RemoveAt(searchInComboBox.Items.IndexOf(searchInComboBox.Text));
				searchInComboBox.Text = text;
			}
			if (newCBMode == CBModeType.Offline)
			{
				searchInComboBox.Items.Insert(4, searchInComboBox.Text);
			}
			else
			{
				searchInComboBox.Items.Insert(3, searchInComboBox.Text);
			}
		}
		while (searchForComboBox.Items.Count > 20)
		{
			searchForComboBox.Items.RemoveAt(20);
		}
		while (searchInComboBox.Items.Count > 20)
		{
			searchInComboBox.Items.RemoveAt(20);
		}
	}

	public void UpdateSymbolCtrl()
	{
		FilterType selectedIndex = (FilterType)resultFilterComboBox.SelectedIndex;
		SNViewCtrl.Clear();
		if (queryResultString == null)
		{
			return;
		}
		if (queryResultString.Length > 39)
		{
			SymbolDataCollection symbolDataCollection = new SymbolDataCollection();
			int symbolCollection = browser.GetSymbolCollection(symbolDataCollection, selectedIndex);
			SNViewCtrl.SetSymbolData(symbolDataCollection);
			if (CBMode == CBModeType.Offline)
			{
				SetStatusBarNoHits(symbolCollection.ToString(CultureInfo.CurrentCulture));
			}
			else
			{
				SetStatusBarNoRefHits(SNViewCtrl.GetNoHitsInRefCtrl.ToString(CultureInfo.CurrentCulture));
			}
		}
		else
		{
			SetStatusBarNoHits("0");
		}
	}

	private void alwaysOnTopCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		CheckBox checkBox = (CheckBox)sender;
		if (checkBox != null)
		{
			base.TopMost = checkBox.Checked;
		}
	}

	private void debugModeButton_Click(object sender, EventArgs e)
	{
		debugModeButton.Visible = false;
		debugRichTextBox.Visible = false;
		SNViewCtrl.SetBounds(SNViewCtrl.Location.X, SNViewCtrl.Location.Y, SNViewCtrl.Size.Width, SNViewCtrl.Size.Height + debugRichTextBox.Size.Height);
	}

	private void searchForComboBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (searchForComboBox.Text == "kunden")
		{
			startDebugMode = true;
			return;
		}
		if (startDebugMode && searchForComboBox.Text == "ifokus")
		{
			debugModeButton.Visible = true;
			debugRichTextBox.Visible = true;
			SNViewCtrl.SetBounds(SNViewCtrl.Location.X, SNViewCtrl.Location.Y, SNViewCtrl.Size.Width, SNViewCtrl.Size.Height - debugRichTextBox.Size.Height);
			searchForComboBox.Text = "";
			startDebugMode = false;
			return;
		}
		if (searchForComboBox.Text == "helhet")
		{
			startExtendedMode = true;
			return;
		}
		if (startExtendedMode && searchForComboBox.Text == "enkelhet")
		{
			startExtendedMode2 = true;
			return;
		}
		if (startExtendedMode2 && startExtendedMode && searchForComboBox.Text == "påhittighet")
		{
			ExtendedForm extendedForm = null;
			try
			{
				extendedForm = new ExtendedForm();
				extendedForm.ShowDialog(base.ParentForm);
				startExtendedMode = false;
				startExtendedMode2 = false;
				searchForComboBox.Text = "";
				return;
			}
			finally
			{
				extendedForm?.Dispose();
			}
		}
		startDebugMode = false;
		startExtendedMode = false;
		startExtendedMode2 = false;
		searchButton.Focus();
		searchButton.Select();
		e.Handled = true;
		searchButton.PerformClick();
	}

	private void searchButton_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			searchButton.PerformClick();
		}
	}

	public void SetStatusBarText(string text, bool clearOld)
	{
		if (clearOld)
		{
			SNStatusBar.Panels[0].Text = text;
		}
		else
		{
			SNStatusBar.Panels[0].Text += text;
		}
	}

	public void SetStatusBarNoHits(string text)
	{
		SNStatusBar.Panels[1].Text = Strings.NoOfHits + text + "     ";
	}

	public void SetStatusBarNoRefHits(string text)
	{
		SNStatusBar.Panels[1].Text = Strings.NoOfRefHits + text + "     ";
	}

	public void ErrorMsgBox(string message)
	{
		MessageBox.Show(message, "Caption", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
	}

	public void SetMode(CBModeType mode)
	{
		newCBMode = mode;
		SNViewCtrl.SetMode(mode);
		rebuildButton.Enabled = true;
		if (searchInComboBox != null)
		{
			if (newCBMode == CBModeType.OnlineIterativeSearchFieldsDisabled || newCBMode == CBModeType.TestModeIterativeSearchFieldsDisabled || newCBMode == CBModeType.OnlineSearchFieldsDisabled || newCBMode == CBModeType.TestModeSearchFieldsDisabled)
			{
				searchInComboBox.Enabled = false;
				searchForComboBox.Enabled = false;
				searchOptGroupBox.Visible = false;
				searchGroupBox.Width = searchOptGroupBox.Width + searchOptGroupBox.Location.X - searchGroupBox.Location.X;
			}
			else if (newCBMode == CBModeType.OnlineIterative || newCBMode == CBModeType.TestModeIterative)
			{
				searchOptGroupBox.Visible = false;
				searchGroupBox.Width = searchOptGroupBox.Width + searchOptGroupBox.Location.X - searchGroupBox.Location.X;
			}
			else
			{
				searchInComboBox.Enabled = true;
				searchForComboBox.Enabled = true;
			}
			if (newCBMode != 0)
			{
				if (searchInComboBox.Items.Contains(Strings.Libraries))
				{
					searchInComboBox.Items.RemoveAt(searchInComboBox.Items.IndexOf(Strings.Libraries));
				}
			}
			else
			{
				if (searchInComboBox.Items.Count > 2 && !searchInComboBox.Items.Contains(Strings.Libraries))
				{
					searchInComboBox.Items.Insert(3, Strings.Libraries);
				}
				searchOptGroupBox.Visible = true;
				searchGroupBox.Width = searchOptGroupBox.Location.X - searchGroupBox.Location.X - searchGroupBox.Margin.Right - searchOptGroupBox.Padding.Right;
				searchInComboBox.Enabled = true;
				searchForComboBox.Enabled = true;
			}
		}
		DisableAlwaysOnTop();
		Clear();
		CBMode = newCBMode;
	}

	public string GetRegistryValueWrapper(string keyName)
	{
		return this.GetRegistryValue(this, new GetRegistryValueEventArgs(keyName));
	}

	private void maxHitsTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyData != Keys.D0 && e.KeyData != Keys.D1 && e.KeyData != Keys.D2 && e.KeyData != Keys.D3 && e.KeyData != Keys.D4 && e.KeyData != Keys.D5 && e.KeyData != Keys.D6 && e.KeyData != Keys.D7 && e.KeyData != Keys.D8 && e.KeyData != Keys.D9 && e.KeyData != Keys.Delete && e.KeyData != Keys.Return && e.KeyData != Keys.Back && e.KeyData != Keys.NumPad0 && e.KeyData != Keys.NumPad1 && e.KeyData != Keys.NumPad2 && e.KeyData != Keys.NumPad3 && e.KeyData != Keys.NumPad4 && e.KeyData != Keys.NumPad5 && e.KeyData != Keys.NumPad6 && e.KeyData != Keys.NumPad7 && e.KeyData != Keys.NumPad8 && e.KeyData != Keys.NumPad9)
		{
			ignoreKeyClickBool = true;
		}
	}

	private void maxHitsTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ignoreKeyClickBool)
		{
			e.Handled = true;
		}
		else if (e.KeyChar == '\r')
		{
			SearchButtonFocus();
		}
	}

	private void maxHitsTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		ignoreKeyClickBool = false;
	}

	private void maxHitsTextBox_TextChanged(object sender, EventArgs e)
	{
		try
		{
			Convert.ToInt64(maxHitsTextBox.Text, CultureInfo.CurrentCulture);
			EnableSearchButton();
		}
		catch (FormatException)
		{
			maxHitsTextBox.SelectAll();
			searchButton.Enabled = false;
		}
		catch
		{
			searchButton.Enabled = false;
			maxHitsTextBox.Text = "100";
		}
	}

	private void resultFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (queryResultString != null && queryResultString.Length > 39)
		{
			browser.SetXml(queryResultString);
			browser.AddProjectToPaths();
		}
		SetStatusBarText("", clearOld: true);
		if (resultFilterComboBox.SelectedIndex == 0)
		{
			resultFilterComboBox.BackColor = SystemColors.Window;
		}
		else
		{
			resultFilterComboBox.BackColor = Color.LightSteelBlue;
		}
		UpdateSymbolCtrl();
		Cursor = Cursors.Default;
	}

	private void resultFilterComboBox_DropDown(object sender, EventArgs e)
	{
		resultFilterComboBox.BackColor = SystemColors.Window;
	}

	private void SearchNavigationForm_SizeChanged(object sender, EventArgs e)
	{
		SNStatusBar.Panels[0].Width = base.Width - SNStatusBar.Panels[1].Width;
	}

	private void SNStatusBar_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
	{
	}

	private void helpButton_Click(object sender, EventArgs e)
	{
		this.OpenHelp();
	}

	public void DisableAlwaysOnTop()
	{
		if (alwaysOnTopCheckBox.Checked)
		{
			alwaysOnTopCheckBox.Checked = false;
			base.TopMost = false;
		}
	}

	public void Clear()
	{
		SNViewCtrl.Clear();
	}

	public void EnableSearchButton()
	{
		string text = searchForComboBox.Text.Trim();
		if (CBMode != 0 && (text.Length <= 0 || text == "*"))
		{
			searchButton.Enabled = false;
		}
		else
		{
			searchButton.Enabled = true;
		}
	}

	public void DisableSearchButton()
	{
		searchButton.Enabled = false;
	}

	internal void AddReferences()
	{
		try
		{
			tmpStringColl.Add(queryDSResultString);
			browser.AddProjectToPaths();
		}
		catch (Exception ex)
		{
			ErrorMsgBox(ex.ToString());
		}
	}

	internal void EmptyReferences()
	{
		string text = string.Empty;
		XmlNode xmlNode = null;
		if (iterativeSearch)
		{
			string xpath = BrowserDataClass.FindSymbolXPath(SNViewCtrl.SelectedSymbolString, "", SNViewCtrl.SelectedSymbolDefinitionString);
			xmlNode = browser.XmlDoc.SelectSingleNode(xpath);
			if (xmlNode != null)
			{
				xmlNode = xmlNode.SelectSingleNode("./References");
				if (xmlNode != null)
				{
					xmlNode.RemoveAll();
					if (tmpStringColl.Count > 0)
					{
						xmlNode.InnerXml = "<IS/>";
					}
				}
			}
		}
		if (debugModeButton.Visible)
		{
			debugRichTextBox.Text += "\nDeep Search results:\n";
		}
		while (tmpStringColl.Count > 0)
		{
			text = tmpStringColl[0];
			if (debugModeButton.Visible)
			{
				debugRichTextBox.Text += text;
				debugRichTextBox.Text += "\n";
			}
			tmpStringColl.RemoveAt(0);
			if (!iterativeSearch)
			{
				browser.AddXmlNode(text);
			}
			else if (xmlNode != null)
			{
				xmlNode.InnerXml += text;
			}
		}
		if (!iterativeSearch && !string.IsNullOrEmpty(text))
		{
			queryResultString = browser.XmlDoc.OuterXml;
		}
	}

	internal void Addsymbol(string projectName)
	{
		if (!iterativeSearch)
		{
			browser.SetXml("<?xml version=\"1.0\" encoding=\"utf-16\"?><Symbols ProjectName=\"" + projectName + "\">" + queryDSSymbolString + "</Symbols>");
		}
		if (debugModeButton.Visible)
		{
			debugRichTextBox.Text += browser.XmlDoc.OuterXml;
		}
		browser.AddProjectToPaths();
	}

	private void searchForComboBoxTextChanged(object sender, EventArgs e)
	{
		EnableSearchButton();
	}

	public static void StringArrayVersionFix(ref string[] stringArray)
	{
		if (stringArray == null)
		{
			throw new ArgumentNullException("stringArray");
		}
		bool flag = false;
		for (int i = 0; i < stringArray.Length; i++)
		{
			if (!flag && stringArray[i].IndexOf(" ", StringComparison.Ordinal) > 0 && i + 1 < stringArray.Length)
			{
				ref string reference = ref stringArray[i];
				reference = reference + "." + stringArray[i + 1];
				flag = true;
			}
			else if (flag)
			{
				if (i < stringArray.Length - 1)
				{
					stringArray[i] = stringArray[i + 1];
				}
				else
				{
					stringArray[i] = "";
				}
			}
		}
		if (flag)
		{
			string[] array = new string[stringArray.Length - 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = stringArray[i];
			}
			stringArray = array;
		}
	}

	internal void SearchIterative(string symbol, string path)
	{
		CBModeType cBMode = CBMode;
		if (CBMode == CBModeType.TestMode)
		{
			CBMode = CBModeType.TestModeIterative;
		}
		if (CBMode == CBModeType.TestModeSearchFieldsDisabled)
		{
			CBMode = CBModeType.TestModeIterative;
		}
		if (CBMode == CBModeType.OnlineSearchFieldsDisabled)
		{
			CBMode = CBModeType.OnlineIterative;
		}
		if (CBMode == CBModeType.Online)
		{
			CBMode = CBModeType.OnlineIterative;
		}
		iterativeSearch = true;
		queryDSResultString = "";
		int num = 0;
		try
		{
			num = Convert.ToInt32(maxHitsTextBox.Text, CultureInfo.CurrentCulture);
		}
		catch
		{
			num = int.MaxValue;
		}
		this.Query(this, new QueryEventArgs(path, symbol, GetMatchType(), num, CBMode));
		EmptyReferences();
		UpdateSymbolCtrl();
		iterativeSearch = false;
		CBMode = cBMode;
	}

	internal static bool IsPathWithVersion(string name)
	{
		bool result = false;
		int num = name.IndexOf(" ", StringComparison.Ordinal);
		if (num > 0)
		{
			int num2 = name.IndexOf(".", num, StringComparison.Ordinal);
			int num3 = name.IndexOf("-", StringComparison.Ordinal);
			if (num3 == -1)
			{
				num3 = name.IndexOf("/", StringComparison.Ordinal);
			}
			result = num != -1 && num2 != -1 && num3 != -1 && num2 - num > 1 && num3 - num2 > 1 && name.Length - num3 > 1;
		}
		return result;
	}
}
