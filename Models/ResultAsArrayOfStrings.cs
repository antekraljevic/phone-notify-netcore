using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models
{
    public class ResultAsArrayOfStrings
    {
        public IEnumerable<string> Result { get; set; }

        public ResultAsArrayOfStrings(List<string> list)
        {
            this.Result = list;
        }
    }
}
