using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class CreditBankAccount : IBankAccount
{
    public CreditBankAccount(string accountId, Client client, int monthsLeft, int creditComission, decimal creditLimit, decimal sumOfMoney)
    {
        AccountId = accountId;
        Client = client;
        SumOfMoney = sumOfMoney;
        MonthsLeft = monthsLeft;
        DebitComission = 0;
        CreditComission = creditComission;
        CreditLimit = creditLimit;
        Transactions = new List<ITransaction>().AsReadOnly();
        DepositComissions = new List<KeyValuePair<decimal, int>>().AsReadOnly();
    }

    public string AccountId { get; }
    public Client Client { get; }
    public decimal SumOfMoney { get; set; }
    public int MonthsLeft { get; set; }
    public int DebitComission { get; }
    public int CreditComission { get; set; }
    public IReadOnlyList<KeyValuePair<decimal, int>> DepositComissions { get; }
    public decimal CreditLimit { get; set; }
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
        if (SumOfMoney - moneySum < 0 && moneySum - SumOfMoney > CreditLimit)
        {
            throw BankException.InvalidOperation();
        }

        SumOfMoney -= moneySum;
    }

    public void RunTimePeriod(int months)
    {
        for (int i = 0; i < months; i++)
        {
            ReducingByCommissionByMonth();
            MonthsLeft--;
        }
    }

    public void ReducingByCommissionByMonth()
    {
        if (SumOfMoney - (SumOfMoney * CreditComission / 100) < 0 && (SumOfMoney * CreditComission / 100) - SumOfMoney > CreditLimit)
        {
            throw BankException.InvalidOperation();
        }

        if (SumOfMoney < 0)
        {
            SumOfMoney -= SumOfMoney * CreditComission / 100;
        }
    }

    public void DecreasingByDebitCommissionByMonth()
    {
    }

    public void DecreasingByDepositCommissionByMonth()
    {
    }
}