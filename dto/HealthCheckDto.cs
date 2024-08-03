using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.dto
{
    internal class HealthCheckDto
    {
        public int id {  get; set; }
        public string system_name { get; set; }
        public string system_uri { get; set; }
        public string environment { get; set; }


    }
}
