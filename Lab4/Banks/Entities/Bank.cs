using System.Reflection;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;
using Banks.Services;

namespace Banks.Entities;

public class Bank : IObservable
{
    public Bank(string bankId, int transactionCommission, int debitComission, IReadOnlyList<KeyValuePair<decimal, int>> depositComissions, int accountValidityPeriod, int creditComission, decimal creditLimit, string bankName, decimal amountLimitingSuspiciousClient)
    {
        BankId = bankId;
        Accounts = new List<IBankAccount>().AsReadOnly();
        TransactionCommission = transactionCommission;
        DebitComission = debitComission;
        Clients = new List<IObserver>().AsReadOnly();
        DepositComissions = depositComissions;
        AccountValidityPeriod = accountValidityPeriod;
        CreditComission = creditComission;
        CreditLimit = creditLimit;
        BankName = bankName;
        AmountLimitingSuspiciousClient = amountLimitingSuspiciousClient;
    }

    // убрать сетеры сделать указатель публичный ридонли на приватный
    public string BankId { get; }
    public string BankName { get; }
    public IReadOnlyList<IBankAccount> Accounts { get; private set; }
    public IReadOnlyList<IObserver> Clients { get; private set; }
    public decimal AmountLimitingSuspiciousClient { get; private set; }
    public int AccountValidityPeriod { get; }
    public int DebitComission { get;  private set; }
    public int CreditComission { get; private set; }
    public IReadOnlyList<KeyValuePair<decimal, int>> DepositComissions { get; }
    public decimal CreditLimit { get; private set; }
    public int TransactionCommission { get; private set; }

    public void RegisterObserver(IObserver observer)
    {
        var newList = new List<IObserver>(Clients);
        if (newList.Contains(observer))
        {
            throw BankException.ElementNotFound();
        }

        newList.Add(observer);
        Clients = newList.AsReadOnly();
    }

    public void RemoveObserver(IObserver observer)
    {
        var newList = new List<IObserver>(Clients);
        if (!newList.Contains(observer))
        {
            throw BankException.ElementNotFound();
        }

        newList.Remove(observer);
        Clients = newList;
    }

    public void NotifyObservers()
    {
        var info = new Info(this);
        foreach (Client observer in Clients)
        {
            observer.Update(info);
        }
    }

    public void ChangeTransactionCommission(int transactionCommission)
    {
        TransactionCommission = transactionCommission;
        NotifyObservers();
    }

    public void ChangeDebitComission(int newDebitComission)
    {
        DebitComission = newDebitComission;
        DebitBankAccount();
        NotifyObservers();
    }

    public void ChangeCreditComission(int newCreditComission)
    {
        CreditComission = newCreditComission;
        CreditBankAccount();
        NotifyObservers();
    }

    public void ChangeCreditLimit(decimal newCreditLimit)
    {
        CreditLimit = newCreditLimit;
        CreditBankAccount();
        NotifyObservers();
    }

    public void ChangeAmountLimitingSuspiciousClient(decimal newAmountLimitingSuspiciousClient)
    {
        AmountLimitingSuspiciousClient = newAmountLimitingSuspiciousClient;
        NotifyObservers();
    }

    public void DebitBankAccount()
    {
        var debits = Accounts.OfType<DebitBankAccount>();
        foreach (DebitBankAccount debit in debits)
        {
            debit.DebitComission = DebitComission;
        }
    }

    public void CreditBankAccount()
    {
        var debits = Accounts.OfType<CreditBankAccount>();
        foreach (CreditBankAccount debit in debits)
        {
            debit.CreditComission = CreditComission;
            debit.CreditLimit = CreditLimit;
        }
    }

    public void DepositBankAccount()
    {
        var debits = Accounts.OfType<DepositBankAccount>();
        foreach (DepositBankAccount debit in debits)
        {
            debit.DepositComissions = DepositComissions;
        }
    }

    public void AddAccount(IBankAccount account)
    {
        var accounts = new List<IBankAccount>(Accounts);
        accounts.Add(account);
        Accounts = accounts.AsReadOnly();
    }

    public void OpenBankAccount(Client client, decimal sumOfMoney, BankAccountType bankAccountType)
    {
        if (!Clients.Contains(client))
        {
            RegisterObserver(client);
        }

        CreatorBankAccount bankAccountCreator = new CreditBankAccountCreator();
        if (bankAccountType == BankAccountType.Debit)
        {
            bankAccountCreator = new DebitBankAccountCreator();
        }
        else if (bankAccountType == BankAccountType.Deposit)
        {
            bankAccountCreator = new DepositBankAccountCreator();
        }

        AddAccount(bankAccountCreator.FactoryMethod(this, client, sumOfMoney));
    }

    public void MakeTransaction(ITransaction transaction)
    {
        if (transaction.Receiver.AccountId == transaction.Sender.AccountId)
        {
            transaction.Receiver.MakeTransaction(transaction);
        }
    }

    public void RunTimePeriod(int months)
    {
        foreach (IBankAccount bankAccount in Accounts)
        {
            bankAccount.RunTimePeriod(months);
        }
    }
}