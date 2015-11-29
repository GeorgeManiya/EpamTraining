using System;
using System.IO;

namespace SalesBisnessLogic.CSV
{
    public class CSVFileReader : IDisposable
    {
        private readonly string _filePath;
        private readonly StreamReader _reader;

        public CSVFileReader(string filePath)
        {
            _filePath = filePath;
            _reader = new StreamReader(_filePath);
        }

        public string[] ReadLine()
        {
            var line = _reader.ReadLine();
            var values = line.Split(';');
            return values;
        }

        public bool EndOfStream { get { return _reader.EndOfStream; } }

        public void Dispose()
        {
            _reader.Close();
            _reader.Dispose();
        }
    }
}
