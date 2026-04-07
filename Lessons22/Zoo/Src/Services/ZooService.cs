using Zoo.Src.Abstractions;
using Zoo.Src.Models;

namespace Zoo.Src.Services
{
    public class ZooService
    {
        public string Name { get; set; }
        public decimal TicketPrice { get; set; }

        public List<Enclosure> Enclosures { get; private set; }
        public List<AnimalPurchaseInfo> AnimalPrices { get; private set; }
        public ZooStatistics Statistics { get; private set; }

        public FoodService FoodService { get; private set; }
        public FinanceService FinanceService { get; private set; }
        public VisitorService VisitorService { get; private set; }
        public AnimalLifecycleService LifecycleService { get; private set; }

        public ZooService(string name, decimal startMoney, decimal ticketPrice)
        {
            Name = name;
            TicketPrice = ticketPrice;

            Enclosures = new List<Enclosure>();
            AnimalPrices = new List<AnimalPurchaseInfo>();
            Statistics = new ZooStatistics();

            FoodService = new FoodService(new List<FoodStorage>());
            FinanceService = new FinanceService(startMoney);
            VisitorService = new VisitorService();
            LifecycleService = new AnimalLifecycleService();
        }

        public void AddEnclosure(Enclosure enclosure)
        {
            Enclosures.Add(enclosure);
        }

        public void AddAnimalPrice(string species, decimal price)
        {
            AnimalPrices.Add(new AnimalPurchaseInfo(species, price));
        }

        public void AddFoodStorage(FoodStorage storage)
        {
            FoodService.FoodStorages.Add(storage);
        }

        public List<Animal> GetAllAnimals()
        {
            return Enclosures.SelectMany(e => e.Animals).ToList();
        }

        public List<Animal> GetAliveAnimals()
        {
            return GetAllAnimals().Where(a => a.IsAlive).ToList();
        }

        public int GetAliveAnimalsCount()
        {
            return GetAliveAnimals().Count;
        }

        public bool AddAnimalToEnclosure(Animal animal)
        {
            Enclosure enclosure = FindSuitableEnclosure(animal)!;

            if (enclosure == null)
                return false;

            return enclosure.AddAnimal(animal);
        }

        public void ShowAllAnimals()
        {
            Console.WriteLine($"\n===== Animals of {Name} =====");

            foreach (var enclosure in Enclosures)
            {
                Console.WriteLine($"\n{enclosure}");
                foreach (var animal in enclosure.Animals)
                {
                    Console.WriteLine(animal);
                }
            }
        }

