using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotify.Models.General;
using PhoneNotify.Models.RequestBodies.Notify;
using PhoneNotify.Shared;
using PhoneNotify.Shared.Helpers;
using PhoneNotify.Shared.Validators;
using PhoneNotifySoapService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class NotifyController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public NotifyController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpPost("NotifyPhoneBasic")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneBasic([FromBody] NotifyPhoneBasicRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneBasicAsync(requestBody.PhoneNumberToDial, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, licenseKey));
        }

        [HttpPost("NotifyPhoneBasicWithTryCount")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneBasicWithTryCount([FromBody] NotifyPhoneBasicWithTryCountRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneBasicWithTryCountAsync(requestBody.TryCount, requestBody.PhoneNumberToDial, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, licenseKey));
        }

        [HttpPost("NotifyPhoneBasicWithTransfer")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneBasicWithTransfer([FromBody] NotifyPhoneBasicWithTransferRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneBasicWithTransferAsync(requestBody.PhoneNumberToDial, requestBody.TransferNumber, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, licenseKey));
        }

        [HttpPost("NotifyPhoneEnglishBasic")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneEnglishBasic([FromBody] NotifyPhoneEnglishBasicRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneEnglishBasicAsync(requestBody.PhoneNumberToDial, requestBody.TextToSay, licenseKey));
        }

        [HttpPost("NotifyMultiplePhoneBasic")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> NotifyMultiplePhoneBasic([FromBody] NotifyMultiplePhoneBasicRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }
            if (!InputParametersValidator.IsValidMultipleValuesSeparatedWithSemicolonParameterFormat(requestBody.PhoneNumbersToDial))
            {
                return BadRequest(ErrorDetails.InvalidPhoneNumbersToDialFormat);
            }

            return Ok(await _client.NotifyMultiplePhoneBasicAsync(requestBody.PhoneNumbersToDial, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, licenseKey));
        }

        [HttpPost("NotifyMultiplePhoneBasicWithCPM")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> NotifyMultiplePhoneBasicWithCPM([FromBody] NotifyMultiplePhoneBasicWithCPMRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }
            if (!InputParametersValidator.IsValidMultipleValuesSeparatedWithSemicolonParameterFormat(requestBody.PhoneNumbersToDial))
            {
                return BadRequest(ErrorDetails.InvalidPhoneNumbersToDialFormat);
            }

            return Ok(await _client.NotifyMultiplePhoneBasicWithCPMAsync(requestBody.PhoneNumbersToDial, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, requestBody.CallsPerMinute,licenseKey));
        }

        [HttpPost("NotifyMultiplePhoneBasicWithCPMandReferenceID")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> NotifyMultiplePhoneBasicWithCPMandReferenceID([FromBody] NotifyMultiplePhoneBasicWithCPMandReferenceIDRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }
            if (!InputParametersValidator.IsValidMultipleValuesSeparatedWithSemicolonParameterFormat(requestBody.PhoneNumbersToDial))
            {
                return BadRequest(ErrorDetails.InvalidPhoneNumbersToDialFormat);
            }

            return Ok(await _client.NotifyMultiplePhoneBasicWithCPMandReferenceIDAsync(requestBody.PhoneNumbersToDial, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, requestBody.CallsPerMinute, requestBody.ReferenceID, licenseKey));
        }

        [HttpPost("NotifyPhoneAdvanced")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneAdvanced([FromBody] NotifyPhoneAdvancedRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneAdvancedAsync(PrepareSoapRequests.PrepareNotifyPhoneAdvancedSoapRequest(requestBody, licenseKey)));
        }

        [HttpPost("NotifyMultiplePhoneAdvanced")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyMultiplePhoneAdvanced([FromBody] List<NotifyPhoneAdvancedRequestBody> requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyMultiplePhoneAdvancedAsync(PrepareSoapRequests.PrepareNotifyMultiplePhoneAdvancedSoapRequest(requestBody, licenseKey)));
        }
    }
}
