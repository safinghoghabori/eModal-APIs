namespace Payment.API.Models
{
    public class PaymentRequest
    {
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string EdiFileId { get; set; }  
    }
}
