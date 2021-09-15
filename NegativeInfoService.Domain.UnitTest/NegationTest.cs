using NegativeInfoService.Domain.Entities;
using NegativeInfoService.Domain.Exceptions;
using System;
using Xunit;

namespace NegativeInfoService.Domain.UnitTest
{
    public class NegationTest
    {
        [Fact]
        public void Create_Negation_With_Future_DueData()
        {
            Assert.Throws<BusinessRuleException>(() => 
                new Negation(1, 12345, "ITAU-1234", DateTime.Now.AddSeconds(10), 100.0f));
        }

        [Fact]
        public void Create_Negation_With_Negative_AmountOwed()
        {
            Assert.Throws<BusinessRuleException>(() =>
                new Negation(1, 12345, "ITAU-1234", DateTime.Now.AddDays(-90), -1));
        }

        [Fact]
        public void Create_Negation_With_Zero_AmountOwed()
        {
            Assert.Throws<BusinessRuleException>(() =>
                new Negation(1, 12345, "ITAU-1234", DateTime.Now.AddDays(-90), 0));
        }

        [Fact]
        public void Create_Negation_With_Null_BankTransitionID()
        {
            Assert.Throws<BusinessRuleException>(() =>
                new Negation(1, 12345, null, DateTime.Now.AddDays(-90), 100.0f));
        }

        [Fact]
        public void Create_Valid_Negation()
        {
            var validDueDate = DateTime.Parse("2021-05-17T01:23:45");

            var negation = new Negation(1, 12345, "ITAU-1234", validDueDate, 123.45f);

            Assert.True(negation.ClientId == 1);
            Assert.True(negation.LegalDocument == 12345);
            Assert.True(negation.BankTransitionId == "ITAU-1234");
            Assert.True(negation.DueDate == validDueDate);
            Assert.True(negation.AmountOwed == 123.45f);
            Assert.True(negation.Status == Negation.StatusType.Active);
        }
    }
}
