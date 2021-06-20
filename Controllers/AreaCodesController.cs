using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneNotify.Models;
using PhoneNotify.Shared.Helpers;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AreaCodesController : ControllerBase
    {
        private readonly ILogger<LicenseController> _logger;

        public AreaCodesController(ILogger<LicenseController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getavailableareacodes")]
        [ProducesResponseType(typeof(AreaCodes), StatusCodes.Status200OK)]
        public ActionResult<AreaCodes> GetAvailableAreaCodes()
        {
            // send incomingPhoneNumber and licenseKey parameters to corresponding SOAP route, get result and parse it to JSON

            var xmlResult = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:soap =""http://www.w3.org/2003/05/soap-envelope"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd =""http://www.w3.org/2001/XMLSchema"">
                <soap:Body>
                    <GetAvailableAreaCodesResponse xmlns=""http://ws.cdyne.com/NotifyWS/"">
                       <GetAvailableAreaCodesResult>
                           <AreaCode>
                               <AreaCodeNumber/>
                               <Location>Toll Free</Location>
                            </AreaCode>
                            <AreaCode>
                                <AreaCodeNumber>571</AreaCodeNumber>
                                <Location>Toll Free</Location>
                            </AreaCode>
                            <AreaCode>
                                <AreaCodeNumber>855</AreaCodeNumber>
                                <Location>Toll Free</Location>
                            </AreaCode>
                            <AreaCode>
                                <AreaCodeNumber>877</AreaCodeNumber>
                                <Location>Toll Free</Location>
                            </AreaCode>
                        </GetAvailableAreaCodesResult>
                    </GetAvailableAreaCodesResponse>
                </soap:Body>
            </soap:Envelope>";

            var json = XmlHelper.ParseXmlToJson(xmlResult);

            if (json == null)
            {
                return BadRequest();
            }

            AreaCodes result = new AreaCodes(
                JsonHelper.GetListOfAreaCodesFromJsonObject(
                    json["soap:Envelope"]["soap:Body"]["GetAvailableAreaCodesResponse"]["GetAvailableAreaCodesResult"]["AreaCode"]
                    )
                );

            return Ok(result);
        }
    }
}
