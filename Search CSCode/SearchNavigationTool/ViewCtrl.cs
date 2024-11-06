using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SearchNavigationTool;

[ComVisible(false)]
public class ViewCtrl : UserControl
{
	private enum IconIndexType
	{
		Variable,
		Parameter,
		GlobalVariable,
		ExternalVariable,
		ExtensibleParameter,
		CommunicationVariable,
		Signal,
		DataType,
		SeqStep,
		SeqTrans,
		SeqMacro,
		ProjectConstant,
		FBType,
		FunctionBlock,
		Function,
		Program,
		ControlModule,
		ModType,
		Diagrams,
		Diagram,
		DiagramType,
		Project,
		Libraries,
		Library,
		Applications,
		Application,
		Controllers,
		Controller,
		definition,
		read,
		write,
		byref,
		call,
		CMGraphConn,
		parref,
		parref_read,
		parref_write,
		parref_byref,
		IOChannelIn,
		IOChannelOut,
		IOunit,
		AccessVariable,
		Undefined,
		NotInCB,
		none
	}

	private SymbolCtrl viewSymbolCtrl;

	private Splitter viewSplitter;

	private ImageList iconImageList;

	private ReferencesCtrl viewReferencesCtrl;

	private IContainer components;

	private LocalStringClass Strings => ((SearchNavigationForm)base.ParentForm).Strings;

	public ImageList IconImageList => iconImageList;

	public int GetSplitPosition => viewSplitter.SplitPosition;

	public string SelectedSymbolString => viewSymbolCtrl.SelectedSymbolString();

	public string SelectedSymbolDefinitionString => viewSymbolCtrl.SelectedSymbolDefinitionString();

	public int GetNoHitsInRefCtrl => viewReferencesCtrl.CountHits();

	public ViewCtrl()
	{
		InitializeComponent();
	}

	public void InitViewCtrl()
	{
		viewSymbolCtrl.SetColumnNames();
	}

	public int TypeImageIndex(string symbolType)
	{
		if (symbolType == null)
		{
			throw new ArgumentNullException("symbolType");
		}
		int num = 99;
		if (symbolType == Strings.Variable)
		{
			return 0;
		}
		if (symbolType == Strings.Parameter)
		{
			return 1;
		}
		if (symbolType == Strings.GlobalVariable)
		{
			return 2;
		}
		if (symbolType == Strings.ExternalVariable)
		{
			return 3;
		}
		if (symbolType == Strings.ExtensibleParameter)
		{
			return 4;
		}
		if (symbolType == Strings.CommunicationVariable)
		{
			return 5;
		}
		if (symbolType == Strings.DataType)
		{
			return 7;
		}
		if (symbolType == Strings.SeqStep)
		{
			return 8;
		}
		if (symbolType == Strings.SeqTrans)
		{
			return 9;
		}
		if (symbolType == Strings.SeqMacro)
		{
			return 10;
		}
		if (symbolType == Strings.StructComponent)
		{
			return 7;
		}
		if (symbolType == Strings.ProjectConstants)
		{
			return 11;
		}
		if (symbolType == Strings.ProjectConstant)
		{
			return 11;
		}
		if (symbolType == Strings.FBType)
		{
			return 12;
		}
		if (symbolType == Strings.FunctionBlock)
		{
			return 13;
		}
		if (symbolType == Strings.Function)
		{
			return 14;
		}
		if (symbolType == Strings.Program)
		{
			return 15;
		}
		if (symbolType == Strings.ControlModule)
		{
			return 16;
		}
		if (symbolType == Strings.SingleModType)
		{
			return 16;
		}
		if (symbolType == Strings.ModType)
		{
			return 17;
		}
		if (symbolType == Strings.Libraries)
		{
			return 22;
		}
		if (symbolType == Strings.Library)
		{
			return 23;
		}
		if (symbolType == Strings.Applications || symbolType == Strings.ApplicationFolder)
		{
			return 24;
		}
		if (symbolType == Strings.Application)
		{
			return 25;
		}
		if (symbolType == Strings.Controllers)
		{
			return 26;
		}
		if (symbolType == Strings.Controller)
		{
			return 27;
		}
		if (symbolType == Strings.Diagram)
		{
			return 19;
		}
		if (symbolType == Strings.DiagramType)
		{
			return 20;
		}
		if (symbolType == Strings.SingleDiagram)
		{
			return 19;
		}
		if (symbolType == "Project")
		{
			return 21;
		}
		if (symbolType == Strings.DefinitionLowercase)
		{
			return 28;
		}
		if (symbolType == Strings.ConnectionLowercase)
		{
			return 28;
		}
		if (symbolType == Strings.InstanceLowercase)
		{
			return 28;
		}
		if (symbolType == Strings.ReadLowercase)
		{
			return 29;
		}
		if (symbolType == Strings.WriteLowercase)
		{
			return 30;
		}
		if (symbolType == "byref")
		{
			return 31;
		}
		if (symbolType == Strings.CallLowercase)
		{
			return 32;
		}
		if (symbolType == Strings.CMGraphConn)
		{
			return 33;
		}
		switch (symbolType)
		{
		case "parref":
			return 34;
		case "parref_read":
			return 29;
		case "parref_write":
			return 30;
		case "parref_byref":
			return 31;
		default:
			if (symbolType == Strings.IOChannelOut)
			{
				return 39;
			}
			if (symbolType == Strings.IOChannelIn)
			{
				return 38;
			}
			if (symbolType == Strings.IOUnit)
			{
				return 40;
			}
			if (symbolType == Strings.AccessVariable)
			{
				return 41;
			}
			if (symbolType == "Undefined")
			{
				return 42;
			}
			if (symbolType.Length > 0 && symbolType[0] == 'I' && symbolType[1] == 'O')
			{
				return 40;
			}
			return symbolType switch
			{
				"HardwareUnit" => 40, 
				"cv_connection" => 5, 
				"Signal" => 6, 
				_ => 44, 
			};
		}
	}

