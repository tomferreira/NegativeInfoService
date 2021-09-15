using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NegativeInfoService.Domain.Entities;

namespace NegativeInfoService.Domain.Interfaces
{
    public interface INegativationRepository
    {
        Task<IEnumerable<Negativation>> AllAsync(Negativation.StatusType? status);
        Task<Negativation> GetAsync(Guid Id);
        void Add(Negativation negativation);
        void Delete(Negativation negativation);

        void SaveChanges();
    }
}
