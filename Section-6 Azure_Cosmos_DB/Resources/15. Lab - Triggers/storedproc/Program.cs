using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using Newtonsoft.Json;

string Endpoint="ENTER_URI/ENDPOINT";
string Key="ENTER_PRIMARY_KEY/SECONDARY_KEY";
string DatabaseId = "orders-db";
string ContainerId = "orders";
string PartitionKeyPath = "/customerId";

var client = new CosmosClient(Endpoint, Key);

Container container = client.GetContainer(DatabaseId, ContainerId);

var order = new { id = "ord-1001", customerId = "C004", total = 199.0 };

var options = new ItemRequestOptions
{
    PreTriggers = new List<string> { "setOrderDefaults" }
};

var resp=await container.CreateItemAsync(order,new PartitionKey("C004"),options);

Console.WriteLine(resp.Resource);