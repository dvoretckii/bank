using System.Transactions;
using Banks.Entities;
using Banks.Models;

namespace Banks.Interfaces;

public interface IBankAccount
{
    string AccountId { get; }
    Client Client { get; }
    decimal SumOfMoney { get; set; }
    int MonthsLeft { get; set; }
    int DebitComission { get; }
    int CreditComission { get; }
    IReadOnlyList<KeyValuePair<decimal, int>> DepositComissions { get; }
    decimal CreditLimit { get; }
    IReadOnlyList<ITransaction> Transactions { get; set; }
    void AddTransaction(ITransaction transaction);
    void MakeTransaction(ITransaction transaction);
    void Refil(decimal moneySum);
    void WithDraw(decimal moneySum);
    void RunTimePeriod(int months);
    void ReducingByCommissionByMonth();
    void DecreasingByDebitCommissionByMonth();
    void DecreasingByDepositCommissionByMonth();
}