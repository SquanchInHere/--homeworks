using CharacterCreator.Abstractions;
using CharacterCreator.Enums;
using CharacterCreator.Models;

namespace CharacterCreator.Entities;

public class Mage : Character
{
    public Mage(
        string name,
        List<CharacterSkill> skills,
        CharacterAttributes attributes,
        CharacterStats stats)
        : base(name, CharacterClassType.Mage, skills, attributes, stats)
    {
    }
}
