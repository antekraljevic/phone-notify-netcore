using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("getqueueidstatus")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        public async Task<ActionResult<NotifyReturn>> GetQueueIDStatus([Required, FromQuery] long queueId)
        {
            return Ok(await _client.GetQueueIDStatusAsync(queueId));
        }

        [HttpGet("getqueueidstatuswithadvancedinfo")]
        [ProducesResponseType(typeof(NotifyReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn>> GetQueueIDStatusWithAdvancedInfo([Required, FromQuery] long queueId, [Required, FromQuery] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest();
            }

            return Ok(await _client.GetQueueIDStatusWithAdvancedInfoAsync(queueId, licenseKey));
        }

        [HttpGet("getqueueidstatusesbyphonenumber")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> GetQueueIDStatusesByPhoneNumber([Required, FromQuery] string phoneNumber, [Required, FromQuery] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest();
            }

            return Ok(await _client.GetQueueIDStatusesByPhoneNumberAsync(phoneNumber, licenseKey));
        }

        [HttpGet("getmultiplequeueidstatus")]
        [ProducesResponseType(typeof(NotifyReturn[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotifyReturn[]>> GetMultipleQueueIdStatus([Required, FromQuery] string queueIds, [Required, FromQuery] string licenseKey)
        {
            if (!InputParametersValidator.IsValidQueueIDsParameterFormat(queueIds) || !InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest();
            }

            return Ok(await _client.GetMultipleQueueIdStatusAsync(queueIds, licenseKey));
        }
    }
}
