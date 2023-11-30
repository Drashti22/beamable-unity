using Beamable.Server;

namespace Beamable.Microservices
{
	[Microservice("MyMicroservice")]
	public class MyMicroservice : Microservice
	{
		[ClientCallable]
        private int AddMyValues(int a, int b)
        {
            return a + b;
        }
    }
}
