using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using SalesBisnessLogic;
using System.Threading;
using System.IO;

namespace SalesWatcherService
{
    partial class WatcherService : ServiceBase
    {
        private FileSystemWatcher _fileWatcher;
        private SalesDataCore _salesCore;

        public WatcherService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _salesCore = new SalesDataCore();
            _fileWatcher = new FileSystemWatcher(@"D:\SalesRepository\", "*.csv");
            _fileWatcher.Changed += OnFileChanged;
            //_fileWatcher.Created += OnFileCreated;
            _fileWatcher.EnableRaisingEvents = true;
        }

        protected override void OnStop()
        {
            _fileWatcher.EnableRaisingEvents = false;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var thread = new Thread(new ParameterizedThreadStart(ReadAndSendData));
            thread.Start(e.FullPath);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            var thread = new Thread(new ParameterizedThreadStart(ReadAndSendData));
            thread.Start(e.FullPath);
        }

        private void ReadAndSendData(object filePath)
        {
            Thread.Sleep(1000);
            var dataSales = _salesCore.Sales;
            var sales = _salesCore.Read(filePath.ToString());
            foreach (var sale in sales.Where(s => !dataSales.Any(dS => dS.SaleDate == s.SaleDate && dS.Manager.Name == s.Manager.Name)))
            {
                _salesCore.AddSale(sale);
            }
            _salesCore.Save();
        }
    }
}
