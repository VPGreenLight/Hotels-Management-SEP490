using HotelManagement.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HotelManagement.Infrastructure.DataContext
{
    public partial class HotelManagementDataContext 
        : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
        UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IHotelManagementDataContext
    {
        public HotelManagementDataContext(DbContextOptions<HotelManagementDataContext> options) : base(options)
        {
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public override DatabaseFacade Database => base.Database;

        /// <summary>
        /// Liệt kê tất cả các bảng trong database
        /// </summary>
        /// <typeparam name="T"></typeparam>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
                }
            }
        }
    }
}
