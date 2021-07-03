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

        /// <summary>
        /// Use this method to create a list. Lists are used to store a list of contacts (members). Use the /ListMember/DialList method to send a message to all the members of a list.
        /// </summary>
        /// <param name="requestBody">
        /// <para>ListName (type: string) - The name you want to apply to the new list.</para>
        /// <para>ParentListID (type: int) - The ID of a list which will be this list's parent. If you don't want your list to have a parent, set this value to 0.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The ID that the system assigned to the list. You will need this ID to alter or send messages to the list.</returns>
        [HttpPost("AddNewList")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddNewList([FromBody] AddNewListRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_AddNewListAsync(requestBody.ListName, requestBody.ParentListID, licenseKey));
        }

        /// <summary>
        /// Use this method to change the parent or name of an existing list.
        /// </summary>
        /// <param name="requestBody">
        /// <para>ListID (type: int) - The list's ID.</para>
        /// <para>ParentListID (type: int) - The ID of a list which will be this list's parent. If you don't want your list to have a parent, set this value to 0. To leave the parent list setting unchanged, set this value to -1.</para>
        /// <para>ListName (type: string) - The name you want to apply to the new list.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Returns true if the operation succeeded, false if not.</returns>
        [HttpPatch("AlterListID")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AlterListID([FromBody] AlterListIDRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_AlterListIDAsync(requestBody.ListID, requestBody.ParentListID, requestBody.ListName, licenseKey));
        }

        /// <summary>
        /// Use this method to delete a list and all its sub-lists (the lists that have it as their ParentListID).
        /// </summary>
        /// <param name="requestBody">ListID (type: int) - The ID of the list you want to delete.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Returns true if the operation succeeded, false if not.</returns>
        [HttpDelete("DeleteList")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteList([Required, FromBody] DeleteListRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_DeleteListAsync(requestBody.ListID, licenseKey));
        }

        /// <summary>
        /// Use this method to dial all the members of a list. Use the /ListMember/DialListAdvanced method to specify advanced options for dialing a list.
        /// </summary>
        /// <param name="requestBody">
        ///<para>ListID (type: int) - The ID of the list to dial.</para>
        ///<para>DialRecursiveLists	(type: boolean)</para>
        ///<para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        ///<para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        ///<para>VoiceID (type: byte) - The text-to-speech voice ID.</para>
        ///<para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>LM_DialReturn object</returns>
        [HttpPost("DialList")]
        [ProducesResponseType(typeof(LM_DialReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LM_DialReturn>> DialList([FromBody] DialListRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_DialListAsync(requestBody.ListID, requestBody.DialRecursiveLists, requestBody.CallerID, requestBody.CallerIDName, requestBody.VoiceID, requestBody.TextToSay, licenseKey));
        }

        /// <summary>
        /// Use this method to dial all the members of a list. This method allows you to set advanced options, including the time to send. If you don't need advanced options, use the /ListMember/DialList method.
        /// </summary>
        /// <param name="requestBody">
        /// <para>CallerID (type: string) - The number to display on the receiving party's Caller ID.</para>
        /// <para>CallerIDName (type: string) - The name to display on the receiving party's Caller ID. (Not commonly used, because most carriers will use their own directory assistance to display name information.)</para>
        /// <para>VoiceID (type: byte) - The text-to-speech voice ID.</para>
        /// <para>TextToSay (type: string) - The text-to-speech text or combination of text-to-speech and script to be read to the receiving party.</para>
        /// <para>TryCount (type: byte) - The number of times to attempt dialing if the initial call is unanswered or busy.</para>
        /// <para>Extension (type: string)</para>
        /// <para>TransferNumber (type: string) - The phone number that the call will be transferred to if the call recipient presses 0. Transfer behavior can be further modified with TextToSay commands.</para>
        /// <para>NextTryInSeconds (type: string) - The number of seconds to wait until the next retry is performed if the original call is unanswered or busy. We recommend waiting at least 60 seconds.</para>
        /// <para>TTSRate (type: byte) - The speed that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 20 (10 being normal). This can also be controlled within the TextToSay parameter.</para>
        /// <para>TTSVolume (type: byte) - The volume that text-to-speech (TTS) will use when speaking the text. The value ranges from 0 to 100 (100 is the default). This can also be controlled within the TextToSay parameter.</para>
        /// <para>ScheduledUTCDatetime (type: string) - The date and time at which to send the call. This is specified as Coordinated Universal Time (UTC).</para>
        /// <para>ListID (type: int) - The ID of the list to dial.</para>
        /// <para>DialRecursiveLists (type: boolean) - </para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>LM_DialReturn object</returns>
        [HttpPost("DialListAdvanced")]
        [ProducesResponseType(typeof(LM_DialReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LM_DialReturn>> DialListAdvanced([FromBody] DialListAdvancedRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_DialListAdvancedAsync(PrepareSoapRequests.PrepareDialListAdvancedSoapRequest(requestBody, licenseKey)));
        }

        /// <summary>
        /// Use this method to get all the list IDs from a specific license key.
        /// </summary>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Array of LM_ListIDs objects</returns>
        [HttpGet("GetListIDsByLicensekey")]
        [ProducesResponseType(typeof(LM_GetListIDsByLicensekeyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LM_GetListIDsByLicensekeyResponse>> GetListIDsByLicensekey([Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_GetListIDsByLicensekeyAsync(new LM_GetListIDsByLicensekeyRequest(licenseKey)));
        }

        /// <summary>
        /// Use this method to add contacts (members) to a list. If you don't have a list yet, create it first using the /ListMember/AddNewList method.
        /// </summary>
        /// <param name="requestBody">
        /// <para>ListID (type: int) - The ID of the list that the new member will be added to.</para>
        /// <para>PhoneNumber (type: string) - The member's phone number.</para>
        /// <para>ClientID (type: string) - The member's business or consumer full name. This parameter can also be used to save additional information about the member.</para>
        /// <para>FirstName (type: string) - The member's first name.</para>
        /// <para>LastName (type: string) - The member's last name.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>The ID that the system assigned to the member. You will need this ID to alter the member.</returns>
        [HttpPost("AddListMember")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddListMember([FromBody] AddListMemberRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_AddListMemberAsync(requestBody.ListID, licenseKey, requestBody.PhoneNumber, requestBody.ClientID, requestBody.FirstName, requestBody.LastName));
        }

        /// <summary>
        /// Use this method to alter an existing list member (contact).
        /// </summary>
        /// <param name="requestBody">
        /// <para>ListMemberID (type: int) - The member's ID.</para>
        /// <para>ClientID (type: string) - The member's business or consumer full name. This parameter can also be used to save additional information about the member.</para>
        /// <para>FirstName (type: string) - The member's first name.</para>
        /// <para>LastName (type: string) - The member's last name.</para>
        /// <para>PhoneNumber (type: string) - The member's phone number.</para>
        /// </param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Returns true if the operation succeeded, false if not.</returns>
        [HttpPatch("AlterListMember")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AlterListMember([FromBody] AlterListMemberRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_AlterListMemberAsync(requestBody.ListMemberID, licenseKey, requestBody.ClientID, requestBody.FirstName, requestBody.LastName, requestBody.PhoneNumber));
        }

        /// <summary>
        /// Use this method to delete a member (contact) from a list.
        /// </summary>
        /// <param name="requestBody">ListMemberID (type: int) - The ID of the member you want to delete.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Returns true if the operation succeeded, false if not.</returns>
        [HttpDelete("DeleteListMember")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteListMember([Required, FromBody] DeleteListMemberRequestBody requestBody, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_DeleteListMemberAsync(requestBody.ListMemberID, licenseKey));
        }

        /// <summary>
        /// Use this method to get all the members (contacts) in a list.    
        /// </summary>
        /// <param name="listID">The ID of the list you want to get.</param>
        /// <param name="licenseKey">Your license key, which is required to invoke this web service.</param>
        /// <returns>Array of LM_ListMembers objects</returns>
        [HttpGet("GetListMembersByListID")]
        [ProducesResponseType(typeof(LM_GetListMembersByListIDResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LM_GetListMembersByListIDResponse>> GetListMembersByListID([Required, FromQuery] int listID, [Required, FromHeader(Name = Constants.RequestParameters.LicenseKey)] string licenseKey)
        {
            if (!InputParametersValidator.IsValidGuidFormat(licenseKey))
            {
                return BadRequest(ErrorDetails.InvalidLicenseKeyFormat);
            }

            return Ok(await _client.LM_GetListMembersByListIDAsync(new LM_GetListMembersByListIDRequest(listID, licenseKey)));
        }
    }
}
