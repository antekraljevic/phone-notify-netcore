using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotify.Models.General;
using PhoneNotify.Models.RequestBodies.Script;
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
    public class ScriptController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public ScriptController(PhoneNotifySoap client)
        {
            _client = client;
        }

        /// <summary>
        /// Use this method to update the call script for incoming calls to a particular number. You must have incoming phone numbers set by CDYNE.
        /// </summary>
        /// <param name="requestBody">
        /// <para>PhoneNumber (type: string) - Your incoming phone number, as set up by CDYNE.</para>
        /// <para>Script (type: string) - The text-to-speech text or combination of text-to-speech and script for inbound phone call.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Returns true if the operation succeeded, false if not.</returns>
        [HttpPost("SetIncomingCallScript")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> SetIncomingCallScript([FromBody] SetIncomingCallScriptRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.SetIncomingCallScriptAsync(requestBody.PhoneNumber, requestBody.Script, licenseKey));
        }

        /// <summary>
        /// Use this method to get the call script for incoming calls to a particular number. You must have incoming phone numbers set by CDYNE.
        /// </summary>
        /// <param name="phoneNumber">Your incoming phone number, as set up by CDYNE.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The script currently in use for incoming calls to the specified phone number.</returns>
        [HttpGet("GetIncomingCallScript")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetIncomingCallScript([Required, FromQuery] string phoneNumber, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetIncomingCallScriptAsync(phoneNumber, licenseKey));
        }

        /// <summary>
        /// Use this method to save a script.
        /// </summary>
        /// <param name="requestBody">
        /// <para>ScriptName (type: string) - The script name. 50 characters or fewer. Using an existing name will overwrite the old script.</para>
        /// <para>ScriptText (type: string) - The text-to-speech text or combination of text-to-speech and script for inbound phone call.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Returns true if the save succeeded, false if not.</returns>
        [HttpPost("ScriptSave")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> ScriptSave([FromBody] ScriptSaveRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.ScriptSaveAsync(requestBody.ScriptName, requestBody.ScriptText, licenseKey));
        }

        /// <summary>
        /// This method return the text (contents) of a script.
        /// </summary>
        /// <param name="scriptName">The script name. 50 characters or fewer.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The specified script.</returns>
        [HttpGet("ScriptLoad")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> ScriptLoad([Required, FromQuery] string scriptName, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.ScriptLoadAsync(scriptName, licenseKey));
        }

        /// <summary>
        /// This method returns a list of the scripts belonging to a license key.
        /// </summary>
        /// <param name="includeGlobalScripts">Set to true if you wish CDYNE Global scripts to be included.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>All scripts saved under the License Key provided.</returns>
        [HttpGet("ScriptList")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string[]>> ScriptList([FromQuery] bool? includeGlobalScripts, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.ScriptListAsync(includeGlobalScripts.HasValue ? includeGlobalScripts.Value : false, licenseKey));
        }

        /// <summary>
        /// Use this method to delete a script.
        /// </summary>
        /// <param name="requestBody">ScriptName (type: string) - The script name. 50 characters or fewer.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Returns true if the deletion succeeded, false if not.</returns>
        [HttpDelete("ScriptDelete")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> ScriptDelete([Required, FromBody] ScriptDeleteRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.ScriptDeleteAsync(requestBody.ScriptName, licenseKey));
        }
    }
}
