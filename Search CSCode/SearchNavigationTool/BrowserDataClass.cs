using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace SearchNavigationTool;

[ComVisible(false)]
public class BrowserDataClass
{
	private XmlDocument xmlDoc;

	private LocalStringClass Strings;

	private bool SymbolAscending = true;

	private bool DefinitionAscending = true;

	private bool SortOnDefinition;

	public XmlDocument XmlDoc => xmlDoc;

	public BrowserDataClass(LocalStringClass strings)
	{
		xmlDoc = new XmlDocument();
		Strings = strings;
	}

	public void SetXml(string xmlValue)
	{
		xmlDoc.InnerXml = xmlValue;
		if (SortOnDefinition)
		{
			SortXML(DefinitionAscending, sortOnDefinition: true);
		}
		else
		{
			SortXML(SymbolAscending, sortOnDefinition: false);
		}
	}

	public int GetSymbolCollection(SymbolDataCollection symbolDataList, FilterType filter)
	{
		if (symbolDataList == null)
		{
			throw new ArgumentNullException("symbolDataList");
		}
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string specification = "";
		string text6 = "";
		int num = 0;
		XmlNode xmlNode = xmlDoc.SelectSingleNode(".//Symbols");
		XmlNode xmlNode2 = null;
		XmlNode xmlNode3 = xmlNode.FirstChild;
		while (xmlNode3 != null)
		{
			text = "";
			text2 = "";
			text3 = "";
			text4 = "";
			text5 = "";
			text6 = "";
			if (xmlNode3.Attributes[Strings.Type] != null)
			{
				text6 = xmlNode3.Attributes[Strings.Type].Value;
			}
			xmlNode2 = xmlNode3.SelectSingleNode("./Definition");
			if (xmlNode2 != null)
			{
				text5 = xmlNode2.Attributes[Strings.Path].Value;
				if (xmlNode2.Attributes[Strings.Specification] != null)
				{
					specification = xmlNode2.Attributes[Strings.Specification].Value;
				}
				xmlNode2 = xmlNode2.SelectSingleNode("./Location");
				if (xmlNode2 != null)
				{
					if (xmlNode2.Attributes[Strings.Tab] != null)
					{
						text = xmlNode2.Attributes[Strings.Tab].Value;
					}
					if (xmlNode2.Attributes[Strings.Row] != null)
					{
						text2 = xmlNode2.Attributes[Strings.Row].Value;
					}
					if (xmlNode2.Attributes[Strings.Position] != null)
					{
						text3 = xmlNode2.Attributes[Strings.Position].Value;
					}
					if (xmlNode2.Attributes[Strings.Element] != null)
					{
						text4 = xmlNode2.Attributes[Strings.Element].Value;
					}
				}
			}
			bool flag = FilterCollectionNode(xmlNode3, filter);
			XmlNode oldChild = xmlNode3;
			if (flag)
			{
				symbolDataList.AddSymbolData(new SymbolDataClass(xmlNode3.Attributes[Strings.Name].Value, xmlNode3.Attributes[Strings.Type].Value, text5, specification, text, text2, text3, text4, text6));
				num++;
			}
			xmlNode3 = xmlNode3.NextSibling;
			if (!flag)
			{
				xmlNode.RemoveChild(oldChild);
			}
		}
		return num;
	}

	private bool FilterSingleNode(XmlNode node, FilterType filter, bool isSymbolBool)
	{
		if (filter == FilterType.NoFilter)
		{
			return true;
		}
		bool result = false;
		if (node.Name == "Reference" && node.Attributes[Strings.Attribute] != null)
		{
			switch (filter)
			{
			case FilterType.Read:
				if (node.Attributes[Strings.Attribute].Value == Strings.ReadLowercase || node.Attributes[Strings.Attribute].Value == Strings.ParRef || node.Attributes[Strings.Attribute].Value == Strings.IOChannelOut)
				{
					result = true;
				}
				break;
			case FilterType.Write:
				if (node.Attributes[Strings.Attribute].Value == Strings.WriteLowercase || node.Attributes[Strings.Attribute].Value == Strings.ParRef || node.Attributes[Strings.Attribute].Value == Strings.IOChannelIn)
				{
					result = true;
				}
				break;
			case FilterType.IOChannelIn:
				if (!isSymbolBool || node.Attributes[Strings.Attribute].Value == Strings.IOChannelIn || node.Attributes[Strings.Attribute].Value == Strings.ParRef)
				{
					result = true;
				}
				break;
			default:
				if ((!isSymbolBool || filter == FilterType.IOChannelOut) && (node.Attributes[Strings.Attribute].Value == Strings.IOChannelOut || node.Attributes[Strings.Attribute].Value == Strings.ParRef))
				{
					result = true;
				}
				break;
			}
		}
		return result;
	}

