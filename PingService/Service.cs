using ManagerLib;
using Microsoft.Win32;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace PingService
{
    public partial class Service : ServiceBase
    {

        private string testFilePath;



        Timer timer;
        public Service()
        {
            InitializeComponent();

            testFilePath = AppDomain.CurrentDomain.BaseDirectory + "TestDoc.txt";
        }


        public void OnDebug()
        {
            OnStart(null);
        }


        protected override void OnStart(string[] args)
        {

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<ClassChecker>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("Checker", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);

        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {


            if (CheckIfPPIsRunning())
            {
                if (Manager.Decrypt(Manager.GetDataFromRegistry("Status")) == "Offline")
                {
                    this.Stop();
                }
            }
            else
            {
                this.Stop();
            }

        }


        private bool CheckIfPPIsRunning()
        {
            Process[] pname = Process.GetProcessesByName("POWERPNT");
            if (pname.Length != 0) return true;
            else return false;
        }

        

        protected override void OnStop()
        {
            /*
            make connect to remote database here
            */
            Random rand = new Random();
            int val = rand.Next(1, 10);
            if (File.Exists(testFilePath))
            {
                File.AppendAllText(testFilePath, val.ToString() + Environment.NewLine);
            }
        }
    }
    public class ClassChecker : IJob
    {
        string testFilePath = AppDomain.CurrentDomain.BaseDirectory + "TestDoc.txt";

        public void Execute(IJobExecutionContext context)
        {
            
        }
    }
}
