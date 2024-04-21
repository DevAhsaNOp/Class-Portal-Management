using Application.Common.Behaviors;
using Application.Features.GenericFeatures;
using Domain.Common;
using System.Linq.Expressions;

namespace Application.Interfaces.Base
{
    public interface IBaseClientRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> Get(long id, CancellationToken cancellationToken = default);
        Task<List<T>> GetAll(CancellationToken cancellationToken = default);
        Task<List<T>> Filter(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);
        PagniationHelper.PagedResult<T> FilterPaginationWithInclude(Expression<Func<T, bool>> where, PaginationRequest paginationRequest, Expression<Func<T, object>>[] includes);
        IQueryable<T> FilterIQueryable(Expression<Func<T, bool>> where, PaginationRequest? paginationRequest = null);
    }
}
