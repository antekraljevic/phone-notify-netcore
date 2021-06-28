namespace PhoneNotify.Models.RequestBodies.Notify
{
    public class NotifyPhoneBasicWithTransferRequestBody
    {
        public string PhoneNumberToDial { get; set; }
        public string TransferNumber { get; set; }
        public string TextToSay { get; set; }
        public string CallerID { get; set; }
        public string CallerIDName { get; set; }
        public string VoiceID { get; set; }
    }
}
