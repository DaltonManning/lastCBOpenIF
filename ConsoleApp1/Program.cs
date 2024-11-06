using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace ControlBuilderViewer
{
    // Reuse the same data model classes from the exporter
    // Base class for all Control Builder objects
    public abstract class ControlBuilderObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class Variable : ControlBuilderObject
    {
        public string DataType { get; set; }
        public string InitialValue { get; set; }
        public string Attribute { get; set; }
        public bool IsRetained { get; set; }
    }

    public class FunctionBlock : ControlBuilderObject
    {
        public List<Variable> Variables { get; set; } = new List<Variable>();
        public List<string> CodeBlocks { get; set; } = new List<string>();
        public string TaskConnection { get; set; }
    }

    public class Program : ControlBuilderObject
    {
        public List<Variable> Variables { get; set; } = new List<Variable>();
        public List<FunctionBlock> FunctionBlocks { get; set; } = new List<FunctionBlock>();
        public string ExecutionOrder { get; set; }
    }

    public class ControlBuilderData
    {
        public List<Program> Programs { get; set; } = new List<Program>();
        public List<Variable> GlobalVariables { get; set; } = new List<Variable>();
        public Dictionary<string, string> ProjectConstants { get; set; } = new Dictionary<string, string>();
        public string ProjectName { get; set; }
        public string ExportDate { get; set; }
        public string Version { get; set; }
    }

    // Viewer-specific classes
    public class DataViewer
    {
        private readonly ControlBuilderData _data;
        private const int MENU_WIDTH = 80;

        public DataViewer(ControlBuilderData data)
        {
            _data = data;
        }

        public void ShowMainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                PrintHeader("Control Builder Data Viewer");
                Console.WriteLine($"Project: {_data.ProjectName} (Version: {_data.Version})");
                Console.WriteLine($"Export Date: {_data.ExportDate}");
                Console.WriteLine(new string('-', MENU_WIDTH));
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. View Programs");
                Console.WriteLine("2. View Global Variables");
                Console.WriteLine("3. View Project Constants");
                Console.WriteLine("4. Search All Objects");
                Console.WriteLine("5. Export Summary Report");
                Console.WriteLine("6. Exit");
                Console.Write("\nSelect an option (1-6): ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1":
                        ViewPrograms();
                        break;
                    case "2":
                        ViewGlobalVariables();
                        break;
                    case "3":
                        ViewProjectConstants();
                        break;
                    case "4":
                        SearchObjects();
                        break;
                    case "5":
                        ExportSummaryReport();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ViewPrograms()
        {
            Console.Clear();
            PrintHeader("Programs");

            if (_data.Programs.Count == 0)
            {
                Console.WriteLine("No programs found.");
                WaitForKey();
                return;
            }

            for (int i = 0; i < _data.Programs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_data.Programs[i].Name}");
            }

            Console.Write("\nSelect a program number (or 0 to return): ");
            if (int.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= _data.Programs.Count)
            {
                ViewProgramDetails(_data.Programs[selection - 1]);
            }
        }

        private void ViewProgramDetails(Program program)
        {
            Console.Clear();
            PrintHeader($"Program: {program.Name}");
            Console.WriteLine($"Description: {program.Description}");
            Console.WriteLine($"Execution Order: {program.ExecutionOrder}");
            Console.WriteLine("\nVariables:");
            foreach (var variable in program.Variables)
            {
                Console.WriteLine($"- {variable.Name} ({variable.DataType}): {variable.InitialValue}");
            }

            Console.WriteLine("\nFunction Blocks:");
            foreach (var fb in program.FunctionBlocks)
            {
                Console.WriteLine($"- {fb.Name} (Task: {fb.TaskConnection})");
            }

            WaitForKey();
        }

        private void ViewGlobalVariables()
        {
            Console.Clear();
            PrintHeader("Global Variables");

            if (_data.GlobalVariables.Count == 0)
            {
                Console.WriteLine("No global variables found.");
                WaitForKey();
                return;
            }

            foreach (var variable in _data.GlobalVariables)
            {
                Console.WriteLine($"Name: {variable.Name}");
                Console.WriteLine($"Type: {variable.DataType}");
                Console.WriteLine($"Initial Value: {variable.InitialValue}");
                Console.WriteLine(new string('-', 40));
            }

            WaitForKey();
        }

        private void ViewProjectConstants()
        {
            Console.Clear();
            PrintHeader("Project Constants");

            if (_data.ProjectConstants.Count == 0)
            {
                Console.WriteLine("No project constants found.");
                WaitForKey();
                return;
            }

            foreach (var constant in _data.ProjectConstants)
            {
                Console.WriteLine($"{constant.Key} = {constant.Value}");
            }

            WaitForKey();
        }

        private void SearchObjects()
        {
            Console.Clear();
            PrintHeader("Search Objects");
            Console.Write("Enter search term: ");
            string searchTerm = Console.ReadLine()?.ToLower() ?? "";

            var results = new List<string>();

            // Search programs
            foreach (var program in _data.Programs)
            {
                if (program.Name.ToLower().Contains(searchTerm))
                    results.Add($"Program: {program.Name}");

                foreach (var variable in program.Variables)
                {
                    if (variable.Name.ToLower().Contains(searchTerm))
                        results.Add($"Variable in {program.Name}: {variable.Name}");
                }

                foreach (var fb in program.FunctionBlocks)
                {
                    if (fb.Name.ToLower().Contains(searchTerm))
                        results.Add($"Function Block in {program.Name}: {fb.Name}");
                }
            }

            // Search global variables
            foreach (var variable in _data.GlobalVariables)
            {
                if (variable.Name.ToLower().Contains(searchTerm))
                    results.Add($"Global Variable: {variable.Name}");
            }

            // Search constants
            foreach (var constant in _data.ProjectConstants)
            {
                if (constant.Key.ToLower().Contains(searchTerm))
                    results.Add($"Project Constant: {constant.Key} = {constant.Value}");
            }

            Console.WriteLine($"\nFound {results.Count} results:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            WaitForKey();
        }

        private async void ExportSummaryReport()
        {
            Console.Clear();
            PrintHeader("Export Summary Report");

            var report = new
            {
                ProjectSummary = new
                {
                    _data.ProjectName,
                    _data.Version,
                    _data.ExportDate,
                    ProgramCount = _data.Programs.Count,
                    GlobalVariableCount = _data.GlobalVariables.Count,
                    ConstantCount = _data.ProjectConstants.Count
                },
                Programs = _data.Programs.Select(p => new
                {
                    p.Name,
                    VariableCount = p.Variables.Count,
                    FunctionBlockCount = p.FunctionBlocks.Count
                }).ToList()
            };

            string filename = $"{_data.ProjectName}_Summary_{DateTime.Now:yyyyMMdd_HHmmss}.json";
            object value = await File.WriteAllTextAsync(filename, JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine($"Summary report exported to: {filename}");
            WaitForKey();
        }

        private void PrintHeader(string title)
        {
            Console.WriteLine(new string('=', MENU_WIDTH));
            Console.WriteLine(title.PadLeft((MENU_WIDTH + title.Length) / 2));
            Console.WriteLine(new string('=', MENU_WIDTH));
            Console.WriteLine();
        }

        private void WaitForKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Control Builder Data Viewer");
                Console.WriteLine("==========================");

                // Get the data file to view
                Console.Write("Enter the path to the data file: ");
                string filePath = Console.ReadLine()?.Trim() ?? "";

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found!");
                    return;
                }

                // Load and deserialize the data
                string jsonContent = await File.ReadAllTextAsync(filePath);
                var data = JsonSerializer.Deserialize<ControlBuilderData>(jsonContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (data == null)
                {
                    Console.WriteLine("Unable to read data file!");
                    return;
                }

                // Create and show the viewer
                var viewer = new DataViewer(data);
                viewer.ShowMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}