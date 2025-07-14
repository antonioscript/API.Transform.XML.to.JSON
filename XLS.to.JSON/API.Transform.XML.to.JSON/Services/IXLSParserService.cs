namespace API.Transform.XML.to.JSON.Services;

public interface IXLSParserService
{
    public List<Dictionary<string, object>> ParseXLS(IFormFile file);
    public Dictionary<string, List<Dictionary<string, object>>> ParseAllSheets(IFormFile file);
}
