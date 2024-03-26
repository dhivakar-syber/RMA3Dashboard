using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SyberGate.RMACT.EntityFrameworkCore
{
    public static class RMACTDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<RMACTDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<RMACTDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}