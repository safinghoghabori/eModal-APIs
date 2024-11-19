using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class B4
    {
        [JsonProperty("inquiry_request_number")]
        public string InquiryRequestNumber { get; set; }

        [JsonProperty("shipment_status_code")]
        public string ShipmentStatusCode { get; set; }

        [JsonProperty("shipment_status_code_description")]
        public string ShipmentStatusCodeDescription { get; set; }

        [JsonProperty("inquiry_request_date_time")]
        public string InquiryRequestDateTime { get; set; }

        [JsonProperty("container_number")]
        public string ContainerNumber { get; set; }

        [JsonProperty("equipment_status_code")]
        public string EquipmentStatusCode { get; set; }

        [JsonProperty("equipment_status_code_description")]
        public string EquipmentStatusCodeDescription { get; set; }

        [JsonProperty("equipment_type")]
        public string EquipmentType { get; set; }

        [JsonProperty("equipment_type_description")]
        public string EquipmentTypeDescription { get; set; }
    }
}
