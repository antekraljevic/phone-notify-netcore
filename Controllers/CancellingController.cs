using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhoneNotify.Models;
using PhoneNotify.Shared;
using System.ComponentModel.DataAnnotations;

namespace PhoneNotify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CancellingController : ControllerBase
    {
        private readonly ILogger<CancellingController> _logger;

        public CancellingController(ILogger<CancellingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("cancelconference")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult CancelConference([Required, FromQuery] string conferenceKey)
        {
            // send conferenceKey parameter to corresponding SOAP route, get result and parse it to JSON, surround with try catch, if there's no exception, return Ok();

            return Ok();
        }

        [HttpGet("cancelnotify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult CancelNotify([Required, FromQuery] string queueId, [Required, FromQuery] string licenseKey)
        {
            // send queueId and licenseKey parameters to corresponding SOAP route, get result and parse it to JSON

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

        [HttpGet("cancelnotifybyreferenceid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ResultAsInt> CancelNotifyByReferenceID([Required, FromQuery] string referenceId, [Required, FromQuery] string licenseKey)
        {
            var xmlResult = @"<?xml version='1.0' encoding='utf-8'?>
            <int xmlns='http://ws.cdyne.com/NotifyWS/'>1</int>";

            var json = XmlHelper.ParseXmlToJson(xmlResult);

            if (json == null)
            {
                return BadRequest();
            }

            ResultAsInt result = new ResultAsInt((int)json["int"]["#text"]);

            return Ok(result);
        }
    }
}
