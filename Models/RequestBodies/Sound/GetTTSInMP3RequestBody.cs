namespace PhoneNotify.Models.RequestBodies.Sound
{
    public class GetTTSInMP3RequestBody
    {
        public string TextToSay { get; set; }
        public int VoiceID { get; set; }
        public int BitRate { get; set; }
        public byte TTSrate { get; set; }
        public byte TTSvolume { get; set; }
    }
}
