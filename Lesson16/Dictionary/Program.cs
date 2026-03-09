using Dictionary.src;
using Dictionary.src.Data;
using System.Text;

namespace Dictionary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Menu.Run(new DictionaryManager(DataSeed.GetInitialData()));
        }
    }

}
