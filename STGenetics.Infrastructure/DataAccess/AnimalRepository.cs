using Dapper;
using STGenetics.Application.Models.Animal;
using STGenetics.Application.Ports;
using STGenetics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace STGenetics.Infrastructure.DataAccess
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly DapperContext _context;

        public AnimalRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddAnimalAsync(Animal animal, CancellationToken cancellationToken)
        {
            string sqlInsert = "INSERT INTO Animal ([Name], Breed, BirthDate, Sex, Price, [Status])";
            string sqlValues = " VALUES (@Name, @Breed, @BirthDate, @Sex, @Price, @Status);";
            string sqlSelectId = "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var connection = _context.CreateConnection();

            string sqlQuery = sqlInsert + sqlValues + sqlSelectId;

            int id = await connection.ExecuteScalarAsync<int>(sqlQuery, animal);

            return id;
        }


        public async Task<List<int>> CheckAnimalAvalaibilityAsync(List<int> Ids, CancellationToken cancellationToken)
        {
            string sqlQuery = "SELECT AnimalId FROM Animal WHERE AnimalId IN @Ids";

            using var connection = _context.CreateConnection();

            var animals = await connection.QueryAsync<int>(sqlQuery, Ids);

            return animals.ToList();
        }

        public async Task<bool> DeleteAnimalAsync(int Id, CancellationToken cancellationToken)
        {
            string sqlQuery = "DELETE FROM Animal WHERE AnimalId = @Id";

            using var connection = _context.CreateConnection();

            var animals = await connection.ExecuteAsync(sqlQuery, Id);

            if (animals > 0) return true; 
            
            return false;
        }

        public async Task<List<Animal>> FilterAnimalAsync(AnimalFilterDto filter, CancellationToken cancellationToken)
        {
            var filterQuery = QueryFilterBuilder.FilterBuilder(filter);

            using var connection = _context.CreateConnection();

            var animals = await connection.QueryAsync<Animal>(filterQuery.Item1, filterQuery.Item2);

            if (animals.Any()) return animals.ToList();

            return default!;
        }


        public Task<bool> UpdateAnimalAsync(int Id, Animal animal, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAnimalsStateAsync(List<int> Ids, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }








        //DELETE THIS AT THE END


        public async Task<List<Animal>> Getand()
        {
            using var connection = _context.CreateConnection();

            var h = await connection.QueryAsync<Animal>("SELECT TOP(5) * FROM ANIMAL");

            return h.ToList();
        }

        public async Task<int> GetAnimalsQuantity(CancellationToken cancellationToken)
        {
            using var connection = _context.CreateConnection();

            var animalCount = await connection.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM ANIMAL");

            return animalCount;
        }
    }
    
    internal class QueryFilterBuilder {

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
            parameters.Add("@Page", (filter.Page-1) * filter.PageSize);
            parameters.Add("@PageSize", filter.PageSize );
            

            return (sql, parameters);
        } 
    
    }

   
}
