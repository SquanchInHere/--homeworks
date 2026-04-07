using CharacterCreator.Models;

namespace CharacterCreator.Services;

public class CharacterStatsCalculator
{
    public CharacterStats Calculate(CharacterAttributes attributes)
    {
        CharacterStats stats = new CharacterStats();

        stats.Health = 50 + attributes.Endurance * 10 + attributes.Strength * 2;
        stats.Mana = 30 + attributes.Intelligence * 12;
        stats.Attack = attributes.Strength * 3 + attributes.Agility;
        stats.Defense = attributes.Endurance * 2 + attributes.Agility;
        stats.CriticalChance = attributes.Agility * 2;

        return stats;
    }
}
