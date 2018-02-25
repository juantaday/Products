using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyReset.Domain.Models
{
    public class ObjectTriggers
    {
        public string TableName { get; set; }

        public string  TriggerName { get; set; }

        public string  Type { get; set; }

        public string  Insert { get; set; }

        public string Update { get; set; }

        public string Delete { get; set; }

        public string State { get; set; }
    }
}