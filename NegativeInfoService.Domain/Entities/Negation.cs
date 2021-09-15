using NegativeInfoService.Domain.Exceptions;
using System;

namespace NegativeInfoService.Domain.Entities
{
    public class Negation
    {
        public enum StatusType
        {
            Active,
            Resolved
        };

        public Guid Id { get; private set; }

        public int ClientId { get; private set; }

        public long LegalDocument { get; private set; }

        public string BankTransitionId { get; private set; }

        public DateTime DueDate { get; private set; }

        public float AmountOwed { get; private set; }

        public StatusType Status { get; private set; }

        public Negation(int clientId, long legalDocument, string bankTransitionId, 
            DateTime dueDate, float amountOwed)
        {
            ClientId = clientId;
            LegalDocument = legalDocument;
            BankTransitionId = bankTransitionId;
            DueDate = dueDate;
            AmountOwed = amountOwed;

            if (string.IsNullOrWhiteSpace(bankTransitionId))
                throw new BusinessRuleException("BankTransitionId", "Bank transition ID is invalid");

            if (DueDate > DateTime.Now)
                throw new BusinessRuleException("DueDate", "Due date is invalid");

            if (amountOwed <= 0.0f)
                throw new BusinessRuleException("AmountOwed", "AmountOwed is zero or negative");

            Status = StatusType.Active;
        }

        public void Resolve()
        {
            if (Status == StatusType.Resolved)
                throw new BusinessRuleException("Status", "Status already resolved");

            Status = StatusType.Resolved;
        }
    }
}
