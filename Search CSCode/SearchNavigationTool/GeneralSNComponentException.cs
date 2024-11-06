using System;
using System.Runtime.Serialization;

namespace SearchNavigationTool;

[Serializable]
public class GeneralSNComponentException : Exception
{
	public GeneralSNComponentException()
	{
	}

	public GeneralSNComponentException(string value)
		: base(value)
	{
	}

	public GeneralSNComponentException(string value, Exception exception)
		: base(value, exception)
	{
	}

	protected GeneralSNComponentException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		: base(serializationInfo, streamingContext)
	{
	}
}
