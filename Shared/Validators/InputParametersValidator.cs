using System;
using System.Text.RegularExpressions;

namespace PhoneNotify.Shared.Validators
{
    public static class InputParametersValidator
    {
        public static bool IsValidMultipleValuesSeparatedWithSemicolonParameterFormat(string queueIds)
        {
            Regex regex = new Regex("^\\d+(;\\s*\\d+)*$");
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
