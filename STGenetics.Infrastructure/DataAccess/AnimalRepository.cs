using Dapper;
using STGenetics.Application.Ports;
using STGenetics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<List<int>> CheckAnimalAvalaibilityAsync(List<int> Ids, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAnimalAsync(int Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Animal>> FilterAnimalAsync(Animal animal, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Animal>> Getand()
        {
            using var connection = _context.CreateConnection();

            var h = await connection.QueryAsync<Animal>("SELECT TOP(5) * FROM ANIMAL");

            return h.ToList();
        }

        public Task<bool> UpdateAnimalAsync(int Id, Animal animal, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAnimalsStateAsync(List<int> Ids, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
