using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyReset.Domain.Models
{
    public class TableObject
    {
        [PrimaryKey]
        public string ObjectName { get; set; }

        public string Object_SCHEMA { get; set; }

        public bool IsMappend { get; set; }

        public bool  IsRelation_Inference { get; set; }

        public int Reference_Count { get; set; }

        public bool  Clear { get; set; }

        public bool  Reset_Key { get; set; }

    }
}
