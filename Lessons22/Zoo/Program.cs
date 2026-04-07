using Zoo.Src.Data;
using Zoo.Src.Services;


ZooService zoo = DataSeed.CreateZoo();
MenuService.RunMenu(zoo);
