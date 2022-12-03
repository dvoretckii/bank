namespace Banks.Exceptions;

public class BankException : Exception
{
    private BankException(string message)
        : base(message) { }

    public static BankException ElementNotFound()
    {
        return new BankException("The element was not found");
    }

    public static BankException NullableVariable()
    {
        return new BankException("The result is null");
    }

    public static BankException InvalidOperation()
    {
        return new BankException("This operation is not possible");
    }
}