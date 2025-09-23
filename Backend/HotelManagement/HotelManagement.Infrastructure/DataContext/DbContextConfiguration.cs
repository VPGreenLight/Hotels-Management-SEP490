using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Infrastructure.DataContext
{
	/// <summary>
	/// Cấu hình phương thức kết nối tới SQL Server (Cơ sở dữ liệu)
	/// </summary>
	public static class DbContextConfiguration
	{
		public static void AddDbConfig(this IServiceCollection services, IConfiguration configs) 
		{
			services.AddDbContext<HotelManagementDataContext>(options => options.UseSqlServer(configs.GetConnectionString("SQLServer")));
		}
	}
}
