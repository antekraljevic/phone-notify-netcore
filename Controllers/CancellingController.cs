using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpGet("cancelconference")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelConference([Required, FromQuery] string conferenceKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(conferenceKey))
            {
                return BadRequest();
            }

            await _client.CancelConferenceAsync(conferenceKey);
            return Ok();
        }

        [HttpGet("cancelnotify")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CancelNotify([Required, FromQuery] long queueId, [Required, FromQuery] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest();
            }

            return Ok(await _client.CancelNotifyAsync(queueId, licenseKey));
        }

        [HttpGet("cancelnotifybyreferenceid")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CancelNotifyByReferenceID([Required, FromQuery] string referenceId, [Required, FromQuery] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest();
            }

            return Ok(await _client.CancelNotifyByReferenceIDAsync(referenceId, licenseKey));
        }
    }
}
