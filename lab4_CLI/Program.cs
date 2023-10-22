using McMaster.Extensions.CommandLineUtils;
using lab4_classes;

class Program
{
    public static string GetInputFilesPath(CommandOption option, string defFileName)
    {
        
        string optionValue = option.HasValue() ? option.Value() : null;

        if (!string.IsNullOrEmpty(optionValue))
        {
            return optionValue;
        }

        string labPath = Environment.GetEnvironmentVariable("LAB_PATH", EnvironmentVariableTarget.User);
        string labPathFile = Path.Combine(labPath, defFileName);
        if (!string.IsNullOrEmpty(labPath) && File.Exists(labPathFile))
        {
            return labPathFile;
        }

        string currentDirectory = Directory.GetCurrentDirectory();
        string defaultPath = Path.Combine(currentDirectory, defFileName);

        if (File.Exists(defaultPath))
        {
            return defaultPath;
        }

        Console.WriteLine($"{defFileName} file not found.");
        Environment.Exit(1);
        return null;
    }

    static int Main(string[] args)
    {
        var app = new CommandLineApplication
        {
            Name = "lab4_CLI"
        };

        app.HelpOption("-?|-h|--help");

        app.Command("version", versionCmd =>
        {
            versionCmd.Description = "Display information about the version of the application and the author";

            versionCmd.OnExecute(() =>
            {
                Console.WriteLine("Lab 4 CLI");
                Console.WriteLine("Author: Yaroslav Chushenko");
                Console.WriteLine("Version: 1.0.0");
                return 0;
            });
        });

        app.Command("run", runCmd =>
        {
            runCmd.Description = "Run a specific lab";

            runCmd.Command("lab1", labCmd =>
            {
                labCmd.Description = "Run lab1";
                labCmd.UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect;

                var inputOption = labCmd.Option("-I|--input <INPUT>", "Input file path", CommandOptionType.SingleValue);
                var outputOption = labCmd.Option("-o|--output <OUTPUT>", "Output file path", CommandOptionType.SingleValue);

                labCmd.OnExecute(() =>
                {
                    string inputPath = GetInputFilesPath(inputOption, "INPUT.TXT");
                    string outputPath = GetInputFilesPath(outputOption, "OUTPUT.TXT");
                    Lab1.Run(inputPath, outputPath);
                    return 0;
                });
            });

            runCmd.Command("lab2", labCmd =>
            {
                labCmd.Description = "Run lab1";
                labCmd.UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect;

                var inputOption = labCmd.Option("-I|--input <INPUT>", "Input file path", CommandOptionType.SingleValue);
                var outputOption = labCmd.Option("-o|--output <OUTPUT>", "Output file path", CommandOptionType.SingleValue);

                labCmd.OnExecute(() =>
                {
                    string inputPath = GetInputFilesPath(inputOption, "INPUT.TXT");
                    string outputPath = GetInputFilesPath(outputOption, "OUTPUT.TXT");
                    Lab2.Run(inputPath, outputPath);
                    return 0;
                });
            });

            runCmd.Command("lab3", labCmd =>
            {
                labCmd.Description = "Run lab3";
                labCmd.UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect;

                var inputOption = labCmd.Option("-I|--input <INPUT>", "Input file path", CommandOptionType.SingleValue);
                var outputOption = labCmd.Option("-o|--output <OUTPUT>", "Output file path", CommandOptionType.SingleValue);

                labCmd.OnExecute(() =>
                {
                    string inputPath = GetInputFilesPath(inputOption, "INPUT.TXT");
                    string outputPath = GetInputFilesPath(outputOption, "OUTPUT.TXT");
                    Lab3.Run(inputPath, outputPath);
                    return 0;
                });
            });

            runCmd.OnExecute(() =>
            {
                Console.WriteLine("Please specify a lab to run (lab1, lab2, lab3).");
                return 1;
            });
        });

        app.Command("set-path", setPathCmd =>
        {
            setPathCmd.Description = "Set the path for input and output files.";

            var pathOption = setPathCmd.Option("-p|--path <PATH>", "Path to the directory with input and output files.", CommandOptionType.SingleValue);
            pathOption.IsRequired();
            setPathCmd.OnExecute(() =>
            {
                string pathValue = pathOption.Value();
                if (!string.IsNullOrEmpty(pathValue))
                {
                    Environment.SetEnvironmentVariable("LAB_PATH", pathValue, EnvironmentVariableTarget.User);
                    Console.WriteLine($"LAB_PATH has been set to: {pathValue}");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Please provide a valid path using the -p or --path option.");
                    return 1;
                }
            });
        });

        app.OnExecute(() =>
        {
            Console.WriteLine("Unknown command or incorrect syntax.");
            app.ShowHelp();
            return 1;
        });
        try
        {
            return app.Execute(args);
        }
        catch (UnrecognizedCommandParsingException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Use --help to see available commands and options.");
            return 1;
        }
    }
}
