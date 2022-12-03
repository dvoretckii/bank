using System.Transactions;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public class CancelTransaction : ITransaction
{
    public CancelTransaction(ITransaction dependentTransaction, Guid transactionId)
    {
        StatusOfCancelling = false;
        DependentTransaction = dependentTransaction;
        TransactionID = transactionId;
        Receiver = dependentTransaction.Sender;
        Sender = dependentTransaction.Receiver;
        TransactionSum = dependentTransaction.TransactionSum;
    }

    public bool StatusOfCancelling { get; set; }
    public ITransaction? DependentTransaction { get; }
    public Guid TransactionID { get; }
    public IBankAccount Receiver { get; }
    public IBankAccount Sender { get; }
    public decimal TransactionSum { get; set; }
    public void MakeTransaction()
    {
        if (DependentTransaction!.StatusOfCancelling)
        {
            throw BankException.InvalidOperation();
        }

        Sender.SumOfMoney -= TransactionSum;
        Receiver.SumOfMoney += TransactionSum;
        DependentTransaction.StatusOfCancelling = true;
    }
}