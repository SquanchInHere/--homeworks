using Animal.Src.Lib;
using Animal.Src.Services;

namespace Animal.Src
{
    public static class Menu
    {
        public static void Run(WildAnimal[] animals)
        {
            while (true)
            {
                ShowMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine()!;

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAllAnimals(animals);
                        break;
                    case "2":
                        ShowAnimalsWithLargestPopulation(animals);
                        break;
                    case "3":
                        ShowAnimalsNeedingProtection(animals);
                        break;
                    case "4":
                        AnalyzePopulationSuccess(animals);
                        break;
                    case "5":
                        TestFieldChanges();
                        break;
                    case "0":
                        Console.WriteLine("Program finished.");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("=== MENU ===");
            Console.WriteLine("1 - Show all animals");
            Console.WriteLine("2 - Show animals with the largest population");
            Console.WriteLine("3 - Show animals that need protection");
            Console.WriteLine("4 - Analyze population success");
            Console.WriteLine("5 - Test changing fields");
            Console.WriteLine("0 - Exit");
            Console.WriteLine();
        }

        private static void ShowAllAnimals(WildAnimal[] animals)
        {
            Console.WriteLine("=== All animals ===");
            foreach (WildAnimal animal in animals)
            {
                Console.WriteLine(animal);
            }
        }

        private static void ShowAnimalsWithLargestPopulation(WildAnimal[] animals)
        {
            int maxPopulation = animals.Max(a => a.AveragePopulation);
            var mostNumerousAnimals = animals.Where(a => a.AveragePopulation == maxPopulation);

            Console.WriteLine("=== Animals with the largest population ===");
            foreach (WildAnimal animal in mostNumerousAnimals)
            {
                Console.WriteLine(animal);
            }
        }

        private static void ShowAnimalsNeedingProtection(WildAnimal[] animals)
        {
            int minPopulation = animals.Min(a => a.AveragePopulation);
            var protectedAnimals = animals.Where(a => a.AveragePopulation == minPopulation);

            Console.WriteLine("=== Animals that need protection ===");
            foreach (WildAnimal animal in protectedAnimals)
            {
                Console.WriteLine(animal);
            }
        }

        private static void AnalyzePopulationSuccess(WildAnimal[] animals)
        {
            Console.WriteLine("=== Population success analysis ===");
            foreach (WildAnimal animal in animals)
            {
                Console.WriteLine($"{animal.Name}: {animal.GetPopulationSuccess()}");
            }
        }

        private static void TestFieldChanges()
        {
            Console.WriteLine("=== Testing field changes ===");

            WildAnimal animal = new WildAnimal("Hare", 1, 6, "Meadows", 5000);
            Console.WriteLine("Before changes:");
            Console.WriteLine(animal);

            animal.ChangeName("Mountain Hare");
            animal.ChangeLocation("Mountain meadows");
            animal.ChangePopulation(4200);

            Console.WriteLine("After changes:");
            Console.WriteLine(animal);

            Console.WriteLine();

            Predator predator = new Predator("Lynx", 2, 14, "Northern forests", 1500, 0.97);
            Console.WriteLine("Before changes:");
            Console.WriteLine(predator);

            predator.ChangePopulation(1800);
            predator.ChangePopulationCoefficient(1.03);

            Console.WriteLine("After changes:");
            Console.WriteLine(predator);
        }
    }
}
