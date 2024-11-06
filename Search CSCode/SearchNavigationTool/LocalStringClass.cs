using System.Resources;

namespace SearchNavigationTool;

public class LocalStringClass
{
	private ResourceManager resources;

	private string m__all_;

	private string m_Access_Variables;

	private string m_Applications2;

	private string m_by_reference2;

	private string m_call2;

	private string m_connection2;

	private string m_Controllers2;

	private string m_Definition;

	private string m_Done;

	private string m_External_variables;

	private string m_Function_Blocks;

	private string m_Global_variables;

	private string m_instance2;

	private string m_Libraries2;

	private string m_No_of_Hits;

	private string m_No_of_Ref_Hits;

	private string m_parameters;

	private string m_read2;

	private string m_Rebuilding_Search_Data;

	private string m_References;

	private string m_ReferencesIterativeSearch;

	private string m_reportMsgBoxTitle;

	private string m_reportMsgBoxText;

	private string m_Searching;

	private string m_Symbol;

	private string m_Undefined2;

	private string m_variables;

	private string m_write2;

	private string m_tryToRebuild;

	private string m_AccessVariable = "AccessVariable";

	private string m_Application = "Application";

	private string m_ApplicationFolder = "ApplicationFolder";

	private string m_Applications = "Applications";

	private string m_Attribute = "Attribute";

	private string m_ByReferenceLowercase = "by reference";

	private string m_CallLowercase = "call";

	private string m_CMConnections = "CMConnections";

	private string m_CMGraphConn = "CMGraphConn";

	private string m_CommunicationVariable = "CommunicationVariable";

	private string m_Components = "Components";

	private string m_ConnectionLowercase = "connection";

	private string m_Controller = "Controller";

	private string m_Controllers = "Controllers";

	private string m_ControlModule = "ControlModule";

	private string m_cv_connection = "cv_connection";

	private string m_DefinitionLowercase = "definition";

	private string m_DataType = "DataType";

	private string m_Diagram = "Diagram";

	private string m_Diagrams = "Diagrams";

	private string m_DiagramType = "DiagramType";

	private string m_Element = "Element";

	private string m_ExtensibleParameter = "ExtensibleParameter";

	private string m_ExternalVariable = "ExternalVariable";

	private string m_FBType = "FBType";

	private string m_Function = "Function";

	private string m_FunctionBlock = "FunctionBlock";

	private string m_GlobalVariable = "GlobalVariable";

	private string m_HardwareUnit = "HardwareUnit";

	private string m_InstanceLowercase = "instance";

	private string m_IOChannelIn = "IOChannelIn";

	private string m_IOChannelOut = "IOChannelOut";

	private string m_IOUnit = "IOunit";

	private string m_Libraries = "Libraries";

	private string m_Library = "Library";

	private string m_ModType = "ModType";

	private string m_Name = "Name";

	private string m_Parameter = "Parameter";

	private string m_ParRef = "parref";

	private string m_Path = "Path";

	private string m_Position = "Position";

	private string m_ProjectConstant = "ProjectConstant";

	private string m_ProjectConstants = "ProjectConstants";

	private string m_Program = "Program";

	private string m_ReadLowercase = "read";

	private string m_RefAttribute = "RefAttribute";

	private string m_RefInstance = "RefInstance";

	private string m_Row = "Row";

	private string m_SeqMacro = "SeqMacro";

	private string m_SeqStep = "SeqStep";

	private string m_SeqTrans = "SeqTrans";

	private string m_Specification = "Specification";

	private string m_SingleDiagram = "SingleDiagram";

	private string m_SingleModType = "SingleModType";

	private string m_StructComponent = "StructComponent";

	private string m_Tab = "Tab";

	private string m_Type = "Type";

	private string m_Variable = "Variable";

	private string m_WriteLowercase = "write";

	public string All
	{
		get
		{
			if (m__all_ == null)
			{
				m__all_ = (string)resources.GetObject("_all_");
			}
			return m__all_;
		}
	}

	public string AccessVariable => m_AccessVariable;

	public string AccessVariables
	{
		get
		{
			if (m_Access_Variables == null)
			{
				m_Access_Variables = (string)resources.GetObject("Access_Variables");
			}
			return m_Access_Variables;
		}
	}

	public string Application => m_Application;

