using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdmundsApi.Models
{
    public class Style
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public SubModel SubModel { get; set; }
        public string Trim { get; set; }
    }
}
