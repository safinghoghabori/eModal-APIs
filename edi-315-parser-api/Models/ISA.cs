using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class ISA
    {
        [JsonProperty("authorization_information_qualifier")]
        public string AuthorizationInformationQualifier { get; set; }

        [JsonProperty("authorization_information")]
        public string AuthorizationInformation { get; set; }

        [JsonProperty("security_information_qualifier")]
        public string SecurityInformationQualifier { get; set; }

        [JsonProperty("security_information")]
        public string SecurityInformation { get; set; }

        [JsonProperty("interchange_id_qualifier_sender")]
        public string InterchangeIdQualifierSender { get; set; }

        [JsonProperty("interchange_sender_id")]
        public string InterchangeSenderId { get; set; }

        [JsonProperty("interchange_id_qualifier_receiver")]
        public string InterchangeIdQualifierReceiver { get; set; }

        [JsonProperty("interchange_receiver_id")]
        public string InterchangeReceiverId { get; set; }

        [JsonProperty("interchange_date_time")]
        public string InterchangeDateTime { get; set; }

        [JsonProperty("interchange_control_standards_id")]
        public string InterchangeControlStandardsId { get; set; }

        [JsonProperty("interchange_control_version_number")]
        public string InterchangeControlVersionNumber { get; set; }

        [JsonProperty("interchange_control_number")]
        public string InterchangeControlNumber { get; set; }

        [JsonProperty("acknowledgment_requested")]
        public string AcknowledgmentRequested { get; set; }

        [JsonProperty("usage_indicator")]
        public string UsageIndicator { get; set; }
    }
}
