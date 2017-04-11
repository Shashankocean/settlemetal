using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace settleMetal
{
    class UserDB
    {
        [PrimaryKey, AutoIncrement]
        public int user_id { get; set; }
        public string user_name { get; set; }
        [Unique]
        public string user_email { get; set; }
        public string pass4word { get; set; }
        public long user_phone { get; set; }
        public string active { get; set; }
    }
}
