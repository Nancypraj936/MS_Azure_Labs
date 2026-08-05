using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using Newtonsoft.Json;

string Endpoint="ENTER_URI/ENDPOINT";
string Key="ENTER_PRIMARY_KEY/SECONDARY_KEY";
string DatabaseId = "orders-db";
string ContainerId = "orders";
string PartitionKeyPath = "/customerId";
string sprocId = "placeOrder";

var client = new CosmosClient(Endpoint, Key);

Container container = client.GetContainer(DatabaseId, ContainerId);

var customerId = "C004"; 
var orderId = $"ord-001";
var auditId = "audit-001";   

var order = new
{
    id = orderId,
    customerId = customerId,
    type = "order",
    total = 199.00
};

var audit = new
{
    id = auditId,
    customerId = customerId,
    type = "audit",
    message = $"Order {orderId} placed"
};

StoredProcedureExecuteResponse<dynamic> resp=await container.Scripts.ExecuteStoredProcedureAsync<dynamic>(
    storedProcedureId: sprocId,
    partitionKey: new PartitionKey(customerId),
     parameters: new dynamic[] {order,audit}
);

Console.WriteLine($"Status: {resp.StatusCode}");