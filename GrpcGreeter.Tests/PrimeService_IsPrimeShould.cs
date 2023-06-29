using GrpcGreeter.Services;

namespace GrpcGreeter.Tests;

[TestFixture]
public class PrimeService_IsPrimeShould
{
    private PrimeService _primeService;

    [SetUp]
    public void SetUp()
    {
        _primeService = new PrimeService();
    }

    [Test]
    public void IsPrime_InputIs1_ReturnFalse()
    {
        var result = _primeService.IsPrime(1);

        Assert.IsFalse(result, "1 should not be prime");
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    public void IsPrime_InputLessThan2_ReturnFalse(int value) {
        var result = _primeService.IsPrime(value);

        Assert.IsFalse(result, $"{value} should not be prime!");
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(11)]
    [TestCase(17)]
    public void IsPrime_InputPrimeNumbers_ReturnTrue(int value) {
        var result = _primeService.IsPrime(value);

        Assert.IsTrue(result, $"{value} should be prime!");
    }
}