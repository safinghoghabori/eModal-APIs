using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class GS
    {
        [JsonProperty("functional_identifier_code")]
        public string FunctionalIdentifierCode { get; set; }

        [JsonProperty("application_receiver_code")]
        public string ApplicationReceiverCode { get; set; }

        [JsonProperty("functional_group_date_time")]
        public string FunctionalGroupDateTime { get; set; }

        [JsonProperty("group_control_number")]
        public string GroupControlNumber { get; set; }

        [JsonProperty("responsible_agency_code")]
        public string ResponsibleAgencyCode { get; set; }

        [JsonProperty("version_release_industry_identifier_code")]
        public string VersionReleaseIndustryIdentifierCode { get; set; }
    }
}
