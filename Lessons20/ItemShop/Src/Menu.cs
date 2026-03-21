using ItemShop.Src.Enums;
using ItemShop.Src.Models;
using ItemShop.Src.Services;

namespace ItemShop.Src
{
    public static class Menu
    {
        public static void Run(ItemShopService shop)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n=== GAME ITEM SHOP MENU ===");
                Console.WriteLine("1. Show all items");
                Console.WriteLine("2. Add item");
                Console.WriteLine("3. Update item");
                Console.WriteLine("4. Remove item");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        shop.ShowItems();
                        break;

                    case "2":
                        AddItemMenu(shop);
                        break;

                    case "3":
                        UpdateItemMenu(shop);
                        break;

                    case "4":
                        RemoveItemMenu(shop);
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("Exiting the program...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void AddItemMenu(ItemShopService shop)
        {
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();

            int quantity = ReadInt("Enter quantity: ");
            double price = ReadDouble("Enter price: ");
            ItemCategory category = ReadCategory();
            int levelRequired = ReadInt("Enter required level: ");

            Item item = new Item(name, quantity, price, category, levelRequired);
            shop.AddItem(item);
        }

        static void UpdateItemMenu(ItemShopService shop)
        {
            Console.Write("Enter the name of the item to update: ");
            string oldName = Console.ReadLine();

            Console.Write("Enter new item name: ");
            string newName = Console.ReadLine();

            int quantity = ReadInt("Enter new quantity: ");
            double price = ReadDouble("Enter new price: ");
            ItemCategory category = ReadCategory();
            int levelRequired = ReadInt("Enter new required level: ");

            Item updatedItem = new Item(newName, quantity, price, category, levelRequired);
            shop.UpdateItem(oldName, updatedItem);
        }

        static void RemoveItemMenu(ItemShopService shop)
        {
            Console.Write("Enter item name to remove: ");
            string name = Console.ReadLine();
            shop.RemoveItem(name);
        }

        static int ReadInt(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value) && value >= 0)
                    return value;

                Console.WriteLine("Invalid number. Try again.");
            }
        }

        private static double ReadDouble(string message)
        {
            double value;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out value) && value >= 0)
                    return value;

                Console.WriteLine("Invalid number. Try again.");
            }
        }

        private static ItemCategory ReadCategory()
        {
            while (true)
            {
                Console.WriteLine("Choose category:");
                foreach (var category in Enum.GetValues(typeof(ItemCategory)))
                {
                    Console.WriteLine($"{(int)category} - {category}");
                }

                Console.Write("Enter category number: ");
                if (int.TryParse(Console.ReadLine(), out int categoryNumber) && Enum.IsDefined(typeof(ItemCategory), categoryNumber))
                {
                    return (ItemCategory)categoryNumber;
                }

                Console.WriteLine("Invalid category. Try again.");
            }
        }

    }
}
