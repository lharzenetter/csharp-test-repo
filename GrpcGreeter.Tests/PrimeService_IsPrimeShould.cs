using GrpcGreeter.Services;

namespace GrpcGreeter.Tests;

[TestFixture]
public class PrimeService_IsPrimeShould
{
    private PrimeService _primeService;

    [SetUp]
    public void SetUp() {
        _primeService = new PrimeService();
    }

    [Test]
    public void IsPrime_InputIs1_ReturnFalse() {
        var result = _primeService.IsPrime(1);

        Assert.That(result, Is.False);
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    public void IsPrime_InputLessThan2_ReturnFalse(int value) {
        var result = _primeService.IsPrime(value);

        Assert.That(result, Is.False);
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(11)]
    [TestCase(17)]
    public void IsPrime_InputPrimeNumbers_ReturnTrue(int value) {
        var result = _primeService.IsPrime(value);

        Assert.That(result, Is.True);
    }

    [TestCase(77)]
    public void IsNotPrime_InputPrimeNumbers_ReturnFalse(int value) {
        var result = _primeService.IsPrime(value);

        Assert.That(result, Is.False);
    }
}