using System;
using System.Windows.Forms;
using System.Xml;
using System.Linq;

namespace XmlTreeViewApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loadXmlButton_Click(object sender, EventArgs e)
        {
            // Example XML (you can replace this with actual XML loading from a file)
            string xmlContent = @"<root>
                                      <element name='Item1' value='10' type='A'/>
                                      <element name='Item2' value='20' type='B'/>
                                      <element name='Item3' value='30' type='A'/>
                                  </root>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            // Clear previous items in TreeView
            xmlTreeView.Nodes.Clear();
            attributeComboBox.Items.Clear();

            // Load XML into TreeView
            LoadXmlIntoTreeView(xmlDoc.DocumentElement, xmlTreeView.Nodes);

            // Populate ComboBox with attributes from the first element (for sorting)
            if (xmlDoc.DocumentElement.HasChildNodes)
            {
                var firstElement = xmlDoc.DocumentElement.FirstChild;
                PopulateComboBox(firstElement);
            }

            // Handle sorting or filtering changes
            attributeComboBox.SelectedIndexChanged += (s, ev) => SortTreeViewByAttribute();
            filterCheckBox.CheckedChanged += (s, ev) => ApplyAttributeFilter();
        }

        private void LoadXmlIntoTreeView(XmlNode xmlNode, TreeNodeCollection treeNodes)
        {
            TreeNode newNode = new TreeNode(xmlNode.Name);

            // Add attributes to TreeView nodes
            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attr in xmlNode.Attributes)
                {
                    newNode.Nodes.Add(new TreeNode($"{attr.Name} = {attr.Value}"));
                }
            }

            // Recursively add child nodes
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                LoadXmlIntoTreeView(childNode, newNode.Nodes);
            }

            treeNodes.Add(newNode);
        }

        private void PopulateComboBox(XmlNode xmlNode)
        {
            // Add available attributes to ComboBox
            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attr in xmlNode.Attributes)
                {
                    attributeComboBox.Items.Add(attr.Name);
                }
            }

            if (attributeComboBox.Items.Count > 0)
            {
                attributeComboBox.SelectedIndex = 0; // Select first attribute by default
            }
        }

        private void SortTreeViewByAttribute()
        {
            if (attributeComboBox.SelectedItem != null)
            {
                string selectedAttribute = attributeComboBox.SelectedItem.ToString();

                foreach (TreeNode rootNode in xmlTreeView.Nodes)
                {
                    var childNodes = rootNode.Nodes.Cast<TreeNode>().ToList();

                    // Sort nodes by the attribute value
                    var sortedNodes = childNodes.OrderBy(n =>
                    {
                        var attributeNode = n.Nodes.Cast<TreeNode>().FirstOrDefault(attr => attr.Text.StartsWith($"{selectedAttribute} ="));
                        return attributeNode?.Text.Split('=')[1].Trim();
                    }).ToList();

                    // Clear and re-add the sorted nodes
                    rootNode.Nodes.Clear();
                    sortedNodes.ForEach(node => rootNode.Nodes.Add(node));
                }
            }
        }

        private void ApplyAttributeFilter()
        {
            if (filterCheckBox.Checked && attributeComboBox.SelectedItem != null)
            {
                string selectedAttribute = attributeComboBox.SelectedItem.ToString();

                foreach (TreeNode rootNode in xmlTreeView.Nodes)
                {
                    foreach (TreeNode elementNode in rootNode.Nodes)
                    {
                        // Check if the selected attribute exists in each element
                        bool hasAttribute = elementNode.Nodes.Cast<TreeNode>().Any(attr => attr.Text.StartsWith($"{selectedAttribute} ="));

                        // Hide nodes that do not have the attribute
                        elementNode.Toggle(hasAttribute);
                    }
                }
            }
            else
            {
                // Unhide all nodes if filter is unchecked
                foreach (TreeNode rootNode in xmlTreeView.Nodes)
                {
                    foreach (TreeNode elementNode in rootNode.Nodes)
                    {
                        elementNode.Toggle(true);
                    }
                }
            }
        }
    }

    // Helper method to toggle visibility of nodes (TreeView doesn't have built-in support for hiding nodes)
    public static class TreeNodeExtensions
    {
        public static void Toggle(this TreeNode node, bool isVisible)
        {
            node.BackColor = isVisible ? System.Drawing.Color.White : System.Drawing.Color.LightGray;
            node.ForeColor = isVisible ? System.Drawing.Color.Black : System.Drawing.Color.Gray;
        }
    }
}
