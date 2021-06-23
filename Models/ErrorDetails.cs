using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorDetails()
        {

        }

        public ErrorDetails(int statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ErrorDetails InvalidLicenseKeyFormat = new ErrorDetails() { StatusCode = 500, Message = "Invalid licenseKey format." };
        public static ErrorDetails InvalidConferenceKeyFormat = new ErrorDetails() { StatusCode = 500, Message = "Invalid conferenceKey format." };
    }
}
