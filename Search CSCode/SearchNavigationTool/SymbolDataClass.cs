using System.Runtime.InteropServices;

namespace SearchNavigationTool;

[ComVisible(false)]
public class SymbolDataClass
{
	internal string symbol;

	internal string type;

	internal NavigationDataClass navigationData;

	public SymbolDataClass(string symbol, string type, string path, string specification, string tab, string row, string position, string element, string guiType)
	{
		this.symbol = symbol;
		this.type = type;
		navigationData = new NavigationDataClass(path, specification, tab, row, position, element, guiType, "");
	}

	public override string ToString()
	{
		string text = " : ";
		string text2 = navigationData.guiType + text;
		text2 = text2 + symbol + text;
		text2 = text2 + navigationData.path + text;
		text2 = text2 + navigationData.tab + text;
		text2 = text2 + navigationData.row + text;
		text2 = text2 + navigationData.position + text;
		return text2 + navigationData.element;
	}
}
