namespace CodeScanner
{
	using System.IO.Ports;

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