namespace PhoneNotify.Models.RequestBodies.Sound
{
    public class UploadSoundFileRequestBody
    {
        public byte[] FileBinary { get; set; }
        public string SoundFileID { get; set; }
    }
}
