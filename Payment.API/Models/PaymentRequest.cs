namespace Payment.API.Models
{
    public class PaymentRequest
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string EdiFileId { get; set; }  
    }
}
