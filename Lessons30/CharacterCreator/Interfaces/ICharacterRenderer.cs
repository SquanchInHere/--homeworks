using CharacterCreator.Abstractions;

namespace CharacterCreator.Interfaces;

public interface ICharacterRenderer
{
    void Render(Character character);
}