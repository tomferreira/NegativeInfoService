using System;

namespace NegativeInfoService.Application.ViewModels
{
    public class NegativationViewModel
    {
        public Guid Id { get; set; }

        public int ClientId { get; set; }

        public long LegalDocument { get; set; }

        public string? BankTransitionId { get; set; }

        public DateTime DueDate { get; set; }

        public float AmountOwed { get; set; }

        public string StatusName { get; set; }
    }
}
