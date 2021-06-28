namespace PhoneNotify.Models.RequestBodies.Notify
{
    public class NotifyPhoneBasicWithTryCountRequestBody
    {
        public short TryCount { get; set; }
        public string PhoneNumberToDial { get; set; }
        public string TextToSay { get; set; }
        public string CallerID { get; set; }
        public string CallerIDName { get; set; }
        public string VoiceID { get; set; }
    }
}
