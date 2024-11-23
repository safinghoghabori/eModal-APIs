using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class ContainerFees
    {
        [JsonProperty("fees")]
        public List<Fee> Fees { get; set; } = new List<Fee>();
        
        [JsonProperty("total_fees")]
        public int TotalFees { get; set; } = 0;
        
        [JsonProperty("is_fees_paid")]
        public bool IsFeesPaid { get; set; } = false;
    }

    public class Fee
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
