using Dapper;
using STGenetics.Application.Models.Animal;

namespace STGenetics.Infrastructure.DataAccess.QueryBuilders
{
    internal class QueryFilterBuilder
    {

        public static (string, DynamicParameters) FilterBuilder(AnimalFilterDto filter)
        {
            string sql = "SELECT * FROM Animal WHERE 1=1";
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(filter.Sex))
            {
                sql += " AND Sex = @Sex";
                parameters.Add("@Sex", filter.Sex);
            }

            if (filter.AnimalId > 0)
            {
                sql += " AND AnimalId = @AnimalId";
                parameters.Add("@AnimalId", filter.AnimalId.Value);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql += " AND Name = @Name";
                parameters.Add("@Name", filter.Name);
            }

            if (filter.Status.HasValue)
            {
                sql += " AND Status = @Status";
                parameters.Add("@Status", filter.Status.Value);
            }

            sql += " ORDER BY AnimalId";

            sql += " OFFSET @Page ROWS FETCH NEXT @PageSize ROWS ONLY";

            parameters.Add("@Page", (filter.Page - 1) * filter.PageSize);

            parameters.Add("@PageSize", filter.PageSize);

            return (sql, parameters);
        }

    }


}
