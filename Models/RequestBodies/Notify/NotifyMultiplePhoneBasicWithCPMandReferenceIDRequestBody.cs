namespace PhoneNotify.Models.RequestBodies.Notify
{
    public class NotifyMultiplePhoneBasicWithCPMandReferenceIDRequestBody
    {
        public string PhoneNumbersToDial { get; set; }
        public string TextToSay { get; set; }
        public string CallerID { get; set; }
        public string CallerIDName { get; set; }
        public string VoiceID { get; set; }
        public int CallsPerMinute { get; set; }
        public string ReferenceID { get; set; }
    }
}
