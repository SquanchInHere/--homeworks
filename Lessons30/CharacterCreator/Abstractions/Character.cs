using CharacterCreator.Enums;
using CharacterCreator.Models;

namespace CharacterCreator.Abstractions;

public abstract class Character
{
    public string Name { get; set; }
    public CharacterClassType ClassType { get; protected set; }
    public List<CharacterSkill> Skills { get; set; }
    public CharacterAttributes Attributes { get; set; }
    public CharacterStats Stats { get; set; }

    protected Character(
        string name,
        CharacterClassType classType,
        List<CharacterSkill> skills,
        CharacterAttributes attributes,
        CharacterStats stats)
    {
        Name = name;
        ClassType = classType;
        Skills = skills;
        Attributes = attributes;
        Stats = stats;
    }
}
