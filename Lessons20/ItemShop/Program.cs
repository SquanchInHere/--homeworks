using ItemShop.Src;
using ItemShop.Src.DataSeed;
using ItemShop.Src.Services;

Menu.Run(new ItemShopService(DataSeed.GetItems()));