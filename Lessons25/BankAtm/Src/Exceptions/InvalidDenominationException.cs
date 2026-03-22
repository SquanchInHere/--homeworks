namespace BankAtm.Src.Exceptions
{
    public class InvalidDenominationException : Exception
    {
        public InvalidDenominationException(int denomination)
            : base($"Invalid denomination: {denomination}. Allowed denominations are 100, 200, 500, 1000.")
        {
        }
    }
}
