using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotify.Models.General;
using PhoneNotify.Models.RequestBodies.Notify;
using PhoneNotify.Shared;
using PhoneNotify.Shared.Helpers;
using PhoneNotify.Shared.Validators;
using PhoneNotifySoapService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// Use this method to call any phone number in the US/Canada and read the value of TextToSay to that phone number.
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumberToDial (type: string) - The phone number to call. It can be in any format, as long as there are 10 digits. To dial an extension, add "x" followed by the extension.</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>VoiceID (type: string) - The text-to-speech voice ID.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>NotifyReturn object</returns>
        [HttpPost("NotifyPhoneBasic")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneBasic([FromBody] NotifyPhoneBasicRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneBasicAsync(requestBody.PhoneNumberToDial, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, licenseKey));
        }

        /// <summary>
        /// Use this method to call any phone number in the US/Canada and read the value of TextToSay to that phone number. Use the TryCount parameter to specify the number of times the operation should retry the call if the original call is unanswered or busy.
        /// </summary>
        /// <param name="requestBody">
        /// <para>TryCount (type: string) - The number of retries to attempt if original call is unanswered or busy. The maximum is 3.</para>
        /// <para>PhoneNumberToDial (type: string) - The phone number to call. It can be in any format, as long as there are 10 digits. To dial an extension, add "x" followed by the extension.</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>VoiceID (type: string) - The text-to-speech voice ID.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>NotifyReturn object</returns>
        [HttpPost("NotifyPhoneBasicWithTryCount")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneBasicWithTryCount([FromBody] NotifyPhoneBasicWithTryCountRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneBasicWithTryCountAsync(requestBody.TryCount, requestBody.PhoneNumberToDial, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, licenseKey));
        }

        /// <summary>
        /// Use this method to call any phone number in the US/Canada and read the value of TextToSay to that phone number. It also allows you to transfer a call by pressing 0.
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumberToDial (type: string) - The phone number to call. It can be in any format, as long as there are 10 digits. To dial an extension, add "x" followed by the extension.</para>
        /// <para>TransferNumber (type: string) - The phone number that the call will be transferred to if the call recipient presses 0. Transfer behavior can be further modified with TextToSay commands.</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>VoiceID (type: string) - The text-to-speech voice ID.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>NotifyReturn object</returns>
        [HttpPost("NotifyPhoneBasicWithTransfer")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneBasicWithTransfer([FromBody] NotifyPhoneBasicWithTransferRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneBasicWithTransferAsync(requestBody.PhoneNumberToDial, requestBody.TransferNumber, requestBody.TextToSay, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, licenseKey));
        }

        /// <summary>
        /// Use this method to call any phone number in the US/Canada and read the value of TextToSay to that phone number using the voice of Diane (VoiceID: 0).
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumberToDial (type: string) - The phone number to call. It can be in any format, as long as there are 10 digits. To dial an extension, add "x" followed by the extension.</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>NotifyReturn object</returns>
        [HttpPost("NotifyPhoneEnglishBasic")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneEnglishBasic([FromBody] NotifyPhoneEnglishBasicRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneEnglishBasicAsync(requestBody.PhoneNumberToDial, requestBody.TextToSay, licenseKey));
        }

        /// <summary>
        /// Use this method to call multiple phone numbers in the US/Canada and read the value of TextToSay to them.
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumbersToDial (type: string) - The phone numbers to call. Separate each number with a semicolon.</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>VoiceID (type: string) - The text-to-speech voice ID.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Array of NotifyReturn objects</returns>
        [HttpPost("NotifyMultiplePhoneBasic")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> NotifyMultiplePhoneBasic([FromBody] NotifyMultiplePhoneBasicRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
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

        /// <summary>
        /// Use this method to call multiple phone numbers in the US/Canada and read the value of TextToSay to them. Additionally, you can use the CallsPerMinute parameter to set the number of calls per minute (CPM).
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumbersToDial (type: string) - The phone numbers to call. Separate each number with a semicolon.</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>VoiceID (type: string) - The text-to-speech voice ID.</para>
        /// <para>CallsPerMinute (type: int) - The value to set the calls per minute.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Array of NotifyReturn objects</returns>
        [HttpPost("NotifyMultiplePhoneBasicWithCPM")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> NotifyMultiplePhoneBasicWithCPM([FromBody] NotifyMultiplePhoneBasicWithCPMRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
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

        /// <summary>
        /// Use this method to call multiple phone numbers in the US/Canada and read the value of TextToSay to them. Additionally, you can use the CallsPerMinute and ReferenceID parameters.
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumbersToDial (type: string) - The phone numbers to call. Separate each number with a semicolon.</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>VoiceID (type: string) - The text-to-speech voice ID.</para>
        /// <para>CallsPerMinute (type: int) - The value to set the calls per minute.</para>
        /// <para>ReferenceID (type: string) - Unique ID that can be set.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Array of NotifyReturn objects</returns>
        [HttpPost("NotifyMultiplePhoneBasicWithCPMandReferenceID")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> NotifyMultiplePhoneBasicWithCPMandReferenceID([FromBody] NotifyMultiplePhoneBasicWithCPMandReferenceIDRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
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

        /// <summary>
        /// Use this method to call any phone number in the US/Canada and read the value of TextToSay to that phone number. Use this method if you need to define detailed options for your message.
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumberToDial (type: string) - The phone number to call. It can be in any format, as long as there are 10 digits. To dial an extension, add "x" followed by the extension.</para>
        /// <para>TransferNumber (type: string) - The phone number that the call will be transferred to if the call recipient presses 0. Transfer behavior can be further modified with TextToSay commands.</para>
        /// <para>VoiceID (type: int) - The text-to-speech voice ID.</para>
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>TryCount (type: int) - The number of times to attempt dialing if the initial call is unanswered or busy.</para>
        /// <para>NextTryInSeconds (type: int) - The number of seconds to wait until the next retry is performed if the original call is unanswered or busy. We recommend waiting at least 60 seconds.</para>
        /// <para>UTCScheduledDateTime (type: string) - The date and time at which to send the call. This is specified as Coordinated Universal Time (UTC).</para>
        /// <para>TTSRate (type: byte) - The speed that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 20 (10 being normal). This can also be controlled within the TextToSay parameter.</para>
        /// <para>TTSVolume (type: byte) - The volume that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 100 (100 is the default). This can also be controlled within the TextToSay parameter.</para>
        /// <para>MaxCallLength (type: int) - The maximum time duration of the call. We suggest you do not change this unless you absolutely need to.</para>
        /// <para>StatusChangePostUrl (type: string) - The URL to post call status changes to. The URL must be in lowercase. The system posts OutgoingPostback objects for outgoing calls and IncomingPostback objects for incoming calls.</para>
        /// <para>ReferenceID (type: string)</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>NotifyReturn object</returns>
        [HttpPost("NotifyPhoneAdvanced")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyPhoneAdvanced([FromBody] NotifyPhoneAdvancedRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyPhoneAdvancedAsync(PrepareSoapRequests.PrepareNotifyPhoneAdvancedSoapRequest(requestBody, licenseKey)));
        }

        /// <summary>
        /// Use this method to send multiple notifies at a time. We suggest using our list management features for batches of over 100 messages. (See the /ListMember/DialListAdvanced method.)
        /// </summary>
        /// <param name="requestBody">
        /// <para>Array of following:</para>
        /// <para>PhoneNumberToDial (type: string) - The phone number to call. It can be in any format, as long as there are 10 digits. To dial an extension, add "x" followed by the extension.</para>
        /// <para>TransferNumber (type: string) - The phone number that the call will be transferred to if the call recipient presses 0. Transfer behavior can be further modified with TextToSay commands.</para>
        /// <para>VoiceID (type: int) - The text-to-speech voice ID.</para>
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>TryCount (type: int) - The number of times to attempt dialing if the initial call is unanswered or busy.</para>
        /// <para>NextTryInSeconds (type: int) - The number of seconds to wait until the next retry is performed if the original call is unanswered or busy. We recommend waiting at least 60 seconds.</para>
        /// <para>UTCScheduledDateTime (type: string) - The date and time at which to send the call. This is specified as Coordinated Universal Time (UTC).</para>
        /// <para>TTSRate (type: byte) - The speed that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 20 (10 being normal). This can also be controlled within the TextToSay parameter.</para>
        /// <para>TTSVolume (type: byte) - The volume that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 100 (100 is the default). This can also be controlled within the TextToSay parameter.</para>
        /// <para>MaxCallLength (type: int) - The maximum time duration of the call. We suggest you do not change this unless you absolutely need to.</para>
        /// <para>StatusChangePostUrl (type: string) - The URL to post call status changes to. The URL must be in lowercase. The system posts OutgoingPostback objects for outgoing calls and IncomingPostback objects for incoming calls.</para>
        /// <para>ReferenceID (type: string)</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Array of NotifyReturn objects</returns>
        [HttpPost("NotifyMultiplePhoneAdvanced")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> NotifyMultiplePhoneAdvanced([FromBody] List<NotifyPhoneAdvancedRequestBody> requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.NotifyMultiplePhoneAdvancedAsync(PrepareSoapRequests.PrepareNotifyMultiplePhoneAdvancedSoapRequest(requestBody, licenseKey)));
        }
    }
}
