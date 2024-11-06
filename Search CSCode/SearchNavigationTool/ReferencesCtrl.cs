using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SearchNavigationTool.Properties;

namespace SearchNavigationTool;

[ComVisible(false)]
public class ReferencesCtrl : UserControl
{
	private TreeView referencesTreeView;

	internal Label referencesLabel;

	private string symbolName = "";

	private string symbolPath = "";

	private string symbolSpecification = "";

	private string symbolGuiType = "";

	private ContextMenuStrip refCtrlContextMenu;

	private ToolStripMenuItem searchMenuItem;

	private ToolStripMenuItem gotoEditorMenuItem;

	private ToolStripMenuItem gotoProjExpMenuItem;

	private ToolStripSeparator toolStripSeparator1;

	private IContainer components;

	private LocalStringClass Strings => ((SearchNavigationForm)base.ParentForm).Strings;

	public ReferencesCtrl()
	{
		InitializeComponent();
		ClearTree();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchNavigationTool.ReferencesCtrl));
		this.referencesTreeView = new System.Windows.Forms.TreeView();
		this.refCtrlContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.gotoEditorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.referencesLabel = new System.Windows.Forms.Label();
		this.searchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.gotoProjExpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.refCtrlContextMenu.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(this.referencesTreeView, "referencesTreeView");
		this.referencesTreeView.ContextMenuStrip = this.refCtrlContextMenu;
		this.referencesTreeView.HideSelection = false;
		this.referencesTreeView.ItemHeight = 16;
		this.referencesTreeView.Name = "referencesTreeView";
		this.referencesTreeView.DoubleClick += new System.EventHandler(referencesTreeView_DoubleClick);
		this.referencesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(referencesTreeView_AfterSelect);
		this.referencesTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(referencesTreeView_MouseDown);
		this.referencesTreeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(referencesTreeView_KeyPress);
		this.refCtrlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.searchMenuItem, this.toolStripSeparator1, this.gotoEditorMenuItem, this.gotoProjExpMenuItem });
		this.refCtrlContextMenu.Name = "refCtrlContextMenu";
		resources.ApplyResources(this.refCtrlContextMenu, "refCtrlContextMenu");
		this.refCtrlContextMenu.Opened += new System.EventHandler(refCtrlContextMenu_Popup);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.gotoEditorMenuItem.Name = "gotoEditorMenuItem";
		resources.ApplyResources(this.gotoEditorMenuItem, "gotoEditorMenuItem");
		this.gotoEditorMenuItem.Click += new System.EventHandler(referencesTreeView_DoubleClick);
		resources.ApplyResources(this.referencesLabel, "referencesLabel");
		this.referencesLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.referencesLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.referencesLabel.Name = "referencesLabel";
		this.searchMenuItem.Image = SearchNavigationTool.Properties.Resources.Search;
		resources.ApplyResources(this.searchMenuItem, "searchMenuItem");
		this.searchMenuItem.Name = "searchMenuItem";
		this.searchMenuItem.Click += new System.EventHandler(searchMenuItem_Click);
		this.gotoProjExpMenuItem.Image = SearchNavigationTool.Properties.Resources.ProjectExplorer;
		resources.ApplyResources(this.gotoProjExpMenuItem, "gotoProjExpMenuItem");
		this.gotoProjExpMenuItem.Name = "gotoProjExpMenuItem";
		this.gotoProjExpMenuItem.Click += new System.EventHandler(gotoProjExpMenuItem_Click);
		base.Controls.Add(this.referencesLabel);
		base.Controls.Add(this.referencesTreeView);
		base.Name = "ReferencesCtrl";
		resources.ApplyResources(this, "$this");
		base.Enter += new System.EventHandler(ReferencesCtrl_Enter);
		this.refCtrlContextMenu.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public void ClearTree()
	{
		referencesTreeView.Nodes.Clear();
	}

	public void SetData(ReferencesDataCollection referenceCollection, string symbol, string path, string specification, string guiType)
	{
		if (referenceCollection == null)
		{
			throw new ArgumentNullException("referenceCollection");
		}
		symbolName = symbol;
		symbolPath = path;
		symbolSpecification = specification;
		symbolGuiType = guiType;
		referencesTreeView.BeginUpdate();
		ClearTree();
		if (referencesTreeView.ImageList == null)
		{
			referencesTreeView.ImageList = ((ViewCtrl)base.Parent).IconImageList;
		}
		TreeNode treeNode = null;
		string text = "";
		bool flag = false;
		int num = 0;
		string text2 = "";
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		foreach (ReferencesDataClass item in referenceCollection)
		{
			num = 0;
			flag = true;
			treeNode = null;
			symbolName = symbol;
			flag4 = item.navigationData.tab == Strings.CMGraphConn;
			string[] stringArray = item.navigationData.path.Split('.');
			string[] array = item.specification.Split('.');
			text2 = "";
			if (base.ParentForm is SearchNavigationForm)
			{
				SearchNavigationForm.StringArrayVersionFix(ref stringArray);
			}
			string[] array2 = stringArray;
			foreach (string text3 in array2)
			{
				if (text3.Length > 0)
				{
					if (flag)
					{
						bool newNodeCreated = false;
						treeNode = GetRootNode(text3, ref newNodeCreated);
						if (newNodeCreated && num < array.Length)
						{
							SetTreeNodeIcon(treeNode, array[num]);
						}
						flag = false;
					}
					else if (array[0] == Strings.Controllers)
					{
						BuildHWTree(num, ref text2, text3, ref treeNode, item, array);
					}
					else
					{
						if (base.ParentForm is SearchNavigationForm)
						{
							if (num < array.Length)
							{
								if (array[num] == Strings.ControlModule)
								{
									flag3 = SearchNavigationForm.Mode == CBModeType.Offline;
								}
								else if (array[num] == Strings.FunctionBlock)
								{
									flag2 = SearchNavigationForm.Mode == CBModeType.Offline;
								}
							}
							else
							{
								flag3 = SearchNavigationForm.Mode == CBModeType.Offline;
							}
						}
						BuildTree(text + text3.Replace("&#46;", "."), ref treeNode);
						if (num < array.Length)
						{
							SetTreeNodeIcon(treeNode, array[num]);
						}
						text = "";
					}
				}
				num++;
			}
			if (array[1] != Strings.Controllers)
			{
				if (item.navigationData.codeBlockType == "SFC")
				{
					BuildSFCTree(item, ref treeNode);
					NavigationDataClass navigationData = item.navigationData;
					navigationData.element = navigationData.element + "." + GetSearchForText();
				}
				else if (item.navigationData.attribute.StartsWith(Strings.ParRef, StringComparison.Ordinal))
				{
					string[] stringArray2 = item.refPath.Split('.');
					if (base.ParentForm is SearchNavigationForm)
					{
						SearchNavigationForm.StringArrayVersionFix(ref stringArray2);
					}
					StringBuilder stringBuilder = new StringBuilder("");
					if (item.refInstance.Length > 0)
					{
						stringBuilder.Append(item.refInstance);
						stringBuilder.Append("(..");
						stringBuilder.Append(stringArray2[stringArray2.Length - 1]);
						stringBuilder.Append("..)");
					}
					else
					{
						stringBuilder.Append(stringArray2[stringArray2.Length - 2]);
						stringBuilder.Append(".");
						stringBuilder.Append(stringArray2[stringArray2.Length - 1]);
					}
					if (item.navigationData.tab != Strings.CMGraphConn)
					{
						stringBuilder.Append(item.navigationData.RowPositionString());
					}
					if (item.navigationData.tab != Strings.CMConnections && item.navigationData.tab != Strings.CMGraphConn)
					{
						BuildTree(item.navigationData.tab + ", " + stringBuilder, ref treeNode);
					}
					else
					{
						BuildTree(stringBuilder.ToString(), ref treeNode);
					}
					if (item.navigationData.codeBlockType == "FBD" || item.navigationData.codeBlockType == "LD")
					{
						item.navigationData.element = symbol;
						int num2 = 0;
						for (TreeNode treeNode2 = treeNode; treeNode2 != null; treeNode2 = treeNode2.PrevNode)
						{
							if (treeNode2.Text.StartsWith(item.navigationData.tab, StringComparison.Ordinal))
							{
								num2++;
							}
						}
						item.navigationData.position = num2.ToString(CultureInfo.CurrentCulture);
					}
					if (item.navigationData.codeBlockType == "FD" && item.navigationData.path.Equals(item.navigationData.refPath.Substring(0, item.navigationData.path.Length)))
					{
						switch (guiType)
						{
						case "Parameter":
						case "Variable":
						case "CommunicationVariable":
						case "Function":
							if (item.navigationData.element.Length > 0)
							{
								string[] array3 = item.navigationData.element.Split(',');
								if (HasLeapNumber(array3[array3.GetUpperBound(0)]) || guiType == "Parameter")
								{
									symbolName = array3[array3.GetUpperBound(0)].Trim();
								}
							}
							break;
						}
						string element = item.navigationData.element;
						string text4 = element.Substring(0, element.IndexOf(','));
						item.navigationData.element = text4 + "," + item.navigationData.refPath.Substring(item.navigationData.path.Length + 1);
					}
				}
				else if (item.navigationData.attribute.StartsWith(Strings.CallLowercase, StringComparison.Ordinal))
				{
					if (item.navigationData.element.Length > 0)
					{
						string[] array4 = item.navigationData.element.Split(',');
						if (HasLeapNumber(array4[array4.GetUpperBound(0)]) || guiType == "Parameter")
						{
							symbolName = array4[array4.GetUpperBound(0)].Trim();
						}
						BuildTree(item.navigationData.tab + ", " + symbolName, ref treeNode);
					}
					else
					{
						BuildTree(item.navigationData.tab + item.navigationData.RowPositionString(), ref treeNode);
					}
				}
				else if (item.navigationData.tab.Length > 0)
				{
					if (!nodeIsDeclarationPaneTab(item.navigationData.tab) && !flag3 && !flag2)
					{
						BuildTree(item.navigationData.tab + item.navigationData.RowPositionString(), ref treeNode);
						if (item.navigationData.codeBlockType == "FBD" || item.navigationData.codeBlockType == "LD")
						{
							item.navigationData.element = symbol;
							int num3 = 0;
							for (TreeNode treeNode3 = treeNode; treeNode3 != null; treeNode3 = treeNode3.PrevNode)
							{
								if (treeNode3.Text.StartsWith(item.navigationData.tab, StringComparison.Ordinal))
								{
									num3++;
								}
							}
							item.navigationData.position = num3.ToString(CultureInfo.CurrentCulture);
						}
					}
					if (item.navigationData.codeBlockType == "ST")
					{
						item.navigationData.element = symbol;
					}
				}
			}
			treeNode.ForeColor = Color.Blue;
			if (flag2 || item.navigationData.attribute == Strings.ConnectionLowercase || (item.navigationData.attribute == Strings.InstanceLowercase && flag2))
			{
				SetTreeNodeIcon(treeNode, item.navigationData.guiType);
			}
			else if (flag3)
			{
				SetTreeNodeIcon(treeNode, array[array.Length - 1]);
			}
			else if (item.navigationData.attribute == Strings.IOChannelOut)
			{
				SetTreeNodeIcon(treeNode, Strings.ReadLowercase);
			}
			else if (item.navigationData.attribute == Strings.IOChannelIn)
			{
				SetTreeNodeIcon(treeNode, Strings.WriteLowercase);
			}
			else if (item.navigationData.guiType == Strings.StructComponent)
			{
				treeNode.Text += item.navigationData.RowPositionString();
				SetTreeNodeIcon(treeNode, Strings.Variable);
			}
			else if (item.navigationData.attribute == Strings.InstanceLowercase)
			{
				treeNode.Text += item.navigationData.RowPositionString();
				SetTreeNodeIcon(treeNode, array[array.Length - 1]);
			}
			else if (flag4)
			{
				SetTreeNodeIcon(treeNode, Strings.CMGraphConn);
			}
			else
			{
				SetTreeNodeIcon(treeNode, item.navigationData.attribute);
			}
			if (!string.IsNullOrEmpty(item.navigationData.tab) && guiType != Strings.DataType && guiType != Strings.FBType && guiType != Strings.ModType && item.navigationData.tab != Strings.CMGraphConn && item.navigationData.tab != Strings.CMConnections && guiType != Strings.IOChannelIn && guiType != Strings.IOChannelOut && guiType != Strings.IOUnit && guiType != Strings.AccessVariable)
			{
				NavigationDataClass navigationData2 = item.navigationData;
				navigationData2.path = navigationData2.path + "." + item.navigationData.tab + "." + symbolName + "." + item.navigationData.attribute;
				NavigationDataClass navigationData3 = item.navigationData;
				navigationData3.specification = navigationData3.specification + ".CodeBlock." + symbolGuiType + ".attribute";
			}
			treeNode.Tag = item.navigationData;
		}
		referencesTreeView.ExpandAll();
		referencesTreeView.EndUpdate();
	}

	private static bool HasLeapNumber(string name)
	{
		int num = name.LastIndexOf("(", StringComparison.Ordinal);
		if (num > 0)
		{
			int num2 = name.LastIndexOf(")", StringComparison.Ordinal);
			if (num2 > num)
			{
				string s = name.Substring(num + 1, num2 - num - 1);
				if (int.TryParse(s, out var _))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void SetTreeNodeIcon(TreeNode node, string iconTypeString)
	{
		node.ImageIndex = ((ViewCtrl)base.Parent).TypeImageIndex(iconTypeString);
		node.SelectedImageIndex = ((ViewCtrl)base.Parent).TypeImageIndex(iconTypeString);
	}

	private void BuildSFCTree(ReferencesDataClass rd, ref TreeNode startNode)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = true;
		string[] array = rd.navigationData.element.Split('@');
		foreach (string text in array)
		{
			if (flag3)
			{
				if (text == Strings.SeqStep)
				{
					flag2 = true;
				}
				if (text == Strings.SeqTrans)
				{
					flag = true;
				}
				flag3 = false;
			}
			else
			{
				rd.navigationData.element = text;
			}
		}
		if (flag2)
		{
			flag3 = true;
			string[] array2 = rd.navigationData.element.Split('.');
			foreach (string text2 in array2)
			{
				if (flag3)
				{
					BuildTree(rd.navigationData.tab + " ( " + text2 + " )", ref startNode);
					SetTreeNodeIcon(startNode, Strings.SeqStep);
					flag3 = false;
				}
				else
				{
					BuildTree(text2 + rd.navigationData.RowPositionString(), ref startNode);
				}
			}
		}
		else if (flag)
		{
			BuildTree(rd.navigationData.tab + " ( " + rd.navigationData.element + " )", ref startNode);
			SetTreeNodeIcon(startNode, Strings.SeqTrans);
			flag = false;
			BuildTree("Condition" + rd.navigationData.RowPositionString(), ref startNode);
		}
	}

	private void BuildHWTree(int index, ref string hwString, string nodeName, ref TreeNode startNode, ReferencesDataClass rd, string[] specificationStringArray)
	{
		if (index == 1)
		{
			hwString = "";
			BuildTree(nodeName, ref startNode);
			SetTreeNodeIcon(startNode, Strings.Controller);
			return;
		}
		if (index < specificationStringArray.Length - 2)
		{
			hwString = hwString + nodeName + ".";
			return;
		}
		if (index < specificationStringArray.Length - 1)
		{
			BuildTree(hwString + nodeName, ref startNode);
			SetTreeNodeIcon(startNode, Strings.IOUnit);
			return;
		}
		BuildTree(nodeName, ref startNode);
		if (rd.navigationData.guiType == Strings.AccessVariable)
		{
			SetTreeNodeIcon(startNode, Strings.AccessVariable);
			BuildTree(rd.navigationData.tab + rd.navigationData.RowPositionString(), ref startNode);
		}
	}

	private static void BuildTree(string nodeName, ref TreeNode currentNode)
	{
		TreeNode treeNode = GetTreeNode(currentNode, nodeName);
		if (treeNode == null)
		{
			currentNode.Nodes.Add(nodeName);
			currentNode = GetTreeNode(currentNode, nodeName);
			currentNode.ImageIndex = 99;
			currentNode.SelectedImageIndex = 99;
		}
		else
		{
			currentNode = treeNode;
		}
	}

	private static TreeNode GetTreeNode(TreeNode node, string nodeName)
	{
		node = node.FirstNode;
		while (node != null && node.Text != nodeName)
		{
			node = node.NextNode;
		}
		return node;
	}

	private TreeNode GetTreeNode(string nodeName)
	{
		TreeNode treeNode = null;
		if (referencesTreeView.Nodes.Count > 0)
		{
			treeNode = referencesTreeView.Nodes[0];
			while (treeNode != null && treeNode.Text != nodeName)
			{
				treeNode = treeNode.NextNode;
			}
		}
		return treeNode;
	}

	private TreeNode GetRootNode(string nodeName, ref bool newNodeCreated)
	{
		bool flag = false;
		TreeNode treeNode = GetTreeNode(nodeName);
		if (treeNode == null)
		{
			treeNode = new TreeNode(nodeName);
			if (nodeName == Strings.Libraries)
			{
				referencesTreeView.Nodes.Insert(0, treeNode);
				flag = true;
			}
			else if (nodeName == Strings.Applications)
			{
				if (flag)
				{
					referencesTreeView.Nodes.Insert(1, treeNode);
				}
				else
				{
					referencesTreeView.Nodes.Insert(0, treeNode);
				}
			}
			else if (nodeName == Strings.Controllers)
			{
				referencesTreeView.Nodes.Add(treeNode);
			}
			else
			{
				referencesTreeView.Nodes.Add(treeNode);
			}
			newNodeCreated = true;
		}
		return treeNode;
	}

	private bool nodeIsDeclarationPaneTab(string tabString)
	{
		bool result = false;
		if (tabString == Strings.Variables || tabString == Strings.Parameters || tabString == Strings.ExternalVariables || tabString == Strings.GlobalVariables || tabString == Strings.Components)
		{
			result = true;
		}
		return result;
	}

	private void referencesTreeView_DoubleClick(object sender, EventArgs e)
	{
		NavigationDataClass navigationDataClass = null;
		if (referencesTreeView.SelectedNode != null)
		{
			if (referencesTreeView.SelectedNode.Tag != null)
			{
				if (referencesTreeView.SelectedNode.Tag is NavigationDataClass)
				{
					navigationDataClass = (NavigationDataClass)referencesTreeView.SelectedNode.Tag;
				}
			}
			else
			{
				navigationDataClass = NavDataFromMiddleChild();
			}
		}
		if (navigationDataClass != null && base.ParentForm is SearchNavigationForm)
		{
			SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
			searchNavigationForm.GoToCBGui(navigationDataClass);
		}
	}

	private void gotoProjExpMenuItem_Click(object sender, EventArgs e)
	{
		NavigationDataClass navigationDataClass = null;
		if (referencesTreeView.SelectedNode.Tag != null)
		{
			if (referencesTreeView.SelectedNode.Tag is NavigationDataClass)
			{
				navigationDataClass = (NavigationDataClass)referencesTreeView.SelectedNode.Tag;
			}
		}
		else
		{
			navigationDataClass = NavDataFromMiddleChild();
		}
		if (navigationDataClass != null && base.ParentForm is SearchNavigationForm)
		{
			SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
			if (navigationDataClass != null)
			{
				searchNavigationForm.GoToCBProjectExplorer(navigationDataClass);
			}
		}
	}

	private NavigationDataClass NavDataFromMiddleChild()
	{
		NavigationDataClass result = null;
		if (!Strings.IsCollectionObject(referencesTreeView.SelectedNode.Text))
		{
			string fullPath = referencesTreeView.SelectedNode.FullPath;
			fullPath = fullPath.Replace("\\", ".");
			TreeNode treeNode = referencesTreeView.SelectedNode.FirstNode;
			if (treeNode != null)
			{
				while (treeNode.NextNode != null)
				{
					treeNode = treeNode.NextNode;
				}
				if (treeNode.Tag != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					string[] array = ((NavigationDataClass)treeNode.Tag).specification.Split('.');
					string[] array2 = fullPath.Split('.');
					int num = array2.Length;
					if (SearchNavigationForm.IsPathWithVersion(fullPath))
					{
						num--;
					}
					int num2 = 0;
					while (num2 < num)
					{
						if (num2 < array.Length)
						{
							stringBuilder.Append(array[num2]);
						}
						num2++;
						if (num2 < num)
						{
							stringBuilder.Append('.');
						}
					}
					if (treeNode.Tag is NavigationDataClass)
					{
						int num3 = num - 1;
						if (num3 >= array.Length)
						{
							num3 = array.Length - 1;
						}
						result = new NavigationDataClass(fullPath, stringBuilder.ToString(), "", "", "", "", array[num3], "");
					}
				}
			}
		}
		return result;
	}

	private static string RemoveHWChannelFromPath(ref string path)
	{
		StringBuilder stringBuilder = new StringBuilder("");
		string[] array = path.Split('.');
		int num = 0;
		while (num < array.Length - 1)
		{
			stringBuilder.Append(array[num]);
			num++;
			if (num < array.Length - 1)
			{
				stringBuilder.Append(".");
			}
		}
		path = stringBuilder.ToString();
		return array[array.Length - 1];
	}

	private void referencesTreeView_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			referencesTreeView.SelectedNode = referencesTreeView.GetNodeAt(e.X, e.Y);
		}
	}

	private void refCtrlContextMenu_Popup(object sender, EventArgs e)
	{
		if (!(base.ParentForm is SearchNavigationForm))
		{
			return;
		}
		if (referencesTreeView.SelectedNode != null)
		{
			if (!Strings.IsCollectionObject(referencesTreeView.SelectedNode.Text))
			{
				gotoProjExpMenuItem.Enabled = true;
				gotoEditorMenuItem.Enabled = true;
				searchMenuItem.Enabled = SearchNavigationForm.Mode == CBModeType.Offline;
			}
			else
			{
				gotoProjExpMenuItem.Enabled = false;
				gotoEditorMenuItem.Enabled = false;
				searchMenuItem.Enabled = SearchNavigationForm.Mode == CBModeType.Offline;
			}
		}
		else
		{
			gotoProjExpMenuItem.Enabled = false;
			gotoEditorMenuItem.Enabled = false;
			searchMenuItem.Enabled = false;
		}
	}

	private void searchMenuItem_Click(object sender, EventArgs e)
	{
		if (!(base.ParentForm is SearchNavigationForm))
		{
			return;
		}
		SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
		NavigationDataClass navigationDataClass = (NavigationDataClass)referencesTreeView.SelectedNode.Tag;
		string[] array = referencesTreeView.SelectedNode.Text.Split('(');
		string text = "";
		if (navigationDataClass == null && array.Length > 1)
		{
			text = array[1].Trim();
			text = text.TrimEnd(')', ' ');
			text = text.Trim();
		}
		else if (navigationDataClass != null)
		{
			text = navigationDataClass.search;
		}
		bool flag = false;
		if (SearchNavigationForm.Mode != 0 && string.Compare(symbolGuiType, Strings.StructComponent, StringComparison.Ordinal) == 0 && referencesTreeView.SelectedNode.FirstNode == null && string.Compare(text, "", StringComparison.Ordinal) != 0 && string.Compare(navigationDataClass.refInstance, "", StringComparison.Ordinal) != 0)
		{
			flag = true;
			string[] array2 = symbolSpecification.Split('.');
			string[] array3 = symbolPath.Split('.');
			int num = -1;
			for (int i = 0; i < array2.Length; i++)
			{
				if (string.Compare(array2[i], Strings.Parameter, StringComparison.Ordinal) == 0)
				{
					num = i;
					break;
				}
			}
			if (num > -1)
			{
				if (array3.Length == array2.Length + 1)
				{
					num++;
				}
				for (int j = num + 1; j < array3.Length; j++)
				{
					text = text + "." + array3[j];
				}
				text = text + "." + symbolName;
			}
		}
		searchNavigationForm.SetSearchFor(text);
		if (text.Length > 0)
		{
			searchNavigationForm.SearchButtonFocus();
		}
		if (referencesTreeView.SelectedNode.Parent != null)
		{
			text = referencesTreeView.SelectedNode.Parent.FullPath;
			text = text.Replace("\\", ".");
		}
		if (SearchNavigationForm.Mode != 0 && navigationDataClass != null && navigationDataClass.attribute.StartsWith(Strings.ParRef, StringComparison.Ordinal) && string.Compare(navigationDataClass.refInstance, "", StringComparison.Ordinal) == 0 && !flag)
		{
			int num2 = navigationDataClass.refPath.LastIndexOf(".", StringComparison.Ordinal);
			text = navigationDataClass.refPath.Remove(num2, navigationDataClass.refPath.Length - num2);
		}
		searchNavigationForm.SetSearchIn(text);
		if (searchNavigationForm.GetExecuteSearchInstantly)
		{
			searchNavigationForm.PerformSearch();
		}
	}

	private void referencesTreeView_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			referencesTreeView_DoubleClick(sender, e);
			e.Handled = true;
		}
	}

	private void referencesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
	{
		SetStatusBarText();
	}

	private void ReferencesCtrl_Enter(object sender, EventArgs e)
	{
		SetStatusBarText();
	}

	private void SetStatusBarText()
	{
		if (!(base.ParentForm is SearchNavigationForm))
		{
			return;
		}
		SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
		if (referencesTreeView.SelectedNode != null)
		{
			if (referencesTreeView.SelectedNode.Tag == null)
			{
				searchNavigationForm.SetStatusBarText(referencesTreeView.SelectedNode.Text, clearOld: true);
				return;
			}
			NavigationDataClass navigationDataClass = (NavigationDataClass)referencesTreeView.SelectedNode.Tag;
			string path = navigationDataClass.path;
			path = path + navigationDataClass.RowPositionString() + " : ";
			path = ((!(navigationDataClass.attribute == "parref_byref") && !(navigationDataClass.attribute == Strings.ByReferenceLowercase)) ? ((!(navigationDataClass.attribute == "parref_read") && !(navigationDataClass.attribute == Strings.ReadLowercase)) ? ((navigationDataClass.attribute == "parref_write" || navigationDataClass.attribute == Strings.WriteLowercase) ? (path + Strings.Write2) : ((navigationDataClass.attribute == Strings.CallLowercase) ? (path + Strings.Call2) : ((navigationDataClass.attribute == Strings.InstanceLowercase) ? (path + Strings.Instance2) : ((!(navigationDataClass.attribute == Strings.ConnectionLowercase)) ? (path + navigationDataClass.attribute) : (path + Strings.Connection2))))) : (path + Strings.Read2)) : (path + Strings.ByReference2));
			searchNavigationForm.SetStatusBarText(path, clearOld: true);
		}
	}

	private string GetSearchForText()
	{
		if (base.ParentForm is SearchNavigationForm)
		{
			SearchNavigationForm searchNavigationForm = (SearchNavigationForm)base.ParentForm;
			return searchNavigationForm.GetSearchForString;
		}
		return "";
	}

	public int CountHits()
	{
		int num = 0;
		if (referencesTreeView.Nodes.Count > 0)
		{
			for (TreeNode treeNode = referencesTreeView.Nodes[0]; treeNode != null; treeNode = treeNode.NextNode)
			{
				num = ((treeNode.FirstNode != null) ? (num + CountHitsInChild(treeNode)) : (num + 1));
			}
		}
		return num;
	}

	private int CountHitsInChild(TreeNode startNode)
	{
		int num = 0;
		if (startNode.Nodes.Count > 0)
		{
			for (TreeNode treeNode = startNode.FirstNode; treeNode != null; treeNode = treeNode.NextNode)
			{
				num = ((treeNode.FirstNode != null) ? (num + CountHitsInChild(treeNode)) : (num + 1));
			}
		}
		return num;
	}

	private static string StripVersionFromPath(string path)
	{
		string result = path;
		int num = path.IndexOf(" ", StringComparison.Ordinal);
		int num2 = path.IndexOf(".", num, StringComparison.Ordinal);
		int num3 = path.IndexOf(".", num2 + 1, StringComparison.Ordinal);
		if (num3 < 0)
		{
			num3 = path.Length;
		}
		if (num > -1 && num3 > -1)
		{
			result = path.Remove(num, num3 - num);
		}
		return result;
	}
}
