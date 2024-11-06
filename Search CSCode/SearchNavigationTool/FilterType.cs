using System.Runtime.InteropServices;

namespace SearchNavigationTool;

[ComVisible(false)]
public enum FilterType
{
	NoFilter,
	Read,
	Write,
	IOChannelOut,
	IOChannelIn
}
