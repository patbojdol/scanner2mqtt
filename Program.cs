// MQTT: https://dev.to/eduardojuliao/basic-mqtt-with-c-1f88
// Serial: https://gist.github.com/mkchandler/4300971

namespace CodeScanner
{
	public class Program
	{
		private static void Main(string[] args)
		{
			var port = Environment.GetEnvironmentVariable("SERIAL_PORT");

			if (String.IsNullOrEmpty(port))
			{
				Console.WriteLine("You need to specify SERIAL_PORT environment variable");
				System.Environment.Exit(1);
			}
			else
			{
				var scanner = new ScannerReader(port, 115200);
				Console.ReadLine();
			}
		}
	}
}