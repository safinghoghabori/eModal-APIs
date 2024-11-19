using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class Q2
    {
        [JsonProperty("vessel_code")]
        public string VesselCode { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("departure_date")]
        public string DepartureDate { get; set; }

        [JsonProperty("lading_quantity")]
        public string LadingQuantity { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("weight_qualifier")]
        public string WeightQualifier { get; set; }

        [JsonProperty("voyage_number")]
        public string VoyageNumber { get; set; }

        [JsonProperty("vessel_code_qualifier")]
        public string VesselCodeQualifier { get; set; }

        [JsonProperty("vessel_name")]
        public string VesselName { get; set; }

        [JsonProperty("weight_unit_code")]
        public string WeightUnitCode { get; set; }
    }
}