	public string ApplicationFolder => m_ApplicationFolder;

	public string Applications => m_Applications;

	public string Applications2
	{
		get
		{
			if (m_Applications2 == null)
			{
				m_Applications2 = (string)resources.GetObject("Applications2");
			}
			return m_Applications2;
		}
	}

	public string Attribute => m_Attribute;

	public string ByReferenceLowercase => m_ByReferenceLowercase;

	public string ByReference2
	{
		get
		{
			if (m_by_reference2 == null)
			{
				m_by_reference2 = (string)resources.GetObject("by_reference2");
			}
			return m_by_reference2;
		}
	}

	public string CallLowercase => m_CallLowercase;

	public string Call2
	{
		get
		{
			if (m_call2 == null)
			{
				m_call2 = (string)resources.GetObject("call2");
			}
			return m_call2;
		}
	}

	public string CMConnections => m_CMConnections;

	public string CMGraphConn => m_CMGraphConn;

	public string CommunicationVariable => m_CommunicationVariable;

	public string Components => m_Components;

	public string ConnectionLowercase => m_ConnectionLowercase;

	public string Connection2
	{
		get
		{
			if (m_connection2 == null)
			{
				m_connection2 = (string)resources.GetObject("connection2");
			}
			return m_connection2;
		}
	}

	public string ControlModule => m_ControlModule;

	public string Controller => m_Controller;

	public string Controllers => m_Controllers;

	public string Controllers2
	{
		get
		{
			if (m_Controllers2 == null)
			{
				m_Controllers2 = (string)resources.GetObject("Controllers");
			}
			return m_Controllers2;
		}
	}

	public string CVConnection => m_cv_connection;

	public string DataType => m_DataType;

	public string Diagram => m_Diagram;

	public string Diagrams => m_Diagrams;

	public string DiagramType => m_DiagramType;

	public string Definition
	{
		get
		{
			if (m_Definition == null)
			{
				m_Definition = (string)resources.GetObject("Definition");
			}
			return m_Definition;
		}
	}

	public string DefinitionLowercase => m_DefinitionLowercase;

	public string Done
	{
		get
		{
			if (m_Done == null)
			{
				m_Done = (string)resources.GetObject("Done");
			}
			return m_Done;
		}
	}

	public string Element => m_Element;

	public string ExtensibleParameter => m_ExtensibleParameter;

	public string ExternalVariable => m_ExternalVariable;

	public string ExternalVariables
	{
		get
		{
			if (m_External_variables == null)
			{
				m_External_variables = (string)resources.GetObject("External_variables");
			}
			return m_External_variables;
		}
	}

	public string FBType => m_FBType;

	public string Function => m_Function;

	public string FunctionBlock => m_FunctionBlock;

	public string FunctionBlocks
	{
		get
		{
			if (m_Function_Blocks == null)
			{
				m_Function_Blocks = (string)resources.GetObject("Function_Blocks");
			}
			return m_Function_Blocks;
		}
	}

	public string GlobalVariable => m_GlobalVariable;

	public string GlobalVariables
	{
		get
		{
			if (m_Global_variables == null)
			{
				m_Global_variables = (string)resources.GetObject("Global_variables");
			}
			return m_Global_variables;
		}
	}

	public string HardwareUnit => m_HardwareUnit;

	public string InstanceLowercase => m_InstanceLowercase;

	public string Instance2
	{
		get
		{
			if (m_instance2 == null)
			{
				m_instance2 = (string)resources.GetObject("instance2");
			}
			return m_instance2;
		}
	}

	public string IOChannelIn => m_IOChannelIn;

	public string IOChannelOut => m_IOChannelOut;

	public string IOUnit => m_IOUnit;

	public string Libraries => m_Libraries;

	public string Library => m_Library;

	public string Libraries2
	{
		get
		{
			if (m_Libraries2 == null)
			{
				m_Libraries2 = (string)resources.GetObject("Libraries2");
			}
			return m_Libraries2;
		}
	}

	public string ModType => m_ModType;

	public string Name => m_Name;

	public string NoOfHits
	{
		get
		{
			if (m_No_of_Hits == null)
			{
				m_No_of_Hits = (string)resources.GetObject("No_of_Hits");
			}
			return m_No_of_Hits;
		}
	}

