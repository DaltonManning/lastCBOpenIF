using System.Runtime.InteropServices;

namespace SearchNavigationTool;

[ComVisible(false)]
public class ReferencesDataClass
{
	internal string specification;

	internal string refPath;

	internal string refInstance;

	internal NavigationDataClass navigationData;

	public ReferencesDataClass(string attribute, string path, string specification, string refPath, string refInstance, string tab, string row, string position, string element, string guiType, string search, string codeBlockType)
	{
		this.specification = specification;
		this.refPath = refPath;
		this.refInstance = refInstance;
		navigationData = new NavigationDataClass(attribute, path, specification, tab, row, position, element, guiType, search, refInstance, codeBlockType, refPath);
	}

	public override string ToString()
	{
		string text = " : ";
		string text2 = navigationData.guiType + text;
		text2 = text2 + navigationData.attribute + text;
		text2 = text2 + navigationData.path + text;
		text2 = text2 + navigationData.tab + text;
		text2 = text2 + navigationData.row + text;
		text2 = text2 + navigationData.position + text;
		return text2 + navigationData.element;
	}
}
