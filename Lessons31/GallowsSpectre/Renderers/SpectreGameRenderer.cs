using GallowsSpectre.Enums;
using GallowsSpectre.Interfaces;
using GallowsSpectre.Models;
using GallowsSpectre.Records;
using Spectre.Console;
using System.Text;

namespace GallowsSpectre.Renderers;

public class SpectreGameRenderer : IGameRenderer
{
    private const int Rows = 7;
    private const int Cols = 9;
    private const char LeftCorner = '┌';
    private const char RightCorner = '┐';
    private const int PostColumn = 2;
    private const int BeamRow = 0;
    private const int BeamLength = 4;

    private static readonly string[] CrowdLines =
    {
        "  \\o/    (^_^)    \\o/  ",
        "   |      /|\\      |   ",
        "  / \\     / \\     / \\",
    };

    private static readonly string[] CrowdMood =
    {
        "The crowd is relaxed and ready.",
        "The spectators notice the first mistake.",
        "The audience starts whispering nervously.",
        "The crowd gasps as the body appears.",
        "The spectators throw their hands up in shock.",
        "The whole audience is panicking.",
        "The crowd freezes at the final state."
    };

    public MainMenuAction ShowMainMenu()
    {
        AnsiConsole.Clear();

        var hero = new Panel(
            "[bold yellow]Classic Hangman[/]" +
            "[grey]Spectre.Console UI, encrypted word storage, per-word statistics, and a decrypted TXT export.[/]")
            .Header("[red]Game menu[/]")
            .Border(BoxBorder.Double)
            .Expand();

        var menuTable = new Table().Border(TableBorder.Rounded).Expand();
        menuTable.AddColumn("[yellow]Action[/]");
        menuTable.AddColumn("[yellow]Description[/]");
        menuTable.AddRow("Play - Easy", "Short and simple words");
        menuTable.AddRow("Play - Medium", "Balanced difficulty");
        menuTable.AddRow("Play - Hard", "Longer and trickier words");
        menuTable.AddRow("Show word statistics", "Per-word wins, losses and win rate");
        menuTable.AddRow("Show decrypted words", "View the plain TXT export");
        menuTable.AddRow("Exit", "Close the game");

        AnsiConsole.Write(new Rows(
            new FigletText("Gallows").LeftJustified().Color(Color.Red),
            hero,
            new Panel(menuTable).Header("[green]Available actions[/]").Border(BoxBorder.Rounded),
            new Panel("[grey]Use arrows and Enter to navigate the menu. Guess one English letter or the whole word. The hangman drawing follows the exact classic body progression from your old project.[/]")
                .Header("[blue]Rules[/]")
                .Border(BoxBorder.Rounded)
        ));

        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold green]Choose an action[/]")
                .PageSize(8)
                .HighlightStyle(new Style(Color.Black, Color.Green, Decoration.Bold))
                .AddChoices(new[]
                {
                    "Play - Easy",
                    "Play - Medium",
                    "Play - Hard",
                    "Show word statistics",
                    "Show decrypted words",
                    "Exit"
                }));

        return choice switch
        {
            "Play - Easy" => MainMenuAction.StartEasy,
            "Play - Medium" => MainMenuAction.StartMedium,
            "Play - Hard" => MainMenuAction.StartHard,
            "Show word statistics" => MainMenuAction.ShowStatistics,
            "Show decrypted words" => MainMenuAction.ShowDecryptedWords,
            _ => MainMenuAction.Exit
        };
    }

    public void RenderGame(GameSession session)
    {
        AnsiConsole.Clear();

        int stage = Math.Clamp(session.WrongAttempts, 0, 6);
        string scene = BuildClassicScene(stage);

        var scenePanel = new Panel($"[white]{Markup.Escape(scene)}[/]")
            .Header("[red]Hangman scene[/]")
            .Border(BoxBorder.Double)
            .Expand();

        var statusPanel = new Panel($"[bold]{Markup.Escape(CrowdMood[stage])}[/]")
            .Header("[purple]Spectators[/]")
            .Border(BoxBorder.Rounded)
            .Expand();

        var infoTable = new Table().Border(TableBorder.Rounded).Expand();
        infoTable.AddColumn("[yellow]Field[/]");
        infoTable.AddColumn("[yellow]Value[/]");
        infoTable.AddRow("Difficulty", session.Difficulty.ToString());
        infoTable.AddRow("Hidden word", $"[bold]{Markup.Escape(session.BuildMaskedWord())}[/]");
        infoTable.AddRow("Attempts", $"{session.AttemptsUsed}/{session.MaxAttempts}");
        infoTable.AddRow("Wrong attempts", $"{session.WrongAttempts}/6");
        infoTable.AddRow("Used letters", Markup.Escape(session.GetUsedLettersText()));
        infoTable.AddRow("Elapsed", session.Duration.ToString(@"mm\:ss"));

        var keyboardPanel = new Panel(BuildAlphabetMarkup(session))
            .Header("[green]Alphabet[/]")
            .Border(BoxBorder.Rounded)
            .Expand();

        var tipsPanel = new Panel(
            "[grey]Input one English letter or the whole word.[/]\n" +
            "[grey]Correct letters are green, wrong letters are red.[/]")
            .Header("[blue]Input help[/]")
            .Border(BoxBorder.Rounded)
            .Expand();

        var left = new Rows(scenePanel, statusPanel);
        var right = new Rows(infoTable, keyboardPanel, tipsPanel);

        AnsiConsole.Write(new Columns(left, right).Expand());
        AnsiConsole.WriteLine();
    }

    public string AskGuess()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[bold green]Your guess:[/]")
                .PromptStyle("green"));
    }

    public void ShowMessage(string message, string style = "yellow")
    {
        AnsiConsole.Write(
            new Panel($"[{style}]{Markup.Escape(message)}[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(ParseColor(style))));

        Thread.Sleep(800);
    }

    public void ShowSummary(GameSummaryRecord summary)
    {
        AnsiConsole.Clear();

        string resultText = summary.IsWin ? "VICTORY" : "DEFEAT";
        string titleColor = summary.IsWin ? "green" : "red";
        string bannerText = summary.IsWin ? "The spectators explode with applause!" : "The crowd watches the final fall in silence.";
        string scene = BuildClassicScene(Math.Clamp(summary.WrongAttempts, 0, 6));

        var banner = new Panel($"[bold {titleColor}]{resultText}[/]\n[grey]{bannerText}[/]")
            .Border(BoxBorder.Heavy)
            .Header("[yellow]Final result[/]")
            .Expand();

        var summaryTable = new Table().Border(TableBorder.Rounded).Expand();
        summaryTable.AddColumn("[yellow]Statistic[/]");
        summaryTable.AddColumn("[yellow]Value[/]");
        summaryTable.AddRow("Result", $"[bold {titleColor}]{(summary.IsWin ? "You win" : "You lose")}[/]");
        summaryTable.AddRow("Time", summary.DurationText);
        summaryTable.AddRow("Attempts", $"{summary.AttemptsUsed}/{summary.MaxAttempts}");
        summaryTable.AddRow("Wrong attempts", $"{summary.WrongAttempts}/6");
        summaryTable.AddRow("Hidden word", Markup.Escape(summary.Word));
        summaryTable.AddRow("Player letters", Markup.Escape(summary.UsedLetters));

        var left = new Rows(
            banner,
            new Panel($"[white]{Markup.Escape(scene)}[/]")
                .Header("[red]Final scene[/]")
                .Border(BoxBorder.Double)
                .Expand());

        var right = new Panel(summaryTable)
            .Header("[blue]Game summary[/]")
            .Border(BoxBorder.Heavy)
            .Expand();

        AnsiConsole.Write(new Columns(left, right).Expand());
    }

    public bool AskPlayAgain()
    {
        return AnsiConsole.Confirm("[bold]Return to menu?[/]", true);
    }

    public void ShowWordStatistics(List<WordStatistics> statistics)
    {
        AnsiConsole.Clear();

        var table = new Table().Border(TableBorder.Rounded).Expand();
        table.AddColumn("[yellow]Word[/]");
        table.AddColumn("[yellow]Played[/]");
        table.AddColumn("[yellow]Wins[/]");
        table.AddColumn("[yellow]Losses[/]");
        table.AddColumn("[yellow]Win rate[/]");

        foreach (WordStatistics item in statistics.OrderBy(x => x.Word))
        {
            table.AddRow(
                Markup.Escape(item.Word),
                item.GamesPlayed.ToString(),
                item.Wins.ToString(),
                item.Losses.ToString(),
                $"{item.WinRate:F1}%");
        }

        if (statistics.Count == 0)
        {
            table.AddRow("-", "0", "0", "0", "0.0%");
        }

        AnsiConsole.Write(new Rows(
            new FigletText("Stats").Color(Color.MediumPurple),
            new Panel(table)
                .Header("[purple]Word statistics[/]")
                .Border(BoxBorder.Rounded)
        ));
    }

    public void ShowDecryptedWords(List<WordEntry> words, string filePath)
    {
        AnsiConsole.Clear();

        var table = new Table().Border(TableBorder.Rounded).Expand();
        table.AddColumn("[yellow]Difficulty[/]");
        table.AddColumn("[yellow]Word[/]");

        foreach (WordEntry word in words.OrderBy(w => w.Difficulty).ThenBy(w => w.Value))
        {
            table.AddRow(word.Difficulty.ToString(), Markup.Escape(word.Value));
        }

        if (words.Count == 0)
        {
            table.AddRow("-", "No words available");
        }

        AnsiConsole.Write(new Rows(
            new FigletText("Words").Color(Color.Aqua),
            new Panel(table)
                .Header("[blue]Decrypted word list[/]")
                .Border(BoxBorder.Rounded),
            new Panel($"[grey]A plain-text copy is also saved to:[/]\n[bold]{Markup.Escape(filePath)}[/]")
                .Header("[green]TXT export[/]")
                .Border(BoxBorder.Double)
        ));
    }

    private static void ShowInvalidMenuChoice()
    {
        AnsiConsole.Write(
            new Panel("[red]Invalid menu key. Please choose 0, 1, 2, 3, 4 or 5.[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Red)));
        Thread.Sleep(700);
    }

    private static string BuildClassicScene(int wrongAttempts)
    {
        var canvas = new char[Rows, Cols];
        int ropeColumn = PostColumn + BeamLength;

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Cols; column++)
            {
                canvas[row, column] = ' ';
            }
        }

        canvas[BeamRow, PostColumn] = LeftCorner;
        canvas[BeamRow, ropeColumn] = RightCorner;

        for (int x = PostColumn + 1; x < ropeColumn; x++)
        {
            canvas[BeamRow, x] = '-';
        }

        for (int row = BeamRow + 1; row < Rows - 1; row++)
        {
            canvas[row, PostColumn] = '|';
        }

        canvas[BeamRow + 1, ropeColumn] = '|';

        for (int x = 0; x < Cols; x++)
        {
            canvas[Rows - 1, x] = '=';
        }

        int baseRow = Rows - 1;
        if (PostColumn - 1 >= 0)
        {
            canvas[baseRow - 1, PostColumn - 1] = '/';
        }

        canvas[baseRow - 1, PostColumn] = '|';

        if (PostColumn + 1 < Cols)
        {
            canvas[baseRow - 1, PostColumn + 1] = '\\';
        }

        DrawBody(canvas, wrongAttempts, ropeColumn);

        var sb = new StringBuilder();
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Cols; column++)
            {
                sb.Append(canvas[row, column]);
            }

            sb.AppendLine();
        }

        foreach (string crowdLine in BuildSpectators(wrongAttempts))
        {
            sb.AppendLine(crowdLine);
        }

        return sb.ToString().TrimEnd();
    }

    private static void DrawBody(char[,] canvas, int wrongAttempts, int ropeColumn)
    {
        int wrong = Math.Clamp(wrongAttempts, 0, 6);

        for (int i = 1; i <= wrong; i++)
        {
            int row;
            int column;
            char value;

            switch (i)
            {
                case 1:
                    row = 2; column = ropeColumn; value = 'O';
                    break;
                case 2:
                    row = 3; column = ropeColumn; value = '|';
                    break;
                case 3:
                    row = 3; column = ropeColumn - 1; value = '/';
                    break;
                case 4:
                    row = 3; column = ropeColumn + 1; value = '\\';
                    break;
                case 5:
                    row = 4; column = ropeColumn - 1; value = '/';
                    break;
                default:
                    row = 4; column = ropeColumn + 1; value = '\\';
                    break;
            }

            if (row >= 0 && row < Rows && column >= 0 && column < Cols)
            {
                canvas[row, column] = value;
            }
        }
    }

    private static IEnumerable<string> BuildSpectators(int wrongAttempts)
    {
        int stage = Math.Clamp(wrongAttempts, 0, 6);

        string leftFace = stage switch
        {
            <= 1 => "(^_^)",
            2 => "(o_o)",
            3 or 4 => "(O_O)",
            _ => "(T_T)"
        };

        string centerFace = stage switch
        {
            0 => "\\o/",
            1 => "\\o ",
            2 => " o ",
            3 => "_o_",
            4 => "\\o/",
            5 => " x ",
            _ => "x_x"
        };

        string rightFace = stage switch
        {
            <= 1 => "(^_^)",
            2 => "(o_o)",
            3 or 4 => "(O_O)",
            _ => "(T_T)"
        };

        return new[]
        {
            $" {leftFace}   {centerFace,-5}   {rightFace}",
            stage >= 4 ? "   /|\\      |      /|\\  " : CrowdLines[1],
            CrowdLines[2]
        };
    }

    private static string BuildAlphabetMarkup(GameSession session)
    {
        var letters = Enumerable.Range('a', 26)
            .Select(code => (char)code)
            .Select(ch =>
            {
                if (session.CorrectLetters.Contains(ch))
                {
                    return $"[black on green] {ch} [/]";
                }

                if (session.UsedLetters.Contains(ch))
                {
                    return $"[white on red] {ch} [/]";
                }

                return $"[grey] {ch} [/]";
            })
            .Chunk(7)
            .Select(chunk => string.Join(" ", chunk));

        return string.Join(Environment.NewLine, letters);
    }

    private static Color ParseColor(string style)
    {
        return style.ToLowerInvariant() switch
        {
            "green" => Color.Green,
            "red" => Color.Red,
            "yellow" => Color.Yellow,
            _ => Color.Grey
        };
    }
}
