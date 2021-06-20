using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace PhoneNotify.Shared.Helpers
{
    public static class XmlHelper
    {
        public static JObject ParseXmlToJson(string xml)
        {
            var result = ValidateXml(xml);

            if(result.Item1 == false && result.Item2 == String.Empty)
            {
                return null;
            }

            return JObject.Parse(result.Item2);
        }

        private static (bool, string) ValidateXml(string xml)
        {
            try
            {
                if (string.IsNullOrEmpty(xml) == false)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);
                    var jsonAsString = JsonConvert.SerializeXmlNode(xmlDoc);

                    return (true, jsonAsString);
                }
                else
                {
                    return (false, String.Empty);
                }
            }
            catch (XmlException e)
            {
                return (false, String.Empty);
            }
        }
    }
}