	public string NoOfRefHits
	{
		get
		{
			if (m_No_of_Ref_Hits == null)
			{
				m_No_of_Ref_Hits = (string)resources.GetObject("No_of_Ref_Hits");
			}
			return m_No_of_Ref_Hits;
		}
	}

	public string Parameter => m_Parameter;

	public string Parameters
	{
		get
		{
			if (m_parameters == null)
			{
				m_parameters = (string)resources.GetObject("Parameters");
			}
			return m_parameters;
		}
	}

	public string ParRef => m_ParRef;

	public string Path => m_Path;

	public string Position => m_Position;

	public string Program => m_Program;

	public string ProjectConstant => m_ProjectConstant;

	public string ProjectConstants => m_ProjectConstants;

	public string ReadLowercase => m_ReadLowercase;

	public string Read2
	{
		get
		{
			if (m_read2 == null)
			{
				m_read2 = (string)resources.GetObject("read2");
			}
			return m_read2;
		}
	}

	public string ReportMsgBoxTitle
	{
		get
		{
			if (m_reportMsgBoxTitle == null)
			{
				m_reportMsgBoxTitle = (string)resources.GetObject("ReportMsgBoxTitle");
			}
			return m_reportMsgBoxTitle;
		}
	}

	public string ReportMsgBoxText
	{
		get
		{
			if (m_reportMsgBoxText == null)
			{
				m_reportMsgBoxText = (string)resources.GetObject("ReportMsgBoxText");
			}
			return m_reportMsgBoxText;
		}
	}

	public string RebuildingSearchData
	{
		get
		{
			if (m_Rebuilding_Search_Data == null)
			{
				m_Rebuilding_Search_Data = (string)resources.GetObject("Rebuilding_Search_Data");
			}
			return m_Rebuilding_Search_Data;
		}
	}

	public string RefAttribute => m_RefAttribute;

	public string RefInstance => m_RefInstance;

	public string References
	{
		get
		{
			if (m_References == null)
			{
				m_References = (string)resources.GetObject("References");
			}
			return m_References;
		}
	}

	public string ReferencesIterativeSearch
	{
		get
		{
			if (m_ReferencesIterativeSearch == null)
			{
				m_ReferencesIterativeSearch = (string)resources.GetObject("References_iterative_search");
			}
			return m_ReferencesIterativeSearch;
		}
	}

	public string Row => m_Row;

	public string Searching
	{
		get
		{
			if (m_Searching == null)
			{
				m_Searching = (string)resources.GetObject("Searching");
			}
			return m_Searching;
		}
	}

	public string SeqMacro => m_SeqMacro;

	public string SeqStep => m_SeqStep;

	public string SeqTrans => m_SeqTrans;

	public string SingleDiagram => m_SingleDiagram;

	public string SingleModType => m_SingleModType;

	public string Specification => m_Specification;

	public string StructComponent => m_StructComponent;

	public string Symbol
	{
		get
		{
			if (m_Symbol == null)
			{
				m_Symbol = (string)resources.GetObject("Symbol");
			}
			return m_Symbol;
		}
	}

	public string Tab => m_Tab;

	public string Undefined2
	{
		get
		{
			if (m_Undefined2 == null)
			{
				m_Undefined2 = (string)resources.GetObject("Undefined2");
			}
			return "< " + m_Undefined2 + " >";
		}
	}

	public string Variable => m_Variable;

	public string Variables
	{
		get
		{
			if (m_variables == null)
			{
				m_variables = (string)resources.GetObject("Variables");
			}
			return m_variables;
		}
	}

	public string WriteLowercase => m_WriteLowercase;

	public string Write2
	{
		get
		{
			if (m_write2 == null)
			{
				m_write2 = (string)resources.GetObject("write2");
			}
			return m_write2;
		}
	}

	public string Type => m_Type;

	public string TryToRebuild
	{
		get
		{
			if (m_tryToRebuild == null)
			{
				m_tryToRebuild = (string)resources.GetObject("TryToRebuild");
			}
			return m_tryToRebuild;
		}
	}

	public LocalStringClass()
	{
		resources = new ResourceManager(typeof(LocalStringClass));
	}

	public bool IsCollectionObject(string value)
	{
		if (value == ProjectConstants || value == Applications || value == Controllers || value == Libraries)
		{
			return true;
		}
		return false;
	}
}
