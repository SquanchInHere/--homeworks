using GallowsSpectre.Enums;
using GallowsSpectre.Models;

namespace GallowsSpectre.Interfaces;

public interface IWordRepository
{
    List<WordEntry> GetAll();
    string GetRandomWord(DifficultyLevel difficulty);
}