	private bool FilterCollectionNode(XmlNode node, FilterType filter)
	{
		if (filter == FilterType.NoFilter)
		{
			return true;
		}
		bool flag = false;
		XmlNode xmlNode = null;
		if (node.Name == "Symbol")
		{
			if ((filter == FilterType.IOChannelIn || filter == FilterType.IOChannelOut) && node.Attributes[Strings.Type] != null)
			{
				if (node.Attributes[Strings.Type].Value == Strings.IOChannelIn)
				{
					flag = filter == FilterType.IOChannelIn;
				}
				else if (node.Attributes[Strings.Type].Value == Strings.IOChannelOut)
				{
					flag = filter == FilterType.IOChannelOut;
				}
			}
			if (!flag)
			{
				XmlNode xmlNode2 = node.SelectSingleNode("./References");
				if (xmlNode2 != null)
				{
					xmlNode = xmlNode2.SelectSingleNode(".//Reference");
					while (xmlNode != null)
					{
						bool flag2 = FilterSingleNode(xmlNode, filter, node.Name == "Symbol");
						flag = flag || flag2;
						XmlNode oldChild = xmlNode;
						xmlNode = xmlNode.NextSibling;
						if (!flag2)
						{
							xmlNode2.RemoveChild(oldChild);
						}
					}
				}
			}
		}
		return flag;
	}

	internal static string FindSymbolXPath(string symbol, string path, string guiType)
	{
		string text = ".//Symbol[@Name = \"";
		text += symbol;
		if (guiType.Length > 0)
		{
			text += "\"";
			text += " and @Type = \"";
			text += guiType;
		}
		if (path.Length > 0)
		{
			text += "\"";
			text += " and Definition/@Path = \"";
			text += path;
		}
		return text + "\"]";
	}

	public int GetReferencesCollection(string symbol, string path, string guiType, ReferencesDataCollection referenceCollection, ref bool iterativeSearch)
	{
		if (referenceCollection == null)
		{
			throw new ArgumentNullException("referenceCollection");
		}
		if (path == Strings.Undefined2)
		{
			path = "";
		}
		string xpath = FindSymbolXPath(symbol, path, guiType);
		XmlNode xmlNode = null;
		StringBuilder stringBuilder = new StringBuilder();
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string text6 = "";
		string text7 = "";
		string text8 = "";
		string text9 = "";
		string text10 = "";
		string text11 = "";
		int num = 0;
		XmlNode xmlNode2 = xmlDoc.SelectSingleNode(xpath);
		if (xmlNode2 != null)
		{
			xmlNode2 = xmlNode2.SelectSingleNode("./References");
			if (xmlNode2 != null)
			{
				xmlNode2 = xmlNode2.FirstChild;
				if (xmlNode2 != null && xmlNode2.Name == "IS")
				{
					iterativeSearch = true;
					xmlNode2 = xmlNode2.NextSibling;
				}
				while (xmlNode2 != null)
				{
					stringBuilder.Remove(0, stringBuilder.Length);
					text = "";
					text2 = "";
					text3 = "";
					text4 = "";
					text5 = "";
					text6 = "";
					text7 = "";
					text8 = "";
					text9 = "";
					text10 = "";
					text11 = "";
					if (xmlNode2.Attributes[Strings.Attribute] != null)
					{
						stringBuilder.Append(xmlNode2.Attributes[Strings.Attribute].Value);
						if (stringBuilder.ToString() == Strings.ParRef && xmlNode2.Attributes[Strings.RefAttribute] != null)
						{
							stringBuilder.Append('_');
							stringBuilder.Append(xmlNode2.Attributes[Strings.RefAttribute].Value);
						}
					}
					if (xmlNode2.Attributes[Strings.Path] != null)
					{
						text = xmlNode2.Attributes[Strings.Path].Value;
					}
					if (xmlNode2.Attributes[Strings.Specification] != null)
					{
						text2 = xmlNode2.Attributes[Strings.Specification].Value;
						string[] array = text2.Split('.');
						text9 = array[array.Length - 1];
					}
					if (xmlNode2.Attributes["RefPath"] != null)
					{
						text3 = xmlNode2.Attributes["RefPath"].Value;
						string[] array2 = text3.Split('.');
						text10 = array2[array2.Length - 1];
					}
					_ = xmlNode2.Attributes["RefSpecification"];
					if (xmlNode2.Attributes[Strings.RefInstance] != null)
					{
						text7 = "0";
						text4 = xmlNode2.Attributes[Strings.RefInstance].Value;
					}
					if (xmlNode2.Attributes[Strings.Attribute] != null && stringBuilder.ToString() == Strings.CVConnection && xmlNode2.Attributes[Strings.RefAttribute] != null)
					{
						text += "(";
						text += xmlNode2.Attributes[Strings.RefAttribute].Value;
						text += ")";
					}
					if (xmlNode2.Attributes["Channel"] != null)
					{
						text += " ( ";
						text += xmlNode2.Attributes["Channel"].Value;
						text += " ) ";
					}
					xmlNode = xmlNode2.SelectSingleNode("./Location");
					if (xmlNode != null)
					{
						if (xmlNode.Attributes[Strings.Tab] != null)
						{
							text5 = xmlNode.Attributes[Strings.Tab].Value;
						}
						if (xmlNode.Attributes[Strings.Row] != null)
						{
							text6 = xmlNode.Attributes[Strings.Row].Value;
						}
						if (xmlNode.Attributes[Strings.Position] != null)
						{
							text7 = xmlNode.Attributes[Strings.Position].Value;
						}
						if (xmlNode.Attributes[Strings.Element] != null)
						{
							text8 = xmlNode.Attributes[Strings.Element].Value;
						}
						if (xmlNode.Attributes[Strings.Type] != null)
						{
							text11 = xmlNode.Attributes[Strings.Type].Value;
						}
						num++;
					}
					referenceCollection.AddReferencesData(new ReferencesDataClass(stringBuilder.ToString(), text, text2, text3, text4, text5, text6, text7, text8, text9, text10, text11));
					xmlNode2 = xmlNode2.NextSibling;
				}
			}
		}
		return num;
	}

