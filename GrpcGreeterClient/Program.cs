using System;
using CommandLine;

using Grpc.Net.Client;
using GrpcGreeter;

using Lukas.Commandline;

var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;

var endpoint = $"http://{options.Host}:{options.Port}";

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress(endpoint);
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

