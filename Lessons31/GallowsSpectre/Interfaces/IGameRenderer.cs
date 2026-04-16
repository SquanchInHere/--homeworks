using GallowsSpectre.Enums;
using GallowsSpectre.Models;
using GallowsSpectre.Records;

namespace GallowsSpectre.Interfaces;

public interface IGameRenderer
{
    MainMenuAction ShowMainMenu();
    void RenderGame(GameSession session);
    string AskGuess();
    void ShowMessage(string message, string style = "yellow");
    void ShowSummary(GameSummaryRecord summary);
    bool AskPlayAgain();
    void ShowWordStatistics(List<WordStatistics> statistics);
    void ShowDecryptedWords(List<WordEntry> words, string filePath);
}
