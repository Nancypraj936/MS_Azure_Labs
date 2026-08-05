using Azure.Storage.Blobs;

string connectionString = " ";

string containerName = " ";

var serviceClient = new BlobServiceClient(connectionString);

var containerClient = serviceClient.GetBlobContainerClient(containerName);

await CreateContainerAsync(containerClient);

static async Task CreateContainerAsync(BlobContainerClient containerClient)
{
    await containerClient.CreateIfNotExistsAsync();
    Console.WriteLine("Container created");
}