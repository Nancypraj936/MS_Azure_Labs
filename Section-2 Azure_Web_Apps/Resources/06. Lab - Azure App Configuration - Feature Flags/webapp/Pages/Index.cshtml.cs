using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using webapp;

namespace webapp.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IConfiguration _config;

     public IndexModel(IConfiguration config)
    {
        _config = config;
    }

    public List<CourseOrder> Orders { get; private set; } = new();

    public async Task OnGetAsync()
    {
       string? connString = _config.GetConnectionString("AzureSql");
    
        // Keep this super simple for demo purposes
        using var conn = new SqlConnection(connString);
        await conn.OpenAsync();

        string sql = @"
            SELECT TOP (50)
                OrderId, CustomerName, CustomerEmail, CourseName, Amount, OrderDateUtc, Notes
            FROM dbo.CourseOrders
            ORDER BY OrderId DESC;";

        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Orders.Add(new CourseOrder
            {
                OrderId = reader.GetInt32(0),
                CustomerName = reader.GetString(1),
                CustomerEmail = reader.GetString(2),
                CourseName = reader.GetString(3),
                Amount = reader.GetDecimal(4),
                OrderDateUtc = reader.GetDateTime(5),
                Notes = reader.IsDBNull(6) ?null : reader.GetString(6)
            });
        }
    }
}
