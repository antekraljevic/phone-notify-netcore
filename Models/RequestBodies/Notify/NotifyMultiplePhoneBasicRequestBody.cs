using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models.RequestBodies.Notify
{
    public class NotifyMultiplePhoneBasicRequestBody
    {
        public string PhoneNumbersToDial { get; set; }
        public string TextToSay { get; set; }
        public string CallerID { get; set; }
        public string CallerIDName { get; set; }
        public string VoiceID { get; set; }
    }
}
