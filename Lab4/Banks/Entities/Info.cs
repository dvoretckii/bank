namespace Banks.Entities;

public class Info
    {
        public Info(Bank bank)
        {
            Bank = bank;
            CommissionToTransferInfo = bank.TransactionCommission;
            DebitComissionInfo = bank.DebitComission;
            CreditComissionInfo = bank.CreditComission;
            CreditLimitInfo = bank.CreditLimit;
            DepositComissionsInfo = bank.DepositComissions;
            AmountLimitingSuspiciousClientInfo = bank.AmountLimitingSuspiciousClient;
        }

        public Bank Bank { get; }
        public float CommissionToTransferInfo { get; }
        public float DebitComissionInfo { get; }
        public float CreditComissionInfo { get; }
        public decimal CreditLimitInfo { get; }
        public IReadOnlyList<KeyValuePair<decimal, int>> DepositComissionsInfo { get; }
        public decimal AmountLimitingSuspiciousClientInfo { get; }
    }