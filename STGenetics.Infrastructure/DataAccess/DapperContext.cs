using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Infrastructure.DataAccess
{
    public class DapperContext
    {
        private readonly string _connectionString;
        public DapperContext(string connectionString)
        {            
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
