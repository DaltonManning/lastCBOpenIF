using System;
using System.Windows.Forms;
using System.Xml.Linq;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using static System.Collections.Specialized.BitVector32;

namespace OPCUAWinFormApp
{
    public partial class MainForm : Form
    {
        private ApplicationInstance opcApplication;
        private Session opcSession;

        public MainForm()
        {
            InitializeComponent();
            opcApplication = new ApplicationInstance
            {
                ApplicationName = "OPC UA WinForm Application",
                ApplicationType = ApplicationType.Client,
                ConfigSectionName = "Opc.Ua.Client"
            };
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Load the application configuration
                await opcApplication.LoadApplicationConfiguration(false);
                await opcApplication.CheckApplicationInstanceCertificate(false, 0);

                // Get endpoint from registry or provide default endpoint
                string endpointUrl = "opc.tcp://localhost:4840"; // Replace with the endpoint URL from the registry if needed
                EndpointDescription endpoint = CoreClientUtils.SelectEndpoint(endpointUrl, useSecurity: false);
                EndpointConfiguration endpointConfiguration = EndpointConfiguration.Create(opcApplication.ApplicationConfiguration);
                ConfiguredEndpoint configuredEndpoint = new ConfiguredEndpoint(null, endpoint, endpointConfiguration);

                // Create a session
                opcSession = await Session.Create(opcApplication.ApplicationConfiguration, configuredEndpoint, false, "OPC Session", 60000, null, null);
                MessageBox.Show("Connected to OPC UA server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to OPC UA server: " + ex.Message);
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (opcSession == null || !opcSession.Connected)
            {
                MessageBox.Show("Please connect to the OPC server first.");
                return;
            }

            try
            {
                // Browse available nodes in the OPC server
                ReferenceDescriptionCollection references;
                Byte[] continuationPoint;

                opcSession.Browse(null, null, Objects.ObjectsFolder, 0u, BrowseDirection.Forward, ReferenceTypeIds.HierarchicalReferences, true, (uint)NodeClass.Variable | (uint)NodeClass.Object, out continuationPoint, out references);

                // Display nodes in a DataGridView
                opcDataGridView.Rows.Clear();
                opcDataGridView.Columns.Clear();
                opcDataGridView.Columns.Add("NodeId", "Node ID");
                opcDataGridView.Columns.Add("DisplayName", "Display Name");

                foreach (ReferenceDescription node in references)
                {
                    opcDataGridView.Rows.Add(node.NodeId.ToString(), node.DisplayName.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error browsing OPC nodes: " + ex.Message);
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (opcDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item from the list.");
                return;
            }

            try
            {
                foreach (DataGridViewRow row in opcDataGridView.SelectedRows)
                {
                    string nodeId = row.Cells["NodeId"].Value.ToString();
                    string displayName = row.Cells["DisplayName"].Value.ToString();
                    MessageBox.Show($"You selected: {displayName} ({nodeId})");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting OPC node: " + ex.Message);
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            if (opcSession != null && opcSession.Connected)
            {
                opcSession.Close();
                MessageBox.Show("Disconnected from OPC UA server.");
            }
        }
    }
}
