namespace BankAtm.Src.Exceptions
{
    public class CannotDispenseAmountException : AtmException
    {
        public CannotDispenseAmountException(string message) : base(message) { }
    }
}
