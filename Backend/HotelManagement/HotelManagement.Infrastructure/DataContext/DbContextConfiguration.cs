using HotelManagement.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using EFCore.NamingConventions;

namespace HotelManagement.Infrastructure.DataContext
{
    /// <summary>
    /// Cấu hình phương thức kết nối tới PostgreSQL Database
    /// Bao gồm connection pooling, retry logic, và performance optimizations
    /// </summary>
    public static class DbContextConfiguration
    {
        public static void AddDbConfig(this IServiceCollection services, IConfiguration configs) 
        {
            var connectionString = configs.GetConnectionString("PostgreSQL") 
                ?? throw new InvalidOperationException("Connection string 'PostgreSQL' not found");

            // Cấu hình connection string với pooling settings
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Pooling = true,
                MinPoolSize = 5,
                MaxPoolSize = 100,
                ConnectionLifetime = 300, // 5 minutes
                ConnectionIdleLifetime = 60, // 1 minute
                Timeout = 30,
                CommandTimeout = 30,
                Enlist = false, // Disable TransactionScope enlisting cho performance
                NoResetOnClose = false // Enable connection reset
            };

            services.AddDbContext<HotelManagementDataContext>(options =>
            {
                options.UseNpgsql(builder.ConnectionString, npgsqlOptions =>
                {
                    // Enable retry on failure
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorCodesToAdd: null
                    );

                    // Command timeout
                    npgsqlOptions.CommandTimeout(30);

                    // Use query splitting để tránh cartesian explosion
                    npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

                    // Migration assembly
                    npgsqlOptions.MigrationsAssembly("HotelManagement.Infrastructure");

                    // Map enums to PostgreSQL enum types
                    npgsqlOptions.MapEnum<BookingStatus>("booking_status");
                    npgsqlOptions.MapEnum<RoomStatus>("room_status");
                    npgsqlOptions.MapEnum<InvoiceStatus>("invoice_status");
                    npgsqlOptions.MapEnum<JobStatus>("job_status");
                    // TODO: Map tất cả các enums khác
                }).UseSnakeCaseNamingConvention();

                // Enable sensitive data logging in development only
                if (configs.GetValue<bool>("Logging:EnableSensitiveDataLogging"))
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }

                // Use lazy loading proxies (optional)
                // options.UseLazyLoadingProxies();
            });
        }
    }
}
