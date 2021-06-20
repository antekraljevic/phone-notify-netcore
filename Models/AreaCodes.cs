using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models
{
    public class AreaCodes
    {
        public List<AreaCode> Result { get; set; }

        public AreaCodes(List<AreaCode> list)
        {
            this.Result = list;
        }
    }
}
