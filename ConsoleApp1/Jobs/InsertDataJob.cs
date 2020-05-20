using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApp1.Jobs
{
    class InsertDataJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            if (QuerrySql.getMaxTimeHistory(362).Rows[0]["Time"].ToString().Equals(""))
            {
                insertHistory(DateTime.Now.AddDays(-1), DateTime.Now, 362); //insert Data from previous day until now
            }
            else
            {
                insertHistory(Convert.ToDateTime(QuerrySql.getMaxTimeHistory(362).Rows[0]["Time"]).AddSeconds(1), DateTime.Now, 362); //insert data from last time inserted until now
            }
            return Task.FromResult(true);
        }

        static void insertHistory(DateTime start, DateTime end, int id)
        {
            WebClient web = new WebClient();
            string jsonStr = web.DownloadString("https://www.nldc.evn.vn/Renewable/Scada/GetScadaNhaMay?start=" + start.ToString("yyyyMMddHHmmss") + "&end=" + end.ToString("yyyyMMddHHmmss") + "&idNhaMay=" + id);//get JSON from Request
            JavaScriptSerializer java = new JavaScriptSerializer();
            Success list = (Success)java.Deserialize(jsonStr, typeof(Success)); //convert JSON to List of Model
            if (list.success.Equals("True"))
            {
                foreach (DataModel item in list.data)
                {
                    QuerrySql.insertHistory(Convert.ToDateTime(item.time.Replace('-', '/').Replace('T', ' ')), item.capacity, item.ghi, item.envtemp, id); //Convert 2020-05-19T07:53:00 to 2020/05/19 07:53:00
                }
            }
            else
            {
                Console.WriteLine("Can't get factory's information");
            }
        }
    }
}
