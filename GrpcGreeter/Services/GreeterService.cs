using Grpc.Core;
using GrpcGreeter;

namespace GrpcGreeter.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly PrimeService _primeService;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
        _primeService = new PrimeService();
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        string msg = "Hello " + request.Name + "!\n";
        string isPrime = _primeService.IsPrime(request.CheckNumer) ? "" : "not ";

        return Task.FromResult(new HelloReply
        {
            Message = $"Hello {request.Name}!\nYour supplied number {request.CheckNumer} is {isPrime}prime"
        });
    }
}
