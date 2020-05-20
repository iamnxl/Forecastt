using MathNet.Numerics.LinearRegression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using ConsoleApp1;
using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

namespace ConsoleApp1
{
    class Service
    {
        public Service(){ }

        public void Start()
        {
            while (true)
            {
                DateTime temp = DateTime.Now;
                if (Math.Floor((double)temp.Minute / 1.0) * 1 == temp.Minute) //timer start each 5 minutes: Ex: 5h00, 5h05, 5h10 ....
                {
                    var properties = new NameValueCollection
                    {
                        ["quartz.scheduler.instanceName"] = "XmlConfiguredInstance",
                        ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                        ["quartz.threadPool.threadCount"] = "5",
                        ["quartz.plugin.xml.type"] = "Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin, Quartz.Plugins",
                        ["quartz.plugin.xml.fileNames"] = "~/quartzJobsAndTriggers.xml",
                        ["quartz.plugin.xml.FailOnFileNotFound"] = "true",
                        ["quartz.plugin.xml.failOnSchedulingError"] = "true"
                    };
                    IScheduler scheduler = new StdSchedulerFactory(properties).GetScheduler().Result;
                    scheduler.Start();
                    break;
                }
            }
        }
        public void Stop() { }
    }
}
