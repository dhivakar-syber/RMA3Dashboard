using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SyberGate.RMACT.Configuration;
using SyberGate.RMACT.Web;

namespace SyberGate.RMACT.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class RMACTDbContextFactory : IDesignTimeDbContextFactory<RMACTDbContext>
    {
        public RMACTDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RMACTDbContext>();
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSecrets: true
            );

            RMACTDbContextConfigurer.Configure(builder, configuration.GetConnectionString(RMACTConsts.ConnectionStringName));

            return new RMACTDbContext(builder.Options);
        }
    }
}