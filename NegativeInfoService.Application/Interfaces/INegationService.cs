using NegativeInfoService.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace NegativeInfoService.Application.Interfaces
{
    public interface INegationService
    {
        Task<ListNegationViewModel> AllActiveAsync();

        Task<NegationViewModel> GetAsync(Guid id);

        NegationViewModel Add(CreateNegationViewModel model);

        Task ResolveAsync(Guid id);
    }
}
