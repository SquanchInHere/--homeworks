using CharacterCreator.Enums;

namespace CharacterCreator.Providers;

public class CharacterVisualProvider
{
    public string GetClassDisplayName(CharacterClassType classType)
    {
        switch (classType)
        {
            case CharacterClassType.Warrior:
                return "⚔ Warrior";

            case CharacterClassType.Mage:
                return "🔮 Mage";

            case CharacterClassType.Archer:
                return "🏹 Archer";

            default:
                return "Unknown";
        }
    }

    public string GetClassColor(CharacterClassType classType)
    {
        switch (classType)
        {
            case CharacterClassType.Warrior:
                return "red";

            case CharacterClassType.Mage:
                return "deepskyblue1";

            case CharacterClassType.Archer:
                return "green3";

            default:
                return "white";
        }
    }

    public string GetSkillDisplayName(CharacterSkill skill)
    {
        switch (skill)
        {
            case CharacterSkill.Strength:
                return "💪 Strength";

            case CharacterSkill.Magic:
                return "✨ Magic";

            case CharacterSkill.Archery:
                return "🎯 Archery";

            case CharacterSkill.Defense:
                return "🛡 Defense";

            case CharacterSkill.Agility:
                return "🌀 Agility";

            case CharacterSkill.Alchemy:
                return "🧪 Alchemy";

            case CharacterSkill.Stealth:
                return "🕶 Stealth";

            case CharacterSkill.Healing:
                return "💚 Healing";

            default:
                return skill.ToString();
        }
    }
}