using Zoo.Src.Abstractions;
using Zoo.Src.Enums;

namespace Zoo.Src.Models
{

    public class Enclosure
    {
        public string Name { get; set; }
        public EnclosureType Type { get; set; }
        public int Capacity { get; set; }
        public List<Animal> Animals { get; private set; }

        public Enclosure(string name, EnclosureType type, int capacity)
        {
            Name = name;
            Type = type;
            Capacity = capacity;
            Animals = new List<Animal>();
        }

        public bool CanAddAnimal(Animal animal)
        {
            return HasFreeSpace() && IsAnimalSuitable(animal);
        }

        public bool AddAnimal(Animal animal)
        {
            if (!CanAddAnimal(animal))
                return false;

            Animals.Add(animal);
            return true;
        }

        public void RemoveDeadAnimals()
        {
            Animals.RemoveAll(a => !a.IsAlive);
        }

        private bool HasFreeSpace()
        {
            return Animals.Count < Capacity;
        }

        private bool IsAnimalSuitable(Animal animal)
        {
            switch (Type)
            {
                case EnclosureType.Herbivores:
                    return animal.DietType == AnimalDietType.Herbivore && !animal.IsDangerous;

                case EnclosureType.Predators:
                    return animal.DietType == AnimalDietType.Carnivore;

                case EnclosureType.Dangerous:
                    return animal.IsDangerous;

                case EnclosureType.Safe:
                    return !animal.IsDangerous;

                case EnclosureType.Reptiles:
                    return animal is Crocodile;

                default:
                    return false;
            }
        }

        public override string ToString()
        {
            return $"Enclosure: {Name}, Type: {Type}, Capacity: {Capacity}, Animals: {Animals.Count}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Enclosure other)
                return false;

            return Name == other.Name &&
                   Type == other.Type &&
                   Capacity == other.Capacity &&
                   Animals.SequenceEqual(other.Animals);
        }

        public override int GetHashCode()
        {
            int hash = HashCode.Combine(Name, Type, Capacity);

            foreach (var animal in Animals)
            {
                hash = HashCode.Combine(hash, animal);
            }

            return hash;
        }
    }
}
