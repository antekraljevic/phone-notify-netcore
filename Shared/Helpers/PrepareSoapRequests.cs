using PhoneNotify.Models.RequestBodies.ListMember;
using PhoneNotify.Models.RequestBodies.Sound;
using PhoneNotifySoapService;
using System;

namespace PhoneNotify.Shared.Helpers
{
    public static class PrepareSoapRequests
    {
        public static UploadSoundFileRequest PrepareUploadSoundFileSoapRequest(UploadSoundFileRequestBody requestBody, string licenseKey)
        {
            return new UploadSoundFileRequest(requestBody.FileBinary, requestBody.SoundFileID, licenseKey);
        }

        public static GetSoundFileRequest PrepareGetSoundFileSoapRequest(string soundFileId, string licenseKey)
        {
            return new GetSoundFileRequest(soundFileId, licenseKey);
        }

        public static GetSoundFileInMP3Request PrepareGetSoundFileInMP3SoapRequest(string soundFileId, int bitRate, string licenseKey)
        {
            return new GetSoundFileInMP3Request(soundFileId, bitRate, licenseKey);
        }

        public static GetSoundFileInUlawRequest PrepareGetSoundFileInUlawSoapRequest(string soundFileId, string licenseKey)
        {
            return new GetSoundFileInUlawRequest(soundFileId, licenseKey);
        }

        public static GetTTSInMP3Request PrepareGetTTSInMP3SoapRequest(GetTTSInMP3RequestBody requestBody, string licenseKey)
        {
            return new GetTTSInMP3Request(requestBody.TextToSay, requestBody.VoiceID, requestBody.BitRate, requestBody.TTSrate, requestBody.TTSvolume, licenseKey);
        }

        public static GetTTSInULAWRequest PrepareGetTTSInULAWSoapRequest(GetTTSInULAWRequestBody requestBody, string licenseKey)
        {
            return new GetTTSInULAWRequest(requestBody.TextToSay, requestBody.VoiceID, requestBody.TTSrate, requestBody.TTSvolume, licenseKey);
        }

        public static LM_Functions PrepareDialListAdvancedSoapRequest(DialListAdvancedRequestBody requestBody, string licenseKey)
        {
            return new LM_Functions() 
            {
                CallerID = requestBody.CallerID,
                CallerIDName = requestBody.CallerIdName,
                VoiceID = requestBody.VoiceId,
                TextToSay = requestBody.TextToSay,
                TryCount = requestBody.TryCount,
                Extension = requestBody.Extension,
                TransferNumber = requestBody.TransferNumber,
                NextTryInSeconds = TryToParseStringToShort(requestBody.NextTryInSeconds),
                TTSRate = requestBody.TTSRate,
                TTSVolume = requestBody.TTSVolume,
                ScheduledUTCDatetime = DateTimeOffset.Parse(requestBody.ScheduledUTCDatetime).UtcDateTime,
                ListID = requestBody.ListID,
                DialRecursiveLists = requestBody.DialRecursiveList
            };
        }

        private static short TryToParseStringToShort(string valueAsString)
        {
            try
            {
                short number;
                bool result = Int16.TryParse(valueAsString, out number);
                return result == true ? number : (short)60;
            }
            catch (Exception)
            {
                return 60;
            }
        }
    }
}
