using NegativeInfoService.Domain.Entities;
using System.Threading.Tasks;

namespace NegativeInfoService.Domain.Interfaces
{
    public interface IBureauNotificationQueue
    {
        Task NotifyInclusionAsync(Negativation negativation);

        Task NotifyExclusionAsync(Negativation negativation);
    }
}
