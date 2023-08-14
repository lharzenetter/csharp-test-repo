namespace GrpcGreeter.Services;

public class EvenOddCheckerService {
	
	public bool isEven(int value) {
		return value % 2 == 0;
	}

	public bool isOdd(int value) {  return value % 2 == 1; }
}
