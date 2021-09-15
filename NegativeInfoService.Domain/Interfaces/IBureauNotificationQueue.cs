using NegativeInfoService.Domain.Entities;
using System.Threading.Tasks;

namespace NegativeInfoService.Domain.Interfaces
{
    public interface IBureauNotificationQueue
    {
        Task NotifyInclusionAsync(Negation negation);

        Task NotifyExclusionAsync(Negation negation);
    }
}
