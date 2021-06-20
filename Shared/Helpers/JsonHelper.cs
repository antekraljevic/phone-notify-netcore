using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Shared.Helpers
{
    public class JsonHelper
    {
        public static List<string> GetListOfStringsFromJsonObject(JToken jsonToken)
        {
            List<string> list = new List<string>();

            foreach (string value in jsonToken)
            {
                list.Add(value);
            }

            return list;
        }
    }
}
