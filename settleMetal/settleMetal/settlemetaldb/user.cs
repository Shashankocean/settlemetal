
using SQLite;
using System.Collections.Generic;

namespace settleMetal
{
    public class User
    {

        public string user_name { get; set; }

        public string user_phone { get; set; }
        [PrimaryKey, AutoIncrement]
        public int user_id { get; set; }
        [Unique]
        public string user_email { get; set; }

        public string pass4word { get; set; }

        public string Active { get; set; }
    }
    public class Get_Set_User
    {
        public List<User> user { get; set; }
    }
    //------------------------------------------------------
    public class OnlyUser
    {
        public string user_email { get; set; }

        public string pass4word { get; set; }
    }
    public class Get_Set_OnlyUser
    {
        public List<User> user { get; set; }
    }
}
