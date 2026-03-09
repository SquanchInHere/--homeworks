using Journal.Src.Models;
using Journal.Src.Services;

namespace Journal.Src
{
    public class Menu
    {
        public void Run()
        {
            JournalModel journal = new JournalModel();
            JournalService journalService = new JournalService();

            journalService.InputData(journal);
            journalService.ShowData(journal);

            Console.WriteLine("\nSeparate field access:");
            Console.WriteLine("Journal name: " + journal.Name);
            Console.WriteLine("Email: " + journal.Email);
        }
    }
}
