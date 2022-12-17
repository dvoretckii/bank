using System.Transactions;
using Banks.Interfaces;
using Banks.Services;

namespace Banks.Models;

public class RefilTransaction : ITransaction
{
    public RefilTransaction(Guid transactionId, IBankAccount user, decimal transactionSum)
    {
        StatusOfCancelling = false;
        TransactionID = transactionId;
        Receiver = user;
        Sender = user;
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
        Receiver.Refil(TransactionSum);
    }
}