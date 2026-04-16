using GallowsSpectre.Enums;
using GallowsSpectre.Models;

namespace GallowsSpectre.Helpers;

public static class WordSeedData
{
    public static List<WordEntry> GetDefaultWords()
    {
        return new List<WordEntry>
        {
            new() { Value = "apple", Difficulty = DifficultyLevel.Easy },
            new() { Value = "house", Difficulty = DifficultyLevel.Easy },
            new() { Value = "water", Difficulty = DifficultyLevel.Easy },
            new() { Value = "doctor", Difficulty = DifficultyLevel.Medium },
            new() { Value = "member", Difficulty = DifficultyLevel.Medium },
            new() { Value = "orange", Difficulty = DifficultyLevel.Medium },
            new() { Value = "biscuit", Difficulty = DifficultyLevel.Hard },
            new() { Value = "journey", Difficulty = DifficultyLevel.Hard },
            new() { Value = "monster", Difficulty = DifficultyLevel.Hard }
        };
    }
}
