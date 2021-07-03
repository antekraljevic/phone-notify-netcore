using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneNotify.Models.General;
using PhoneNotify.Models.RequestBodies.License;
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
    public class LicenseController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public LicenseController(PhoneNotifySoap client)
        {
            _client = client;
        }

        /// <summary>
        /// Use this method to assign an incoming number to a license key. Numbers are billed at $5/month per number. Normal fees for transactions apply.
        /// </summary>
        /// <param name="requestBody">Incoming phone number (type: string)</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Returns true if assignment was successful. Returns false if the number is already claimed or otherwise unavailable.</returns>
        [HttpPost("AssignIncomingNumber")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AssignIncomingNumber([Required, FromBody] AssignIncomingNumberRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.AssignIncomingNumberAsync(requestBody.IncomingPhoneNumber, licenseKey));
        }

        /// <summary>
        /// Gets a list of Incoming Numbers assigned to a LicenseKey
        /// </summary>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>List of incoming numbers (strings)</returns>
        [HttpGet("GetAssignedNumbers")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string[]>> GetAssignedNumbers([Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetAssignedNumbersAsync(licenseKey));
        }

        /// <summary>
        /// Variable Management - Load a key's variable (The Variable name can be up to 50 characters long).
        /// </summary>
        /// <param name="variableName">Variable name</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>string</returns>
        [HttpGet("LicenseKeyVariableLoad")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> LicenseKeyVariableLoad([Required, FromQuery] string variableName, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LicenseKeyVariableLoadAsync(variableName, licenseKey));
        }

        /// <summary>
        /// Variable Management - Save a variable (VariableName can only be 50 characters or less). Saving a Variable with an existing name will overwrite the old Variable. Saving a blank value will delete the variable.
        /// </summary>
        /// <param name="requestBody">
        /// <para>Variable name (type: string)</para>
        /// <para>Variable value (type: string)</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Success info as boolean value</returns>
        [HttpPost("LicenseKeyVariableSave")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> LicenseKeyVariableSave([FromBody] LicenseKeyVariableSaveRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LicenseKeyVariableSaveAsync(requestBody.VariableName, requestBody.VariableValue, licenseKey));
        }
    }
}
