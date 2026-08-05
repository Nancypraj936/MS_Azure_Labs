using System.Net;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

string Endpoint="ENTER_URI/ENDPOINT";
string Key="ENTER_PRIMARY_KEY/SECONDARY_KEY";

var cosmosClient = new CosmosClient(Endpoint,Key);

static async Task CreateDBContainer(CosmosClient client)
{
    string DatabaseId="orders-db";
    string ContainerId="orders";
    string PartitionKey="/customerId";
    
    Database database=client.GetDatabase(DatabaseId);
    await client.CreateDatabaseIfNotExistsAsync(DatabaseId);
    Console.WriteLine("Database created");

    Container container=database.GetContainer(ContainerId);
    await database.CreateContainerIfNotExistsAsync(ContainerId,PartitionKey);
    Console.WriteLine("Container created");

}

await CreateDBContainer(cosmosClient);