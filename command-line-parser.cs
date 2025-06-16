// dotnet run .\command-line-parser.cs --echo "Hello World!"
#:property TargetFramework net10.0

#:package CommandLineParser@2.9.1

using CommandLine;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(RunOptions);

static void RunOptions(Options options)
{
    Console.WriteLine("Echo: " + options.Echo);
}

public class Options
{
    [Option("echo", Required = true, HelpText = "Specify what should be echoed back")]
    public required string Echo { get; set; }
}
