using edi_315_parser_api.DTOs;

namespace edi_315_parser_api.Services
{
    public interface IEDIParserService
    {
        Task ParseEDIFile(IFormFile file);
        Task<EDIRespDTO> GetEDIDataByContainerNoAsync(string containerNo);
    }
}
