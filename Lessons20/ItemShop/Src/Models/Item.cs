using ItemShop.Src.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemShop.Src.Models
{
    public struct Item
    {
        public string Name;
        public int Quantity;
        public double Price;
        public ItemCategory Category;
        public int LevelRequired;

        public Item(string name, int quantity, double price, ItemCategory category, int levelRequired)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Category = category;
            LevelRequired = levelRequired;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Quantity: {Quantity}, Price: {Price}, Category: {Category}, Level Required: {LevelRequired}";
        }
    }
}
