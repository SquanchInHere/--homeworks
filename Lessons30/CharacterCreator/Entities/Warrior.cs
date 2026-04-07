using CharacterCreator.Abstractions;
using CharacterCreator.Enums;
using CharacterCreator.Models;

namespace CharacterCreator.Entities;

public class Warrior : Character
{
    public Warrior(
        string name,
        List<CharacterSkill> skills,
        CharacterAttributes attributes,
        CharacterStats stats)
        : base(name, CharacterClassType.Warrior, skills, attributes, stats)
    {
    }
}
