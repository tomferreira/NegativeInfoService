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
    public class NegationService : INegationService
    {
        private INegationRepository _negationRepository;
        private IBureauNotificationQueue _notificationQueue;

        public NegationService(
            INegationRepository negationRepository,
            IBureauNotificationQueue notificationQueue)
        {
            _negationRepository = negationRepository;
            _notificationQueue = notificationQueue;
        }

        public async Task<ListNegationViewModel> AllActiveAsync()
        {
            var negations = await _negationRepository.AllAsync(Negation.StatusType.Active);

            return new ListNegationViewModel()
            {
                List = negations.Select(negation => new NegationViewModel()
                {
                    Id = negation.Id,
                    ClientId = negation.ClientId,
                    LegalDocument = negation.LegalDocument,
                    BankTransitionId = negation.BankTransitionId,
                    DueDate = negation.DueDate,
                    AmountOwed = negation.AmountOwed,
                    StatusName = negation.Status.ToString()
                })
            };
        }

        public async Task<NegationViewModel> GetAsync(Guid clientId)
        {
            var negation = await _negationRepository.GetAsync(clientId);

            if (negation == null)
                return null;

            return new NegationViewModel()
            {
                Id = negation.Id,
                ClientId = negation.ClientId,
                LegalDocument = negation.LegalDocument,
                BankTransitionId = negation.BankTransitionId,
                DueDate = negation.DueDate,
                AmountOwed = negation.AmountOwed,
                StatusName = negation.Status.ToString()
            };
        }

        public NegationViewModel Add(CreateNegationViewModel model)
        {
            var negation = new Negation(
                model.ClientId.Value, model.LegalDocument.Value, 
                model.BankTransitionId, model.DueDate.Value, model.AmountOwed.Value);

            _negationRepository.Add(negation);
            _negationRepository.SaveChanges();

            _notificationQueue.NotifyInclusionAsync(negation);

            return new NegationViewModel()
            {
                Id = negation.Id,
                ClientId = negation.ClientId,
                LegalDocument = negation.LegalDocument,
                BankTransitionId = negation.BankTransitionId,
                DueDate = negation.DueDate,
                AmountOwed = negation.AmountOwed,
                StatusName = negation.Status.ToString()
            };
        }

        public async Task ResolveAsync(Guid Id)
        {
            var negation = await _negationRepository.GetAsync(Id);

            if (negation == null)
                throw new EntityNotFoundException();

            negation.Resolve();

            _negationRepository.SaveChanges();

            _notificationQueue.NotifyExclusionAsync(negation);
        }
    }
}
