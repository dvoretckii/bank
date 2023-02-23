using Banks.Interfaces;

namespace Banks.Models;

public interface INameBuilder
{
    ISurnameBuilder WithName(string name);
}

public interface ISurnameBuilder
{
    IClientBuilder WithSurname(string surname);
}

public interface IClientBuilder
{
    IClientBuilder WithAddress(string? address);
    IClientBuilder WithPassportNumber(int passport);
    Client Build();
}

public class Client : IObserver
{
    private int _passportNumber;
    public static INameBuilder Builder => new ClientBuilder();

    public string? Name { get; private set; }
    public string? Surname { get; private set; }
    public string? Address { get; set; }
    public IReadOnlyList<IBankAccount>? Accounts { get; private set; }
    public List<string?>? Messages { get; private set; }

    public int PassportNumber
    {
        get
        {
            return _passportNumber;
        }

        set
        {
            if (_passportNumber != 0 && (int)Math.Log10(value) + 1 != 10)
            {
                throw new Exception();
            }

            _passportNumber = value;
        }
    }

    public void Update(object obj)
    {
        var newList = new List<string?>();
        if (Messages != null)
        {
            newList = new List<string?>(Messages);
        }

        newList.Add(obj.ToString());
        Messages = newList;
    }

    public void AddAccount(IBankAccount account)
    {
        var newList = new List<IBankAccount>();
        if (Accounts != null)
        {
            newList = new List<IBankAccount>(Accounts);
        }

        newList.Add(account);
        Accounts = newList;
    }

    public bool SuspicionOfAttacker()
    {
        return Address is null || PassportNumber == 0;
    }

    private class ClientBuilder : INameBuilder, ISurnameBuilder, IClientBuilder
    {
        private Client _client = new Client();

        public ISurnameBuilder WithName(string name)
        {
            _client.Name = name;
            return this;
        }

        public IClientBuilder WithSurname(string surname)
        {
            _client.Surname = surname;
            return this;
        }

        public IClientBuilder WithAddress(string? address)
        {
            _client.Address = address;
            return this;
        }

        public IClientBuilder WithPassportNumber(int passport)
        {
            if (passport != 0 && (int)Math.Log10(passport) + 1 != 10)
            {
                throw new Exception();
            }

            _client.PassportNumber = passport;
            return this;
        }

        public Client Build()
        {
            _client.Accounts = new List<IBankAccount>();
            _client.Messages = new List<string?>();
            return _client;
        }
    }
}