	public void SortOnColumn(int column)
	{
		if (column == 0)
		{
			if (!SortOnDefinition)
			{
				SymbolAscending = !SymbolAscending;
			}
			SortXML(SymbolAscending, sortOnDefinition: false);
			SortOnDefinition = false;
		}
		else
		{
			if (SortOnDefinition)
			{
				DefinitionAscending = !DefinitionAscending;
			}
			SortOnDefinition = true;
			SortXML(DefinitionAscending, sortOnDefinition: true);
		}
	}

	private void SortXML(bool ascending, bool sortOnDefinition)
	{
		if (xmlDoc == null)
		{
			return;
		}
		XmlElement documentElement = xmlDoc.DocumentElement;
		if (documentElement == null)
		{
			return;
		}
		XmlElement xmlElement = (XmlElement)documentElement.FirstChild;
		XmlElement xmlElement2 = null;
		XmlElement xmlElement3 = null;
		if (xmlElement != null)
		{
			xmlElement2 = (XmlElement)xmlElement.NextSibling;
		}
		while (xmlElement2 != null)
		{
			documentElement.RemoveChild(xmlElement2);
			xmlElement3 = (XmlElement)documentElement.FirstChild;
			string text = "";
			string text2 = "";
			if (xmlElement3 != null)
			{
				if (sortOnDefinition)
				{
					XmlElement xmlElement4 = (XmlElement)xmlElement2.FirstChild;
					if (xmlElement4 != null && string.Compare(xmlElement4.Name, "Definition", StringComparison.Ordinal) == 0)
					{
						text = xmlElement4.Attributes[0].Value;
					}
					xmlElement4 = (XmlElement)xmlElement3.FirstChild;
					if (xmlElement4 != null && string.Compare(xmlElement4.Name, "Definition", StringComparison.Ordinal) == 0)
					{
						text2 = xmlElement4.Attributes[0].Value;
					}
				}
				else
				{
					text = xmlElement2.Attributes[0].Value;
					text2 = xmlElement3.Attributes[0].Value;
				}
			}
			if (!ascending)
			{
				string text3 = text;
				text = text2;
				text2 = text3;
			}
			while (xmlElement3 != xmlElement.NextSibling && string.Compare(text2, text, StringComparison.Ordinal) < 0)
			{
				xmlElement3 = (XmlElement)xmlElement3.NextSibling;
				if (xmlElement3 == null)
				{
					continue;
				}
				string text4 = null;
				if (sortOnDefinition)
				{
					XmlElement xmlElement5 = (XmlElement)xmlElement3.FirstChild;
					if (xmlElement5 != null && string.Compare(xmlElement5.Name, "Definition", StringComparison.Ordinal) == 0)
					{
						text4 = xmlElement5.Attributes[0].Value;
					}
				}
				else
				{
					text4 = xmlElement3.Attributes[0].Value;
				}
				if (ascending && text4 != null)
				{
					text2 = text4;
				}
				else
				{
					text = text4;
				}
			}
			string text5 = "";
			string text6 = "";
			if (xmlElement3 != null)
			{
				if (sortOnDefinition)
				{
					text5 = xmlElement2.Attributes[0].Value;
					text6 = xmlElement3.Attributes[0].Value;
				}
				else
				{
					XmlElement xmlElement6 = (XmlElement)xmlElement2.FirstChild;
					if (xmlElement6 != null && string.Compare(xmlElement6.Name, "Definition", StringComparison.Ordinal) == 0)
					{
						text5 = xmlElement6.Attributes[0].Value;
					}
					xmlElement6 = (XmlElement)xmlElement3.FirstChild;
					if (xmlElement6 != null && string.Compare(xmlElement6.Name, "Definition", StringComparison.Ordinal) == 0)
					{
						text6 = xmlElement6.Attributes[0].Value;
					}
				}
				if (!ascending)
				{
					string text7 = text5;
					text5 = text6;
					text6 = text7;
				}
				while (xmlElement3 != xmlElement.NextSibling && string.Compare(text2, text, StringComparison.Ordinal) == 0 && string.Compare(text6, text5, StringComparison.Ordinal) < 0)
				{
					xmlElement3 = (XmlElement)xmlElement3.NextSibling;
					if (xmlElement3 != null)
					{
						string text8 = "";
						string text9 = "";
						string text10 = "";
						XmlElement xmlElement7 = (XmlElement)xmlElement3.FirstChild;
						if (xmlElement7 != null && string.Compare(xmlElement7.Name, "Definition", StringComparison.Ordinal) == 0)
						{
							text10 = xmlElement7.Attributes[0].Value;
						}
						string value = xmlElement3.Attributes[0].Value;
						if (sortOnDefinition)
						{
							text8 = text10;
							text9 = value;
						}
						else
						{
							text9 = text10;
							text8 = value;
						}
						if (ascending)
						{
							text2 = text8;
							text6 = text9;
						}
						else
						{
							text = text8;
							text5 = text9;
						}
					}
				}
			}
			if (xmlElement3 == null)
			{
				documentElement.AppendChild(xmlElement2);
			}
			else
			{
				documentElement.InsertBefore(xmlElement2, xmlElement3);
			}
			if (xmlElement2 == xmlElement.NextSibling)
			{
				xmlElement = xmlElement2;
			}
			xmlElement2 = (XmlElement)xmlElement.NextSibling;
		}
	}

