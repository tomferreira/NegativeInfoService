using Microsoft.EntityFrameworkCore;
using NegativeInfoService.Domain.Entities;
using NegativeInfoService.Domain.Interfaces;
using NegativeInfoService.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegativeInfoService.Infra.Data.Repositories
{
    public class NegativationRepository : BaseRepository, INegativationRepository
    {
        public NegativationRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Negativation>> AllAsync(Negativation.StatusType? status)
        {
            IQueryable<Negativation> query = _context.Negativations;

            if (status != null)
                query.Where(n => n.Status == status);

            return await query.ToListAsync();
        }

        public async Task<Negativation> GetAsync(Guid Id)
        {
            return await _context.Negativations
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public void Add(Negativation negativation)
        {
            _context.Negativations.Add(negativation);
        }

        public void Delete(Negativation negativation)
        {
            _context.Negativations.Remove(negativation);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
