using CharacterCreator.Abstractions;
using CharacterCreator.Enums;
using CharacterCreator.Models;

namespace CharacterCreator.Entities;

public class Archer : Character
{
    public Archer(
        string name,
        List<CharacterSkill> skills,
        CharacterAttributes attributes,
        CharacterStats stats)
        : base(name, CharacterClassType.Archer, skills, attributes, stats)
    {
    }
}
