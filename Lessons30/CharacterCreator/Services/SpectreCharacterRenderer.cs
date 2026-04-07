using CharacterCreator.Abstractions;
using CharacterCreator.Enums;
using CharacterCreator.Interfaces;
using CharacterCreator.Providers;
using Spectre.Console;

namespace CharacterCreator.Services;

public class SpectreCharacterRenderer : ICharacterRenderer
{
    private readonly CharacterVisualProvider _visualProvider;

    public SpectreCharacterRenderer()
    {
        _visualProvider = new CharacterVisualProvider();
    }

    public void Render(Character character)
    {
        string classDisplayName = _visualProvider.GetClassDisplayName(character.ClassType);
        string classColor = _visualProvider.GetClassColor(character.ClassType);

        Table attributesTable = new Table();
        attributesTable.Border = TableBorder.Rounded;
        attributesTable.BorderColor(Color.Grey);
        attributesTable.AddColumn("[bold yellow]Attributes[/]");
        attributesTable.AddColumn("[bold green]Value[/]");

        attributesTable.AddRow("💪 Strength", $"[orange1]{character.Attributes.Strength}[/]");
        attributesTable.AddRow("🌀 Agility", $"[green]{character.Attributes.Agility}[/]");
        attributesTable.AddRow("🧠 Intelligence", $"[deepskyblue1]{character.Attributes.Intelligence}[/]");
        attributesTable.AddRow("🛡 Endurance", $"[yellow]{character.Attributes.Endurance}[/]");

        Table statsTable = new Table();
        statsTable.Border = TableBorder.Rounded;
        statsTable.BorderColor(Color.Grey);
        statsTable.AddColumn("[bold yellow]Derived Stats[/]");
        statsTable.AddColumn("[bold green]Value[/]");

        statsTable.AddRow("❤ Health", $"[red]{character.Stats.Health}[/]");
        statsTable.AddRow("🔷 Mana", $"[blue]{character.Stats.Mana}[/]");
        statsTable.AddRow("⚔ Attack", $"[orange1]{character.Stats.Attack}[/]");
        statsTable.AddRow("🛡 Defense", $"[green]{character.Stats.Defense}[/]");
        statsTable.AddRow("🎯 Crit Chance", $"[yellow]{character.Stats.CriticalChance}%[/]");

        Table infoTable = new Table();
        infoTable.Border = TableBorder.Rounded;
        infoTable.BorderColor(Color.Grey);
        infoTable.AddColumn("[bold yellow]Profile[/]");
        infoTable.AddColumn("[bold green]Value[/]");

        infoTable.AddRow("👤 Name", $"[bold]{character.Name}[/]");
        infoTable.AddRow("🧭 Class", $"[{classColor}]{classDisplayName}[/]");

        Table skillsTable = new Table();
        skillsTable.Border = TableBorder.Rounded;
        skillsTable.BorderColor(Color.Grey);
        skillsTable.AddColumn("[bold yellow]Selected Skills[/]");

        if (character.Skills.Count == 0)
        {
            skillsTable.AddRow("[grey]No skills selected[/]");
        }
        else
        {
            foreach (CharacterSkill skill in character.Skills)
            {
                skillsTable.AddRow(_visualProvider.GetSkillDisplayName(skill));
            }
        }

        Rows content = new Rows(
            new Panel(infoTable)
                .Header("[bold white]Hero Profile[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Cyan1)),
            new Panel(attributesTable)
                .Header("[bold white]Primary Attributes[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Yellow)),
            new Panel(statsTable)
                .Header("[bold white]Final Stats[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Orange1)),
            new Panel(skillsTable)
                .Header("[bold white]Skill Set[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Green3)));

        Panel finalPanel = new Panel(content);
        finalPanel.Header = new PanelHeader("[bold yellow]⭐ Final Hero Card ⭐[/]");
        finalPanel.Border = BoxBorder.Double;
        finalPanel.BorderStyle = new Style(Color.Orange1);
        finalPanel.Padding = new Padding(1, 1, 1, 1);

        AnsiConsole.Write(finalPanel);
    }
}