using System.Transactions;
using Banks.Interfaces;

namespace Banks.Models;

public class WithDrawTransaction : ITransaction
{
    public WithDrawTransaction(Guid transactionId, IBankAccount user, decimal transactionSum)
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
        Receiver.WithDraw(TransactionSum);
    }
}