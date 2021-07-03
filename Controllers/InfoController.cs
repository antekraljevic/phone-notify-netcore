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

        /// <summary>
        /// Use this method to get all the available area codes in our system.
        /// </summary>
        /// <returns>A list of area codes in our system.</returns>
        [HttpGet("GetAvailableAreaCodes")]
        [ProducesResponseType(typeof(AreaCode[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<AreaCode[]>> GetAvailableAreaCodes()
        {
           return Ok(await _client.GetAvailableAreaCodesAsync());
        }

        /// <summary>
        /// This method returns all response codes that may be returned when invoking Notify methods.
        /// </summary>
        /// <returns>List of reponse codes.</returns>
        [HttpGet("GetResponseCodes")]
        [ProducesResponseType(typeof(Response[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<Response[]>> GetResponseCodes()
        {
            return Ok(await _client.GetResponseCodesAsync());
        }

        /// <summary>
        /// This method returns CDYNE Version information
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVersion")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetVersion()
        {
            return Ok(await _client.GetVersionAsync());
        }

        /// <summary>
        /// This method returns all the voices available for your notification. You can use the VoiceIDs to change the voice used when reading a message.
        /// </summary>
        /// <returns>List of voices available for your notification</returns>
        [HttpGet("GetVoices")]
        [ProducesResponseType(typeof(Voice[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<Voice[]>> GetVoices()
        {
            return Ok(await _client.getVoicesAsync());
        }
    }
}
