using EdmundsApi.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdmundsApi.Models
{
    public class Model
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public Year[] Years { get; set; }
        public State[] States { get; set; }

        public override string ToString()
        {
            return Name;
        }
        
        public class Year
        {
            public int ID { get; set; }
            public int Value { get; set; }
            public State[] States { get; set; }
            
            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}
