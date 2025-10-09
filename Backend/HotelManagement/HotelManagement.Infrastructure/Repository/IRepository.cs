using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using HotelManagement.Domain.Models.Pagination;

namespace HotelManagement.Infrastructure.Repository
{
    //Todo: Add Pagination support
    //Todo: Use tracking as input attribute instead of creating multiple methods as below.
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);
        Task<T?> GetByIdAsync(object id, Func<IQueryable<T>, IQueryable<T>> include);

        Task<T?> GetOneAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            // Expression<Func<T, TResult>>? selector = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null, bool disableTracking = true);

        Task<List<T>> GetListAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null, bool disableTracking = true);

        Task<Pagination<T>> GetPaginationAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            int pageSize = 10,
            int pageNumber = 1, bool disableTracking = true);

        Task<int> GetCount(Expression<Func<T, bool>>? filter = null);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entitíe);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        Task RollbackTransactionAsync(IDbContextTransaction transaction);
        Task AddRangeAsync(List<T> billTickets);
    }
}