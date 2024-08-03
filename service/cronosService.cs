using Cronos.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.service
{
    internal class cronosService
    {
        public static string httpServices() {

            string returnString = "Failed";
            LogDto newLog = new LogDto();

            try
            {
                newLog.id = 1;
                newLog.execId = 1;
                newLog.type = 1;

                HttpWebResponse response = httpRequests.GetResponse("http://localhost:8084/EnemSimulado");
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    returnString =  reader.ReadToEnd();


                    newLog.result = returnString;

                    sqlService.saveLog(newLog);
                }
            }
            catch (Exception ex)
            {
                returnString = ex.Message;
                newLog.result = returnString;

                sqlService.saveLog(newLog);
            }

            return returnString;
        }


        public static List<RichText> healthChecks(){

            List<RichText> returnString = new List<RichText>();           
                
            returnString = sqlService.getHealthChecks();


            return returnString;
        }
    }
}