	internal void AddXmlNode(string queryResultString)
	{
		if (xmlDoc == null)
		{
			return;
		}
		XPathNavigator xPathNavigator = xmlDoc.CreateNavigator();
		if (xPathNavigator == null)
		{
			throw new SNXmlException("garmin == null");
		}
		if (!xPathNavigator.HasChildren)
		{
			xPathNavigator.AppendChild("<Symbols/>");
		}
		if (!xPathNavigator.MoveToFirstChild())
		{
			return;
		}
		if (xPathNavigator == null)
		{
			throw new SNXmlException("<Symbols/> missing");
		}
		if (!xPathNavigator.MoveToFirstChild())
		{
			xPathNavigator.AppendChild("<Symbol/>");
			if (xPathNavigator == null)
			{
				throw new SNXmlException("<Symbol/> missing");
			}
		}
		if (!xPathNavigator.MoveToFirstChild())
		{
			xPathNavigator.AppendChild("<Definition/>");
			if (xPathNavigator == null)
			{
				throw new SNXmlException("<Definition/> missing");
			}
		}
		if (!xPathNavigator.MoveToNext())
		{
			xPathNavigator.AppendChild("<References/>");
			if (xPathNavigator == null)
			{
				throw new SNXmlException("<References/> missing");
			}
		}
		if (!xPathNavigator.MoveToFirstChild())
		{
			xPathNavigator.AppendChild(queryResultString);
			if (xPathNavigator == null)
			{
				throw new SNXmlException("Failed to add <Reference>");
			}
		}
		else
		{
			xPathNavigator.InsertAfter(queryResultString);
		}
	}

