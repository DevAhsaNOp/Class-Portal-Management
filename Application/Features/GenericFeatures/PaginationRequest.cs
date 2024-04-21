using System.Linq.Expressions;

namespace Application.Features.GenericFeatures
{
    public sealed class PaginationRequest
    {
        public int page { get; set; }
        public int pageLength { get; set; }
        public string filter { get; set; }
        public bool desc { get; set; }
        public string orderBy { get; set; }
        public List<int> statuses { get; set; }

        public int parentId { get; set; }

        public PaginationRequest()
        {
            page = 1;
            pageLength = 1000;
        }
    }

    public static class LinqExtensions
    {
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source,
                                      string propertyName,
                                      bool desc)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (string.IsNullOrEmpty(propertyName)) return source;

            // Create a parameter to pass into the Lambda expression
            //(Entity => Entity.OrderByField).
            var parameter = Expression.Parameter(typeof(T), "Entity");

            //  create the selector part, but support child properties (it works without . too)
            string[] childProperties = propertyName.Split('.');
            MemberExpression property = Expression.Property(parameter, childProperties[0]);
            for (int i = 1; i < childProperties.Length; i++)
            {
                property = Expression.Property(property, childProperties[i]);
            }

            LambdaExpression selector = Expression.Lambda(property, parameter);

            string methodName = (desc) ? "OrderByDescending" : "OrderBy";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                                            [source.ElementType, property.Type],
                                            source.Expression, Expression.Quote(selector));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
