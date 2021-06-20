using Newtonsoft.Json.Linq;
using PhoneNotify.Models;
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

        public static List<AreaCode> GetListOfAreaCodesFromJsonObject(JToken jsonToken)
        {
            List<AreaCode> list = new List<AreaCode>();

            foreach (var item in jsonToken)
            {
                AreaCode areaCode = new AreaCode((string)item["AreaCodeNumber"], (string)item["Location"]);
                list.Add(areaCode);
            }

            return list;
        }
    }
}
