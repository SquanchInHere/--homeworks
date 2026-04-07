using Zoo.Src.Abstractions;
using Zoo.Src.Enums;
using Zoo.Src.Factories;

namespace Zoo.Src.Services
{
    public class AnimalLifecycleService
    {
        private readonly Random _random = new Random();

        public int ProcessBirths(List<Animal> animals, Func<Animal, bool> addAnimalFunc)
        {
            int born = 0;

            foreach (var group in GetBreedableGroups(animals))
            {
                foreach (var animal in group)
                {
                    if (animal.TryReproduce())
                    {
                        Animal baby = CreateBaby(animal.Species)!;

                        if (baby != null && addAnimalFunc(baby))
                            born++;
                    }
                }
            }

            return born;
        }

        public int ProcessDeaths(List<Animal> animals)
        {
            int died = 0;

            foreach (var animal in animals.Where(a => a.IsAlive))
            {
                if (animal.TryDie())
                    died++;
            }

            return died;
        }

        public void AgeAnimals(List<Animal> animals)
        {
            foreach (var animal in animals.Where(a => a.IsAlive))
            {
                animal.LiveOneDay();
            }
        }

        private IEnumerable<IGrouping<string, Animal>> GetBreedableGroups(List<Animal> animals)
        {
            return animals
                .Where(a => a.IsAlive)
                .GroupBy(a => a.Species)
                .Where(HasBothGenders);
        }

        private bool HasBothGenders(IGrouping<string, Animal> group)
        {
            bool hasMale = group.Any(a => a.Gender == GenderType.Male);
            bool hasFemale = group.Any(a => a.Gender == GenderType.Female);

            return hasMale && hasFemale;
        }

        private Animal? CreateBaby(string species)
        {
            GenderType babyGender = _random.Next(0, 2) == 0
                ? GenderType.Male
                : GenderType.Female;

            return AnimalFactory.CreateBaby(species, babyGender);
        }
    }
}
