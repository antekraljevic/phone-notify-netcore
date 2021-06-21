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
    public class LicenseController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public LicenseController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpGet("assignincomingnumber")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AssignIncomingNumber([Required, FromQuery] string incomingPhoneNumber, [Required, FromQuery] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest();
            }

            return Ok(await _client.AssignIncomingNumberAsync(incomingPhoneNumber, licenseKey));
        }

        [HttpGet("getassignednumbers")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string[]>> GetAssignedNumbers([Required, FromQuery] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest();
            }

            return Ok(await _client.GetAssignedNumbersAsync(licenseKey));
        }
    }
}
