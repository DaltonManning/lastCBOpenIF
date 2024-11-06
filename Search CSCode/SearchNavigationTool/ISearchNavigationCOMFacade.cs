using System.Runtime.InteropServices;

namespace SearchNavigationTool;

[Guid("002E90D0-5AB2-497a-BD93-996E194FD43D")]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface ISearchNavigationCOMFacade
{
	void Open(string symbol, string path, CBModeType mode, string searchResult);

	void Enable(bool enableGui);

	void SetMode(CBModeType mode);

	void CloseSNForm();

	void CloseCBConnection();
}
