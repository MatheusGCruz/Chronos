using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.dto
{
    internal class LogDto
    {
        public int id { get; set; }
        public int type { get; set; }
        public int execId { get; set; }
        public string result { get; set; }
        public DateTime loggedAt { get; set; }

    }
}
