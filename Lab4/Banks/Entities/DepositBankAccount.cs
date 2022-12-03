using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DepositBankAccount : IBankAccount
{
    public DepositBankAccount(string accountId, Client client, int monthLeft, IReadOnlyList<KeyValuePair<decimal, int>> depositComissions, decimal sumOfMoney)
    {
        AccountId = accountId;
        Client = client;
        SumOfMoney = sumOfMoney;
        DebitComission = 0;
        CreditComission = 0;
        DepositComissions = new List<KeyValuePair<decimal, int>>(depositComissions).AsReadOnly();
        CreditLimit = 0;
        Transactions = new List<ITransaction>().AsReadOnly();
        MonthsLeft = monthLeft;
    }

    public string AccountId { get; }
    public Client Client { get; }
    public decimal SumOfMoney { get; set; }
    public int MonthsLeft { get; set; }
    public int DebitComission { get; }
    public int CreditComission { get; }
    public IReadOnlyList<KeyValuePair<decimal, int>> DepositComissions { get; set; }
    public decimal CreditLimit { get; }
    public IReadOnlyList<ITransaction> Transactions { get; set; }

    public void AddTransaction(ITransaction transaction)
    {
        var transactions = new List<ITransaction>(Transactions);
        transactions.Add(transaction);
        Transactions = transactions.AsReadOnly();
    }

    public void MakeTransaction(ITransaction transaction)
    {
        transaction.MakeTransaction();
        AddTransaction(transaction);
    }

    public void Refil(decimal moneySum)
    {
        SumOfMoney += moneySum;
    }

    public void WithDraw(decimal moneySum)
    {
        if (MonthsLeft > 0)
        {
            throw new Exception();
        }

        SumOfMoney -= moneySum;
    }

    public void RunTimePeriod(int months)
    {
        for (int i = 0; i < months; i++)
        {
            DecreasingByDepositCommissionByMonth();
            MonthsLeft--;
        }
    }

    public void ReducingByCommissionByMonth()
    {
    }

    public void DecreasingByDebitCommissionByMonth()
    {
    }

    public void DecreasingByDepositCommissionByMonth()
    {
        for (int i = 0; i < DepositComissions.Count - 1; i++)
        {
            if (SumOfMoney <= DepositComissions[i].Key || SumOfMoney > DepositComissions[i + 1].Key) continue;
            SumOfMoney += SumOfMoney * DepositComissions[i].Value / 100;
            return;
        }

        SumOfMoney += SumOfMoney * DepositComissions[^1].Value / 100;
    }
}