namespace PhoneNotify.Models.RequestBodies.Sound
{
    public class RenameSoundFileRequestBody
    {
        public string SoundFileID { get; set; }
        public string NewSoundFileID { get; set; }
    }
}
