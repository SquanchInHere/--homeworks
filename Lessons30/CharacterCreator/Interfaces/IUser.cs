using CharacterCreator.Enums;
using CharacterCreator.Models;

namespace CharacterCreator.Interfaces;

public interface IUser
{
    string AskCharacterName();
    CharacterClassType AskCharacterType();
    List<CharacterSkill> AskSkills();
    CharacterAttributes AskAttributes(CharacterClassType classType);
}