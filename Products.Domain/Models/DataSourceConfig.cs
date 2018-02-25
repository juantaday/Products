using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyReset.Domain.Models
{
    public class DataSourceConfig
    {
        [PrimaryKey]
        public string  DataSourceConfigID { get; set; }

        public string  DataSource { get; set; }

        public string  ServerName { get; set; }

        public string  TypeAuthentication { get; set; }

        public string  UserName { get; set; }

        public string  Password { get; set; }

        public bool  SaveMyPassword { get; set; }

        public string  DataBaseName { get; set; }

        public string StringConnection { get; set; }
    }
}
