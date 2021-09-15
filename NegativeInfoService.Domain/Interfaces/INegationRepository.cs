using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NegativeInfoService.Domain.Entities;

namespace NegativeInfoService.Domain.Interfaces
{
    public interface INegationRepository
    {
        Task<IEnumerable<Negation>> AllAsync(Negation.StatusType? status);
        Task<Negation> GetAsync(Guid Id);
        void Add(Negation negation);
        void Delete(Negation negation);

        void SaveChanges();
    }
}
