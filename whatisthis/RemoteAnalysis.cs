using System;
using System.Windows.Forms;
using System.Xml;
using CBOpenIFHelper;

namespace RemoteAnalysis
{
    public partial class RemoteAnalysisView : UserControl
    {
        private XmlDocument xmlDocument;
        private XmlSchemaSet schemaSet;
        private CBOpenIf cbOpenIf;

        public RemoteAnalysisView()
        {
            InitializeComponent();
            InitializeEnvironment();
        }

        private void InitializeEnvironment()
        {
            try
            {
                xmlDocument = new XmlDocument();
                schemaSet = new XmlSchemaSet();
                schemaSet.Add("CBOpenIFSchema3_0", "CBOpenIFSchema3_0.xsd");

                cbOpenIf = new CBOpenIf();

                MessageBox.Show("Environment initialized successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization error: {ex.Message}");
            }
        }

        private void CreateObjects()
        {
            try
            {
                cbOpenIf.NewProject("MyProject", "", "", "");
                cbOpenIf.NewLibrary("MyLibrary", "", "");
                cbOpenIf.NewApplication("MyApp", "", "", "");
                cbOpenIf.NewController("MyController", "AC 800M", "", "", "");

                string xmlStr = "<?xml version='1.0' encoding='UTF-16' ?>" +
                                "<Program Name='MyProgram' xmlns='CBOpenIFSchema3_0'>" +
                                "<Variables>" +
                                "<Variable Name='Var1' Type='dint' InitialValue='7' />" +
                                "<Variable Name='Var2' Type='dint' />" +
                                "</Variables>" +
                                "<CodeBlocks>" +
                                "<STCodeBlock Name='CodeBlock1'>" +
                                "<ST_Code>Var2 := Var1 +1;</ST_Code>" +
                                "</STCodeBlock>" +
                                "</CodeBlocks>" +
                                "</Program>";

                cbOpenIf.NewProgram("MyProg", "MyApp", xmlStr);

                xmlStr = "<?xml version='1.0' encoding='UTF-16' ?>" +
                         "<FunctionBlockType Name='MyFBType' xmlns='CBOpenIFSchema3_0'>" +
                         "<Variables>" +
                         "<Variable Name='Var1' Type='dint' InitialValue='7' />" +
                         "<Variable Name='Var2' Type='dint' />" +
                         "</Variables>" +
                         "<CodeBlocks>" +
                         "<STCodeBlock Name='CodeBlock1'>" +
                         "<ST_Code>Var2 := Var1 +1;</ST_Code>" +
                         "</STCodeBlock>" +
                         "</CodeBlocks>" +
                         "</FunctionBlockType>";

                cbOpenIf.NewFunctionBlockType("MyFBType", "MyApp", xmlStr);

                cbOpenIf.NewDataType("MyDataType1", "MyLibrary", "");
                cbOpenIf.NewDataType("MyDataType2", "MyLibrary", "");
                cbOpenIf.NewDataType("MyDataType3", "MyLibrary", "");

                cbOpenIf.InsertHardwareLibrary("HWLibraries:\\S800IoModulebusHwLib");
                cbOpenIf.ConnectHardwareLibrary("MyController", "S800IoModulebusHwLib");

                cbOpenIf.NewHardwareUnit("MyController.0", "PM860 / TP830", "", "", "");
                cbOpenIf.NewHardwareUnit("MyController.0.11.1", "AI810", "", "", "");

                MessageBox.Show("Objects created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void AddVariable()
        {
            try
            {
                string programContent = cbOpenIf.GetProgram("MyApp.MyProg");

                xmlDocument.LoadXml(programContent);
                XmlNode variablesNode = xmlDocument.SelectSingleNode("//cb:Variables");

                XmlElement newVariableNode = xmlDocument.CreateElement("Variable", variablesNode.NamespaceURI);
                newVariableNode.SetAttribute("Name", "Kalle");
                newVariableNode.SetAttribute("Type", "Real");

                variablesNode.AppendChild(newVariableNode);

                cbOpenIf.SetProgram("MyApp.MyProg", xmlDocument.OuterXml);

                MessageBox.Show("Variable added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void CopyFBType()
        {
            try
            {
                string fbTypeContent = cbOpenIf.GetFunctionBlockType("MyApp.MyFBtype");

                xmlDocument.LoadXml(fbTypeContent);
                XmlNode fbTypeNode = xmlDocument.DocumentElement;

                cbOpenIf.NewFunctionBlockType("MyLibFBtype", "MyLibrary", xmlDocument.OuterXml);

                MessageBox.Show("Function block type copied successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ListLibs()
        {
            try
            {
                string libsContent = cbOpenIf.GetProjectTree("", 1, false);

                xmlDocument.LoadXml(libsContent);
                XmlNode librariesNode = xmlDocument.SelectSingleNode("//cb:Libraries");
                XmlNode libraryNode = librariesNode.FirstChild;

                listBox1.Items.Clear();
                while (libraryNode != null)
                {
                    listBox1.Items.Add("LibraryName: " + libraryNode.Attributes["Name"].Value);
                    libraryNode = libraryNode.NextSibling;
                }

                MessageBox.Show("Libraries listed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ListDatatypes()
        {
            try
            {
                string dataTypesContent = cbOpenIf.GetProjectTree("Libraries.MyLibrary.DataTypes", 1, false);

                xmlDocument.LoadXml(dataTypesContent);
                XmlNode dataTypesNode = xmlDocument.SelectSingleNode("cb:DataTypes");
                XmlNode dataTypeNode = dataTypesNode.FirstChild;

                listBox1.Items.Clear();
                while (dataTypeNode != null)
                {
                    listBox1.Items.Add("DataType Name: " + dataTypeNode.Attributes["Name"].Value);
                    dataTypeNode = dataTypeNode.NextSibling;
                }

                MessageBox.Show("Data types listed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void FindDatatype()
        {
            try
            {
                string dataTypesContent = cbOpenIf.GetProjectTree("Libraries.MyLibrary.DataTypes", 1, false);

                xmlDocument.LoadXml(dataTypesContent);
                string dataTypeToFind = textBox1.Text;

                string xPathStr = $"//cb:DataType[@Name='{dataTypeToFind}']";
                XmlNode dataTypeNode = xmlDocument.SelectSingleNode(xPathStr);

                listBox1.Items.Clear();
                if (dataTypeNode != null)
                {
                    listBox1.Items.Add("DataType: " + dataTypeNode.Attributes["Name"].Value + " Found");
                }
                else
                {
                    listBox1.Items.Add("DataType: " + dataTypeToFind + " Not Found");
                }

                MessageBox.Show("Data type search completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void GetHwUnit()
        {
            try
            {
                string hwUnitContent = cbOpenIf.GetHardwareUnit("MyController.0.11.1", false);

                xmlDocument.LoadXml(hwUnitContent);
                XmlNode parSettingNode = xmlDocument.SelectSingleNode("//cb:ParameterSetting[@Name='SignalRange_1']");
                textBoxSignalRange.Text = parSettingNode.Attributes["Value"].Value;

                XmlNode hwChannelNode = xmlDocument.SelectSingleNode("//cb:HWChannel[@Name='Input 1']");
                textBoxHWUnit.Text = hwChannelNode.Attributes["Unit"].Value;

                XmlNode connStrNode = hwChannelNode.FirstChild.FirstChild;
                textBoxHWConn.Text = connStrNode != null ? connStrNode.Value : string.Empty;

                MessageBox.Show("Hardware unit details retrieved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void SetHwUnit()
        {
            try
            {
                string hwUnitContent = cbOpenIf.GetHardwareUnit("MyController.0.11.1", false);

                xmlDocument.LoadXml(hwUnitContent);
                XmlNode parSettingNode = xmlDocument.SelectSingleNode("//cb:ParameterSetting[@Name='SignalRange_1']");
                parSettingNode.Attributes["Value"].Value = textBoxSignalRange.Text;

                XmlNode hwChannelNode = xmlDocument.SelectSingleNode("//cb:HWChannel[@Name='Input 1']");
                hwChannelNode.Attributes["Unit"].Value = textBoxHWUnit.Text;

                XmlNode connStrNode = hwChannelNode.FirstChild.FirstChild;
                if (connStrNode == null)
                {
                    XmlNode newTextNode = xmlDocument.CreateTextNode(textBoxHWConn.Text);
                    hwChannelNode.FirstChild.AppendChild(newTextNode);
                }
                else
                {
                    connStrNode.Value = textBoxHWConn.Text;
                }

                cbOpenIf.SetHardwareUnit("MyController.0.11.1", xmlDocument.OuterXml);

                MessageBox.Show("Hardware unit updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
