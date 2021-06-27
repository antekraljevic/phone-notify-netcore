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

        [HttpGet("AssignIncomingNumber")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AssignIncomingNumber([Required, FromQuery] string incomingPhoneNumber)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.AssignIncomingNumberAsync(incomingPhoneNumber, licenseKey));
        }

        [HttpGet("GetAssignedNumbers")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string[]>> GetAssignedNumbers()
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.GetAssignedNumbersAsync(licenseKey));
        }

        [HttpGet("LicenseKeyVariableLoad")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> LicenseKeyVariableLoad([Required, FromQuery] string variableName)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LicenseKeyVariableLoadAsync(variableName, licenseKey));
        }

        [HttpPost("LicenseKeyVariableSave")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> LicenseKeyVariableSave([FromBody] LicenseKeyVariableSaveRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LicenseKeyVariableSaveAsync(requestBody.VariableName, requestBody.VariableValue, licenseKey));
        }
    }
}
