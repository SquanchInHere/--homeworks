using GallowsSpectre.Enums;
using GallowsSpectre.Interfaces;
using GallowsSpectre.Models;
using GallowsSpectre.Records;

namespace GallowsSpectre.Services;

public class GameService : IGameService
{
    private readonly IWordRepository _wordRepository;
    private readonly IWordStatisticsService _wordStatisticsService;
    private readonly IGameRenderer _gameRenderer;
    private readonly string _decryptedWordsFilePath;

    public GameService(
        IWordRepository wordRepository,
        IWordStatisticsService wordStatisticsService,
        IGameRenderer gameRenderer,
        string decryptedWordsFilePath)
    {
        _wordRepository = wordRepository;
        _wordStatisticsService = wordStatisticsService;
        _gameRenderer = gameRenderer;
        _decryptedWordsFilePath = decryptedWordsFilePath;
    }

    public void Run()
    {
        while (true)
        {
            MainMenuAction action = _gameRenderer.ShowMainMenu();

            switch (action)
            {
                case MainMenuAction.StartEasy:
                    Play(DifficultyLevel.Easy);
                    break;
                case MainMenuAction.StartMedium:
                    Play(DifficultyLevel.Medium);
                    break;
                case MainMenuAction.StartHard:
                    Play(DifficultyLevel.Hard);
                    break;
                case MainMenuAction.ShowStatistics:
                    _gameRenderer.ShowWordStatistics(_wordStatisticsService.GetAll());
                    if (!_gameRenderer.AskPlayAgain())
                    {
                        return;
                    }
                    continue;
                case MainMenuAction.ShowDecryptedWords:
                    _gameRenderer.ShowDecryptedWords(_wordRepository.GetAll(), _decryptedWordsFilePath);
                    if (!_gameRenderer.AskPlayAgain())
                    {
                        return;
                    }
                    continue;
                default:
                    return;
            }

            _gameRenderer.ShowWordStatistics(_wordStatisticsService.GetAll());

            if (!_gameRenderer.AskPlayAgain())
            {
                return;
            }
        }
    }

    private void Play(DifficultyLevel difficulty)
    {
        string word = _wordRepository.GetRandomWord(difficulty);
        var session = new GameSession
        {
            Difficulty = difficulty,
            Word = word,
            MaxAttempts = word.Length + 2,
            StartedAtUtc = DateTime.UtcNow
        };

        while (!session.IsFinished)
        {
            _gameRenderer.RenderGame(session);
            string input = _gameRenderer.AskGuess();

            if (string.IsNullOrWhiteSpace(input))
            {
                _gameRenderer.ShowMessage("Input cannot be empty.", "red");
                continue;
            }

            input = input.Trim().ToLowerInvariant();

            if (!input.All(ch => ch is >= 'a' and <= 'z'))
            {
                _gameRenderer.ShowMessage("Use only English letters a-z.", "red");
                continue;
            }

            if (input.Length == 1)
            {
                ProcessLetterGuess(session, input[0]);
            }
            else
            {
                ProcessWordGuess(session, input);
            }
        }

        session.FinishedAtUtc = DateTime.UtcNow;
        _wordStatisticsService.RegisterGame(session.Word, session.IsWon);

        var summary = new GameSummaryRecord(
            session.IsWon,
            session.Word,
            session.GetUsedLettersText(),
            session.AttemptsUsed,
            session.MaxAttempts,
            session.WrongAttempts,
            session.Duration.ToString(@"mm\:ss")
        );

        _gameRenderer.ShowSummary(summary);
    }

    private void ProcessLetterGuess(GameSession session, char letter)
    {
        if (session.UsedLetters.Contains(letter))
        {
            _gameRenderer.ShowMessage($"Letter '{letter}' has already been used.", "yellow");
            return;
        }

        session.UsedLetters.Add(letter);
        session.AttemptsUsed++;

        if (session.Word.Contains(letter))
        {
            session.CorrectLetters.Add(letter);
            _gameRenderer.ShowMessage($"Correct letter: {letter}", "green");
        }
        else
        {
            session.WrongAttempts++;
            _gameRenderer.ShowMessage($"Wrong letter: {letter}", "red");
        }
    }

    private void ProcessWordGuess(GameSession session, string guessedWord)
    {
        session.AttemptsUsed++;

        if (guessedWord.Equals(session.Word, StringComparison.OrdinalIgnoreCase))
        {
            foreach (char ch in session.Word)
            {
                session.CorrectLetters.Add(ch);
            }

            _gameRenderer.ShowMessage("You guessed the whole word!", "green");
            return;
        }

        session.WrongAttempts++;
        _gameRenderer.ShowMessage("Wrong whole-word guess.", "red");
    }
}