        public void ShowAnimalCountBySpecies()
        {
            Console.WriteLine("\n===== Animal Count By Species =====");

            var groups = GetAliveAnimals().GroupBy(a => a.Species);

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Key}: {group.Count()}");
            }
        }

        public void ShowEnclosures()
        {
            Console.WriteLine("\n===== Enclosures =====");

            foreach (var enclosure in Enclosures)
            {
                Console.WriteLine(enclosure);
            }
        }

        public void ShowFoodStatus()
        {
            Console.WriteLine("\n===== Food Storage =====");
            Console.WriteLine(FoodService.GetFoodStatus());
        }

        public void ShowFinance()
        {
            Console.WriteLine("\n===== Finance =====");
            Console.WriteLine(FinanceService);
        }

        public void ShowStatistics()
        {
            Console.WriteLine("\n===== Total Statistics =====");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Alive animals: {GetAliveAnimalsCount()}");
            Console.WriteLine($"Ticket price: {TicketPrice:C}");
            Console.WriteLine(Statistics);
            Console.WriteLine(FinanceService);
        }

        public DailyReport SimulateOneDay()
        {
            Statistics.TotalDays++;

            int diedFromHunger = ProcessFeeding();
            ProcessAnimalAging();

            int visitors = ProcessVisitors();
            int bornToday = ProcessBirths();
            int diedNaturally = ProcessDeaths();
            decimal expenses = ProcessDailyExpenses();

            int totalDiedToday = diedFromHunger + diedNaturally;
            decimal income = visitors * TicketPrice;

            UpdateStatistics(visitors, bornToday, totalDiedToday, income, expenses);
            RemoveDeadAnimals();

            return BuildDailyReport(visitors, bornToday, totalDiedToday, income, expenses);
        }

        public void SimulateDays(int days)
        {
            for (int i = 0; i < days; i++)
            {
                DailyReport report = SimulateOneDay();

                Console.WriteLine("\n----------------------------");
                Console.WriteLine(report);
                Console.WriteLine("----------------------------");
            }
        }

        public void BuyFood(string foodType, double amountKg, decimal pricePerKg)
        {
            bool success = FinanceService.BuyFood(FoodService, foodType, amountKg, pricePerKg);

            if (success)
                Console.WriteLine($"Purchased {amountKg} kg of '{foodType}'.");
            else
                Console.WriteLine("Not enough money to buy food.");
        }

        public bool TryBuyAnimal(string species, Animal animal)
        {
            bool paid = FinanceService.BuyAnimal(AnimalPrices, species);

            if (!paid)
                return false;

            return AddAnimalToEnclosure(animal);
        }

        public bool IsInCrisis()
        {
            return FinanceService.Money < 0 || GetAliveAnimalsCount() == 0;
        }

        private Enclosure? FindSuitableEnclosure(Animal animal)
        {
            return Enclosures.FirstOrDefault(e => e.CanAddAnimal(animal));
        }

        private int ProcessFeeding()
        {
            int diedFromHunger = 0;

            foreach (var animal in GetAliveAnimals())
            {
                bool fed = FoodService.FeedAnimal(animal);

                if (!fed)
                {
                    animal.GetType(); // keeps compiler calm if needed during edits
                    animal.GetType();
                    animal.GetType();
                    animal.GetType();
                    diedFromHunger++;
                    SetAnimalDead(animal);
                }
            }

            return diedFromHunger;
        }

        private void ProcessAnimalAging()
        {
            LifecycleService.AgeAnimals(GetAliveAnimals());
        }

        private int ProcessVisitors()
        {
            int visitors = VisitorService.GenerateVisitors(GetAliveAnimals());
            decimal income = visitors * TicketPrice;

            FinanceService.AddIncome(income);
            return visitors;
        }

        private int ProcessBirths()
        {
            return LifecycleService.ProcessBirths(
                GetAliveAnimals(),
                baby => AddAnimalToEnclosure(baby));
        }

        private int ProcessDeaths()
        {
            return LifecycleService.ProcessDeaths(GetAliveAnimals());
        }

        private decimal ProcessDailyExpenses()
        {
            decimal expenses = 500m + GetAliveAnimalsCount() * 20m;
            FinanceService.AddExpense(expenses);
            return expenses;
        }

        private void UpdateStatistics(int visitors, int born, int died, decimal income, decimal expenses)
        {
            Statistics.TotalBorn += born;
            Statistics.TotalDied += died;
            Statistics.TotalVisitors += visitors;
            Statistics.TotalIncomeFromVisitors += income;
            Statistics.TotalExpenses += expenses;
        }

        private void RemoveDeadAnimals()
        {
            foreach (var enclosure in Enclosures)
            {
                enclosure.RemoveDeadAnimals();
            }
        }

        private DailyReport BuildDailyReport(int visitors, int born, int died, decimal income, decimal expenses)
        {
            return new DailyReport
            {
                DayNumber = Statistics.TotalDays,
                Visitors = visitors,
                Income = income,
                Expenses = expenses,
                BornAnimals = born,
                DiedAnimals = died,
                AliveAnimals = GetAliveAnimalsCount()
            };
        }

        private void SetAnimalDead(Animal animal)
        {
            var property = animal.GetType().BaseType?.GetProperty("IsAlive");
            property?.SetValue(animal, false);
        }

        public override string ToString()
        {
            return $"Zoo: {Name}, Balance: {FinanceService.Money:C}, " +
                   $"Ticket: {TicketPrice:C}, Alive animals: {GetAliveAnimalsCount()}, Enclosures: {Enclosures.Count}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ZooService other)
                return false;

            return Name == other.Name &&
                   TicketPrice == other.TicketPrice &&
                   FinanceService.Money == other.FinanceService.Money;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, TicketPrice, FinanceService.Money);
        }
    }
}
