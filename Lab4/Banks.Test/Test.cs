using System.Collections.ObjectModel;
using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class Test
{
    [Fact]
    public void CanCreateAndAddBank()
    {
        var centralBank = new CentroBank();
        centralBank.CreateBank("SberBank", 1, 10, new List<KeyValuePair<decimal, int>>(), 5, 1, 50000, 5);

        Assert.Equal(1, centralBank.Banks.Count);
    }

    [Fact]
    public void CanCreateClientAndAddHimCreditAccount()
    {
        var centralBank = new CentroBank();
        var sberBank = centralBank.CreateBank("SberBank", 1, 10, new List<KeyValuePair<decimal, int>>(), 5, 1, 50000, 5);
        var client = Client.Builder
            .WithName("qwer")
            .WithSurname("ty")
            .WithAddress("ui")
            .Build();

        Assert.True(client.SuspicionOfAttacker());
        client.PassportNumber = 1234567890;

        Assert.False(client.SuspicionOfAttacker());

        sberBank.OpenBankAccount(client, 100, BankAccountType.Credit);

        if (client.Accounts != null) Assert.Equal(1, client.Accounts[0].CreditComission);
    }

    [Fact]
    public void CanCreateClientAndAddHimDebitAccount()
    {
        var centralBank = new CentroBank();
        var sberBank = centralBank.CreateBank("SberBank", 1, 10, new List<KeyValuePair<decimal, int>>(), 5, 1, 50000, 5);
        var client = Client.Builder
            .WithName("qwer")
            .WithSurname("ty")
            .WithAddress("ui")
            .Build();

        Assert.True(client.SuspicionOfAttacker());
        client.PassportNumber = 1234567890;

        Assert.False(client.SuspicionOfAttacker());

        sberBank.OpenBankAccount(client, 100, BankAccountType.Debit);

        if (client.Accounts != null) Assert.Equal(10, client.Accounts[0].DebitComission);
    }
}