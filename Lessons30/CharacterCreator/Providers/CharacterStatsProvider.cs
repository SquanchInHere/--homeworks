using CharacterCreator.Enums;
using CharacterCreator.Models;

namespace CharacterCreator.Providers;

public class CharacterStatsProvider
{
    public CharacterAttributes GetBaseAttributes(CharacterClassType classType)
    {
        CharacterAttributes attributes = new CharacterAttributes();

        switch (classType)
        {
            case CharacterClassType.Warrior:
                attributes.Strength = 7;
                attributes.Agility = 4;
                attributes.Intelligence = 2;
                attributes.Endurance = 7;
                break;

            case CharacterClassType.Mage:
                attributes.Strength = 2;
                attributes.Agility = 4;
                attributes.Intelligence = 8;
                attributes.Endurance = 4;
                break;

            case CharacterClassType.Archer:
                attributes.Strength = 5;
                attributes.Agility = 8;
                attributes.Intelligence = 4;
                attributes.Endurance = 5;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(classType), "Unknown character class.");
        }

        return attributes;
    }
}