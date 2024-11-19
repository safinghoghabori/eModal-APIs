namespace edi_315_parser_api.Services
{
    public interface IEDIParserService
    {
        Task ParseEDIFile(IFormFile file);
        //Task GetEDIDataAsync(IFormFile file);
    }
}
