using Zoo.Src.Abstractions;
using Zoo.Src.Enums;
using Zoo.Src.Factories;

namespace Zoo.Src.Services
{
    public static class MenuService
    {
        public static void RunMenu(ZooService zoo)
        {
            bool exit = false;

            while (!exit)
            {
                ShowMenu();
                Console.Write("Your choice: ");

                string choice = Console.ReadLine()!;
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        zoo.ShowAllAnimals();
                        break;

                    case "2":
                        zoo.ShowAnimalCountBySpecies();
                        break;

                    case "3":
                        zoo.ShowEnclosures();
                        break;

                    case "4":
                        zoo.ShowFoodStatus();
                        break;

                    case "5":
                        zoo.ShowFinance();
                        break;

                    case "6":
                        zoo.ShowStatistics();
                        break;

                    case "7":
                        SimulateAndCheckCrisis(zoo, 1);
                        break;

                    case "8":
                        SimulateAndCheckCrisis(zoo, 7);
                        break;

                    case "9":
                        BuyFoodMenu(zoo);
                        break;

                    case "10":
                        BuyAnimalMenu(zoo);
                        break;

                    case "11":
                        Console.WriteLine(zoo);
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid menu item.");
                        break;
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("              ZOO MENU");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Show all animals");
            Console.WriteLine("2. Show animal count by species");
            Console.WriteLine("3. Show enclosures");
            Console.WriteLine("4. Show food storage");
            Console.WriteLine("5. Show finance");
            Console.WriteLine("6. Show statistics");
            Console.WriteLine("7. Simulate 1 day");
            Console.WriteLine("8. Simulate 7 days");
            Console.WriteLine("9. Buy food");
            Console.WriteLine("10. Buy animal");
            Console.WriteLine("11. Show short zoo status");
            Console.WriteLine("0. Exit");
        }

        private static void SimulateAndCheckCrisis(ZooService zoo, int days)
        {
            zoo.SimulateDays(days);

            if (zoo.IsInCrisis())
            {
                Console.WriteLine("\n!!! THE ZOO IS IN CRISIS !!!");
                Console.WriteLine("Either the zoo has no money left or there are no alive animals.");
            }
        }

        private static void BuyFoodMenu(ZooService zoo)
        {
            Console.WriteLine("Choose food type:");
            Console.WriteLine("1. Meat");
            Console.WriteLine("2. Fish");
            Console.WriteLine("3. Grass");
            Console.Write("Your choice: ");

            string foodChoice = Console.ReadLine()!;
            string? foodType;

            switch (foodChoice)
            {
                case "1":
                    foodType = "Meat";
                    break;

                case "2":
                    foodType = "Fish";
                    break;

                case "3":
                    foodType = "Grass";
                    break;

                default:
                    foodType = null;
                    break;
            }

            if (foodType == null)
            {
                Console.WriteLine("Invalid food type.");
                return;
            }

            Console.Write("Enter amount in kg: ");
            if (!double.TryParse(Console.ReadLine(), out double amountKg) || amountKg <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            decimal pricePerKg;

            switch (foodType)
            {
                case "Meat":
                    pricePerKg = 25m;
                    break;

                case "Fish":
                    pricePerKg = 18m;
                    break;

                case "Grass":
                    pricePerKg = 8m;
                    break;

                default:
                    pricePerKg = 0m;
                    break;
            }

            zoo.BuyFood(foodType, amountKg, pricePerKg);
        }

        private static void BuyAnimalMenu(ZooService zoo)
        {
            Console.WriteLine("Which animal do you want to buy?");
            Console.WriteLine("1. Tiger");
            Console.WriteLine("2. Crocodile");
            Console.WriteLine("3. Kangaroo");
            Console.Write("Your choice: ");

            string animalChoice = Console.ReadLine()!;

            Console.Write("Enter name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter age: ");
            if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
            {
                Console.WriteLine("Invalid age.");
                return;
            }

            Console.Write("Enter weight: ");
            if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
            {
                Console.WriteLine("Invalid weight.");
                return;
            }

            GenderType gender = ReadGender();
            Animal? animal = null;
            string species = string.Empty;

            switch (animalChoice)
            {
                case "1":
                    species = "Tiger";
                    animal = AnimalFactory.CreateTiger(name, age, weight, gender, 60);
                    break;

                case "2":
                    species = "Crocodile";
                    animal = AnimalFactory.CreateCrocodile(name, age, weight, gender, true);
                    break;

                case "3":
                    species = "Kangaroo";
                    animal = AnimalFactory.CreateKangaroo(name, age, weight, gender, 8);
                    break;

                default:
                    Console.WriteLine("Invalid animal type.");
                    return;
            }

            bool success = zoo.TryBuyAnimal(species, animal);

            if (success)
                Console.WriteLine("Animal was successfully purchased and placed into an enclosure.");
            else
                Console.WriteLine("Could not purchase the animal or place it into an enclosure.");
        }

        private static GenderType ReadGender()
        {
            Console.WriteLine("Choose gender:");
            Console.WriteLine("1. Male");
            Console.WriteLine("2. Female");
            Console.Write("Your choice: ");

            string genderChoice = Console.ReadLine()!;
            return genderChoice == "1" ? GenderType.Male : GenderType.Female;
        }
    }
}
