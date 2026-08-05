using Azure.Storage.Blobs;

string connectionstring=" ";

string containerName=" ";

var serviceClient= new BlobServiceClient(connectionstring);

var containerClient=serviceClient.GetBlobContainerClient(containerName);

static async Task CreateContainerAsync(BlobContainerClient containerClient)
{
await containerClient.CreateIfNotExistsAsync();
Console.WriteLine("Container created");   

}

static async Task UploadBlob(BlobContainerClient containerClient)
{
    string localFilePath=" ";
    string blobName=" ";

    var blobClient=containerClient.GetBlobClient(blobName);
    await blobClient.UploadAsync(localFilePath,overwrite: true);
    Console.WriteLine("Blob uploaded to storage account");

}

await UploadBlob(containerClient);