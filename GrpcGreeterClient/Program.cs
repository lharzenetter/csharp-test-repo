using System;
using CommandLine;

using Grpc.Net.Client;
using GrpcGreeter;

using Lukas.Commandline;

await Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsedAsync(async (CommandLineOptions options) => {
    var endpoint = $"http://{options.Host}:{options.Port}";

    // The port number must match the port of the gRPC server.
    using var channel = GrpcChannel.ForAddress(endpoint);
    var client = new Greeter.GreeterClient(channel);
    var reply = await client.SayHelloAsync(
                      new HelloRequest { Name = "GreeterClient", CheckNumer = options.InputNumber });
    Console.WriteLine(reply.Message);
});
