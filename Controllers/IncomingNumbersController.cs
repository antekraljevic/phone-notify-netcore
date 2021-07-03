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

        /// <summary>
        /// This method will list a random 10 lines available for incoming calls. You can assign these lines via the License/AssignIncomingNumber method to your license key. You can leave the area code field blank to get any available toll-free lines (888 and 877 numbers are currently the most common).
        /// </summary>
        /// <param name="areaCodeFilter">Area code filter value</param>
        /// <returns>A list of incoming numbers.</returns>
        [HttpGet("GetAvailableIncomingNumbers")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<string[]>> GetAvailableIncomingNumbers([FromQuery] string areaCodeFilter = "")
        {
            return Ok(await _client.GetAvailableIncomingNumbersAsync(areaCodeFilter));
        }
    }
}
