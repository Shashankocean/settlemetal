using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace settleMetal
{
    class Price_Service
    {
        public int price_id { get; set; }
        public float price { get; set; }
        public int service_id { get; set; }
        public string service_name { get; set; }
        public string image_s { get; set; }
    }
    class GetSet_Price_service
    {
        public List<Price_Service> price_service { get; set; }
    }

    class SelectedService
    {
        public static List<Price_Service> choosService { get; set; }
    }
}
