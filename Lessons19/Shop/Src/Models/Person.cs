namespace Shop.Src.Models
{
    public class Person
    {
        private string firstName;
        private string lastName;
        private DateTime birthDate;

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set => birthDate = value;
        }

        public Person()
        {
            firstName = "John";
            lastName = "Doe";
            birthDate = new DateTime(2000, 1, 1);
        }

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
        }

        public override string ToString()
        {
            return $"First name: {firstName}, Last name: {lastName}, Birth date: {birthDate.ToShortDateString()}";
        }
    }
}
