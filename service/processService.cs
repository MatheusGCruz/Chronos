using Cronos.dal;
using Cronos.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cronos.service
{
    internal class processService
    {
        public static int getIntResponse(ProcessCheckDto processCheck) {

            int response = 0;
            LogDto newLog = new LogDto();
            newLog.execId = processCheck.id;
            newLog.type = 1;            

            try
            {
                Process[] pname = Process.GetProcessesByName(processCheck.system_name);
                if (pname.Length == 0)
                {
                    try
                    {
                        Process.Start(processCheck.system_uri);
                        newLog.result = ("Starting");
                        newLog.httpResult = 1001;
                        response = 1;
                    }
                    catch (Exception ex)
                    {
                        newLog.httpResult = 408;
                        newLog.result = ex.Message;
                        sqlDalOperations.saveLog(newLog);
                    }

                }
                    
                else
                {
                    newLog.result = ("Running");
                    newLog.httpResult = 1002;
                    response = 2;
                }
                sqlDalOperations.saveLog(newLog);
            }
            catch (Exception ex)
            {
                newLog.httpResult = 408;
                newLog.result = ex.Message;
                sqlDalOperations.saveLog(newLog);
            }

            return response;

        }
    }
}
