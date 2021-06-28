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

        [HttpPost("UploadSoundFile")]
        [ProducesResponseType(typeof(UploadSoundFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UploadSoundFileResponse>> UploadSoundFile([FromBody] UploadSoundFileRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.UploadSoundFileAsync(PrepareSoapRequests.PrepareUploadSoundFileSoapRequest(requestBody, (string)HttpContext.Items["licenseKey"])));
        }

        [HttpGet("GetSoundFile")]
        [ProducesResponseType(typeof(GetSoundFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetSoundFileResponse>> GetSoundFile([Required, FromQuery] string soundFileId)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileAsync(PrepareSoapRequests.PrepareGetSoundFileSoapRequest(soundFileId, licenseKey)));
        }

        [HttpGet("GetSoundFileInMP3")]
        [ProducesResponseType(typeof(GetSoundFileInMP3Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetSoundFileInMP3Response>> GetSoundFileInMP3([Required, FromQuery] string soundFileId, [Required, FromQuery] int bitRate)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileInMP3Async(PrepareSoapRequests.PrepareGetSoundFileInMP3SoapRequest(soundFileId, bitRate, licenseKey)));
        }

        [HttpGet("GetSoundFileInUlaw")]
        [ProducesResponseType(typeof(GetSoundFileInUlawResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetSoundFileInUlawResponse>> GetSoundFileInUlaw([Required, FromQuery] string soundFileId)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileInUlawAsync(PrepareSoapRequests.PrepareGetSoundFileInUlawSoapRequest(soundFileId, licenseKey)));
        }

        [HttpGet("GetSoundFileLength")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<double>> GetSoundFileLength([Required, FromQuery] string soundFileId)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileLengthAsync(soundFileId, licenseKey));
        }

        [HttpGet("GetSoundFileURL")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetSoundFileURL([Required, FromQuery] string soundFileId)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetSoundFileURLAsync(soundFileId, licenseKey));
        }

        [HttpPost("GetTTSInMP3")]
        [ProducesResponseType(typeof(GetTTSInMP3Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTTSInMP3Response>> GetTTSInMP3([FromBody] GetTTSInMP3RequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetTTSInMP3Async(PrepareSoapRequests.PrepareGetTTSInMP3SoapRequest(requestBody, licenseKey)));
        }

        [HttpPost("GetTTSInULAW")]
        [ProducesResponseType(typeof(GetTTSInULAWResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTTSInULAWResponse>> GetTTSInULAW([FromBody] GetTTSInULAWRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetTTSInULAWAsync(PrepareSoapRequests.PrepareGetTTSInULAWSoapRequest(requestBody, licenseKey)));
        }

        [HttpPost("RecordSoundViaPhoneCall")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RecordSoundViaPhoneCall([FromBody] RecordSoundViaPhoneCallRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.RecordSoundViaPhoneCallAsync(requestBody.PhoneNumberToDial, requestBody.SoundFileID, licenseKey));
        }

        [HttpDelete("RemoveSoundFile")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RemoveSoundFile([Required, FromBody] RemoveSoundFileRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.RemoveSoundFileAsync(requestBody.SoundFileID, licenseKey));
        }

        [HttpPatch("RenameSoundFile")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RenameSoundFile([FromBody] RenameSoundFileRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.RenameSoundFileAsync(requestBody.SoundFileID, requestBody.NewSoundFileID, licenseKey));
        }

        [HttpGet("ReturnSoundFileIDs")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string[]>> ReturnSoundFileIDs()
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.ReturnSoundFileIDsAsync(licenseKey));
        }
    }
}
