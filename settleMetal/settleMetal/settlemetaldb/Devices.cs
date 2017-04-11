using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace settleMetal
{
    public class Devices_db
    {
        public int device_id { get; set; }
        public string device_name { get; set; }
        public long category_id { get; set; }
        public string image { get; set; }
        public bool active { get; set; }
    }
    public class Get_set_Devices_db
    {
        public List<Devices_db> devices_db { get; set; }
    }
    

}
