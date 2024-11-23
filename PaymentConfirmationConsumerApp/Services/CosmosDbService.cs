using Microsoft.Azure.Cosmos;
using PaymentConfirmationConsumerApp.Models;
using System.Reflection.Metadata;

namespace PaymentConfirmationConsumerApp.Services
{
    public class CosmosDbService: ICosmosDbService
    {
        private const string EndpointUrl = "https://safinraytex07.documents.azure.com:443/";
        private const string PrimaryKey = "wdWpnssWSBYESxLZZXNOvTMiCxm4LX2nbyKbrZJEckKWOb2hMr7Aw7pIQaNhR1nK5ii8NyO8BT8ZACDbRmkZHw==";
        private const string DatabaseName = "EDI315";
        private const string ContainerName = "EDI315Data";

        private readonly Container _container;
        private readonly CosmosClient _cosmosClient;

        public CosmosDbService()
        {
            _cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);
            _container = _cosmosClient.GetContainer(DatabaseName, ContainerName);
        }

        public async Task UpdateFeesPaidStatusAsync(PaymentConfirmation paymentConfirmation)
        {
            try
            {
                string documentId = paymentConfirmation.EdiFileId;
                var response = await _container.ReadItemAsync<dynamic>(documentId, new PartitionKey(documentId));
                var document = response.Resource;

                if (document?.ContainerFees != null)
                {
                    // Update the 'is_fees_paid' field to true
                    document.ContainerFees.is_fees_paid = true;

                    // Replace the document with the updated one
                    var upsertResponse = await _container.ReplaceItemAsync(document, documentId, new PartitionKey(documentId));
                    Console.WriteLine("Document updated successfully.");
                }
                else
                {
                    Console.WriteLine("Document does not contain container fees.");
                }
            }
            catch (CosmosException cosmosException)
            {
                Console.WriteLine($"Error updating document: {cosmosException.Message}");
            }
        }
    }
}
