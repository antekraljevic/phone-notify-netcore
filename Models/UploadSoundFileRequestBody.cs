using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneNotify.Models
{
    public class UploadSoundFileRequestBody
    {
        public byte[] FileBinary { get; set; }
        public string SoundFileID { get; set; }
    }
}
