using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotifySoapService;
using System.Threading.Tasks;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class IncomingNumbersController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public IncomingNumbersController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpGet("getavailableincomingnumbers")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<string[]>> GetAvailableIncomingNumbers([FromQuery] string areaCodeFilter)
        {
            return Ok(await _client.GetAvailableIncomingNumbersAsync(areaCodeFilter));
        }
    }
}
