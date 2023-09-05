using Core.Pagination;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Core.Extensions
{
    public static class PagingExtension
    {
        public static IPaginate ToPaginate(IQueryable<dynamic> query,PaginateRequest request)
        {
            var response = new Paginate(request.Page,request.Size,query.Count());

            response.Items=query.SelectIt(request?.Fields).Skip((response.Page-1)* response.Size).Take(response.Size).WhereIt(request.Filters).ToList().ToDynamicList();

            return response;
        }
        static IQueryable<dynamic> SelectIt(this IQueryable<dynamic> query, string[] fields) {
            if (fields == null || fields.Length == 0)
                return query;

            return query.Select($"new ({string.Join(", ", fields)})") as IQueryable<dynamic>;
        }
        static IQueryable<dynamic> WhereIt(this IQueryable<dynamic> query, PaginateFilter[] filters) {
            if (filters == null || filters.Length == 0)
                return query;

            var sb =new StringBuilder();

            for (int i = 0;i < filters.Length;i++)
            {
                var filter = filters[i];

                switch (filter.Operator) {
                    case "contains" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                    case "like" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                        sb.Append($"{filter.FieldName}.Contains(\"{filter.Operator}\")");
                        break;
                    case "equals" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                    case "=" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                        sb.Append($"{filter.FieldName}=={filter.Operator}");
                        break;
                    case "start" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                    case ">=" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                    case ">" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                        sb.Append($"{filter.FieldName}.StartWith(\"{filter.Operator}\")");
                        break;
                    case "ends" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                    case "<=" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                    case "<" when typeof(string).IsAssignableFrom(filter.Value.GetType()):
                        sb.Append($"{filter.FieldName}.EndsWith(\"{filter.Operator}\")");
                        break;
                    default:
                        sb.Append($"{filter.FieldName} {filter.Operator} \"{filter.Value}\"");
                        break;
                }

                if (i < filters.Length - 1)
                    sb.Append(" and ");
            }

            return query.Where(sb.ToString()) as IQueryable<dynamic>;
        }

    }
}