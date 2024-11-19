using edi_315_parser_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace edi_315_parser_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EDIParserController : ControllerBase
    {
        private readonly IEDIParserService _ediParserService;
        public EDIParserController(IEDIParserService ediParserService) 
        {
            _ediParserService = ediParserService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "File is empty or not provided." });

            try
            {
                await _ediParserService.ParseEDIFile(file);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            return Ok(new {message = "successfully parsed."});
        }
    }
}
