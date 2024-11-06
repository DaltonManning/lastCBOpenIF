using System;
using System.Runtime.Serialization;

namespace SearchNavigationTool;

[Serializable]
public class SNXmlException : GeneralSNComponentException
{
	public SNXmlException()
	{
	}

	public SNXmlException(string value)
		: base(value)
	{
	}

	public SNXmlException(string value, Exception exception)
		: base(value, exception)
	{
	}

	protected SNXmlException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		: base(serializationInfo, streamingContext)
	{
	}
}
