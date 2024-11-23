using Newtonsoft.Json;

namespace edi_315_parser_api.Models
{
    public class EDI315Data
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string PartitionKey { get; set; }
        public ISA ISA { get; set; }
        public GS GS { get; set; }
        public TransectionSet TransectionSet { get; set; }
        public ContainerFees ContainerFees { get; set; }
    }
}
