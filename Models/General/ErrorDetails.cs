using Newtonsoft.Json;

namespace PhoneNotify.Models.General
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
        public static ErrorDetails InvalidQueueIDsFormat = new ErrorDetails() { StatusCode = 500, Message = "Invalid queueIDs format." };
        public static ErrorDetails InvalidPhoneNumbersToDialFormat = new ErrorDetails() { StatusCode = 500, Message = "Invalid phoneNumbersToDial format." };
    }
}
