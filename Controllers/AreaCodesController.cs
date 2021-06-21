using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotifySoapService;
using System.Threading.Tasks;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AreaCodesController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public AreaCodesController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpGet("getavailableareacodes")]
        [ProducesResponseType(typeof(AreaCode[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<AreaCode[]>> GetAvailableAreaCodes()
        {
           return Ok(await _client.GetAvailableAreaCodesAsync());
        }
    }
}
