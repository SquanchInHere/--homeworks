using ItemShop.Src.Enums;
using ItemShop.Src.Models;

namespace ItemShop.Src.DataSeed
{
    public static class DataSeed
    {
        public static Dictionary<string, Item> GetItems()
        {
            return new Dictionary<string, Item>
            {
                { "HP Potion", new Item("HP Potion", 10, 50, ItemCategory.HP, 1) },
                { "MP Potion", new Item("MP Potion", 5, 60, ItemCategory.MP, 2) },
                { "Speed Buff", new Item("Speed Buff", 3, 120, ItemCategory.Buff, 4) },
                { "Fire Scroll", new Item("Fire Scroll", 2, 200, ItemCategory.Scroll, 5) },
                { "Iron Sword", new Item("Iron Sword", 1, 500, ItemCategory.Weapon, 3) }
            };
        }
    }
}
