using System.Diagnostics;
using Banks.Entities;
using Banks.Models;
using Banks.Services;
namespace Banks.Console;
public class ConsoleProgram
{
    public static void Main(string[] args)
    {
        var centralBank = new CentroBank();
        var clients = new List<Client>();
        string? key = " ";
        while (key != "8")
        {
            System.Console.WriteLine(
                "What do you want to do?\n1 - Create bank\n2 - Create client\n3 - Add information about existing client");
            System.Console.WriteLine(
                "4 - Add account to client\n5 - Put money\n6 - Withdraw money\n7 - Transfer money\n8 - Exit");
            key = System.Console.ReadLine();
            switch (key)
            {
                case "1":
                    System.Console.WriteLine(
                        "Input: name, commission to transfer, commission for credit account, limit for credit account, commission for debit account, limit for suspicious, account validity period");
                    string? name = System.Console.ReadLine();
                    int transactionComission = Convert.ToInt32(System.Console.ReadLine());
                    int debitComission = Convert.ToInt32(System.Console.ReadLine());
                    int creditComission = Convert.ToInt32(System.Console.ReadLine());
                    decimal creditLimit = Convert.ToDecimal(System.Console.ReadLine());
                    decimal amountLimitingSuspiciousClient = Convert.ToDecimal(System.Console.ReadLine());
                    int accountValidityPeriod = Convert.ToInt32(System.Console.ReadLine());
                    System.Console.WriteLine("Input: number of control points in debit commission");
                    int controlPointsNumber = Convert.ToInt32(System.Console.ReadLine());
                    var depositComissions = new List<KeyValuePair<decimal, int>>();
                    for (int i = 0; i < controlPointsNumber; i++)
                    {
                        System.Console.WriteLine("New control point");
                        System.Console.WriteLine("Input: control point value");
                        decimal controlPointValue = Convert.ToDecimal(System.Console.ReadLine());
                        System.Console.WriteLine("Input: commission value");
                        int commissionValue = Convert.ToInt32(System.Console.ReadLine());
                        var pair = new KeyValuePair<decimal, int>(controlPointValue, commissionValue);
                        depositComissions.Add(pair);
                    }

                    if (name != null)
                    {
                        Bank newBank = centralBank.CreateBank(name, transactionComission, debitComission, depositComissions, accountValidityPeriod, creditComission, creditLimit, amountLimitingSuspiciousClient);
                        System.Console.WriteLine(newBank.InfoBank.SendMessage());
                    }

                    break;
                case "2":
                    System.Console.WriteLine("Input name, sername, address, passport");
                    name = System.Console.ReadLine();
                    string? sername = System.Console.ReadLine();
                    string? address = null;
                    int passport = 0;
                    System.Console.WriteLine("Do you want to input address? Y/N");
                    string? flag = System.Console.ReadLine();
                    if (flag == "Y")
                    {
                        address = System.Console.ReadLine();
                    }

                    System.Console.WriteLine("Do you want to input passport? Y/N");
                    flag = System.Console.ReadLine();
                    if (flag == "Y")
                    {
                        passport = Convert.ToInt32(System.Console.ReadLine());
                    }

                    if (name != null && sername != null)
                    {
                        var client = Client.Builder
                            .WithName(name)
                            .WithSurname(sername)
                            .WithAddress(address)
                            .WithPassportNumber(passport)
                            .Build();
                        clients.Add(client);
                    }

                    break;
                case "3":
                    System.Console.WriteLine("Input name and sername client");
                    name = System.Console.ReadLine();
                    sername = System.Console.ReadLine();
                    foreach (Client client in clients)
                    {
                        if (client.Name == name && client.Surname == sername)
                        {
                            System.Console.WriteLine("Do you want to input address? Y/N");
                            flag = System.Console.ReadLine();
                            if (flag == "Y")
                            {
                                address = System.Console.ReadLine();
                                client.Address = address;
                            }

                            System.Console.WriteLine("Do you want to input passport? Y/N");
                            flag = System.Console.ReadLine();
                            if (flag == "Y")
                            {
                                passport = Convert.ToInt32(System.Console.ReadLine());
                                client.PassportNumber = passport;
                            }

                            System.Console.WriteLine("Client's data successfully update");
                            break;
                        }
                    }

                    break;
                case "4":
                    if (centralBank.Banks.Count == 0)
                    {
                        System.Console.WriteLine("There is no bank, you can't do it");
                    }
                    else
                    {
                        System.Console.WriteLine("Input bank's name");
                        string? nameBank = System.Console.ReadLine();
                        if (!centralBank.BankExist(nameBank))
                        {
                            System.Console.WriteLine("No such bank");
                        }
                        else
                        {
                            System.Console.WriteLine("Input name and sername client");
                            name = System.Console.ReadLine();
                            sername = System.Console.ReadLine();
                            bool flagB = false;
                            foreach (Client client in clients)
                            {
                                if (client.Name == name && client.Surname == sername)
                                {
                                    Bank bank = centralBank.FindBank(nameBank);
                                    System.Console.WriteLine("What type of bank? Credit/Debit/Deposit");
                                    string type = System.Console.ReadLine() ?? throw new InvalidOperationException();
                                    BankAccountType accountType = BankAccountType.Credit;
                                    if (type == "Credit")
                                    {
                                        accountType = BankAccountType.Credit;
                                    }
                                    else if (type == "Debit")
                                    {
                                        accountType = BankAccountType.Debit;
                                    }
                                    else if (type == "Deposit")
                                    {
                                        accountType = BankAccountType.Deposit;
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }

                                    System.Console.WriteLine("What sum of account?");
                                    int sum = Convert.ToInt32(System.Console.ReadLine());
                                    bank.OpenBankAccount(client, sum, accountType);
                                    System.Console.WriteLine("Client's account successfully create");
                                    if (client.Accounts != null) System.Console.WriteLine(client.Accounts[0].AccountId);
                                    flagB = true;
                                    break;
                                }
                            }

                            if (!flagB)
                            {
                                System.Console.WriteLine("Do you want to input address? Y/N");
                                flag = System.Console.ReadLine();
                                if (flag == "Y")
                                {
                                    address = System.Console.ReadLine();
                                }

                                System.Console.WriteLine("Do you want to input passport? Y/N");
                                flag = System.Console.ReadLine();
                                if (flag == "Y")
                                {
                                    passport = Convert.ToInt32(System.Console.ReadLine());
                                }

                                address = null;
                                passport = 0;
                                if (name != null && sername != null)
                                {
                                    var client = Client.Builder
                                        .WithName(name)
                                        .WithSurname(sername)
                                        .WithAddress(address)
                                        .WithPassportNumber(passport)
                                        .Build();
                                    clients.Add(client);
                                    Bank bank = centralBank.FindBank(nameBank);
                                    System.Console.WriteLine("What type of bank? Credit/Debit/Deposit");
                                    string type = System.Console.ReadLine() ?? throw new InvalidOperationException();
                                    System.Console.WriteLine("What sum of account?");
                                    int sum = Convert.ToInt32(System.Console.ReadLine());
                                    var accountType = BankAccountType.Credit;
                                    if (type == "Credit")
                                    {
                                        accountType = BankAccountType.Credit;
                                    }
                                    else if (type == "Debit")
                                    {
                                        accountType = BankAccountType.Debit;
                                    }
                                    else if (type == "Deposit")
                                    {
                                        accountType = BankAccountType.Deposit;
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }

                                    bank.OpenBankAccount(client, sum, accountType);
                                    System.Console.WriteLine("Client's account successfully create");
                                    if (client.Accounts != null) System.Console.WriteLine(client.Accounts[0].AccountId);
                                }
                            }
                        }
                    }

                    break;
                case "5":
                    if (centralBank.Banks.Count == 0)
                    {
                        System.Console.WriteLine("There is no bank, you can't do it");
                    }
                    else
                    {
                        System.Console.WriteLine("Input bank's name");
                        string? nameBank = System.Console.ReadLine();
                        if (!centralBank.BankExist(nameBank))
                        {
                            System.Console.WriteLine("There is no such bank, you can't do it");
                        }

                        System.Console.WriteLine("Input name and sername client");
                        name = System.Console.ReadLine();
                        sername = System.Console.ReadLine();
                        foreach (Client client in clients)
                        {
                            if (client.Name == name && client.Surname == sername)
                            {
                                Bank bank = centralBank.FindBank(nameBank);
                                System.Console.WriteLine($"List of Client's accounts, chose the number:\n{client.Accounts}");
                                int number = Convert.ToInt32(System.Console.ReadLine());
                                System.Console.WriteLine("What sum does client want to put?");
                                int sum = Convert.ToInt32(System.Console.ReadLine());
                                if (client.Accounts != null) centralBank.NewTransaction(null, client.Accounts[number - 1], client.Accounts[number - 1], sum, TransactionType.Refil);
                                if (client.Accounts != null)
                                    System.Console.WriteLine($"{client.Name}'s budget is {client.Accounts[number - 1].SumOfMoney}");
                                break;
                            }
                        }
                    }

                    break;
                case "6":
                    if (centralBank.Banks.Count == 0)
                    {
                        System.Console.WriteLine("There is no bank, you can't do it");
                    }
                    else
                    {
                        System.Console.WriteLine("Input bank's name");
                        string? nameBank = System.Console.ReadLine();
                        if (!centralBank.BankExist(nameBank))
                        {
                            System.Console.WriteLine("There is no such bank, you can't do it");
                        }

                        System.Console.WriteLine("Input name and sername client");
                        name = System.Console.ReadLine();
                        sername = System.Console.ReadLine();
                        foreach (Client client in clients)
                        {
                            if (client.Name == name && client.Surname == sername)
                            {
                                Bank bank = centralBank.FindBank(nameBank);
                                System.Console.WriteLine($"List of Client's accounts, chose the number:\n{client.Accounts}");
                                int number = Convert.ToInt32(System.Console.ReadLine());
                                System.Console.WriteLine("What sum does client want to withdraw?");
                                int sum = Convert.ToInt32(System.Console.ReadLine());
                                if (client.Accounts != null) centralBank.NewTransaction(null, client.Accounts[number - 1], client.Accounts[number - 1], sum, TransactionType.WithDraw);
                                if (client.Accounts != null)
                                    System.Console.WriteLine($"{client.Name}'s budget is {client.Accounts[number - 1].SumOfMoney}");
                                break;
                            }
                        }
                    }

                    break;
                case "7":
                    if (centralBank.Banks.Count == 0)
                    {
                        System.Console.WriteLine("There is no bank, you can't do it");
                    }
                    else
                    {
                        System.Console.WriteLine("Input bank's name");
                        string? nameBank = System.Console.ReadLine();
                        if (!centralBank.BankExist(nameBank))
                        {
                            System.Console.WriteLine("There is no such bank, you can't do it");
                        }

                        System.Console.WriteLine("Input name and sername client");
                        name = System.Console.ReadLine();
                        sername = System.Console.ReadLine();
                        foreach (Client client in clients)
                        {
                            if (client.Name == name && client.Surname == sername)
                            {
                                Bank bank = centralBank.FindBank(nameBank);
                                System.Console.WriteLine($"List of Client's accounts, chose the number:\n{client.Accounts}");
                                int number = Convert.ToInt32(System.Console.ReadLine());
                                System.Console.WriteLine("Input other bank's name");
                                string? nameOtherBank = System.Console.ReadLine();
                                Bank otherBank = centralBank.FindBank(nameOtherBank);
                                System.Console.WriteLine("Input name and sername client to transfer");
                                name = System.Console.ReadLine();
                                sername = System.Console.ReadLine();
                                foreach (Client otherClient in clients)
                                {
                                    if (otherClient.Name == name && otherClient.Surname == sername)
                                    {
                                        System.Console.WriteLine($"List of Client's accounts, chose the number:\n{client.Accounts}");
                                        int otherNumber = Convert.ToInt32(System.Console.ReadLine());
                                        System.Console.WriteLine("What sum does client want to transfer?");
                                        decimal sum = Convert.ToDecimal(System.Console.ReadLine());
                                        if (client.Accounts != null)
                                        {
                                            if (otherClient.Accounts != null)
                                            {
                                                centralBank.TransferMoney(client.Accounts[number - 1], otherClient.Accounts[otherNumber - 1], sum);
                                                if (client.Accounts != null)
                                                {
                                                    System.Console.WriteLine(
                                                        $"{client.Name}'s budget is {client.Accounts[number - 1].SumOfMoney}");
                                                    System.Console.WriteLine($"{otherClient.Name}'s budget is {otherClient.Accounts[otherNumber - 1].SumOfMoney}");
                                                }
                                            }
                                        }

                                        break;
                                    }
                                }

                                break;
                            }
                        }
                    }

                    break;
            }
        }
    }
}