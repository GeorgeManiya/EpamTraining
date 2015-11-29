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
            _fileWatcher.Created += OnFileCreater;
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void OnFileCreater(object sender, FileSystemEventArgs e)
        {
            var filePath = e.FullPath;
            var sales = _salesCore.Read(filePath);
            foreach(var sale in sales)
            {
                _salesCore.AddSale(sale);
            }
            _salesCore.Save();
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            
        }

        protected override void OnStop()
        {
            
        }
    }
}
