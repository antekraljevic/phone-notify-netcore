namespace PhoneNotify.Models.RequestBodies.ListMember
{
    public class DialListRequestBody
    {
        public int ListID { get; set; }
        public bool DialRecursiveLists { get; set; }
        public byte VoiceID { get; set; }
        public string CallerID { get; set; }
        public string CallerIDName { get; set; }
        public string TextToSay { get; set; }
    }
}
