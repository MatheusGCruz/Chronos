using Cronos.dto;
using Cronos.service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.dal
{
    internal class sqlDalOperations
    {
        static string sqlString = System.Configuration.ConfigurationSettings.AppSettings["ChronosSqlConnection"];

        public static List<HealthCheckDto> queryHealthChecks() {
            SqlConnection sqlConnection = new SqlConnection(sqlString);
            string queryString = "SELECT id, system_name, system_uri, environment FROM health_checks";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);

            List < HealthCheckDto > returnList = new List<HealthCheckDto>();

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}, {2}",reader["id"], reader["system_name"], reader["system_uri"]));// etc
                        HealthCheckDto returnDto = new HealthCheckDto();
                        returnDto.id = Int32.Parse(reader["id"].ToString());
                        returnDto.system_name = reader["system_name"].ToString();
                        returnDto.system_uri = reader["system_uri"].ToString() ;
                        returnDto.environment = reader["environment"].ToString();

                        if(returnDto.environment == System.Configuration.ConfigurationSettings.AppSettings["Enviroment"])
                        {
                            returnList.Add(returnDto);
                        }
                        
                }
                reader.Close();
            }
                catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return returnList;
            }

            sqlConnection.Close();
            return returnList;
        }

        public static List<ProcessCheckDto> queryProcessChecks()
        {
            SqlConnection sqlConnection = new SqlConnection(sqlString);
            string queryString = "SELECT id, system_name, system_uri, environment FROM process_checks";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);

            List<ProcessCheckDto> returnList = new List<ProcessCheckDto>();

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0}, {1}, {2}", reader["id"], reader["system_name"], reader["system_uri"]));// etc
                    ProcessCheckDto returnDto = new ProcessCheckDto();
                    returnDto.id = Int32.Parse(reader["id"].ToString());
                    returnDto.system_name = reader["system_name"].ToString();
                    returnDto.system_uri = reader["system_uri"].ToString();
                    returnDto.environment = reader["environment"].ToString();

                    if (returnDto.environment == System.Configuration.ConfigurationSettings.AppSettings["Enviroment"])
                    {
                        returnList.Add(returnDto);
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return returnList;
            }

            sqlConnection.Close();
            return returnList;
        }

        public static void saveLog(LogDto newLog) {
            SqlConnection sqlConnection = new SqlConnection(sqlString);
            string insertString = "INSERT INTO exec_logs (type,exec_id,http_result,result,logged_at) VALUES(@type, @execId,@httpResult,@result, GETDATE())";

            try
            {
                SqlCommand command = new SqlCommand(insertString, sqlConnection);

                command.Parameters.AddWithValue("@type", newLog.type);
                command.Parameters.AddWithValue("@execId", newLog.execId);
                command.Parameters.AddWithValue("@httpResult", newLog.httpResult);
                command.Parameters.AddWithValue("@result", TextAuxiliaryService.truncate(newLog.result));
                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sqlConnection.Close();
        }




    }


}
