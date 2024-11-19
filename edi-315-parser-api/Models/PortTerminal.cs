using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class PortTerminal
    {
        [JsonProperty("port_function_code")]
        public string PortFunctionCode { get; set; }

        [JsonProperty("port_function_code_description")]
        public string PortFunctionCodeDescription { get; set; }

        [JsonProperty("location_type_code")]
        public string LocationTypeCode { get; set; }

        [JsonProperty("location_type_code_description")]
        public string LocationTypeCodeDescription { get; set; }

        [JsonProperty("location_identifier")]
        public string LocationIdentifier { get; set; }

        [JsonProperty("port_name")]
        public string PortName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("terminal_name")]
        public string TerminalName { get; set; }

        [JsonProperty("state_or_province_code")]
        public string StateOrProvinceCode { get; set; }
    }
}
