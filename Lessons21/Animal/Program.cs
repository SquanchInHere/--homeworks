
using Animal.Src;
using Animal.Src.Lib;
using Animal.Src.Services;

WildAnimal[] animals = {
    new WildAnimal("Deer", 2, 15, "Forests of Europe", 12000),
    new WildAnimal("Rabbit", 1, 8, "Fields and forests", 25000),
    new WildAnimal("Bison", 5, 20, "Protected reserves", 800),
    new Predator("Wolf", 3, 16, "Forests and mountains", 3500, 1.04),
    new Predator("Tiger", 4, 20, "Asian forests", 900, 0.88),
    new Predator("Fox", 2, 10, "Forests and steppes", 7000, 1.08)
};

Menu.Run(animals);
