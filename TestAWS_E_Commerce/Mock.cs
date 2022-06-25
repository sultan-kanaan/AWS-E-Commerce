using AWS_E_Commerce.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAWS_E_Commerce
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AWSDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AWSDbContext(
                new DbContextOptionsBuilder<AWSDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}
