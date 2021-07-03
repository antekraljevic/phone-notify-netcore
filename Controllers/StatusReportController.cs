using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotify.Models.General;
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
    public class StatusReportController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public StatusReportController(PhoneNotifySoap client)
        {
            _client = client;
        }

        /// <summary>
        /// This method returns the status of a particular message (message).
        /// </summary>
        /// <param name="queueId">The message's ID, as returned from any Notify operation.</param>
        /// <returns>NotifyStatusReturn object</returns>
        [HttpGet("GetQueueIDStatus")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        public async Task<ActionResult<NotifyReturn>> GetQueueIDStatus([Required, FromQuery] long queueId)
        {
            return Ok(await _client.GetQueueIDStatusAsync(queueId));
        }

        /// <summary>
        /// This method returns the status of a particular message. This method includes variable information and more.
        /// </summary>
        /// <param name="queueId">The message's ID, as returned from any Notify operation.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>NotifyStatusReturn object</returns>
        [HttpGet("GetQueueIDStatusWithAdvancedInfo")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> GetQueueIDStatusWithAdvancedInfo([Required, FromQuery] long queueId, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetQueueIDStatusWithAdvancedInfoAsync(queueId, licenseKey));
        }

        /// <summary>
        /// This method returns the last 10 phone notifications for a particular phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number for which you want to get statuses.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Array of NotifyStatusReturn objects</returns>
        [HttpGet("GetQueueIDStatusesByPhoneNumber")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> GetQueueIDStatusesByPhoneNumber([Required, FromQuery] string phoneNumber, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetQueueIDStatusesByPhoneNumberAsync(phoneNumber, licenseKey));
        }

        /// <summary>
        /// This method returns the statuses of multiple notifies (messages).
        /// </summary>
        /// <param name="queueIds">The messages' IDs, as returned from any Notify operation. Separate the IDs with semicolons.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Array of NotifyStatusReturn objects</returns>
        [HttpGet("GetMultipleQueueIdStatus")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> GetMultipleQueueIdStatus([Required, FromQuery] string queueIds, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }
            if (!InputParametersValidator.IsValidMultipleValuesSeparatedWithSemicolonParameterFormat(queueIds))
            {
                return BadRequest(ErrorDetails.InvalidQueueIDsFormat);
            }

            return Ok(await _client.GetMultipleQueueIdStatusAsync(queueIds, licenseKey));
        }
    }
}
