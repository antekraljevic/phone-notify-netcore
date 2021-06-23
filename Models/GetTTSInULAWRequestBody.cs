using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models
{
    public class GetTTSInULAWRequestBody
    {
        public string TextToSay { get; set; }
        public int VoiceID { get; set; }
        public byte TTSrate { get; set; }
        public byte TTSvolume { get; set; }
    }
}
