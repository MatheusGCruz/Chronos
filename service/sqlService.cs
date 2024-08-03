using Cronos.dal;
using Cronos.dto;
using Cronos.service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.service
{
    internal class sqlService
    {
        public static List<RichText> getHealthChecks() {

            List < HealthCheckDto > healthChecks = new List < HealthCheckDto > ();
            List<RichText> returnText = new List <RichText> ();

            LogDto newLog = new LogDto();

            newLog.id = 1;
            newLog.execId = 1;
            newLog.type = 1;

                try
            {
                healthChecks = sqlDalOperations.queryHealthChecks();
                foreach (HealthCheckDto healthCheck in healthChecks) {
                    try
                    {
                        if (httpRequests.GetBooleanResponse(healthCheck.system_uri))
                        {
                            returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Green));
                            newLog.result = "Valid";
                        }
                        else
                        {
                            returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Red));
                            newLog.result = "Invalid";
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Red));
                        newLog.result = ex.ToString();
                    }
                    returnText.Add(TextAuxiliaryService.getRichText(healthCheck.system_name, Color.Blue));
                    returnText.Add(TextAuxiliaryService.getRichText(Environment.NewLine, Color.Black));
                }
                sqlDalOperations.saveLog(newLog);
                return returnText;
            }
            catch(Exception ex)
            {
                returnText.Add(TextAuxiliaryService.getRichText(ex.Message, Color.Red));
                newLog.result = ex.ToString();
                sqlDalOperations.saveLog(newLog);

                return returnText;
            }

        }

        public static void saveLog(LogDto newLog) {sqlDalOperations.saveLog(newLog);}

    }
}
