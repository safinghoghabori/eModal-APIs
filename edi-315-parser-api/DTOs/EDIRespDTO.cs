namespace edi_315_parser_api.DTOs
{
    public class EDIRespDTO
    {
        public string Id { get; set; }
        public string TransactionIdentifierCode { get; set; }
        public DateTime? LastFreeDate { get; set; }
        public DateTime? GoodThroughDate { get; set; }
        public EDIHeader EDIHeader { get; set; }
        public ContainerInfo ContainerInfo { get; set; }
        public ContainerFeesInfo ContainerFeesInfo { get; set; }
        public VesselInfo VesselInfo { get; set; }
        public List<ShipmentStatusInfo> ShipmentStatuses { get; set; }
        public List<PortTerminalInfo> PortTerminals { get; set; }
    }

    public class EDIHeader
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string TransactionDateTime { get; set; }
    }

    public class ContainerInfo
    {
        public string ShipmentStatusCode { get; set; }
        public string ShipmentStatusDesc {  get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerStatusCode { get; set; }
        public string ContainerStatusDesc { get; set; }
        public string ContainerType { get; set; }
        public string ContainerTypeDesc { get; set; }
    }

    public class ContainerFeesInfo
    {
        public List<FeeDesc> Fees { get; set; }
        public int TotalFees { get; set; }
        public bool IsFeesPaid { get; set; }
    }

    public class FeeDesc
    {
        public string Type { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }
    }

    public class VesselInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
        public string Number { get; set; }
    }

    public class ShipmentStatusInfo
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public string DateTime { get; set; }
    }

    public class PortTerminalInfo
    {
        public string PortName { get; set; }
        public string PortCode { get; set; }
        public string PortDesc { get; set; }
        public string TerminalName { get; set; }
        public string CountryCode { get; set; }
        public string StateOrProvinceCode { get; set; }
    }
}
