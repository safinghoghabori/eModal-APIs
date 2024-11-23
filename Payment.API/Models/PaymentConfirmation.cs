namespace Payment.API.Models
{
    public class PaymentConfirmation
    {
        public string UserId { get; set; }
        public string EdiFileId { get; set; }
        public int Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
