using SyberGate.RMACT.EntityFrameworkCore;

namespace SyberGate.RMACT.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly RMACTDbContext _context;

        public InitialHostDbBuilder(RMACTDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
