
namespace NegativeInfoService.Infra
{
    public struct AWSRequestMessage
    {
        public enum ActionType
        {
            Inclusion,
            Exclusion
        };

        public ActionType Action { get; set; }
        public int ClientId { get; set; }
        public long LegalDocument { get; set; }
        public string BankTransitionID { get; set; }
    }
}
