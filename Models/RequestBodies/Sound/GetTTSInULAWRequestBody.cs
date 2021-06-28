namespace PhoneNotify.Models.RequestBodies.Sound
{
    public class GetTTSInULAWRequestBody
    {
        public string TextToSay { get; set; }
        public int VoiceID { get; set; }
        public byte TTSrate { get; set; }
        public byte TTSvolume { get; set; }
    }
}
