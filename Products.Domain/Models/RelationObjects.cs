using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyReset.Domain.Models
{
    public class RelationObjects
    {
        public string ReferenceName { get; set; }
        public string TableSource { get; set; }
        public string TableDestination { get; set; }
    }
}
