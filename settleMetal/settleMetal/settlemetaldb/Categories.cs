using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace settleMetal
{
    public class Categories
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string image { get; set; }
    }
   public class Get_Set_Category
    {
       public List<Categories> categories { get; set; }
    }
    
}
