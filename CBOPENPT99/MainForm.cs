using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using CONTROLBUILDERLib;
using CBOpenIFHelper;

namespace CBOpenInterfaceHelperApp
{
    public partial class MainForm : Form
    {
        private CBOpenIF cb = null;
        private ObjectFactory ObjectFactory = null;

        public MainForm()
        {
            InitializeComponent();
            InitializeCBOpenInterface();
        }

        private void InitializeCBOpenInterface()
        {
            try
            {
                cb = new CBOpenIF();
                ObjectFactory = new ObjectFactory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing CB Open Interface: " + ex.Message);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                string XMLStr = cb.GetFunctionBlockType(txtObjectName.Text);
                FunctionBlockType fbType = ObjectFactory.DeserializeFunctionBlockType(ref XMLStr);
                DisplayObject(fbType);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading object: " + ex.Message);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionBlockType fbType = ObjectFactory.NewFunctionBlockType(txtObjectName.Text, txtDescription.Text);
                fbType.Variables.Add1("MyVariable", "bool");
                string bucket = cb.NewFunctionBlockType(fbType.Name, "MyLib", fbType.Serialize());
                MessageBox.Show("Object created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating object: " + ex.Message);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                string XMLStr = cb.GetFunctionBlockType(txtObjectName.Text);
                FunctionBlockType fbType = ObjectFactory.DeserializeFunctionBlockType(ref XMLStr);
                fbType.Description = txtDescription.Text;
                string bucket = cb.SetFunctionBlockType(fbType.Name, fbType.Serialize());
                MessageBox.Show("Object modified successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error modifying object: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cb.DeleteFunctionBlock(txtObjectName.Text);
                MessageBox.Show("Object deleted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting object: " + ex.Message);
            }
        }

        private void DisplayObject(FunctionBlockType fbType)
        {
            // Clear previous entries
            dataGridView1.Rows.Clear();

            // Add object details to DataGridView
            foreach (Variable var in fbType.Variables)
            {
                dataGridView1.Rows.Add(var.Name, var.TypeName, var.InitialValue, var.Description);
            }
        }

        private void btnListApps_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear the TreeView
                treeView1.Nodes.Clear();

                // Get the project tree as XML
                string AppsAsXML = cb.GetProjectTree("", -1, true);

                // Load the XML into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(AppsAsXML);

                // Select the Applications node
                XmlNode applicationsNode = xmlDoc.SelectSingleNode("//cb:Applications", GetNamespaceManager(xmlDoc));

                if (applicationsNode != null)
                {
                    // Loop through each application node
                    foreach (XmlNode applicationNode in applicationsNode.ChildNodes)
                    {
                        // Get the application name
                        string appName = applicationNode.Attributes["Name"].Value;

                        // Add the application name to the TreeView
                        treeView1.Nodes.Add(appName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error listing applications: " + ex.Message);
            }
        }

        private XmlNamespaceManager GetNamespaceManager(XmlDocument xmlDoc)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("cb", "http://www.example.com/ControlBuilder"); // Replace with actual namespace
            return namespaceManager;
        }

        private void btnGetHardware_Click(object sender, EventArgs e)
        {
            GetControllerHardware();
        }

        private void GetControllerHardware()
        {
            string[] controllers = { "Cont_A", "Cont_B", "Cont_C", "Cont_D", "Cont_E", "Cont_E2", "Cont_P", "Cont_HI1", "Cont_HI2", "Cont_HI3" };

            foreach (string controller in controllers)
            {
                try
                {
                    string CHardAsXML = cb.GetHardwareUnit(controller, true);

                    // Save to file
                    string fileName = $"{controller}.xml";
                    File.WriteAllText(fileName, CHardAsXML);

                    // Load XML into XmlDocument and display in TreeView
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(CHardAsXML);

                    // Add the controller node to the TreeView
                    XmlNode controllerNode = xmlDoc.SelectSingleNode("//cb:HardwareUnit", GetNamespaceManager(xmlDoc));
                    if (controllerNode != null)
                    {
                        TreeNode treeNode = new TreeNode(controller);
                        treeViewHardware.Nodes.Add(treeNode);
                        AddChildNodes(controllerNode, treeNode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error getting hardware for {controller}: " + ex.Message);
                }
            }
        }

        private void AddChildNodes(XmlNode xmlNode, TreeNode treeNode)
        {
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                TreeNode childTreeNode = new TreeNode(childNode.Attributes["Name"]?.Value ?? childNode.Name);
                treeNode.Nodes.Add(childTreeNode);
                AddChildNodes(childNode, childTreeNode);
            }
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text File|*.txt",
                Title = "Save data to file"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        writer.WriteLine(string.Join(",", row.Cells.OfType<DataGridViewCell>().Select(cell => cell.Value)));
                    }
                }
                MessageBox.Show("Data saved to file successfully!");
            }
        }

        private void btnSaveToExcel_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Workbooks.Add();
            Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Save data to Excel"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                worksheet.SaveAs(saveFileDialog.FileName);
                excelApp.Quit();
                MessageBox.Show("Data saved to Excel successfully!");
            }
        }

        private void exampleButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is an example button.");
        }
    }
}
