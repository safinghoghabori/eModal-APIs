using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class N9
    {
        [JsonProperty("reference_qualifier")]
        public string ReferenceQualifier { get; set; }

        [JsonProperty("reference_qualifier_description")]
        public string ReferenceQualifierDescription { get; set; }

        [JsonProperty("reference_identification")]
        public string ReferenceIdentification { get; set; }

    }
}
