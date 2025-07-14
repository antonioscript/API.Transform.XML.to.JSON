using API.Transform.XML.to.JSON.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Transform.XML.to.JSON.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExcelController : ControllerBase
{
    private readonly IXLSParserService _parser;

    public ExcelController(IXLSParserService parser)
    {
        _parser = parser;
    }

    [HttpPost("to-json")]
    public IActionResult ConvertToJson(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo Excel não enviado.");

        var result = _parser.ParseXLS(file);
        return Ok(result);
    }
}
