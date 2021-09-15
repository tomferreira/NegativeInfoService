using NegativeInfoService.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace NegativeInfoService.Application.Interfaces
{
    public interface INegativationService
    {
        Task<ListNegativationViewModel> AllActiveAsync();

        Task<NegativationViewModel> GetAsync(Guid id);

        NegativationViewModel Add(CreateNegativationViewModel model);

        Task ResolveAsync(Guid id);
    }
}
