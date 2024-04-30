using AssetManagement.Server.Infrastructure.DB.Context;
using AssetManagement.Server.Infrastructure.DB.Models;
using AssetManagement.Server.Infrastructure.Exceptions;
using AssetManagement.Server.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace AssetManagement.Server.Infrastructure.BusinessLogic.Services
{
    public interface IBaseServices<T> where T : BaseEntity
    {
        IQueryable<T> Query();
        Task<int> UpsertAsync(T entity, CancellationToken token = default);
        Task<T> CreateAsync(T entity, CancellationToken token = default);
        Task<T> UpdateAsync(T model, CancellationToken token = default);
        Task<int> UpdateRangeAsync(List<T> entities, CancellationToken token = default);
        Task<List<T>> ListAsync(CancellationToken token = default);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
        Task<T> GetAsync(int id, bool isNoTracking = true, CancellationToken token = default);
        Task<int> DeleteAsync(T model, CancellationToken token = default);
    }

    public class BaseService<T> : IBaseServices<T> where T : BaseEntity
    {
        private AssetManagementDbContext _context;
        private readonly IIdentityService _identityService;

        public BaseService(AssetManagementDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public IQueryable<T> Query() =>
            _context.Set<T>().AsQueryable();

        public async Task<int> UpsertAsync(T entity, CancellationToken token = default)
        {
            var function = "";

            try
            {
                if (entity.Id == 0)
                {
                    function = "Creation";

                    var obj = await _context.Set<T>().AddAsync(entity, token);
                    await _context.SaveChangesAsync(_identityService.GetUserEmail(), token);

                    entity = obj.Entity;
                }
                else
                {
                    function = "Update";

                    var obj = _context.Set<T>().Update(entity);
                    await _context.SaveChangesAsync(_identityService.GetUserEmail(), token);

                    entity = obj.Entity;
                }

                return entity.Id;
            }
            catch (Exception ex)
            {
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                throw new UnprocessableEntityHelperException($"{function} of {typeof(T).Name} is Unsuccessful: {message}", ex);
            }
        }

        public async Task<T> CreateAsync(T entity, CancellationToken token = default)
        {
            try
            {
                var obj = await _context.Set<T>().AddAsync(entity, token);
                await _context.SaveChangesAsync(_identityService.GetUserEmail(), token);

                return obj.Entity;
            }
            catch (Exception ex)
            {
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                throw new UnprocessableEntityHelperException($"Creation of {typeof(T).Name} is Unsuccessful: {message}", ex);
            }
        }

        public async Task<T> UpdateAsync(T model, CancellationToken token = default)
        {
            try
            {
                var obj = _context.Set<T>().Update(model);
                await _context.SaveChangesAsync(_identityService.GetUserEmail(), token);

                return obj.Entity;
            }
            catch (Exception ex)
            {
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                throw new NotModifiedHelperException($"Update Operation of {typeof(T).Name} is Unsuccessful: {message}", ex);
            }
        }
        public async Task<int> UpdateRangeAsync(List<T> entities, CancellationToken token = default)
        {
            int count = 0;
            try
            {
                if (entities?.Count > 0)
                {
                    _context.Set<T>().UpdateRange(entities);
                    count = await _context.SaveChangesAsync(_identityService.GetUserEmail(), token);
                }
            }
            catch (Exception ex)
            {
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                throw new NotModifiedHelperException($"Bulk Update Operation of {typeof(T).Name} is Unsuccessful: {message}", ex);
            }

            return count;
        }

        public async Task<T> GetAsync(int id, bool isNoTracking = true, CancellationToken token = default)
        {
            T entity;
            if (!isNoTracking)
                entity = await FirstOrDefaultByIdCompileAsync(_context, id);
            else
                entity = await FirstOrDefaultByIdNoTrackingCompileAsync(_context, id);

            return entity;
        }

        public async Task<List<T>> ListAsync(CancellationToken token = default)
        {
            var result = await MethodsExt.MapDataAsync(ToListCompileAsync(_context));
            return result;
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            var list = await _context.Set<T>().Where(predicate).AsNoTracking()
                                        .OrderByDescending(x => x.CreatedAt)
                                        .ToListAsync(token);
            return list;
        }

        public async Task<int> DeleteAsync(T model, CancellationToken token = default)
        {
            try
            {
                _context.Set<T>().Remove(model);
                await _context.SaveChangesAsync(token);
            }
            catch (ConflictHelperException ex)
            {
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                throw new ConflictHelperException($"Delete Operation of {typeof(T).Name} with Id {model.Id} is Unsuccessful: {message}", ex);
            }

            return model.Id;
        }
         private static readonly Func<AssetManagementDbContext, IAsyncEnumerable<T>> ToListCompileAsync =
            EF.CompileAsyncQuery(
                (AssetManagementDbContext context) =>
                    context.Set<T>().AsNoTracking().OrderBy(x => x.Id).Where(x => true));

        private static readonly Func<AssetManagementDbContext, int, Task<T>> FirstOrDefaultByIdNoTrackingCompileAsync =
           EF.CompileAsyncQuery(
               (AssetManagementDbContext context, int id) => context.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id == id));

        private static readonly Func<AssetManagementDbContext, int, Task<T>> FirstOrDefaultByIdCompileAsync =
            EF.CompileAsyncQuery(
                (AssetManagementDbContext context, int id) => context.Set<T>().FirstOrDefault(x => x.Id == id));

    }
}
