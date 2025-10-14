using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelManagement.Infrastructure.DataContext
{
    public interface IHotelManagementDataContext
    {
        IModel Model { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
        DatabaseFacade Database { get; }
        // DbSet<User> Users { get; set; }
        // DbSet<Role> Roles { get; set; }
        // DbSet<Branch> Branches { get; set; }
        // DbSet<Room> Rooms { get; set; }
        // DbSet<Booking> Bookings { get; set; }
        // DbSet<Customer> Customers { get; set; }
        // DbSet<Invoice> Invoices { get; set; }
        // DbSet<Task> Tasks { get; set; }
        // DbSet<AuditLog> AuditLogs { get; set; }
    }
}
