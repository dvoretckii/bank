using System.Transactions;
using Banks.Interfaces;

namespace Banks.Models;

public class TransferTransaction : ITransaction
{
    public TransferTransaction(Guid transactionId, IBankAccount receiver, IBankAccount sender, decimal transactionSum)
    {
        StatusOfCancelling = false;
        TransactionID = transactionId;
        Receiver = receiver;
        Sender = sender;
        TransactionSum = transactionSum;
        DependentTransaction = null;
    }

    public bool StatusOfCancelling { get; set; }
    public ITransaction? DependentTransaction { get; }
    public Guid TransactionID { get; }
    public IBankAccount Receiver { get; }
    public IBankAccount Sender { get; }
    public decimal TransactionSum { get; set; }
    public void MakeTransaction()
    {
        Sender.SumOfMoney -= TransactionSum;
        Receiver.SumOfMoney += TransactionSum;
    }
}