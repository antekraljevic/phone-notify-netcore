using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneNotify.Models.General;
using PhoneNotify.Models.RequestBodies.Cancelling;
using PhoneNotify.Shared;
using PhoneNotify.Shared.Validators;
using PhoneNotifySoapService;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CancellingController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public CancellingController(PhoneNotifySoap client)
        {
            _client = client;
        }

        /// <summary>
        /// Use this method to cancel a conference.
        /// </summary>
        /// <param name="requestBody">Conference key - The key of the conference you want to cancel (type: string)</param>
        /// <returns>void</returns>
        [HttpPost("CancelConference")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelConference([Required, FromBody] CancelConferenceRequestBody requestBody)
        {
            if (!InputParametersValidator.IsValidGuidFormat(requestBody.ConferenceKey))
            {
                return BadRequest(ErrorDetails.InvalidConferenceKeyFormat);
            }

            await _client.CancelConferenceAsync(requestBody.ConferenceKey);
            return Ok();
        }

        /// <summary>
        /// Use this method to cancel a single notify. This will not cancel completed calls or calls in progress. You will receive credit for any successfully cancelled notify that returns "true."
        /// </summary>
        /// <param name="requestBody">Queue ID - The ID of a single notify (message) to cancel (type: long)</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The result of the request. Finished calls cannot be canceled and will return false.</returns>
        [HttpPost("CancelNotify")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CancelNotify([Required, FromBody] CancelNotifyRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.CancelNotifyAsync(requestBody.QueueID, licenseKey));
        }

        /// <summary>
        /// Cancels a batch notify by ReferenceID. This will not cancel completed calls or calls in progress. You will be credited for any successfully cancelled notifies and the returned value will be greater than zero.
        /// </summary>
        /// <param name="requestBody">Reference ID (type: string)</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>int</returns>
        [HttpPost("CancelNotifyByReferenceID")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CancelNotifyByReferenceID([Required, FromBody] CancelNotifyByReferenceIDRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.CancelNotifyByReferenceIDAsync(requestBody.ReferenceID, licenseKey));
        }
    }
}
