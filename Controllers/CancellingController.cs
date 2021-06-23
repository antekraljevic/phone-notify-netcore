using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class CancellingController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public CancellingController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpPost("CancelConference")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelConference([Required, FromBody] string conferenceKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(conferenceKey))
            {
                return BadRequest(ErrorDetails.InvalidConferenceKeyFormat);
            }

            await _client.CancelConferenceAsync(conferenceKey);
            return Ok();
        }

        [HttpPost("CancelNotify")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CancelNotify([Required, FromBody] long queueId)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.CancelNotifyAsync(queueId, licenseKey));
        }

        [HttpPost("CancelNotifyByReferenceID")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CancelNotifyByReferenceID([Required, FromBody] string referenceId)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.CancelNotifyByReferenceIDAsync(referenceId, licenseKey));
        }
    }
}
