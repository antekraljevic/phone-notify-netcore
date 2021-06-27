using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNotify.Models.General;
using PhoneNotify.Models.RequestBodies.ListMember;
using PhoneNotify.Shared;
using PhoneNotify.Shared.Helpers;
using PhoneNotify.Shared.Validators;
using PhoneNotifySoapService;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ListMemberController : ControllerBase
    {
        private readonly PhoneNotifySoap _client;

        public ListMemberController(PhoneNotifySoap client)
        {
            _client = client;
        }

        [HttpPost("AddNewList")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddNewList([FromBody] AddNewListRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_AddNewListAsync(requestBody.ListName, requestBody.ParentListID, licenseKey));
        }

        [HttpPatch("AlterListID")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AlterListID([FromBody] AlterListIDRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_AlterListIDAsync(requestBody.ListID, requestBody.ParentListID, requestBody.ListName, licenseKey));
        }

        [HttpDelete("DeleteList")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteList([Required, FromBody] DeleteListRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_DeleteListAsync(requestBody.ListID, licenseKey));
        }

        [HttpPost("DialList")]
        [ProducesResponseType(typeof(LM_DialReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LM_DialReturn>> DialList([FromBody] DialListRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_DialListAsync(requestBody.ListID, requestBody.DialRecursiveLists, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, requestBody.TextToSay, licenseKey));
        }

        [HttpPost("DialListAdvanced")]
        [ProducesResponseType(typeof(LM_DialReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LM_DialReturn>> DialListAdvanced([FromBody] DialListAdvancedRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_DialListAdvancedAsync(PrepareSoapRequests.PrepareDialListAdvancedSoapRequest(requestBody, licenseKey)));
        }

        [HttpGet("GetListIDsByLicensekey")]
        [ProducesResponseType(typeof(LM_GetListIDsByLicensekeyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LM_GetListIDsByLicensekeyResponse>> GetListIDsByLicensekey()
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_GetListIDsByLicensekeyAsync(new LM_GetListIDsByLicensekeyRequest(licenseKey)));
        }

        [HttpPost("AddListMember")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddListMember([FromBody] AddListMemberRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_AddListMemberAsync(requestBody.ListID, licenseKey, requestBody.PhoneNumber, requestBody.ClientID, requestBody.FirstName, requestBody.LastName));
        }

        [HttpPatch("AlterListMember")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AlterListMember([FromBody] AlterListMemberRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_AlterListMemberAsync(requestBody.ListMemberID, licenseKey, requestBody.ClientID, requestBody.FirstName, requestBody.LastName, requestBody.PhoneNumber));
        }

        [HttpDelete("DeleteListMember")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteListMember([Required, FromBody] DeleteListMemberRequestBody requestBody)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_DeleteListMemberAsync(requestBody.ListMemberID, licenseKey));
        }

        [HttpGet("GetListMembersByListID")]
        [ProducesResponseType(typeof(LM_GetListMembersByListIDResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LM_GetListMembersByListIDResponse>> GetListMembersByListID([Required, FromQuery] int listID)
        {
            string licenseKey = (string)HttpContext.Items[Constants.RequestParameters.LicenseKey];
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_GetListMembersByListIDAsync(new LM_GetListMembersByListIDRequest(listID, licenseKey)));
        }
    }
}
