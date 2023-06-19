using Dapper;
using Microsoft.IdentityModel.Tokens;
using STGenetics.Application.Models.Animal;
using STGenetics.Application.Ports;
using STGenetics.Domain.Entities;
using STGenetics.Infrastructure.DataAccess.QueryBuilders;
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

        public async Task<int> AddAnimalAsync(Animal animal )
        {
            string sqlInsert = "INSERT INTO Animal ([Name], Breed, BirthDate, Sex, Price, [Status])";
            string sqlValues = " VALUES (@Name, @Breed, @BirthDate, @Sex, @Price, @Status);";
            string sqlSelectId = "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var connection = _context.CreateConnection();

            string sqlQuery = sqlInsert + sqlValues + sqlSelectId;

            int id = await connection.ExecuteScalarAsync<int>(sqlQuery, animal);

            return id;
        }


        public async Task<List<int>> CheckAnimalAvalaibilityAsync(List<int> Ids)
        {
            string sqlQuery = "SELECT AnimalId FROM Animal WHERE AnimalId IN @Ids";

            using var connection = _context.CreateConnection();

            var animals = await connection.QueryAsync<int>(sqlQuery, Ids);

            return animals.ToList();
        }

        public async Task<bool> DeleteAnimalAsync(int Id)
        {
            string sqlQuery = "DELETE FROM Animal WHERE AnimalId = @Id";

            using var connection = _context.CreateConnection();

            var affectedRows = await connection.ExecuteAsync(sqlQuery, Id);

            return affectedRows > 0 ;             
            
        }

        public async Task<List<Animal>> FilterAnimalAsync(AnimalFilterDto filter)
        {
            var filterQuery = QueryFilterBuilder.FilterBuilder(filter);

            using var connection = _context.CreateConnection();

            var animals = await connection.QueryAsync<Animal>(filterQuery.Item1, filterQuery.Item2);

            if (animals.Any()) return animals.ToList();

            return default!;
        }


        public async Task<bool> UpdateAnimalAsync(int Id, AnimalUpdate updatedAnimal)
        {
            var updateFields = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(updatedAnimal.Name))
            {
                updateFields.Add("Name = @Name");
                parameters.Add("@Name", updatedAnimal.Name);
            }

            if (!string.IsNullOrEmpty(updatedAnimal.Breed))
            {
                updateFields.Add("Breed = @Breed");
                parameters.Add("@Breed", updatedAnimal.Breed);
            }

            if (updatedAnimal.BirthDate != default)
            {
                updateFields.Add("BirthDate = @BirthDate");
                parameters.Add("@BirthDate", updatedAnimal.BirthDate);
            }

            if (!string.IsNullOrEmpty(updatedAnimal.Sex))
            {
                updateFields.Add("Sex = @Sex");
                parameters.Add("@Sex", updatedAnimal.Sex);
            }

            if (updatedAnimal.Price != default)
            {
                updateFields.Add("Price = @Price");
                parameters.Add("@Price", updatedAnimal.Price);
            }

            if (updatedAnimal.Status is not null)
            {
                updateFields.Add("Status = @Status");
                parameters.Add("@Status", updatedAnimal.Status);
            }

            
            var updateQuery = $"UPDATE Animal SET {string.Join(", ", updateFields)} WHERE AnimalId = @AnimalId";

            parameters.Add("@AnimalId", Id);


            using var connection = _context.CreateConnection();

            var affectedRows = await connection.ExecuteAsync(updateQuery, parameters);

            return affectedRows > 0;
        }

        public async Task<bool> UpdateAnimalsStateAsync(List<int> Ids, bool status)
        {
            string sqlQuery = "UPDATE Animal SET Status = @Status WHERE AnimalId IN @Ids";

            var parameters = new DynamicParameters();

            parameters.Add("@Status", status);
            parameters.Add("@Ids", Ids);       


            using var connection = _context.CreateConnection();

            var affectedRows = await connection.ExecuteAsync(sqlQuery, parameters);

            return affectedRows > 0 ;

         
        }


        public async Task<int> GetAnimalsQuantity()
        {
            using var connection = _context.CreateConnection();

            var animalCount = await connection.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM ANIMAL");

            return animalCount;
        }


      
    }

   
}
