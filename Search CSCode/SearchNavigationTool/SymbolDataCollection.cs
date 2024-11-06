using System.Collections;
using System.Runtime.InteropServices;

namespace SearchNavigationTool;

[ComVisible(false)]
public class SymbolDataCollection : IEnumerable
{
	private ArrayList symbolDataList;

	public int SymbolDataCount => symbolDataList.Count;

	public SymbolDataCollection()
	{
		symbolDataList = new ArrayList();
	}

	public void AddSymbolData(SymbolDataClass sd)
	{
		symbolDataList.Add(sd);
	}

	public void RemoveSymbolData(int symbolDataToRemove)
	{
		symbolDataList.RemoveAt(symbolDataToRemove);
	}

	public void ClearSymbolDataCollection()
	{
		symbolDataList.Clear();
	}

	public IEnumerator GetEnumerator()
	{
		return symbolDataList.GetEnumerator();
	}
}
