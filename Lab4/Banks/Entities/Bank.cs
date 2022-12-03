using System.Reflection;
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
        InfoBank = new Info(this);
        AmountLimitingSuspiciousClient = amountLimitingSuspiciousClient;
    }

    public string BankId { get; }
    public string BankName { get; }
    public IReadOnlyList<IBankAccount> Accounts { get; set; }
    public IReadOnlyList<IObserver> Clients { get; set; }
    public decimal AmountLimitingSuspiciousClient { get; private set; }
    public int AccountValidityPeriod { get; }
    public int DebitComission { get; set; }
    public int CreditComission { get; set; }
    public IReadOnlyList<KeyValuePair<decimal, int>> DepositComissions { get; set; }
    public decimal CreditLimit { get; set; }
    public int TransactionCommission { get; set; }

    public Info InfoBank { get; private set; }

    public void UpdateInfo()
    {
        var newInfo = new Info(this);
    }

    public void RegisterObserver(IObserver observer)
    {
        var newList = new List<IObserver>(Clients);
        if (newList.Contains(observer))
        {
            throw new Exception();
        }

        newList.Add(observer);
        Clients = newList.AsReadOnly();
    }

    public void RemoveObserver(IObserver observer)
    {
        var newList = new List<IObserver>(Clients);
        if (!newList.Contains(observer))
        {
            throw new Exception();
        }

        newList.Remove(observer);
        Clients = newList;
    }

    public void NotifyObservers()
    {
        Info oldInfo = new Info(InfoBank);
        UpdateInfo();
        foreach (Client observer in Clients)
        {
            if (observer.Accounts != null && oldInfo.DebitComissionInfo.ToString() != InfoBank.DebitComissionInfo.ToString() && observer.Accounts.OfType<DebitBankAccount>().Any())
            {
                observer.Update(InfoBank.SendMessage());
            }
            else if (observer.Accounts != null && oldInfo.CreditComissionInfo.ToString() != InfoBank.CreditComissionInfo.ToString() && oldInfo.CreditLimitInfo != InfoBank.CreditLimitInfo && observer.Accounts.OfType<CreditBankAccount>().Any())
            {
                observer.Update(InfoBank.SendMessage());
            }
            else if (observer.Accounts != null && oldInfo.DepositComissionsInfo != InfoBank.DepositComissionsInfo && observer.Accounts.OfType<DepositBankAccount>().Any())
            {
                observer.Update(InfoBank.SendMessage());
            }
            else
            {
                observer.Update(InfoBank.SendMessage());
            }
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

    public void ChangeDepositComissions(IReadOnlyList<KeyValuePair<decimal, int>> newDepositComissions)
    {
        DepositComissions = newDepositComissions;
        DepositBankAccount();
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

    public IBankAccount? FindBankAccount(string accountId)
    {
        foreach (IBankAccount bankAccount in Accounts)
        {
            if (bankAccount.AccountId == accountId)
            {
                return bankAccount;
            }
        }

        return null;
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

        public Info(Info info)
        {
            Bank = info.Bank;
            CommissionToTransferInfo = info.CommissionToTransferInfo;
            DebitComissionInfo = info.DebitComissionInfo;
            CreditComissionInfo = info.CreditComissionInfo;
            CreditLimitInfo = info.CreditLimitInfo;
            DepositComissionsInfo = info.DepositComissionsInfo;
            AmountLimitingSuspiciousClientInfo = info.AmountLimitingSuspiciousClientInfo;
        }

        public Bank Bank { get; }
        public float CommissionToTransferInfo { get; private set; }
        public float DebitComissionInfo { get; private set; }
        public float CreditComissionInfo { get; private set; }
        public decimal CreditLimitInfo { get; private set; }
        public IReadOnlyList<KeyValuePair<decimal, int>> DepositComissionsInfo { get; private set; }
        public decimal AmountLimitingSuspiciousClientInfo { get; private set; }

        public string InfoToString()
        {
            return $"CommissionToTransferInfo - {CommissionToTransferInfo}\nDebitComissionInfo - {DebitComissionInfo}\nCreditComissionInfo - {CreditComissionInfo}\nCreditLimitInfo - {CreditLimitInfo}\nDepositComissionsInfo - {DepositComissionsInfo}\nAmountLimitingSuspiciousClientInfo - {AmountLimitingSuspiciousClientInfo}";
        }

        public string SendMessage()
        {
            return
                $"The bank's {Bank.BankName} ({Bank.BankId}) policy has changed. Check out the new terms of use of accounts:\n{InfoToString()}";
        }
    }
    }