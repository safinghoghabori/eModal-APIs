using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentConfirmationConsumerApp.Models
{
    public class PaymentConfirmation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string EdiFileId { get; set; }
        public int Amount { get; set; }
        public string ContainerNumber { get; set; }
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
