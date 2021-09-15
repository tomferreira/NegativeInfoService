using NegativeInfoService.Domain.Entities;
using NegativeInfoService.Domain.Exceptions;
using System;
using Xunit;

namespace NegativeInfoService.Domain.UnitTest
{
    public class NegativationTest
    {
        [Fact]
        public void Create_Negativation_With_Future_DueData()
        {
            Assert.Throws<BusinessRuleException>(() => 
                new Negativation(1, 12345, "ITAU-1234", DateTime.Now.AddSeconds(10), 100.0f));
        }

        [Fact]
        public void Create_Negativation_With_Negative_AmountOwed()
        {
            Assert.Throws<BusinessRuleException>(() =>
                new Negativation(1, 12345, "ITAU-1234", DateTime.Now.AddDays(-90), -1));
        }

        [Fact]
        public void Create_Negativation_With_Zero_AmountOwed()
        {
            Assert.Throws<BusinessRuleException>(() =>
                new Negativation(1, 12345, "ITAU-1234", DateTime.Now.AddDays(-90), 0));
        }

        [Fact]
        public void Create_Negativation_With_Null_BankTransitionID()
        {
            Assert.Throws<BusinessRuleException>(() =>
                new Negativation(1, 12345, null, DateTime.Now.AddDays(-90), 100.0f));
        }

        [Fact]
        public void Create_Valid_Negativation()
        {
            var validDueDate = DateTime.Parse("2021-05-17T01:23:45");

            var negativation = new Negativation(1, 12345, "ITAU-1234", validDueDate, 123.45f);

            Assert.True(negativation.ClientId == 1);
            Assert.True(negativation.LegalDocument == 12345);
            Assert.True(negativation.BankTransitionId == "ITAU-1234");
            Assert.True(negativation.DueDate == validDueDate);
            Assert.True(negativation.AmountOwed == 123.45f);
            Assert.True(negativation.Status == Negativation.StatusType.Active);
        }
    }
}
