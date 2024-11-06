using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SearchNavigationTool;

[ComVisible(false)]
public class SymbolCtrl : UserControl
{
	private ListView symbolListView;

	private ContextMenuStrip symbolCtrlContextMenu;

	private ToolStripMenuItem gotoEditorMenuItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem gotoProjExpMenuItem;

	private ToolStripMenuItem reportMenuItem;

	private ToolStripMenuItem iterativeSearchToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator2;

	private IContainer components;

	private LocalStringClass Strings => ((SearchNavigationForm)base.ParentForm).Strings;

	public SymbolCtrl()
	{
		InitializeComponent();
		InitListView();
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchNavigationTool.SymbolCtrl));
		this.symbolListView = new System.Windows.Forms.ListView();
		this.symbolCtrlContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.gotoEditorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.gotoProjExpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.reportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.iterativeSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.symbolCtrlContextMenu.SuspendLayout();
		base.SuspendLayout();
		this.symbolListView.ContextMenuStrip = this.symbolCtrlContextMenu;
		resources.ApplyResources(this.symbolListView, "symbolListView");
		this.symbolListView.HideSelection = false;
		this.symbolListView.Name = "symbolListView";
		this.symbolListView.UseCompatibleStateImageBehavior = false;
		this.symbolListView.View = System.Windows.Forms.View.SmallIcon;
		this.symbolListView.ItemActivate += new System.EventHandler(symbolListView_DoubleClick);
		this.symbolListView.SelectedIndexChanged += new System.EventHandler(symbolListView_SelectedIndexChanged);
		this.symbolListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(symbolListView_ColumnClick);
		this.symbolCtrlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.iterativeSearchToolStripMenuItem, this.toolStripSeparator2, this.gotoEditorMenuItem, this.gotoProjExpMenuItem, this.toolStripSeparator1, this.reportMenuItem });
		this.symbolCtrlContextMenu.Name = "contextMenuStrip";
		resources.ApplyResources(this.symbolCtrlContextMenu, "symbolCtrlContextMenu");
		this.symbolCtrlContextMenu.Opened += new System.EventHandler(symbolCtrlContextMenu_Popup);
		this.gotoEditorMenuItem.Name = "gotoEditorMenuItem";
		resources.ApplyResources(this.gotoEditorMenuItem, "gotoEditorMenuItem");
		this.gotoEditorMenuItem.Click += new System.EventHandler(symbolListView_DoubleClick);
		this.gotoProjExpMenuItem.Name = "gotoProjExpMenuItem";
		resources.ApplyResources(this.gotoProjExpMenuItem, "gotoProjExpMenuItem");
		this.gotoProjExpMenuItem.Click += new System.EventHandler(gotoProjExpMenuItem_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.reportMenuItem.Name = "reportMenuItem";
		resources.ApplyResources(this.reportMenuItem, "reportMenuItem");
		this.reportMenuItem.Click += new System.EventHandler(reportMenuItem_Click);
		this.iterativeSearchToolStripMenuItem.Name = "iterativeSearchToolStripMenuItem";
		resources.ApplyResources(this.iterativeSearchToolStripMenuItem, "iterativeSearchToolStripMenuItem");
		this.iterativeSearchToolStripMenuItem.Click += new System.EventHandler(iterativeSearchToolStripMenuItem_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		base.Controls.Add(this.symbolListView);
		base.Name = "SymbolCtrl";
		resources.ApplyResources(this, "$this");
		base.Enter += new System.EventHandler(SymbolCtrl_Enter);
		this.symbolCtrlContextMenu.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void NavigationDataPathFix(ref NavigationDataClass navData, string itemText)
	{
		if ((navData.guiType == Strings.FBType && navData.tab != Strings.ExternalVariables) || navData.guiType == Strings.Program || (navData.guiType == Strings.ModType && navData.tab != Strings.ExternalVariables) || navData.guiType == Strings.DataType || navData.guiType == Strings.Function || navData.guiType == Strings.IOChannelIn || navData.guiType == Strings.IOChannelOut || navData.guiType == Strings.Diagram || navData.guiType == Strings.Library || navData.guiType == Strings.SingleDiagram)
		{
			NavigationDataClass obj = navData;
			obj.path = obj.path + "." + itemText;
		}
		else if (navData.guiType == Strings.Application || navData.guiType == Strings.Controller)
		{
			NavigationDataClass obj2 = navData;
			obj2.path = obj2.path + "." + itemText;
		}
		else if (navData.guiType == Strings.ProjectConstant)
		{
			navData.path = navData.path.Replace(Strings.ProjectConstants, itemText);
		}
		else if (navData.guiType == Strings.StructComponent)
		{
			NavigationDataClass obj3 = navData;
			obj3.path = obj3.path + "." + itemText;
			NavigationDataClass obj4 = navData;
			obj4.specification = obj4.specification + "." + Strings.StructComponent;
		}
		else if (navData.guiType == Strings.Variable)
		{
			NavigationDataClass obj5 = navData;
			obj5.path = obj5.path + "." + itemText;
			NavigationDataClass obj6 = navData;
			obj6.specification = obj6.specification + "." + Strings.Variable;
		}
		else if (navData.guiType == Strings.CommunicationVariable)
		{
			NavigationDataClass obj7 = navData;
			obj7.path = obj7.path + "." + itemText;
			NavigationDataClass obj8 = navData;
			obj8.specification = obj8.specification + "." + Strings.CommunicationVariable;
		}
		else if (navData.guiType == Strings.Parameter)
		{
			NavigationDataClass obj9 = navData;
			obj9.path = obj9.path + "." + itemText;
			NavigationDataClass obj10 = navData;
			obj10.specification = obj10.specification + "." + Strings.Parameter;
		}
		else if (navData.guiType == Strings.SingleModType)
		{
			NavigationDataClass obj11 = navData;
			obj11.path = obj11.path + "." + itemText;
			NavigationDataClass obj12 = navData;
			obj12.specification = obj12.specification + "." + Strings.SingleModType;
		}
		else if (navData.guiType == Strings.FunctionBlock || navData.guiType == Strings.ControlModule)
		{
			navData.element = itemText;
		}
		else if (navData.guiType == Strings.ApplicationFolder)
		{
			navData.path = itemText;
			navData.specification = Strings.ApplicationFolder;
		}
		else if (navData.guiType == Strings.HardwareUnit)
		{
			navData.path = navData.path.Replace(itemText, itemText.Replace(".", "&#46;"));
		}
		else if (navData.guiType == Strings.DiagramType)
		{
			NavigationDataClass obj13 = navData;
			obj13.path = obj13.path + "." + itemText;
			NavigationDataClass obj14 = navData;
			obj14.specification = obj14.specification + "." + Strings.DiagramType;
		}
	}

	private void symbolListView_DoubleClick(object sender, EventArgs e)
	{
		Thread thread = new Thread(GotoDefinitionInCB);
		thread.TrySetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private void GotoDefinitionInCB()
	{
		NavigationDataClass navigationDataClass = null;
		foreach (ListViewItem selectedItem in symbolListView.SelectedItems)
		{
			if (selectedItem.Tag == null || !(selectedItem.Tag is NavigationDataClass))
			{
				continue;
			}
			navigationDataClass = (NavigationDataClass)selectedItem.Tag;
			string path = navigationDataClass.path;
			string specification = navigationDataClass.specification;
			string text = selectedItem.Text;
			NavigationDataPathFix(ref navigationDataClass, text);
			if (!(base.ParentForm is SearchNavigationForm))
			{
				continue;
			}
			SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
			if (navigationDataClass == null)
			{
				continue;
			}
			if (navigationDataClass.guiType == Strings.GlobalVariable || navigationDataClass.guiType == Strings.FunctionBlock || navigationDataClass.guiType == Strings.ExternalVariable || navigationDataClass.guiType == Strings.ControlModule || navigationDataClass.guiType == Strings.AccessVariable)
			{
				navigationDataClass.path = navigationDataClass.path + "." + text;
				navigationDataClass.specification = navigationDataClass.specification + "." + navigationDataClass.guiType;
			}
			if (navigationDataClass.guiType == Strings.ProjectConstant && navigationDataClass.specification == Strings.ProjectConstants)
			{
				string[] array = navigationDataClass.path.Split('.');
				int num = 0;
				navigationDataClass.specification = Strings.ProjectConstant;
				for (num = 1; num < array.Length; num++)
				{
					NavigationDataClass navigationDataClass2 = navigationDataClass;
					navigationDataClass2.specification = navigationDataClass2.specification + "." + Strings.ProjectConstant;
				}
			}
			if (navigationDataClass.guiType == Strings.DataType || navigationDataClass.guiType == Strings.FBType || navigationDataClass.guiType == Strings.ModType || navigationDataClass.guiType == Strings.Program)
			{
				string[] array2 = navigationDataClass.path.Split('.');
				string[] array3 = navigationDataClass.specification.Split('.');
				if (array2.Length != array3.Length)
				{
					navigationDataClass.specification = navigationDataClass.specification + "." + navigationDataClass.guiType;
				}
			}
			if (navigationDataClass.guiType == "Signal")
			{
				navigationDataClass.path = navigationDataClass.path + "." + text;
				navigationDataClass.specification = navigationDataClass.specification + "." + navigationDataClass.guiType;
			}
			searchNavigationForm.GoToCBGui(navigationDataClass);
			navigationDataClass.path = path;
			navigationDataClass.specification = specification;
		}
	}

	private void gotoProjExpMenuItem_Click(object sender, EventArgs e)
	{
		string path = "";
		string specification = "";
		NavigationDataClass navData = null;
		foreach (ListViewItem selectedItem in symbolListView.SelectedItems)
		{
			if (selectedItem.Tag != null && selectedItem.Tag is NavigationDataClass)
			{
				navData = (NavigationDataClass)selectedItem.Tag;
				path = navData.path;
				specification = navData.specification;
				NavigationDataPathFix(ref navData, selectedItem.Text);
			}
		}
		if (base.ParentForm is SearchNavigationForm)
		{
			SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
			if (navData != null)
			{
				searchNavigationForm.GoToCBProjectExplorer(navData);
				navData.path = path;
				navData.specification = specification;
			}
		}
	}

	protected void InitListView()
	{
		symbolListView.Clear();
		symbolListView.View = View.Details;
		symbolListView.FullRowSelect = true;
		symbolListView.GridLines = true;
		symbolListView.Columns.Add("Strings.Symbol", 100, HorizontalAlignment.Left);
		symbolListView.Columns.Add("Strings.Definition", -2, HorizontalAlignment.Left);
	}

	public void SetColumnNames()
	{
		symbolListView.Columns[0].Text = Strings.Symbol;
		symbolListView.Columns[1].Text = Strings.Definition;
	}

	public void SetDefinitionColumnWidth(int splitterPosition)
	{
		int num = 4;
		if (VScrollVisible())
		{
			num += 17;
		}
		symbolListView.Columns[1].Width = splitterPosition - (symbolListView.Columns[0].Width + num);
	}

	public void Clear()
	{
		symbolListView.Items.Clear();
	}

	public void SetData(SymbolDataCollection symbolDataCollection)
	{
		if (symbolDataCollection == null)
		{
			throw new ArgumentNullException("symbolDataCollection");
		}
		symbolListView.Items.Clear();
		if (symbolListView.SmallImageList == null)
		{
			symbolListView.SmallImageList = ((ViewCtrl)base.Parent).IconImageList;
		}
		foreach (SymbolDataClass item in symbolDataCollection)
		{
			string symbol = item.symbol;
			ListViewItem listViewItem = new ListViewItem(symbol.Replace("&#46;", "."), ((ViewCtrl)base.Parent).TypeImageIndex(item.type));
			if (item.navigationData.path.Length == 0)
			{
				if (item.type == "ApplicationFolder")
				{
					listViewItem.SubItems.Add("Folder");
				}
				else
				{
					listViewItem.SubItems.Add(Strings.Undefined2);
				}
			}
			else
			{
				symbol = item.navigationData.path;
				listViewItem.SubItems.Add(symbol.Replace("&#46;", "."));
			}
			symbolListView.Items.Add(listViewItem);
			if (item.type == "HardwareUnit")
			{
				NavigationDataClass navigationData = item.navigationData;
				navigationData.path = navigationData.path + "." + listViewItem.Text;
				item.navigationData.specification += ".HardwareUnit";
			}
			listViewItem.Tag = item.navigationData;
		}
		if (symbolListView.Items.Count > 0)
		{
			symbolListView.Items[0].Selected = true;
			symbolListView.Focus();
		}
	}

	private void symbolListView_SelectedIndexChanged(object sender, EventArgs e)
	{
		string text = "";
		string text2 = "";
		string guiType = "";
		string specification = "";
		foreach (ListViewItem selectedItem in symbolListView.SelectedItems)
		{
			text = selectedItem.SubItems[0].Text;
			NavigationDataClass navigationDataClass = (NavigationDataClass)selectedItem.Tag;
			guiType = navigationDataClass.guiType;
			text2 = navigationDataClass.path;
			specification = navigationDataClass.specification;
		}
		if (text.Length > 0 && base.ParentForm is SearchNavigationForm)
		{
			SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
			searchNavigationForm.SetReferencesData(text, text2, specification, guiType);
			searchNavigationForm.SetStatusBarText(text + " : " + text2, clearOld: true);
		}
	}

	private void reportMenuItem_Click(object sender, EventArgs e)
	{
		if (base.ParentForm is SearchNavigationForm)
		{
			ReportForm reportForm = new ReportForm(((SearchNavigationForm)base.ParentForm).XmlDoc, (SearchNavigationForm)base.ParentForm);
			reportForm.ShowDialog(base.ParentForm);
		}
	}

	private void symbolCtrlContextMenu_Popup(object sender, EventArgs e)
	{
		if (symbolListView.SelectedItems.Count < 1)
		{
			gotoProjExpMenuItem.Enabled = false;
			gotoEditorMenuItem.Enabled = false;
			reportMenuItem.Enabled = false;
		}
		else
		{
			gotoProjExpMenuItem.Enabled = true;
			gotoEditorMenuItem.Enabled = true;
			reportMenuItem.Enabled = true;
		}
		if (base.ParentForm is SearchNavigationForm)
		{
			if (SearchNavigationForm.Mode == CBModeType.Offline || SearchNavigationForm.Mode == CBModeType.OnlineIterative || SearchNavigationForm.Mode == CBModeType.TestModeIterative || SearchNavigationForm.Mode == CBModeType.OnlineIterativeSearchFieldsDisabled || SearchNavigationForm.Mode == CBModeType.TestModeIterativeSearchFieldsDisabled)
			{
				iterativeSearchToolStripMenuItem.Enabled = false;
			}
			else
			{
				iterativeSearchToolStripMenuItem.Enabled = true;
			}
		}
	}

	private void symbolListView_ColumnClick(object sender, ColumnClickEventArgs e)
	{
		if (base.ParentForm is SearchNavigationForm)
		{
			SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
			searchNavigationForm.GetBrowserData.SortOnColumn(e.Column);
			searchNavigationForm.UpdateSymbolCtrl();
		}
	}

	private bool VScrollVisible()
	{
		bool result = false;
		int num = symbolListView.Bounds.Height;
		int count = symbolListView.Items.Count;
		int num2 = 23;
		int num3 = 0;
		if (symbolListView.Items.Count > 0)
		{
			num3 = symbolListView.Items[0].Bounds.Height;
		}
		int num4 = num3 * count + num2;
		if (num4 > num)
		{
			result = true;
		}
		return result;
	}

	private void SymbolCtrl_Enter(object sender, EventArgs e)
	{
		string text = "";
		string text2 = "";
		foreach (ListViewItem selectedItem in symbolListView.SelectedItems)
		{
			text = selectedItem.SubItems[0].Text;
			text2 = selectedItem.SubItems[1].Text;
		}
		if (text.Length > 0)
		{
			SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
			searchNavigationForm.SetStatusBarText(text + " : " + text2, clearOld: true);
		}
	}

	public string SelectedSymbolString()
	{
		string result = "";
		foreach (ListViewItem selectedItem in symbolListView.SelectedItems)
		{
			result = selectedItem.SubItems[0].Text;
		}
		return result;
	}

	public string SelectedSymbolDefinitionString()
	{
		string result = "";
		foreach (ListViewItem selectedItem in symbolListView.SelectedItems)
		{
			result = selectedItem.SubItems[1].Text;
		}
		return result;
	}

	private void iterativeSearchToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		int num = 0;
		string text3 = "";
		foreach (ListViewItem selectedItem in symbolListView.SelectedItems)
		{
			text = selectedItem.SubItems[0].Text;
			text2 = selectedItem.SubItems[1].Text;
			num = selectedItem.Index;
			if (selectedItem.Tag is NavigationDataClass)
			{
				text3 = ((NavigationDataClass)selectedItem.Tag).specification;
			}
			int num2 = 0;
			string[] array = text3.Split('.');
			int num3 = array.Length - 1;
			string text4 = array[num3];
			while (text4 == Strings.StructComponent)
			{
				num2 = text2.LastIndexOf('.');
				string text5 = text2.Substring(num2 + 1, text2.Length - num2 - 1);
				text = text5 + "." + text;
				text2 = text2.Substring(0, num2);
				num3--;
				text4 = array[num3];
			}
		}
		SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
		searchNavigationForm.SearchIterative(text, text2);
		symbolListView.SelectedItems.Clear();
		foreach (ListViewItem item in symbolListView.Items)
		{
			if (item.Index == num)
			{
				item.Selected = true;
			}
		}
	}
}
