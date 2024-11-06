using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ControlBuilderExporter
{
    public abstract class ControlBuilderObject
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public DateTime LastModified { get; set; }

        protected ControlBuilderObject()
        {
            LastModified = DateTime.Now;
        }
    }

    public class Variable : ControlBuilderObject
    {
        public string? DataType { get; set; }
        public string? InitialValue { get; set; }
        public string? Attribute { get; set; }
        public bool IsRetained { get; set; }
    }

    public class FunctionBlock : ControlBuilderObject
    {
        public List<Variable> Variables { get; set; }
        public List<string> CodeBlocks { get; set; }
        public string? TaskConnection { get; set; }

        public FunctionBlock()
        {
            Variables = new List<Variable>();
            CodeBlocks = new List<string>();
        }
    }

    public class ProgramUnit : ControlBuilderObject
    {
        public List<Variable> Variables { get; set; }
        public List<FunctionBlock> FunctionBlocks { get; set; }
        public string? ExecutionOrder { get; set; }

        public ProgramUnit()
        {
            Variables = new List<Variable>();
            FunctionBlocks = new List<FunctionBlock>();
        }
    }

    public class ControlBuilderData
    {
        public List<ProgramUnit> Programs { get; set; }
        public List<Variable> GlobalVariables { get; set; }
        public Dictionary<string, string> ProjectConstants { get; set; }
        public string? ProjectName { get; set; }
        public string? ExportDate { get; set; }
        public string? Version { get; set; }

        public ControlBuilderData()
        {
            Programs = new List<ProgramUnit>();
            GlobalVariables = new List<Variable>();
            ProjectConstants = new Dictionary<string, string>();
        }
    }

    public class DataExporter
    {
        private readonly string _exportPath;

        public DataExporter(string exportPath)
        {
            _exportPath = exportPath;
            Directory.CreateDirectory(exportPath);
        }

        public async Task ExportDataAsync(ControlBuilderData data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            string filename = $"{data.ProjectName ?? "Export"}_{DateTime.Now:yyyyMMdd_HHmmss}.json";
            await File.WriteAllTextAsync(Path.Combine(_exportPath, filename), json);
        }
    }

    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Control Builder Data Exporter");
            Console.WriteLine("============================");

            try
            {
                Console.Write("Enter export directory (or press Enter for current directory): ");
                string? exportDir = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(exportDir))
                {
                    exportDir = Directory.GetCurrentDirectory();
                }

                var exporter = new DataExporter(exportDir);
                var data = new ControlBuilderData
                {
                    ProjectName = "SampleProject",
                    ExportDate = DateTime.Now.ToString("o"),
                    Version = "1.0"
                };

                await exporter.ExportDataAsync(data);

                Console.WriteLine($"\nData successfully exported to {exportDir}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}