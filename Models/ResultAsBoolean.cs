using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models
{
    public class ResultAsBoolean
    {
        public bool Result { get; set; }

        public ResultAsBoolean(bool result)
        {
            this.Result = result;
        }
    }
}
