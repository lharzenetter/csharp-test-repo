using CommandLine;

namespace Lukas.Commandline;

public class CommandLineOptions {

    [Option(shortName: 'h', longName: "host", Required = false, Default = "localhost", HelpText = "The hostname of the gRPC server")]
    public string Host { get; set; } = "localhost";

    [Option(shortName: 'p', longName: "port", Required = false, Default = 5228, HelpText = "The port of the gRPC server")]
    public int Port { get; set; } = 5228;


    [Value(index: 0, Required = true, HelpText = "The number to check")]
    public int InputNumber { get; set; } = 1;
}
