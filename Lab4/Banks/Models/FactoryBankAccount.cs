using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;
internal abstract class CreatorBankAccount
{
    public abstract IBankAccount FactoryMethod(Bank bank, Client client, decimal sumOfMoney);
}

internal class CreditBankAccountCreator : CreatorBankAccount
{
    public override IBankAccount FactoryMethod(Bank bank, Client client, decimal sumOfMoney)
    {
        string accountId = new AccountId(bank).Id.ToString() !;
        int monthsLeft = bank.AccountValidityPeriod;
        int creditComission = bank.CreditComission;
        decimal creditLimit = creditComission;
        var account = new CreditBankAccount(accountId, client, monthsLeft, creditComission, creditLimit, sumOfMoney);
        client.AddAccount(account);
        return account;
    }
}

internal class DebitBankAccountCreator : CreatorBankAccount
{
    public override IBankAccount FactoryMethod(Bank bank, Client client, decimal sumOfMoney)
    {
        string accountId = new AccountId(bank).Id.ToString() ?? throw new InvalidOperationException();
        int monthsLeft = bank.AccountValidityPeriod;
        int debitComission = bank.DebitComission;
        var account = new DebitBankAccount(accountId, client, monthsLeft, debitComission, sumOfMoney);
        client.AddAccount(account);
        return account;
    }
}

internal class DepositBankAccountCreator : CreatorBankAccount
{
    public override IBankAccount FactoryMethod(Bank bank, Client client, decimal sumOfMoney)
    {
        string accountId = new AccountId(bank).Id.ToString() !;
        int monthsLeft = bank.AccountValidityPeriod;
        IReadOnlyList<KeyValuePair<decimal, int>> depositComissions = new List<KeyValuePair<decimal, int>>(bank.DepositComissions).AsReadOnly();
        var account = new DepositBankAccount(accountId, client, monthsLeft, depositComissions, sumOfMoney);
        client.AddAccount(account);
        return account;
    }
}