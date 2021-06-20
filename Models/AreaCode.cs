using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models
{
    public class AreaCode
    {
        public string AreaCodeNumber { get; set; }
        public string Location { get; set; }

        public AreaCode(string areaCodeNmber, string location)
        {
            this.AreaCodeNumber = areaCodeNmber;
            this.Location = location;
        }
    }
}
