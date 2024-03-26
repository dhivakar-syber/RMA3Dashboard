using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Abp.Auditing;
using Abp.UI;
using Abp.Dependency;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.EFPlus;
using Abp.Logging;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using Abp.BackgroundJobs;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Configuration;
using Microsoft.Extensions.Configuration;

namespace SyberGate.RMACT.Masters.Importing
{
    public class ImportGlobusDataToExcelJobWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly PartsAppService _partsAppService;
        public const bool IsEnabled = true;
        private const int CheckPeriodAsMilliseconds = 1 * 1000 * 60 * 60 * 24; 
        private readonly TimeSpan _logExpireTime = TimeSpan.FromDays(7);
        private FileSystemWatcher _fsw;
        private readonly IConfigurationRoot _appConfiguration;
        protected readonly IBackgroundJobManager BackgroundJobManager;
        private string path = "";

        public ImportGlobusDataToExcelJobWorker(
            AbpTimer timer,
            IAppConfigurationAccessor configurationAccessor,
            IBackgroundJobManager backgroundJobManager,
            PartsAppService partsAppService
            )
            : base(timer)
        {
            _partsAppService = partsAppService;
            LocalizationSourceName = RMACTConsts.LocalizationSourceName;
            _appConfiguration = configurationAccessor.Configuration;
            BackgroundJobManager = backgroundJobManager;

            Timer.Period = _appConfiguration.GetValue<int>("RMA3:BackupPeriodInSeconds") * 1000;
            Timer.RunOnStart = true;
            path = _appConfiguration.GetValue<string>("RMA3:ProcurePath");

            //_fsw = new FileSystemWatcher(path, "*.xlsx");
            //_fsw.NotifyFilter = NotifyFilters.Attributes
            //                     | NotifyFilters.CreationTime
            //                     | NotifyFilters.DirectoryName
            //                     | NotifyFilters.FileName
            //                     | NotifyFilters.LastAccess
            //                     | NotifyFilters.LastWrite
            //                     | NotifyFilters.Security
            //                     | NotifyFilters.Size;
            //_fsw.Created += _fsw_Created;
            //_fsw.Changed += _fsw_Changed;
            //_fsw.Renamed += _fsw_Renamed;
            //_fsw.Error += _fsw_Error;
            //_fsw.EnableRaisingEvents = true;
        }

        protected override void DoWork()
        {
    //        using (var uow = UnitOfWorkManager.Begin())
    //        {
				//var directory = new DirectoryInfo(path);
				//var myFile = directory.GetFiles()
			 //        .OrderByDescending(f => f.LastWriteTime)
			 //        .First();

				//Logger.Info("Procure Data Import worker started running at " + Clock.Now);
    //            _partsAppService.GetGlobusDataExcels(myFile.FullName);
    //            uow.Complete();
    //        }
        }

        private void _fsw_Error(object sender, ErrorEventArgs e) => Logger.Error("File error");
        private void _fsw_Renamed(object sender, RenamedEventArgs e) => Logger.Info("File Renamed");
        private void _fsw_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(e.FullPath);
            var args = new ImportGlobusDatasFromExcelJobArgs
            {
                Bytes = fileBytes
            };
            BackgroundJobManager.EnqueueAsync<ImportGlobusDatasToExcelJob, ImportGlobusDatasFromExcelJobArgs>(args);
            Logger.Info("File changed");
        }
        private void _fsw_Created(object sender, FileSystemEventArgs e) => Logger.Info("File created");
    }
}
