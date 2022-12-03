using Banks.Entities;

namespace Banks.Models;

public class AccountId
{
    public AccountId(Bank bank)
    {
        Id = bank.BankId.ToString() + Guid.NewGuid().ToString();
    }

    public string Id { get; }
}