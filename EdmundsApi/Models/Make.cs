using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdmundsApi.Models
{
    public class Make
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public Model[] Models { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
