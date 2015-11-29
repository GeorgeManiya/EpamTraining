using System;
using System.IO;
using System.Linq;

namespace SalesBisnessLogic.CSV
{
    public class CSVFileWriter : IDisposable
    {
        private readonly string _filePath;
        private readonly StreamWriter _writer;

        public CSVFileWriter(string filePath)
        {
            _filePath = filePath;
            _writer = new StreamWriter(_filePath, true);
        }

        public void WriteLine(object[] values)
        {
            var line = values.Aggregate(string.Empty, (l, current) => l += ";" + current).Substring(1);
            _writer.WriteLine(line);
        }

        public void Dispose()
        {
            _writer.Close();
            _writer.Dispose();
        }
    }
}
