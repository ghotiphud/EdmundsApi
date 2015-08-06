using EdmundsApi.Requests;
using Newtonsoft.Json;
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
        public ModelYear[] Years { get; set; }
        public State[] States { get; set; }

        public override string ToString()
        {
            return Name;
        }
        
        public class ModelYear
        {
            public int ID { get; set; }
            [JsonProperty("year")]
            public int Value { get; set; }
            public Style[] Styles { get; set; }
            public State[] States { get; set; }
            
            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}
