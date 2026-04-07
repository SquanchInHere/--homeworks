using CharacterCreator.Enums;
using CharacterCreator.Interfaces;
using CharacterCreator.Models;
using CharacterCreator.Providers;
using Spectre.Console;

namespace CharacterCreator.Services;

public class SpectreUser : IUser
{
    private readonly CharacterAttributesProvider _attributesProvider;
    private const int BonusPoints = 10;
    private const int MaxSkills = 3;

    public SpectreUser()
    {
        _attributesProvider = new CharacterAttributesProvider();
    }

    public string AskCharacterName()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]👤 Enter character name:[/]")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Name cannot be empty.[/]")
                .Validate(name =>
                {
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        return ValidationResult.Error("[red]Please enter a valid name.[/]");
                    }

                    return ValidationResult.Success();
                }));
    }

    public CharacterClassType AskCharacterType()
    {
        string selectedClass = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]🧭 Choose character type:[/]")
                .PageSize(10)
                .AddChoices(
                    "⚔ Warrior",
                    "🔮 Mage",
                    "🏹 Archer"));

        switch (selectedClass)
        {
            case "⚔ Warrior":
                return CharacterClassType.Warrior;
            case "🔮 Mage":
                return CharacterClassType.Mage;
            case "🏹 Archer":
                return CharacterClassType.Archer;
            default:
                throw new InvalidOperationException("Unknown character type.");
        }
    }

    public List<CharacterSkill> AskSkills()
    {
        while (true)
        {
            List<string> selectedSkills = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title($"[yellow]🧰 Choose up to {MaxSkills} skills:[/]")
                    .InstructionsText("[grey](Press [blue]Space[/] to select, [green]Enter[/] to confirm)[/]")
                    .AddChoices(
                        "💪 Strength",
                        "✨ Magic",
                        "🎯 Archery",
                        "🛡 Defense",
                        "🌀 Agility",
                        "🧪 Alchemy",
                        "🕶 Stealth",
                        "💚 Healing"));

            if (selectedSkills.Count > MaxSkills)
            {
                AnsiConsole.MarkupLine($"[red]You can select no more than {MaxSkills} skills.[/]");
                AnsiConsole.WriteLine();
                continue;
            }

            List<CharacterSkill> skills = new List<CharacterSkill>();

            foreach (string skill in selectedSkills)
            {
                switch (skill)
                {
                    case "💪 Strength":
                        skills.Add(CharacterSkill.Strength);
                        break;
                    case "✨ Magic":
                        skills.Add(CharacterSkill.Magic);
                        break;
                    case "🎯 Archery":
                        skills.Add(CharacterSkill.Archery);
                        break;
                    case "🛡 Defense":
                        skills.Add(CharacterSkill.Defense);
                        break;
                    case "🌀 Agility":
                        skills.Add(CharacterSkill.Agility);
                        break;
                    case "🧪 Alchemy":
                        skills.Add(CharacterSkill.Alchemy);
                        break;
                    case "🕶 Stealth":
                        skills.Add(CharacterSkill.Stealth);
                        break;
                    case "💚 Healing":
                        skills.Add(CharacterSkill.Healing);
                        break;
                }
            }

            return skills;
        }
    }

    public CharacterAttributes AskAttributes(CharacterClassType classType)
    {
        CharacterAttributes attributes = _attributesProvider.GetBaseAttributes(classType);
        int remainingPoints = BonusPoints;

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Spectre.Console.Rule("[yellow]📊 Distribute Attribute Points[/]").Centered());
        AnsiConsole.MarkupLine($"[green]You have {BonusPoints} bonus points to distribute.[/]");
        AnsiConsole.MarkupLine("[grey]Final Health, Mana, Attack and Defense will be calculated automatically.[/]");
        AnsiConsole.WriteLine();

        remainingPoints = DistributePoints("💪 Strength", attributes, remainingPoints, a => a.Strength += 1);
        remainingPoints = DistributePoints("🌀 Agility", attributes, remainingPoints, a => a.Agility += 1);
        remainingPoints = DistributePoints("🧠 Intelligence", attributes, remainingPoints, a => a.Intelligence += 1);
        remainingPoints = DistributePoints("🛡 Endurance", attributes, remainingPoints, a => a.Endurance += 1);

        if (remainingPoints > 0)
        {
            attributes.Endurance += remainingPoints;
            AnsiConsole.MarkupLine($"[grey]Unused points were added to Endurance: +{remainingPoints}[/]");
        }

        return attributes;
    }

    private int DistributePoints(
        string attributeName,
        CharacterAttributes attributes,
        int remainingPoints,
        Action<CharacterAttributes> addPointAction)
    {
        if (remainingPoints <= 0)
        {
            return 0;
        }

        int points = AnsiConsole.Prompt(
            new TextPrompt<int>($"[green]{attributeName}[/] - points to add [grey](remaining: {remainingPoints})[/]")
                .DefaultValue(0)
                .Validate(value =>
                {
                    if (value < 0)
                    {
                        return ValidationResult.Error("[red]Value cannot be negative.[/]");
                    }

                    if (value > remainingPoints)
                    {
                        return ValidationResult.Error("[red]You do not have that many points left.[/]");
                    }

                    return ValidationResult.Success();
                }));

        for (int i = 0; i < points; i++)
        {
            addPointAction(attributes);
        }

        return remainingPoints - points;
    }
}