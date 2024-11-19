using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class ShipmentStatus
    {
        [JsonProperty("shipment_status_code")]
        public string ShipmentStatusCode { get; set; }

        [JsonProperty("shipment_status_code_description")]
        public string ShipmentStatusCodeDescription { get; set; }

        [JsonProperty("shipment_date_time")]
        public string ShipmentDateTime { get; set; }
    }
}
