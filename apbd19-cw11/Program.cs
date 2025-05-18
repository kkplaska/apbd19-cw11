using apbd19_cw11.Data;
using apbd19_cw11.Services;
using Microsoft.EntityFrameworkCore;

namespace apbd19_cw11;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();

        // Konfiguracja kontekstu bazy danych
        // ConnectionString jest pobierany z appsettings.json, oczywiście należy go tam też ustawić
        builder.Services.AddDbContext<DatabaseContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        // Wstrzykiwanie zależności
        // https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
        builder.Services.AddScoped<IDbService, DbService>();

        var app = builder.Build();
        
        // app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}