using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdmundsApi.Models;

namespace EdmundsApi.Responses
{
    public class MakeList
    {
        public Make[] makes { get; set; }
        public int makesCount { get; set; }
    }

}
