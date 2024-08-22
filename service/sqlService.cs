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
                try
            {
                healthChecks = sqlDalOperations.queryHealthChecks();
                foreach (HealthCheckDto healthCheck in healthChecks) {
                    try
                    {
                        if (httpRequests.GetHealthCheckBooleanResponse(healthCheck))
                        {
                            returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Green));
                        }
                        else
                        {
                            returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Red));
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Red));
                    }
                    returnText.Add(TextAuxiliaryService.getRichText(healthCheck.system_name, Color.Blue));
                    returnText.Add(TextAuxiliaryService.getRichText(Environment.NewLine, Color.Black));
                }

                return returnText;
            }
            catch(Exception ex)
            {
                returnText.Add(TextAuxiliaryService.getRichText(ex.Message, Color.Red));
                return returnText;
            }

        }

        public static List<RichText> getProcessChecks()
        {

            List<ProcessCheckDto> processChecks = new List<ProcessCheckDto>();
            List<RichText> returnText = new List<RichText>();
            try
            {
                processChecks = sqlDalOperations.queryProcessChecks();
                foreach (ProcessCheckDto processCheck in processChecks)
                {
                    try
                    {
                        switch (processService.getIntResponse(processCheck))
                        {
                            case 0:
                                returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Red));
                                break;
                            case 1:
                                returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Yellow));
                                break;
                            case 2:
                                returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Green));
                                break;
                            default:
                                returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Red));
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        returnText.Add(TextAuxiliaryService.getRichText(" 🟢 ", Color.Red));
                    }
                    returnText.Add(TextAuxiliaryService.getRichText(processCheck.system_name, Color.Blue));
                    returnText.Add(TextAuxiliaryService.getRichText(Environment.NewLine, Color.Black));
                }

                return returnText;
            }
            catch (Exception ex)
            {
                returnText.Add(TextAuxiliaryService.getRichText(ex.Message, Color.Red));
                return returnText;
            }

        }

        public static void saveLog(LogDto newLog) {sqlDalOperations.saveLog(newLog);}

    }
}
