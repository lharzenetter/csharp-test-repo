using Grpc.Core;

namespace GrpcGreeter.Services;

public class GreeterService : Greeter.GreeterBase {

    private readonly ILogger<GreeterService> _logger;
    private readonly PrimeService _primeService;
    private readonly EvenOddCheckerService _evenOddService;

    public GreeterService(ILogger<GreeterService> logger) {
        _logger = logger;
        _primeService = new PrimeService();
        _evenOddService = new EvenOddCheckerService();
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) {
        switch (request.AnalysisType) {
            case CheckType.Prime:
                string isPrime = _primeService.IsPrime(request.CheckNumer) ? "" : "not ";
                return Task.FromResult(new HelloReply {
                    Message = $"Hello {request.Name}!\nYour supplied number {request.CheckNumer} is {isPrime}prime"
                });
            case CheckType.EvenNumber:
                string isEven = _evenOddService.isEven(request.CheckNumer) ? "" : "not ";
                return Task.FromResult(new HelloReply {
                    Message = $"Hello {request.Name}!\nYour supplied number {request.CheckNumer} is {isEven}even"
                });
            case CheckType.OddNumber:
                string isOdd = _evenOddService.isOdd(request.CheckNumer) ? "" : "not ";
                return Task.FromResult(new HelloReply {
                    Message = $"Hello {request.Name}!\nYour supplied number {request.CheckNumer} is {isOdd}odd"
                });
        }

        return Task.FromResult(new HelloReply {
            Message = $"Hello {request.Name}!\nYou supplied a wrong check type!"
        });
    }

}
