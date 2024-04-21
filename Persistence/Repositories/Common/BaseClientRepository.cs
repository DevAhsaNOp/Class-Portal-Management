using Application.Common.Behaviors;
using Application.Features.GenericFeatures;
using Application.Interfaces.Base;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq.Expressions;
using static Application.Common.Behaviors.PagniationHelper;
using static Application.Common.Behaviors.Statuses;

namespace Persistence.Repositories.Common
{
    public class BaseClientRepository<T> : IBaseClientRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _dataContext;
        protected readonly List<int> _systemStatusesAllowed = new List<int>() { SystemStatus.Active.GetHashCode(), SystemStatus.InActive.GetHashCode() };

        public BaseClientRepository(DataContext myDbContext)
        {
            _dataContext = myDbContext;
        }

        public void Create(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = _dataContext.tblAdmin.FirstOrDefault(x => x.Id == entity.CreatedBy).Id;
            _dataContext.Add(entity);
        }

        public void CreateBulk(IEnumerable<T> entities)
        {
            DateTime currentTime = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                entity.CreatedAt = currentTime;
                int createdBy = _dataContext.tblAdmin.FirstOrDefault(x => x.Id == entity.CreatedBy).Id;
                entity.CreatedBy = createdBy;
            }

            _dataContext.AddRange(entities);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = _dataContext.tblAdmin.FirstOrDefault(x => x.Id == entity.UpdatedBy).Id;
            _dataContext.Update(entity);
        }

        public void UpdateBulk(IEnumerable<T> entities)
        {
            DateTime currentTime = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                entity.UpdatedAt = currentTime;
                int updatedBy = _dataContext.tblAdmin.FirstOrDefault(x => x.Id == entity.UpdatedBy).Id;
                entity.UpdatedBy = updatedBy;
            }

            _dataContext.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.DeletedBy = _dataContext.tblAdmin.FirstOrDefault(x => x.Id == entity.DeletedBy).Id;
            _dataContext.Update(entity);
        }

        public async Task<T> Get(long id, CancellationToken cancellationToken)
        {
            return await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _dataContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<List<T>> Filter(Expression<Func<T, bool>> where, CancellationToken cancellationToken)
        {
            var query = _dataContext.Set<T>().Where(where);

            return await query.ToListAsync(cancellationToken);
        }

        public IQueryable<T> FilterIQueryable(Expression<Func<T, bool>> where, PaginationRequest? paginationRequest = null)
        {
            IQueryable<T> query;
            if (paginationRequest != null && paginationRequest.desc)
            {
                query = _dataContext.Set<T>().Where(where).SortBy(paginationRequest.orderBy, paginationRequest.desc);
            }
            else
                query = _dataContext.Set<T>().Where(where);

            return query;
        }

        public PagedResult<T> FilterPagination(Expression<Func<T, bool>> where, PaginationRequest paginationRequest)
        {
            IQueryable<T> query;
            if (paginationRequest.desc)
            {
                query = _dataContext.Set<T>().Where(where).SortBy(paginationRequest.orderBy, paginationRequest.desc);
            }
            else
                query = _dataContext.Set<T>().Where(where);

            return query.GetPaged(paginationRequest.page, paginationRequest.pageLength);
        }

        public PagedResult<T> FilterPaginationWithInclude(Expression<Func<T, bool>> where, PaginationRequest paginationRequest, Expression<Func<T, object>>[] includes)
        {
            //It will use like this
            //var includes = new Expression<Func<EPRF_Title, object>>[] { (EPRF_Title entity) => entity.Gender };
            //var list = await FilterPaginationWithInclude(x => pagination.statuses.Contains(x.Status), pagination, includes, cancellationToken);

            IQueryable<T> query;
            if (paginationRequest.desc)
            {
                query = _dataContext.Set<T>().Where(where).SortBy(paginationRequest.orderBy, paginationRequest.desc);
            }
            else
                query = _dataContext.Set<T>().Where(where);

            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }

            return query.GetPaged(paginationRequest.page, paginationRequest.pageLength);
        }

    }
}