	public void SetSymbolData(SymbolDataCollection symbolDataCollection)
	{
		viewSymbolCtrl.SetData(symbolDataCollection);
		viewSymbolCtrl.SetDefinitionColumnWidth(viewSplitter.SplitPosition);
	}

	public void SetReferencesData(ReferencesDataCollection referenceCollection, string symbol, string path, string specification, string guiType, bool iterativeSearch)
	{
		if (iterativeSearch)
		{
			viewReferencesCtrl.referencesLabel.Text = Strings.ReferencesIterativeSearch;
		}
		else
		{
			viewReferencesCtrl.referencesLabel.Text = Strings.References;
		}
		viewReferencesCtrl.SetData(referenceCollection, symbol, path, specification, guiType);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	public void Clear()
	{
		viewSymbolCtrl.Clear();
		viewReferencesCtrl.ClearTree();
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchNavigationTool.ViewCtrl));
		this.viewSplitter = new System.Windows.Forms.Splitter();
		this.iconImageList = new System.Windows.Forms.ImageList(this.components);
		this.viewReferencesCtrl = new SearchNavigationTool.ReferencesCtrl();
		this.viewSymbolCtrl = new SearchNavigationTool.SymbolCtrl();
		base.SuspendLayout();
		this.viewSplitter.Location = new System.Drawing.Point(296, 0);
		this.viewSplitter.Name = "viewSplitter";
		this.viewSplitter.Size = new System.Drawing.Size(3, 600);
		this.viewSplitter.TabIndex = 1;
		this.viewSplitter.TabStop = false;
		this.viewSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(viewSplitter_SplitterMoved);
		this.iconImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iconImageList.ImageStream");
		this.iconImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.iconImageList.Images.SetKeyName(0, "variable.ico");
		this.iconImageList.Images.SetKeyName(1, "parameter.ico");
		this.iconImageList.Images.SetKeyName(2, "globalvariable.ico");
		this.iconImageList.Images.SetKeyName(3, "externalvariable.ico");
		this.iconImageList.Images.SetKeyName(4, "extensibleparameter.ico");
		this.iconImageList.Images.SetKeyName(5, "CommunicationVariable.ico");
		this.iconImageList.Images.SetKeyName(6, "Signal.ico");
		this.iconImageList.Images.SetKeyName(7, "datatype.ico");
		this.iconImageList.Images.SetKeyName(8, "step.ico");
		this.iconImageList.Images.SetKeyName(9, "transition.ico");
		this.iconImageList.Images.SetKeyName(10, "seqmacro.ico");
		this.iconImageList.Images.SetKeyName(11, "projconst.ico");
		this.iconImageList.Images.SetKeyName(12, "fb_type.ico");
		this.iconImageList.Images.SetKeyName(13, "fb.ico");
		this.iconImageList.Images.SetKeyName(14, "function.ico");
		this.iconImageList.Images.SetKeyName(15, "program.ico");
		this.iconImageList.Images.SetKeyName(16, "modul.ico");
		this.iconImageList.Images.SetKeyName(17, "module_type.ico");
		this.iconImageList.Images.SetKeyName(18, "Diagrams.ico");
		this.iconImageList.Images.SetKeyName(19, "Diagram.ico");
		this.iconImageList.Images.SetKeyName(20, "DiagramType.ico");
		this.iconImageList.Images.SetKeyName(21, "Project.ico");
		this.iconImageList.Images.SetKeyName(22, "libraries.ico");
		this.iconImageList.Images.SetKeyName(23, "library.ico");
		this.iconImageList.Images.SetKeyName(24, "Applications.ico");
		this.iconImageList.Images.SetKeyName(25, "Application.ico");
		this.iconImageList.Images.SetKeyName(26, "Systems.ico");
		this.iconImageList.Images.SetKeyName(27, "System.ico");
		this.iconImageList.Images.SetKeyName(28, "definition.ico");
		this.iconImageList.Images.SetKeyName(29, "read.ico");
		this.iconImageList.Images.SetKeyName(30, "write.ico");
		this.iconImageList.Images.SetKeyName(31, "byreference.ico");
		this.iconImageList.Images.SetKeyName(32, "call.ico");
		this.iconImageList.Images.SetKeyName(33, "graphconn.ico");
		this.iconImageList.Images.SetKeyName(34, "parref.ico");
		this.iconImageList.Images.SetKeyName(35, "parread.ico");
		this.iconImageList.Images.SetKeyName(36, "parwrite.ico");
		this.iconImageList.Images.SetKeyName(37, "parbyref.ico");
		this.iconImageList.Images.SetKeyName(38, "IOread.ico");
		this.iconImageList.Images.SetKeyName(39, "IOwrite.ico");
		this.iconImageList.Images.SetKeyName(40, "s800io.ico");
		this.iconImageList.Images.SetKeyName(41, "Protocol.ico");
		this.iconImageList.Images.SetKeyName(42, "undefined.ico");
		this.iconImageList.Images.SetKeyName(43, "");
		this.viewReferencesCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.viewReferencesCtrl.Location = new System.Drawing.Point(299, 0);
		this.viewReferencesCtrl.Name = "viewReferencesCtrl";
		this.viewReferencesCtrl.Size = new System.Drawing.Size(519, 600);
		this.viewReferencesCtrl.TabIndex = 2;
		this.viewSymbolCtrl.Dock = System.Windows.Forms.DockStyle.Left;
		this.viewSymbolCtrl.Location = new System.Drawing.Point(0, 0);
		this.viewSymbolCtrl.Name = "viewSymbolCtrl";
		this.viewSymbolCtrl.Size = new System.Drawing.Size(296, 600);
		this.viewSymbolCtrl.TabIndex = 0;
		base.Controls.Add(this.viewReferencesCtrl);
		base.Controls.Add(this.viewSplitter);
		base.Controls.Add(this.viewSymbolCtrl);
		base.Name = "ViewCtrl";
		base.Size = new System.Drawing.Size(818, 600);
		base.SizeChanged += new System.EventHandler(ViewCtrl_SizeChanged);
		base.ResumeLayout(false);
	}

	private void viewSplitter_SplitterMoved(object sender, SplitterEventArgs e)
	{
		viewSymbolCtrl.SetDefinitionColumnWidth(((Splitter)sender).SplitPosition);
	}

	private void ViewCtrl_SizeChanged(object sender, EventArgs e)
	{
		viewSymbolCtrl.SetDefinitionColumnWidth(viewSplitter.SplitPosition);
	}

	internal void SetMode(CBModeType mode)
	{
		if (mode == CBModeType.OnlineIterative || mode == CBModeType.TestModeIterative || mode == CBModeType.OnlineIterativeSearchFieldsDisabled || mode == CBModeType.TestModeIterativeSearchFieldsDisabled)
		{
			viewSymbolCtrl.Visible = false;
		}
		else
		{
			viewSymbolCtrl.Visible = true;
		}
	}
}
