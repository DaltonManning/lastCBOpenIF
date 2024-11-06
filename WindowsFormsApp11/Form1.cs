using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Schema;

namespace WinFormsXmlSchemaParser
{
    public partial class Form1 : Form
    {
        // Dictionary to store elements and their attributes
        Dictionary<string, List<string>> elementAttributeMap = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();
        }

        // Button Click Event: Load the XSD file and populate elements and attributes
        private void btnLoadSchema_Click(object sender, EventArgs e)
        {
            // Open a File Dialog to select the XSD schema
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML Schema Files (*.xsd)|*.xsd|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string xsdFilePath = openFileDialog.FileName;

                try
                {
                    // Clear previous data
                    elementAttributeMap.Clear();
                    cmbElements.Items.Clear();
                    lstAttributes.Items.Clear();
                    txtDetails.Clear();

                    // Load and parse the schema
                    XmlSchemaSet schemaSet = new XmlSchemaSet();
                    schemaSet.Add(null, xsdFilePath);
                    schemaSet.Compile();

                    // Extract elements and attributes
                    foreach (XmlSchema schema in schemaSet.Schemas())
                    {
                        foreach (XmlSchemaElement element in schema.Elements.Values)
                        {
                            ExtractElementsAndAttributes(element, elementAttributeMap);
                        }
                    }

                    // Populate the ComboBox with elements
                    foreach (var element in elementAttributeMap.Keys)
                    {
                        cmbElements.Items.Add(element);
                    }

                    MessageBox.Show("Schema loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading schema: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method to extract elements and attributes recursively
        private void ExtractElementsAndAttributes(XmlSchemaElement element, Dictionary<string, List<string>> elementAttributeMap)
        {
            List<string> attributes = new List<string>();

            if (element.ElementSchemaType is XmlSchemaComplexType complexType)
            {
                // Extract attributes
                foreach (XmlSchemaObject attributeObject in complexType.Attributes)
                {
                    if (attributeObject is XmlSchemaAttribute attribute)
                    {
                        attributes.Add(attribute.Name);
                    }
                }

                // Extract nested elements if they exist
                if (complexType.ContentTypeParticle is XmlSchemaSequence sequence)
                {
                    foreach (XmlSchemaObject item in sequence.Items)
                    {
                        if (item is XmlSchemaElement childElement)
                        {
                            ExtractElementsAndAttributes(childElement, elementAttributeMap);
                        }
                    }
                }
            }

            // Add element and attributes to the dictionary
            elementAttributeMap[element.Name] = attributes;
        }

        // ComboBox SelectedIndexChanged Event: Display attributes for the selected element
        private void cmbElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbElements.SelectedItem != null)
            {
                string selectedElement = cmbElements.SelectedItem.ToString();
                lstAttributes.Items.Clear();

                // Get the attributes of the selected element
                if (elementAttributeMap.ContainsKey(selectedElement))
                {
                    var attributes = elementAttributeMap[selectedElement];

                    // Display attributes in the ListBox
                    foreach (var attribute in attributes)
                    {
                        lstAttributes.Items.Add(attribute);
                    }

                    // Display the element details in the TextBox
                    txtDetails.Text = $"Element: {selectedElement}\r\nAttributes: {string.Join(", ", attributes)}";
                }
            }
        }

        // Button Click Event: Save the selected element and attributes
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbElements.SelectedItem != null)
            {
                string selectedElement = cmbElements.SelectedItem.ToString();
                string selectedAttributes = string.Join(", ", lstAttributes.Items);

                MessageBox.Show($"Saved:\nElement: {selectedElement}\nAttributes: {selectedAttributes}", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an element first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
