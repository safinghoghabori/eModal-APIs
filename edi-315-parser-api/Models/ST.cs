using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class ST
    {
        [JsonProperty("transaction_set_identifier_code")]
        public string TransactionSetIdentifierCode { get; set; }

        [JsonProperty("transaction_set_control_number")]
        public string TransactionSetControlNumber { get; set; }
    }
}
