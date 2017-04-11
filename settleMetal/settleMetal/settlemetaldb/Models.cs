using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace settleMetal
{
    public class Models
    {
        public int model_id { get; set; }
        public string model_name { get; set; }
        public string device { get; set; }
        public string image_m { get; set; }
        public string category_id { get; set; }
        public bool active { get; set; }
    }
    public class Get_set_Models
    {
        public List<Models> models { get; set; }
    }
}
