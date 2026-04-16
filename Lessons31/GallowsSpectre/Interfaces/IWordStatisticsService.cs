using GallowsSpectre.Models;

namespace GallowsSpectre.Interfaces;

public interface IWordStatisticsService
{
    void RegisterGame(string word, bool isWin);
    List<WordStatistics> GetAll();
}
