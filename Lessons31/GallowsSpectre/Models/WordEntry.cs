using GallowsSpectre.Enums;

namespace GallowsSpectre.Models;

public class WordEntry
{
    public string Value { get; set; } = string.Empty;
    public DifficultyLevel Difficulty { get; set; }
}
