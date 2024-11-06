using System.Runtime.InteropServices;

namespace SearchNavigationTool;

[ComVisible(false)]
public class NavigationDataClass
{
	internal string attribute;

	internal string path;

	internal string specification;

	internal string tab;

	internal string row;

	internal string position;

	internal string element;

	internal string guiType;

	internal string search;

	internal string refInstance;

	internal string codeBlockType;

	internal string refPath;

	public NavigationDataClass(string path, string specification, string tab, string row, string position, string element, string guiType, string search)
	{
		attribute = "";
		this.path = path;
		this.specification = specification;
		this.tab = tab;
		this.row = row;
		this.position = position;
		this.element = element;
		this.guiType = guiType;
		this.search = search;
		refInstance = "";
	}

	public NavigationDataClass(string attribute, string path, string specification, string tab, string row, string position, string element, string guiType, string search, string refInstance, string codeBlockType, string refPath)
	{
		this.attribute = attribute;
		this.path = path;
		this.specification = specification;
		this.tab = tab;
		this.row = row;
		this.position = position;
		this.element = element;
		this.guiType = guiType;
		this.search = search;
		this.refInstance = refInstance;
		this.codeBlockType = codeBlockType;
		this.refPath = refPath;
	}

	public string RowPositionString()
	{
		string text = "";
		if (row.Length > 0)
		{
			text = text + " ( " + row;
			if (codeBlockType != "FBD" && codeBlockType != "LD" && position.Length > 0 && position != "-1")
			{
				text = text + ", " + position;
			}
			text += " )";
		}
		return text;
	}
}
