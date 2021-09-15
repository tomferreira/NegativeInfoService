using System;
using System.ComponentModel.DataAnnotations;

namespace NegativeInfoService.Application.ViewModels
{
    public class CreateNegativationViewModel
    {
        [Required(ErrorMessage = "Client ID is required")]
        public int? ClientId { get; set; }

        [Required(ErrorMessage = "Legal document is required")]
        public long? LegalDocument { get; set; }

        [Required(ErrorMessage = "Bank transition ID is required")]
        public string? BankTransitionId { get; set; }

        [Required(ErrorMessage = "Due date is required")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Amount owed is required")]
        public float? AmountOwed { get; set; }
    }
}
