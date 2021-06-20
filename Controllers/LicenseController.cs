using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhoneNotify.Models;
using PhoneNotify.Shared.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class LicenseController : ControllerBase
    {
        private readonly ILogger<LicenseController> _logger;

        public LicenseController(ILogger<LicenseController> logger)
        {
            _logger = logger;
        }

        [HttpGet("assignincomingnumber")]
        [ProducesResponseType(typeof(ResultAsBoolean), StatusCodes.Status200OK)]
        public ActionResult<ResultAsBoolean> AssignIncomingNumber([Required, FromQuery] string incomingPhoneNumber, [Required, FromQuery] string licenseKey)
        {
            // send incomingPhoneNumber and licenseKey parameters to corresponding SOAP route, get result and parse it to JSON

            var xmlResult = @"<?xml version='1.0' encoding='utf-8'?>
            <boolean xmlns='http://ws.cdyne.com/NotifyWS/'>true</boolean>";

            var json = XmlHelper.ParseXmlToJson(xmlResult);

            if (json == null)
            {
                return BadRequest();
            }

            ResultAsBoolean result = new ResultAsBoolean((bool)json["boolean"]["#text"]);

            return Ok(result);
        }

        [HttpGet("getassignednumbers")]
        [ProducesResponseType(typeof(ResultAsArrayOfStrings), StatusCodes.Status200OK)]
        public ActionResult<ResultAsArrayOfStrings> GetAssignedNumbers([Required, FromQuery] string licenseKey)
        {
            // send request to SOAP API endpoint first, get result and return it as json

            var xmlResult = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:soap =""http://www.w3.org/2003/05/soap-envelope"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                        <GetAssignedNumbersResponse xmlns=""http://ws.cdyne.com/NotifyWS/"">
                            <GetAssignedNumbersResult>
                                <string>test1</string>
                                <string>test2</string>
                            </GetAssignedNumbersResult>
                        </GetAssignedNumbersResponse>
                    </soap:Body>
                </soap:Envelope>";

            var json = XmlHelper.ParseXmlToJson(xmlResult);

            if (json == null)
            {
                return BadRequest();
            }

            ResultAsArrayOfStrings result = new ResultAsArrayOfStrings(
                JsonHelper.GetListOfStringsFromJsonObject(
                    json["soap:Envelope"]["soap:Body"]["GetAssignedNumbersResponse"]["GetAssignedNumbersResult"]["string"]
                    )
                );

            return Ok(result);
        }
    }
}
