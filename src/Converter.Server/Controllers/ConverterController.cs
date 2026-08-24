using Converter.Core;
using Microsoft.AspNetCore.Mvc;

namespace Converter.API.Controllers
{
    [ApiController]
    [Route("api/v1/convert")]
    public class ConverterController : ControllerBase
    {
        private const int MAX_DOLLARS = 999999999;
        private const int MAX_CENTS = 99;
        private readonly ConverterFactory mFactory;
        private readonly ILogger<ConverterController> mLogger;

        public ConverterController(ConverterFactory factory, ILogger<ConverterController> logger)
        {
            mFactory = factory;
            mLogger = logger;
        }

        [HttpGet]
        public IActionResult Convert([FromQuery] long dollars, [FromQuery] int cents, [FromQuery] string lang)
        {
            if (dollars < 0 || dollars > MAX_DOLLARS)
            {
                mLogger.LogWarning("Validation failed: Invalid dollars amount ({Dollars}).", dollars);
                return BadRequest("Dollars must be between 0 and 999999999");
            }

            if (cents < 0 || cents > MAX_CENTS)
            {
                mLogger.LogWarning("Validation failed: Invalid cents amount ({Cents}).", cents);
                return BadRequest("Cents must be between 0 and 99.");
            }

            if(string.IsNullOrEmpty(lang))
            {
                mLogger.LogWarning("Validation failed: Language parameter is missing.");
                return BadRequest("Language parameter is required.");
            }

            try
            {
                IConverter converter = mFactory.GetConverter(lang);
                string result = converter.Convert(dollars, cents);

                mLogger.LogInformation("Successfully converted amounts to language: {Lang}", lang);
                return Ok(result);
            }
            catch (NotSupportedException ex)
            {
                mLogger.LogError(ex, "Unsupported eception occured for language requested: {Lang}", lang);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                mLogger.LogError(ex, "An unexpected error occurred while processing conversion for Language: {Lang}", lang);
                return StatusCode(500, $"Internal server error {ex.Message }");
            }
        }
    }
}
