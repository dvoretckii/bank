using System.Transactions;
using Banks.Services;

namespace Banks.Interfaces;

public interface ITransaction
{
    bool StatusOfCancelling { get; set; }
    ITransaction? DependentTransaction { get; }
    Guid TransactionID { get; }
    IBankAccount Receiver { get; }
    IBankAccount Sender { get; }
    decimal TransactionSum { get; set; }
    void MakeTransaction();
}