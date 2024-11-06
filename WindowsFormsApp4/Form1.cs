using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XMLSearchTool
{
    public partial class XMLSearchForm : Form
    {
        private XDocument loadedDocument;
        private Dictionary<string, List<string>> documentStructure;
        private Dictionary<string, Type> fieldTypes;

        public XMLSearchForm()
        {
            InitializeComponent();
            documentStructure = new Dictionary<string, List<string>>();
            fieldTypes = new Dictionary<string, Type>();

            // Wire up events
            cboSearchField.SelectedIndexChanged += cboSearchField_SelectedIndexChanged;
            lstCommonValues.SelectedIndexChanged += lstCommonValues_SelectedIndexChanged;
            btnSearch.Click += btnSearch_Click;
        }

        private async void btnLoadXML_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    await AnalyzeDocument(ofd.FileName);
                }
            }
        }

        private async Task AnalyzeDocument(string filePath)
        {
            try
            {
                using (var progress = new ProgressForm("Analyzing XML Document"))
                {
                    progress.Show();
                    await Task.Run(() =>
                    {
                        progress.UpdateStatus("Loading XML file...");
                        loadedDocument = XDocument.Load(filePath);
                        documentStructure.Clear();
                        fieldTypes.Clear();

                        progress.UpdateStatus("Analyzing document structure...");
                        // Analyze structure
                        foreach (var element in loadedDocument.Descendants())
                        {
                            string friendlyName = MakeFriendlyName(element.Name.LocalName);

                            if (!documentStructure.ContainsKey(friendlyName))
                            {
                                documentStructure[friendlyName] = new List<string>();

                                progress.UpdateStatus($"Analyzing field: {friendlyName}");
                                // Get unique sample values
                                var sampleValues = element.Descendants()
                                    .Take(100)
                                    .Select(e => e.Value)
                                    .Distinct()
                                    .Where(v => !string.IsNullOrWhiteSpace(v))
                                    .ToList();

                                documentStructure[friendlyName].AddRange(sampleValues);
                                DetectAndStoreValueType(friendlyName, sampleValues);
                            }
                        }

                        // Update UI
                        this.Invoke((MethodInvoker)delegate
                        {
                            progress.UpdateStatus("Updating interface...");
                            cboSearchField.Items.Clear();
                            foreach (var field in documentStructure.Keys.OrderBy(k => k))
                            {
                                cboSearchField.Items.Add(field);
                            }

                            if (cboSearchField.Items.Count > 0)
                                cboSearchField.SelectedIndex = 0;

                            statusLabel.Text = "Document loaded successfully";
                            btnSearch.Enabled = true;
                        });
                    });

                    progress.FinishAndClose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading document: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading document";
            }
        }

        private string MakeFriendlyName(string technicalName)
        {
            // Convert camelCase or PascalCase to spaces
            string friendly = System.Text.RegularExpressions.Regex
                .Replace(technicalName, "([A-Z])", " $1").Trim();
            // Capitalize first letter of each word
            return System.Globalization.CultureInfo.CurrentCulture
                .TextInfo.ToTitleCase(friendly.ToLower());
        }

        private void DetectAndStoreValueType(string fieldName, List<string> sampleValues)
        {
            if (!sampleValues.Any())
            {
                fieldTypes[fieldName] = typeof(string);
                return;
            }

            // Try parse dates
            if (sampleValues.All(s => DateTime.TryParse(s, out _)))
            {
                fieldTypes[fieldName] = typeof(DateTime);
                return;
            }

            // Try parse numbers
            if (sampleValues.All(s => decimal.TryParse(s, out _)))
            {
                fieldTypes[fieldName] = typeof(decimal);
                return;
            }

            // Default to string
            fieldTypes[fieldName] = typeof(string);
        }

        private void cboSearchField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSearchField.SelectedItem == null) return;

            string selectedField = cboSearchField.SelectedItem.ToString();
            Type fieldType = fieldTypes[selectedField];

            // Update operators based on field type
            cboSearchCondition.Items.Clear();

            if (fieldType == typeof(DateTime))
            {
                cboSearchCondition.Items.AddRange(new object[] {
                    "Is On",
                    "Is Before",
                    "Is After",
                    "Is Between"
                });
            }
            else if (fieldType == typeof(decimal))
            {
                cboSearchCondition.Items.AddRange(new object[] {
                    "Equals",
                    "Is Greater Than",
                    "Is Less Than",
                    "Is Between"
                });
            }
            else
            {
                cboSearchCondition.Items.AddRange(new object[] {
                    "Contains",
                    "Equals Exactly",
                    "Starts With",
                    "Ends With",
                    "Is Empty",
                    "Has Any Value"
                });
            }

            cboSearchCondition.SelectedIndex = 0;

            // Update common values
            lstCommonValues.Items.Clear();
            if (documentStructure.ContainsKey(selectedField))
            {
                lstCommonValues.Items.AddRange(
                    documentStructure[selectedField]
                        .Take(50)
                        .ToArray()
                );
            }
        }

        private void lstCommonValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCommonValues.SelectedItem != null)
            {
                txtSearchValue.Text = lstCommonValues.SelectedItem.ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (loadedDocument == null)
            {
                MessageBox.Show("Please load an XML document first.", "No Document Loaded",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PerformSearch();
        }
        private void PerformSearch()
        {
            try
            {
                string fieldName = cboSearchField.SelectedItem.ToString();
                string condition = cboSearchCondition.SelectedItem.ToString();
                string searchValue = txtSearchValue.Text;

                // Build LINQ query based on condition
                var query = BuildSearchQuery(fieldName, condition, searchValue);

                // Execute search and display results
                DisplayResults(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Search failed";
            }
        }

        private IEnumerable<XElement> BuildSearchQuery(string fieldName, string condition, string searchValue)
        {
            // Convert friendly name back to XML element name
            string elementName = fieldName.Replace(" ", "");

            // Base query to get all elements of the specified name
            var elements = loadedDocument.Descendants()
                .Where(e => MakeFriendlyName(e.Name.LocalName) == fieldName);

            // Apply the condition
            switch (condition)
            {
                case "Contains":
                    return elements.Where(e => e.Value.Contains(searchValue));

                case "Equals Exactly":
                    return elements.Where(e => e.Value == searchValue);

                case "Starts With":
                    return elements.Where(e => e.Value.StartsWith(searchValue));

                case "Ends With":
                    return elements.Where(e => e.Value.EndsWith(searchValue));

                case "Is Empty":
                    return elements.Where(e => string.IsNullOrWhiteSpace(e.Value));

                case "Has Any Value":
                    return elements.Where(e => !string.IsNullOrWhiteSpace(e.Value));

                case "Is Greater Than":
                    if (decimal.TryParse(searchValue, out decimal numValue))
                    {
                        return elements.Where(e => decimal.TryParse(e.Value, out decimal val) && val > numValue);
                    }
                    break;

                case "Is Less Than":
                    if (decimal.TryParse(searchValue, out numValue))
                    {
                        return elements.Where(e => decimal.TryParse(e.Value, out decimal val) && val < numValue);
                    }
                    break;

                case "Is On":
                    if (DateTime.TryParse(searchValue, out DateTime dateValue))
                    {
                        return elements.Where(e => DateTime.TryParse(e.Value, out DateTime val) &&
                            val.Date == dateValue.Date);
                    }
                    break;

                case "Is Before":
                    if (DateTime.TryParse(searchValue, out dateValue))
                    {
                        return elements.Where(e => DateTime.TryParse(e.Value, out DateTime val) &&
                            val < dateValue);
                    }
                    break;

                case "Is After":
                    if (DateTime.TryParse(searchValue, out dateValue))
                    {
                        return elements.Where(e => DateTime.TryParse(e.Value, out DateTime val) &&
                            val > dateValue);
                    }
                    break;
            }

            return Enumerable.Empty<XElement>();
        }

        private void DisplayResults(IEnumerable<XElement> results)
        {
            gridResults.SuspendLayout();
            gridResults.Rows.Clear();
            gridResults.Columns.Clear();

            // Create columns
            gridResults.Columns.Add("Element", "Element");
            gridResults.Columns.Add("Value", "Value");
            gridResults.Columns.Add("Path", "XML Path");
            gridResults.Columns.Add("Parent", "Parent Element");

            // Add results
            foreach (var element in results)
            {
                string path = GetElementPath(element);
                string parentName = element.Parent != null ?
                    MakeFriendlyName(element.Parent.Name.LocalName) : "";

                gridResults.Rows.Add(
                    MakeFriendlyName(element.Name.LocalName),
                    element.Value,
                    path,
                    parentName
                );
            }

            gridResults.ResumeLayout();
            statusLabel.Text = $"Found {results.Count()} matches";
        }

        private string GetElementPath(XElement element)
        {
            var path = new List<string>();
            var current = element;

            while (current != null)
            {
                path.Add(MakeFriendlyName(current.Name.LocalName));
                current = current.Parent;
            }

            path.Reverse();
            return string.Join(" > ", path);
        }
    }
    public class ProgressForm : Form
    {
        private ProgressBar progressBar;
        private Label statusLabel;

        public ProgressForm(string title)
        {
            // Form settings
            this.Text = title;
            this.Size = new System.Drawing.Size(400, 150);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;  // Remove the control box
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            // Create status label
            statusLabel = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(12, 20),
                Size = new System.Drawing.Size(360, 20),
                Text = "Analyzing document structure..."
            };

            // Create progress bar
            progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,  // Continuous animation
                MarqueeAnimationSpeed = 30,
                Location = new System.Drawing.Point(12, 50),
                Size = new System.Drawing.Size(360, 23)
            };

            // Add controls to form
            this.Controls.AddRange(new Control[] { statusLabel, progressBar });
        }

        public void UpdateStatus(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateStatus), status);
                return;
            }

            statusLabel.Text = status;
        }

        // Optional: Method to close the form safely
        public void FinishAndClose()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(FinishAndClose));
                return;
            }

            this.Close();
        }
    }
}

