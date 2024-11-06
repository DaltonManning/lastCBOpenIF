using System.Collections;
using System.Runtime.InteropServices;

namespace SearchNavigationTool;

[ComVisible(false)]
public class ReferencesDataCollection : IEnumerable
{
	private ArrayList referencesDataList;

	public int ReferencesDataCount => referencesDataList.Count;

	public ReferencesDataCollection()
	{
		referencesDataList = new ArrayList();
	}

	public void AddReferencesData(ReferencesDataClass rd)
	{
		referencesDataList.Add(rd);
	}

	public void RemoveSymbolData(int referencesDataToRemove)
	{
		referencesDataList.RemoveAt(referencesDataToRemove);
	}

	public void ClearReferencesDataCollection()
	{
		referencesDataList.Clear();
	}

	public IEnumerator GetEnumerator()
	{
		return referencesDataList.GetEnumerator();
	}
}
