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
    public class NegationRepository : BaseRepository, INegationRepository
    {
        public NegationRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Negation>> AllAsync(Negation.StatusType? status)
        {
            IQueryable<Negation> query = _context.Negations;

            if (status != null)
                query.Where(n => n.Status == status);

            return await query.ToListAsync();
        }

        public async Task<Negation> GetAsync(Guid Id)
        {
            return await _context.Negations
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public void Add(Negation negation)
        {
            _context.Negations.Add(negation);
        }

        public void Delete(Negation negation)
        {
            _context.Negations.Remove(negation);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
