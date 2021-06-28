namespace PhoneNotify.Models.RequestBodies.ListMember
{
    public class DialListAdvancedRequestBody
    {
        public string CallerID { get; set; }
        public string CallerIdName { get; set; }
        public byte VoiceId { get; set; }
        public string TextToSay { get; set; }
        public byte TryCount { get; set; }  
        public string Extension { get; set; }
        public string TransferNumber { get; set; }
        public string NextTryInSeconds { get; set; }
        public byte TTSRate { get; set; }
        public byte TTSVolume { get; set; }
        public string ScheduledUTCDatetime { get; set; }
        public int ListID { get; set; }
        public bool DialRecursiveList { get; set; }
    }
}