	internal void AddProjectToPaths()
	{
		bool flag = true;
		bool flag2 = true;
		try
		{
			XPathNavigator xPathNavigator = xmlDoc.CreateNavigator();
			xPathNavigator.MoveToFirstChild();
			if (!xPathNavigator.LocalName.Equals("Symbols"))
			{
				throw new GeneralSNComponentException("No Symbols node");
			}
			string attribute = xPathNavigator.GetAttribute("ProjectName", xmlDoc.NamespaceURI);
			if (attribute.Length == 0)
			{
				return;
			}
			flag2 = xPathNavigator.MoveToFirstChild();
			while (flag2 && xPathNavigator.LocalName.Equals("Symbol"))
			{
				xPathNavigator.MoveToFirstChild();
				if (!xPathNavigator.LocalName.Equals("Definition"))
				{
					throw new GeneralSNComponentException("No Definition node");
				}
				xPathNavigator.MoveToAttribute("Specification", xmlDoc.NamespaceURI);
				if (!xPathNavigator.LocalName.Equals("Specification"))
				{
					throw new GeneralSNComponentException("No Specification node");
				}
				string value = xPathNavigator.Value;
				if (value.IndexOf("Project.", StringComparison.Ordinal) < 0)
				{
					value = "Project." + value;
					xPathNavigator.SetValue(value);
					xPathNavigator.MoveToParent();
					if (!xPathNavigator.LocalName.Equals("Definition"))
					{
						throw new GeneralSNComponentException("No Definition node");
					}
					xPathNavigator.MoveToAttribute("Path", xmlDoc.NamespaceURI);
					if (!xPathNavigator.LocalName.Equals("Path"))
					{
						throw new GeneralSNComponentException("No Path node");
					}
					value = attribute + "." + xPathNavigator.Value;
					xPathNavigator.SetValue(value);
				}
				xPathNavigator.MoveToParent();
				if (!xPathNavigator.LocalName.Equals("Definition"))
				{
					throw new GeneralSNComponentException("No Definition node");
				}
				xPathNavigator.MoveToNext();
				if (!xPathNavigator.LocalName.Equals("References"))
				{
					throw new GeneralSNComponentException("No References node");
				}
				flag = xPathNavigator.MoveToFirstChild();
				while (flag)
				{
					xPathNavigator.MoveToAttribute("Specification", xmlDoc.NamespaceURI);
					if (!xPathNavigator.LocalName.Equals("Specification"))
					{
						throw new GeneralSNComponentException("No Specification attribute");
					}
					value = xPathNavigator.Value;
					if (value.IndexOf("Project.", StringComparison.Ordinal) < 0)
					{
						value = "Project." + value;
						xPathNavigator.SetValue(value);
						xPathNavigator.MoveToParent();
						if (!xPathNavigator.LocalName.Equals("Reference"))
						{
							throw new GeneralSNComponentException("No Reference node");
						}
						xPathNavigator.MoveToAttribute("Path", xmlDoc.NamespaceURI);
						if (!xPathNavigator.LocalName.Equals("Path"))
						{
							throw new GeneralSNComponentException("No Path attribute");
						}
						value = attribute + "." + xPathNavigator.Value;
						xPathNavigator.SetValue(value);
					}
					xPathNavigator.MoveToParent();
					if (!xPathNavigator.LocalName.Equals("Reference"))
					{
						throw new GeneralSNComponentException("No Reference node");
					}
					if (xPathNavigator.MoveToAttribute("RefSpecification", xmlDoc.NamespaceURI))
					{
						value = xPathNavigator.Value;
						if (value.IndexOf("Project.", StringComparison.Ordinal) < 0)
						{
							value = "Project." + value;
							xPathNavigator.SetValue(value);
							xPathNavigator.MoveToParent();
							if (!xPathNavigator.LocalName.Equals("Reference"))
							{
								throw new GeneralSNComponentException("No Reference node");
							}
							xPathNavigator.MoveToAttribute("RefPath", xmlDoc.NamespaceURI);
							if (!xPathNavigator.LocalName.Equals("RefPath"))
							{
								throw new GeneralSNComponentException("No RefPath attribute");
							}
							value = attribute + "." + xPathNavigator.Value;
							xPathNavigator.SetValue(value);
						}
						xPathNavigator.MoveToParent();
						if (!xPathNavigator.LocalName.Equals("Reference"))
						{
							throw new GeneralSNComponentException("No Reference node");
						}
					}
					flag = xPathNavigator.MoveToNext();
				}
				if (xPathNavigator.LocalName.Equals("Reference"))
				{
					xPathNavigator.MoveToParent();
				}
				if (xPathNavigator.LocalName.Equals("References"))
				{
					xPathNavigator.MoveToParent();
					flag2 = xPathNavigator.MoveToNext();
				}
				else
				{
					flag2 = false;
				}
			}
		}
		catch (GeneralSNComponentException)
		{
		}
	}
}
