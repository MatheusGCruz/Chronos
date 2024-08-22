using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cronos.dto;
using Cronos.dal;

namespace Cronos.service
{
    internal class httpRequests
    {
        public static HttpWebResponse GetResponse(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //using (Stream stream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    //return reader.ReadToEnd();
            //}

            return (HttpWebResponse)request.GetResponse();
        }

        public static Boolean GetHealthCheckBooleanResponse(HealthCheckDto healthCheck)
        {
            LogDto newLog = new LogDto();
            newLog.execId = healthCheck.id;
            newLog.type = 1;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(healthCheck.system_uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 1000;

            try
            {
                HttpWebResponse newResponse = (HttpWebResponse)request.GetResponse();
                newLog.httpResult = (int)newResponse.StatusCode;

                using (var reader = new StreamReader(newResponse.GetResponseStream()))
                {
                    newLog.result = reader.ReadToEnd();
                }

                // newLog.result = newResponse.
                sqlDalOperations.saveLog(newLog);
                return (newResponse.StatusCode.ToString() == "OK");
            }
            catch (Exception ex)
            {
                newLog.httpResult = 408;
                newLog.result = ex.Message;
                sqlDalOperations.saveLog(newLog);
            }

            return false;

        }
    }
}
