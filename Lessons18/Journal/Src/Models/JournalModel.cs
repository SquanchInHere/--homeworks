namespace Journal.Src.Models
{
    public class JournalModel
    {
        private string name;
        private int year;
        private string description;
        private string phone;
        private string email;

        public JournalModel()
        {
            name = string.Empty;
            year = 0;
            description = string.Empty;
            phone = string.Empty;
            email = string.Empty;
        }

        public JournalModel(string name, int year, string description, string phone, string email)
        {
            this.name = name;
            this.year = year;
            this.description = description;
            this.phone = phone;
            this.email = email;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}