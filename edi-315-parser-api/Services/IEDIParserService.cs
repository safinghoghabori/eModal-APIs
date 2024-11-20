using edi_315_parser_api.Models;

namespace edi_315_parser_api.Services
{
    public interface IEDIParserService
    {
        Task ParseEDIFile(IFormFile file);
        Task<EDI315Data> GetEDIDataByContainerNoAsync(string containerNo);
    }
}
