using NegativeInfoService.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace NegativeInfoService.Application.Interfaces
{
    public interface INegationService
    {
        Task<ListNegationViewModel> AllActiveAsync();

        Task<NegationViewModel> GetAsync(Guid id);

        Task<NegationViewModel> AddAsync(CreateNegationViewModel model);

        Task ResolveAsync(Guid id);
    }
}
