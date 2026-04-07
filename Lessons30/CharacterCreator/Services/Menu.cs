using CharacterCreator.Abstractions;
using CharacterCreator.Entities;
using CharacterCreator.Enums;
using CharacterCreator.Interfaces;
using CharacterCreator.Models;
using CharacterCreator.Services;
using Spectre.Console;

namespace CharacterCreator.Service;

public class CharacterCreatorMenu
{
    private readonly IUser _userPrompter;
    private readonly ICharacterRenderer _renderer;
    private readonly CharacterStatsCalculator _statsCalculator;

    public CharacterCreatorMenu()
    {
        _userPrompter = new SpectreUser();
        _renderer = new SpectreCharacterRenderer();
        _statsCalculator = new CharacterStatsCalculator();
    }

    public void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ShowHeader();

        string name = _userPrompter.AskCharacterName();
        CharacterClassType classType = _userPrompter.AskCharacterType();
        List<CharacterSkill> skills = _userPrompter.AskSkills();
        CharacterAttributes attributes = _userPrompter.AskAttributes(classType);
        CharacterStats stats = _statsCalculator.Calculate(attributes);

        Character character = CreateCharacter(name, classType, skills, attributes, stats);

        AnsiConsole.WriteLine();
        _renderer.Render(character);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold green]✔ Character created successfully![/]");
    }

    private void ShowHeader()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(
            new FigletText("Hero Forge")
                .Centered()
                .Color(Color.Cyan1));

        AnsiConsole.Write(
            new Spectre.Console.Rule("[yellow]Create Your Fantasy Character[/]")
                .Centered()
                .RuleStyle("grey"));

        AnsiConsole.WriteLine();
    }

    private Character CreateCharacter(
        string name,
        CharacterClassType classType,
        List<CharacterSkill> skills,
        CharacterAttributes attributes,
        CharacterStats stats)
    {
        switch (classType)
        {
            case CharacterClassType.Warrior:
                return new Warrior(name, skills, attributes, stats);

            case CharacterClassType.Mage:
                return new Mage(name, skills, attributes, stats);

            case CharacterClassType.Archer:
                return new Archer(name, skills, attributes, stats);

            default:
                throw new InvalidOperationException("Unknown character class.");
        }
    }
}