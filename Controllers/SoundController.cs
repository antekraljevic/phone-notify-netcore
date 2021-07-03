using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotify.Models.General;
using PhoneNotify.Models.RequestBodies.Sound;
using PhoneNotify.Shared;
using PhoneNotify.Shared.Helpers;
using PhoneNotify.Shared.Validators;
using PhoneNotifySoapService;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class SoundController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public SoundController(PhoneNotifySoap client)
        {
            _client = client;
        }

        /// <summary>
        /// Use this method to upload a sound file. Our system accepts WAV files (PCM, u-Law, A-law, or MS ADPCM). To use the sound file in a message, put tildes (~) around it and start it with a caret (^). Example: Hello ~^soundfile~, you are great. (The message would say "Hello," then play the sound file, and then say "you are great.")
        /// </summary>
        /// <param name="requestBody">
        /// <para>FileBinary (type: byte[]) - The u-Law wav file. The maximum file size allowed is 2 MB. The minimum recommended sound quality is 16-bit mono PCM. Files can be split if they are larger than 2 MB.</para>
        /// <para>SoundFileID (type: string) - Name file for future retrieval.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>UploadSoundFileResponse object</returns>
        [HttpPost("UploadSoundFile")]
        [ProducesResponseType(typeof(UploadSoundFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UploadSoundFileResponse>> UploadSoundFile([FromBody] UploadSoundFileRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.UploadSoundFileAsync(PrepareSoapRequests.PrepareUploadSoundFileSoapRequest(requestBody, (string)HttpContext.Items["licenseKey"])));
        }

        /// <summary>
        /// This method returns a sound file.
        /// </summary>
        /// <param name="soundFileId">The sound file's ID.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The Base64 encoded file data.</returns>
        [HttpGet("GetSoundFile")]
        [ProducesResponseType(typeof(GetSoundFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetSoundFileResponse>> GetSoundFile([Required, FromQuery] string soundFileId, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileAsync(PrepareSoapRequests.PrepareGetSoundFileSoapRequest(soundFileId, licenseKey)));
        }

        /// <summary>
        /// This method returns a sound file encoded as an MP3 in 32, 64, or 128 Kpbs.
        /// </summary>
        /// <param name="soundFileId">The sound file's ID.</param>
        /// <param name="bitRate">The desired bitrate for the returned MP3 file. 128 is common for bandwidth reasons, while 192 and 256 are used for higher-quality, larger files.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The Base64 encoded file data.</returns>
        [HttpGet("GetSoundFileInMP3")]
        [ProducesResponseType(typeof(GetSoundFileInMP3Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetSoundFileInMP3Response>> GetSoundFileInMP3([Required, FromQuery] string soundFileId, [Required, FromQuery] int bitRate, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileInMP3Async(PrepareSoapRequests.PrepareGetSoundFileInMP3SoapRequest(soundFileId, bitRate, licenseKey)));
        }

        /// <summary>
        /// This method returns a sound file encoded in u-Law format.
        /// </summary>
        /// <param name="soundFileId">The sound file's ID.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The Base64 encoded file data.</returns>
        [HttpGet("GetSoundFileInUlaw")]
        [ProducesResponseType(typeof(GetSoundFileInUlawResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetSoundFileInUlawResponse>> GetSoundFileInUlaw([Required, FromQuery] string soundFileId, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileInUlawAsync(PrepareSoapRequests.PrepareGetSoundFileInUlawSoapRequest(soundFileId, licenseKey)));
        }

        /// <summary>
        /// This method returns a sound file's length in seconds.
        /// </summary>
        /// <param name="soundFileId">The sound file's ID.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The length of the file, in seconds.</returns>
        [HttpGet("GetSoundFileLength")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<double>> GetSoundFileLength([Required, FromQuery] string soundFileId, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileLengthAsync(soundFileId, licenseKey));
        }

        /// <summary>
        /// This method returns a URL to listen to a particular sound file in MP3.
        /// </summary>
        /// <param name="soundFileId">The sound file's ID.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The URL of MP3 file request.</returns>
        [HttpGet("GetSoundFileURL")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetSoundFileURL([Required, FromQuery] string soundFileId, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileURLAsync(soundFileId, licenseKey));
        }

        /// <summary>
        /// Use this method to convert text into a sound file encoded in MP3 format. Returns an MP3 encoded in 32,64, or 128. Requires additional License Key. Call 1-800-984-3710 or log in at www.cdyne.com to activate the TTS License Key. This key allows you to use the direct download TTS from webservices like Notify. This only allows for TTS to Sound Files. Phone Notify allows use of TTS in the notification call without this key.
        /// </summary>
        /// <param name="requestBody">
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>VoiceID (type: int) - The text-to-speech voice ID.</para>
        /// <para>BitRate (type: int) - The bitrate for returned MP3 file. 128 is common for bandwidth reasons while 192 and 256 are utilized for higher quality larger file sizes.</para>
        /// <para>TTSrate (type: byte) - The speed that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 20 (10 being normal). This can also be controlled within the TextToSay parameter.</para>
        /// <para>TTSvolume (type: byte) - The volume that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 100 (100 is the default). This can also be controlled within the TextToSay parameter.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The result of the text-to-speech file request in MP3 format.</returns>
        [HttpGet("GetTTSInMP3")]
        [ProducesResponseType(typeof(GetTTSInMP3Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTTSInMP3Response>> GetTTSInMP3([FromBody] GetTTSInMP3RequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetTTSInMP3Async(PrepareSoapRequests.PrepareGetTTSInMP3SoapRequest(requestBody, licenseKey)));
        }

        /// <summary>
        /// Use this method to convert text into a sound file encoded in u-Law format. Requires additional License Key. Call 1-800-984-3710 or log in at www.cdyne.com to activate the TTS License Key. This key allows you to use the direct download TTS from webservices like Notify. This only allows for TTS to Sound Files. Phone Notify allows use of TTS in the notification call without this key.
        /// </summary>
        /// <param name="requestBody">
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>VoiceID (type: int) - The text-to-speech voice ID.</para>
        /// <para>TTSrate (type: byte) - The speed that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 20 (10 being normal). This can also be controlled within the TextToSay parameter.</para>
        /// <para>TTSvolume (type: byte) - The volume that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 100 (100 is the default). This can also be controlled within the TextToSay parameter.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The result of the text-to-speech file request in ULAW format.</returns>
        [HttpGet("GetTTSInULAW")]
        [ProducesResponseType(typeof(GetTTSInULAWResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTTSInULAWResponse>> GetTTSInULAW([FromBody] GetTTSInULAWRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetTTSInULAWAsync(PrepareSoapRequests.PrepareGetTTSInULAWSoapRequest(requestBody, licenseKey)));
        }

        /// <summary>
        /// Request the system to call you to record a sound file. The calling ID will be the value of SoundFileID and the number will be 8000000000.
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumberToDial (type: string) - The phone number the system should call to get a voice recording. To dial an extension, add "x" followed by the extension.</para>
        /// <para>SoundFileID (type: string) - The name you want for your file, for use in future retrieval. Must be lowercase and can contain only normal file characters.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>RecordSoundViaPhoneCallResponse object</returns>
        [HttpPost("RecordSoundViaPhoneCall")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RecordSoundViaPhoneCall([FromBody] RecordSoundViaPhoneCallRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.RecordSoundViaPhoneCallAsync(requestBody.PhoneNumberToDial, requestBody.SoundFileID, licenseKey));
        }

        /// <summary>
        /// Use this method to delete the specified sound file.
        /// </summary>
        /// <param name="requestBody">SoundFileID (type: string) - The Sound File ID to delete.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The result of the request.</returns>
        [HttpDelete("RemoveSoundFile")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RemoveSoundFile([Required, FromBody] RemoveSoundFileRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.RemoveSoundFileAsync(requestBody.SoundFileID, licenseKey));
        }

        /// <summary>
        /// Use this method to rename the specified sound file.
        /// </summary>
        /// <param name="requestBody">
        /// <para>SoundFileID (type: string) - The current sound file ID(name).</para>
        /// <para>NewSoundFileID (type: string) - The new sound file ID (name).</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The result of the request.</returns>
        [HttpPatch("RenameSoundFile")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RenameSoundFile([FromBody] RenameSoundFileRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.RenameSoundFileAsync(requestBody.SoundFileID, requestBody.NewSoundFileID, licenseKey));
        }

        /// <summary>
        /// This method returns the IDs of available sound files that you can add to your stream via your license key.
        /// </summary>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The IDs of all uploaded or recorded sound files available on your license key.</returns>
        [HttpGet("ReturnSoundFileIDs")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string[]>> ReturnSoundFileIDs([Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.ReturnSoundFileIDsAsync(licenseKey));
        }
    }
}
