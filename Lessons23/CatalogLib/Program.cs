using CatalogLib.Src;
using CatalogLib.Src.Services;

CatalogService catalogService = new CatalogService();
CatalogConsoleService consoleService = new CatalogConsoleService(catalogService);
Menu.Run(consoleService);