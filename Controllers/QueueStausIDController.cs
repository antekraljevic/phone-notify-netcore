using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotify.Models;
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
    public class QueueStausIDController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public QueueStausIDController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpGet("GetQueueIDStatus")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        public async Task<ActionResult<NotifyReturn>> GetQueueIDStatus([Required, FromQuery] long queueId)
        {
            return Ok(await _client.GetQueueIDStatusAsync(queueId));
        }

        [HttpGet("GetQueueIDStatusWithAdvancedInfo")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> GetQueueIDStatusWithAdvancedInfo([Required, FromQuery] long queueId)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetQueueIDStatusWithAdvancedInfoAsync(queueId, licenseKey));
        }

        [HttpGet("GetQueueIDStatusesByPhoneNumber")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> GetQueueIDStatusesByPhoneNumber([Required, FromQuery] string phoneNumber)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetQueueIDStatusesByPhoneNumberAsync(phoneNumber, licenseKey));
        }

        [HttpGet("GetMultipleQueueIdStatus")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> GetMultipleQueueIdStatus([Required, FromQuery] string queueIds)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetMultipleQueueIdStatusAsync(queueIds, licenseKey));
        }
    }
}
