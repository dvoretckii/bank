using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DebitBankAccount : IBankAccount
{
    public DebitBankAccount(string accountId, Client client, int monthsLeft, int debitComission, decimal sumOfMoney)
    {
        AccountId = accountId;
        Client = client;
        SumOfMoney = sumOfMoney;
        MonthsLeft = monthsLeft;
        DebitComission = debitComission;
        CreditLimit = 0;
        Transactions = new List<ITransaction>().AsReadOnly();
        CreditComission = 0;
        DepositComissions = new List<KeyValuePair<decimal, int>>().AsReadOnly();
    }

    public string AccountId { get; }
    public Client Client { get; }
    public decimal SumOfMoney { get; set; }
    public int MonthsLeft { get; set; }
    public int DebitComission { get; set; }
    public int CreditComission { get; }
    public IReadOnlyList<KeyValuePair<decimal, int>> DepositComissions { get; }
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
        if (moneySum > SumOfMoney)
        {
            throw BankException.InvalidOperation();
        }

        SumOfMoney -= moneySum;
    }

    public void RunTimePeriod(int months)
    {
        for (int i = 0; i < months; i++)
        {
            DecreasingByDebitCommissionByMonth();
            MonthsLeft--;
        }
    }

    public void ReducingByCommissionByMonth()
    {
    }

    public void DecreasingByDebitCommissionByMonth()
    {
        SumOfMoney += SumOfMoney * DebitComission / 100;
    }

    public void DecreasingByDepositCommissionByMonth()
    {
    }
}