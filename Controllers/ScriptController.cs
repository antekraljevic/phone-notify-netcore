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
    public class ScriptController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public ScriptController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpGet("getincomingcallscript")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetIncomingCallScript([Required, FromQuery] string phoneNumber, [Required, FromQuery] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest();
            }

            return Ok(await _client.GetIncomingCallScriptAsync(phoneNumber, licenseKey));
        }
    }
}
