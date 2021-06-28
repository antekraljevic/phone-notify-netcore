using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models.RequestBodies.Notify
{
    public class NotifyPhoneAdvancedRequestBody
    {
        public string PhoneNumberToDial { get; set; }
        public string TransferNumber { get; set; }
        public int VoiceID { get; set; }
        public string CallerID { get; set; }
        public string CallerIDName { get; set; }
        public string TextToSay { get; set; }
        public int TryCount { get; set; }
        public int NextTryInSeconds { get; set; }
        public string UTCScheduledDateTime { get; set; }
        public byte TTSRate { get; set; }
        public byte TTSVolume { get; set; }
        public int MaxCallLength { get; set; }
        public string StatusChangePostUrl { get; set; }
        public string ReferenceID { get; set; }
    }
}
