using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        public static Boolean GetBooleanResponse(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 1000;

            HttpWebResponse newResponse = (HttpWebResponse)request.GetResponse();
            //wRespStatusCode = newResponse.StatusCode;



            return (newResponse.StatusCode.ToString() == "OK");
        }
    }
}
