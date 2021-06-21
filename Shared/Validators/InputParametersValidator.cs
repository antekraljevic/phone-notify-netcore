using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneNotify.Shared.Validators
{
    public static class InputParametersValidator
    {
        public static bool IsValidQueueIDsParameterFormat(string queueIds)
        {
            Regex regex = new Regex("^\\d+(;\\d+)*$");
            return regex.Match(queueIds).Success;
        }

        public static bool IsValidGuidFormat(string licenseKey)
        {
            try
            {
                Guid guid = new Guid(licenseKey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
