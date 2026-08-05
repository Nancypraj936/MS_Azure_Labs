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
        string? connString = "Server=tcp:sqlsvr-dev-eus.database.windows.net,1433;Initial Catalog=appdb;Persist Security Info=False;User ID=sqladmin;Password=Microsoft@2026;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Keep this super simple for demo purposes
        using var conn = new SqlConnection(connString);
        await conn.OpenAsync();

        string sql = @"
            SELECT TOP (50)
                OrderId, CustomerName, CustomerEmail, CourseName, Amount, OrderDateUtc
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
                OrderDateUtc = reader.GetDateTime(5)
            });
        }
    }
}
