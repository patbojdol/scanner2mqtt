using System.IO.Ports;
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

	public class ScannerReader : IDisposable
	{
		private SerialPort _port;

		public ScannerReader(string portName, int baudRate)
		{
			_port = new SerialPort(portName, baudRate);
			_port.Open();
			_port.DataReceived += DataReceived;
			Console.WriteLine("Ready.");
		}

		/// Serial event handler
		void DataReceived(object s, SerialDataReceivedEventArgs e)
		{
			Console.WriteLine(_port.ReadLine());
		}

		public void Dispose()
		{
			if (_port != null)
			{
				_port.Dispose();
			}
		}
	}
}