using Dapper;
using STGenetics.Application.Models.Order;
using STGenetics.Application.Ports;
using STGenetics.Domain.Entities;

namespace STGenetics.Infrastructure.DataAccess
{
    public class OrderRepository : IOrderRepository
    {

        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> SaveOrder(Order order, List<int> Ids)
        {
           int OrderId = await SaveOrderIntoTableAsync(order);

            if(await SaveInAnimalOrder(OrderId,Ids)) return OrderId; 

            return 0;
        }

        public async Task<bool> InactiveOrder(int Id)
        {
            string sqlQuery = "DELETE FROM OrdenAnimal WHERE OrderId = @Id";
            string sqlQueryUpdate = "UPDATE Orden SET DiscountsApplied = 'ORDER INVALID', Total = 0 WHERE OrderId = @Id";

            using var connection = _context.CreateConnection();

            var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id });

            affectedRows = await connection.ExecuteAsync(sqlQueryUpdate, new { Id });
            return affectedRows > 0;
        }

       




        private async Task<int> SaveOrderIntoTableAsync(Order order) 
        {
            string sqlInsert = "INSERT INTO Order (ClientName, Total, PurchaseDate, DiscountsApplied, freight, Paid)";
            string sqlValues = " VALUES (@ClientName, @Total, @PurchaseDate, @DiscountsApplied, @freight, @Paid);";
            string sqlSelectId = "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var connection = _context.CreateConnection();

            string sqlQuery = sqlInsert + sqlValues + sqlSelectId;

            int id = await connection.ExecuteScalarAsync<int>(sqlQuery, order);

            return id;
        }

        private async Task<bool> SaveInAnimalOrder(int OrderId, List<int> animalIds)
        {            
            string sqlQuery = "INSERT INTO OrdenAnimal (OrdenId, AnimalId) VALUES (@OrderId, @AnimalId)";

            var parameters = animalIds.Select(animalId => new { OrderId = OrderId, AnimalId = animalId });

            using var connection = _context.CreateConnection();

            int id = await connection.ExecuteAsync(sqlQuery, parameters);

            return id > 0;
        }



    }

   
}
