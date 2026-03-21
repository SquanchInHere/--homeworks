using ItemShop.Src.Models;

namespace ItemShop.Src.Services
{
    public class ItemShopService
    {
        private Dictionary<string, Item> items;

        public ItemShopService()
        {
            items = new Dictionary<string, Item>();
        }

        public ItemShopService(Dictionary<string, Item> items)
        {
            this.items = items;
        }

        public void AddItem(Item item)
        {
            if (items.ContainsKey(item.Name))
            {
                Console.WriteLine($"Item '{item.Name}' already exists.");
                return;
            }

            items.Add(item.Name, item);
        }

        public void RemoveItem(string name)
        {
            if (items.Remove(name))
            {
                Console.WriteLine($"Item '{name}' removed.");
            }
            else
            {
                Console.WriteLine($"Item '{name}' not found.");
            }
        }

        public void UpdateItem(string name, Item updatedItem)
        {
            if (items.ContainsKey(name))
            {
                items[name] = updatedItem;
                Console.WriteLine($"Item '{name}' updated.");
            }
            else
            {
                Console.WriteLine($"Item '{name}' not found.");
            }
        }

        public void ShowItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("The shop is empty.");
                return;
            }

            Console.WriteLine("Shop items:");
            foreach (KeyValuePair<string, Item> pair in items)
            {
                Console.WriteLine(pair.Value);
            }
        }
    }
}
