using System;
using System.Linq;
using System.Threading.Tasks;
using NegativeInfoService.Application.Exceptions;
using NegativeInfoService.Application.Interfaces;
using NegativeInfoService.Application.ViewModels;
using NegativeInfoService.Domain.Entities;
using NegativeInfoService.Domain.Interfaces;

namespace NegativeInfoService.Application.Services
{
    public class NegativationService : INegativationService
    {
        private INegativationRepository _negativationRepository;
        private IBureauNotificationQueue _notificationQueue;

        public NegativationService(
            INegativationRepository negativationRepository,
            IBureauNotificationQueue notificationQueue)
        {
            _negativationRepository = negativationRepository;
            _notificationQueue = notificationQueue;
        }

        public async Task<ListNegativationViewModel> AllActiveAsync()
        {
            var negativations = await _negativationRepository.AllAsync(Negativation.StatusType.Active);

            return new ListNegativationViewModel()
            {
                List = negativations.Select(negativation => new NegativationViewModel()
                {
                    Id = negativation.Id,
                    ClientId = negativation.ClientId,
                    LegalDocument = negativation.LegalDocument,
                    BankTransitionId = negativation.BankTransitionId,
                    DueDate = negativation.DueDate,
                    AmountOwed = negativation.AmountOwed,
                    StatusName = negativation.Status.ToString()
                })
            };
        }

        public async Task<NegativationViewModel> GetAsync(Guid clientId)
        {
            var negativation = await _negativationRepository.GetAsync(clientId);

            if (negativation == null)
                return null;

            return new NegativationViewModel()
            {
                Id = negativation.Id,
                ClientId = negativation.ClientId,
                LegalDocument = negativation.LegalDocument,
                BankTransitionId = negativation.BankTransitionId,
                DueDate = negativation.DueDate,
                AmountOwed = negativation.AmountOwed,
                StatusName = negativation.Status.ToString()
            };
        }

        public NegativationViewModel Add(CreateNegativationViewModel model)
        {
            var negativation = new Negativation(
                model.ClientId.Value, model.LegalDocument.Value, 
                model.BankTransitionId, model.DueDate.Value, model.AmountOwed.Value);

            _negativationRepository.Add(negativation);
            _negativationRepository.SaveChanges();

            _notificationQueue.NotifyInclusionAsync(negativation);

            return new NegativationViewModel()
            {
                Id = negativation.Id,
                ClientId = negativation.ClientId,
                LegalDocument = negativation.LegalDocument,
                BankTransitionId = negativation.BankTransitionId,
                DueDate = negativation.DueDate,
                AmountOwed = negativation.AmountOwed,
                StatusName = negativation.Status.ToString()
            };
        }

        public async Task ResolveAsync(Guid Id)
        {
            var negativation = await _negativationRepository.GetAsync(Id);

            if (negativation == null)
                throw new EntityNotFoundException();

            negativation.Resolve();

            _negativationRepository.SaveChanges();

            _notificationQueue.NotifyExclusionAsync(negativation);
        }
    }
}
