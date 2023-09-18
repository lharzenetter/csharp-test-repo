using Grpc.Core;
using Grpc.Net.Client;
using Leuze.RES.Services.Sensors.grpc;
using System.CommandLine;

RootCommand rootCommand = new("GRPC Client Program");

Option<string> optionIp = new(
    name: "--ip",
    description: "IP address",
    getDefaultValue: () => "127.0.0.1");
Option<string> optionPort = new(
    name: "--port",
    description: "Port",
    getDefaultValue: () => "5083");
Option<bool> optionHelp = new(
    name: "--help",
    description: "Show help",
    getDefaultValue: () => false);

rootCommand.AddOption(optionIp);
rootCommand.AddOption(optionPort);
rootCommand.AddOption(optionHelp);

rootCommand.SetHandler(async (ip, port, help) => {
    if (help) {
        Console.WriteLine("Usage:  --ip <IP_ADDRESS>  --port <PORT>");
        return;
    }

    string serverAddress = $"http://{ip}:{port}";

    using GrpcChannel channel = GrpcChannel.ForAddress(serverAddress);
    SensorGrpcService.SensorGrpcServiceClient client = new(channel);

    try {
        Console.WriteLine($"Sending request to {serverAddress}");
        ConnectSensorRequest request = new() {
            SensorId = "0815",
            IpAddress = "127.0.0.1",
            Port = "9000"
        };
        ConnectSensorResponse response = await client.ConnectSensorAsync(request);
        Console.WriteLine($"Received response:  Result = {response.Status}");
    } catch (RpcException ex) {
        Console.WriteLine($"Error: {ex.Status}");
    }
}, optionIp, optionPort, optionHelp);

int parseResult = await rootCommand.InvokeAsync(args);
