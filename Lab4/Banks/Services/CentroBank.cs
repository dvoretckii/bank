using System.Transactions;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Services;

public class CentroBank
{
    public CentroBank()
    {
        Banks = new List<Bank>().AsReadOnly();
    }

    public IReadOnlyList<Bank> Banks { get; set; }

    public Bank FindBankWithId(Guid id)
    {
        foreach (Bank myBank in Banks)
        {
            if (myBank.BankId == id.ToString())
            {
                return myBank;
            }
        }

        throw BankException.ElementNotFound();
    }

    public Bank FindBankWithAccountId(string accountId)
    {
        foreach (Bank bank in Banks)
        {
            if (accountId.StartsWith(bank.BankId))
            {
                return bank;
            }
        }

        throw BankException.ElementNotFound();
    }

    public ITransaction CreateTransaction(ITransaction? dependentTransaction, IBankAccount receiver, IBankAccount sender, decimal transactionSum, TransactionType transactionType)
    {
        var transactionId = default(Guid);
        if (transactionType == TransactionType.Cancel)
        {
            if (dependentTransaction != null) return new CancelTransaction(dependentTransaction, transactionId);
        }

        if (transactionType == TransactionType.Refil)
        {
            return new RefilTransaction(transactionId, receiver, transactionSum);
        }

        if (transactionType == TransactionType.Transfer)
        {
             return new TransferTransaction(transactionId, receiver, sender, transactionSum);
        }

        return new WithDrawTransaction(transactionId, receiver, transactionSum);
    }

    public void MakeTransaction(ITransaction transaction)
    {
        if (transaction.Receiver.AccountId != transaction.Sender.AccountId)
        {
            transaction.TransactionSum -= transaction.TransactionSum *
                FindBankWithAccountId(transaction.Receiver.AccountId).TransactionCommission / 100;
            FindBankWithAccountId(transaction.Receiver.AccountId).MakeTransaction(transaction);
        }
        else
        {
            FindBankWithAccountId(transaction.Receiver.AccountId).MakeTransaction(transaction);
        }
    }

    public void NewTransaction(ITransaction? dependentTransaction, IBankAccount receiver, IBankAccount sender, decimal transactionSum, TransactionType transactionType)
    {
        ITransaction transaction = CreateTransaction(dependentTransaction, receiver, sender, transactionSum, transactionType);
        MakeTransaction(transaction);
    }

    public void RunTimePeriod(int months)
    {
        foreach (Bank bank in Banks)
        {
            bank.RunTimePeriod(months);
        }
    }

    public void TransferMoney(IBankAccount sender, IBankAccount receiver, decimal sum)
    {
        sender.SumOfMoney -= sum;
        receiver.SumOfMoney += sum;
    }

    public Bank CreateBank(string bankName, int transactionCommission, int debitComission, IReadOnlyList<KeyValuePair<decimal, int>> depositComissions, int accountValidityPeriod, int creditComission, decimal creditLimit, decimal amountLimitingSuspiciousClient)
    {
        string bankId = default(Guid).ToString();
        var bank = new Bank(bankId, transactionCommission, debitComission, depositComissions, accountValidityPeriod, creditComission, creditLimit, bankName, amountLimitingSuspiciousClient);
        var banks = new List<Bank>(Banks);
        banks.Add(bank);
        Banks = banks.AsReadOnly();
        return bank;
    }

    public bool BankExist(string? name)
    {
        return Banks.Any(bank => bank.BankName == name);
    }

    public Bank FindBank(string? name)
    {
        foreach (Bank bank in Banks)
        {
            if (bank.BankName == name)
            {
                return bank;
            }
        }

        throw BankException.ElementNotFound();
    }
}