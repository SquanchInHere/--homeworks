namespace GallowsSpectre.Models;

public class WordStatistics
{
    public string Word { get; set; } = string.Empty;
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }

    public double WinRate => GamesPlayed == 0 ? 0 : (double)Wins / GamesPlayed * 100;
}
