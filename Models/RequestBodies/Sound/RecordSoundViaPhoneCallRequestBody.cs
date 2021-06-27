namespace PhoneNotify.Models.RequestBodies.Sound
{
    public class RecordSoundViaPhoneCallRequestBody
    {
        public string PhoneNumberToDial { get; set; }
        public string SoundFileID { get; set; }
    }
}
