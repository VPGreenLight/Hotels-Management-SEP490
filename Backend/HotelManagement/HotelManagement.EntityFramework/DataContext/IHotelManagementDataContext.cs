using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelManagement.EntityFramework.DataContext
{
    public interface IHotelManagementDataContext
    {
        IModel Model { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
        DatabaseFacade Database { get; }
    }
}
