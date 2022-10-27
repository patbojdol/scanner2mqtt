namespace CodeScanner
{
	using System.IO.Ports;
	using System.Text.RegularExpressions;

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

		bool ValidateEAN13(string code)
		{
			if (!Regex.IsMatch(code, @"^\d{13}$"))
			{
				return false;
			}

			var codeNumbers = code.Select(digit => int.Parse(digit.ToString())).ToArray();

			int even = 0, odd = 0;
			for (var i = 0; i < code.Length - 1; i += 1)
			{
				if (i % 2 == 0) even += codeNumbers[i];
				else odd += codeNumbers[i];
			}

			int total = even + odd * 3;
			int checksum = total % 10;
			if (checksum != 0) checksum = 10 - checksum;

			return codeNumbers.Last() == checksum;
		}

		/// Serial event handler
		void DataReceived(object s, SerialDataReceivedEventArgs e)
		{
			var code = _port.ReadLine().Trim();

			if (ValidateEAN13(code))
			{
				Console.WriteLine($"EAN13: {code} valid");
			}
			else
			{
				Console.WriteLine(code);
			}
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