using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotifySoapService;
using System.Threading.Tasks;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class InfoController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public InfoController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpGet("GetAvailableAreaCodes")]
        [ProducesResponseType(typeof(AreaCode[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<AreaCode[]>> GetAvailableAreaCodes()
        {
           return Ok(await _client.GetAvailableAreaCodesAsync());
        }

        [HttpGet("GetResponseCodes")]
        [ProducesResponseType(typeof(Response[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response[]>> GetResponseCodes()
        {
            return Ok(await _client.GetResponseCodesAsync());
        }

        [HttpGet("GetVersion")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetVersion()
        {
            return Ok(await _client.GetVersionAsync());
        }

        [HttpGet("GetVoices")]
        [ProducesResponseType(typeof(Voice[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<Voice[]>> GetVoices()
        {
            return Ok(await _client.getVoicesAsync());
        }
    }
}
