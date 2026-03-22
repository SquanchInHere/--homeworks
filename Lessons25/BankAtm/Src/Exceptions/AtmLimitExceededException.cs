namespace BankAtm.Src.Exceptions
{
    public class AtmLimitExceededException : AtmException
    {
        public AtmLimitExceededException(string message) : base(message) { }
    }
}
