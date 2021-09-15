using NegativeInfoService.Infra.Data.Context;

namespace NegativeInfoService.Infra.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context) => _context = context;
    }
}
