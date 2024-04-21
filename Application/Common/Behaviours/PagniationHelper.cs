namespace Application.Common.Behaviors
{
    public static class PagniationHelper
    {
        public abstract class PagedResultBase
        {
            public int CurrentPage { get; set; }
            public int PageCount { get; set; }
            public int PageSize { get; set; }
            public int RowCount { get; set; }

            public int FirstRowOnPage
            {
                get { return (CurrentPage - 1) * PageSize + 1; }
            }

            public int LastRowOnPage
            {
                get { return Math.Min(CurrentPage * PageSize, RowCount); }
            }
        }

        public class PagedResult<T> : PagedResultBase where T : class
        {
            public IList<T> Data { get; set; }

            public PagedResult()
            {
                Data = [];
            }
        }

        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
            int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Data = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
