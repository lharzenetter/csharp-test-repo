using System;

namespace GrpcGreeter.Services;

public class PrimeService
{
    public bool IsPrime(int candidate)
    {
        if (candidate == 2 || candidate == 3) {
            return true;
        }

        if (candidate <= 1 || candidate % 2 == 0 || candidate % 3 == 0) {
            return false;
        }

        for (int i = 5; i * i <= candidate; i += 6) {
            if (candidate % i == 0 || candidate % (i + 2) == 0) {
                return false;
            }
        }

        return true;
    }
}