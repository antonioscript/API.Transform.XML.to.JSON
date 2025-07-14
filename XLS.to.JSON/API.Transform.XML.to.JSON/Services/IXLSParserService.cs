namespace API.Transform.XML.to.JSON.Services;

public interface IXLSParserService
{
    public List<Dictionary<string, object>> ParseXLS(IFormFile file);
}
