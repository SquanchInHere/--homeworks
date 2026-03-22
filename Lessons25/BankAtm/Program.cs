using BankAtm.Src.Data;
using BankAtm.Src.Models;
using BankAtm.Src.Services;
using BankAtm.Src;

Bank bank = DataSeed.CreateBank();
BankService bankService = new BankService(bank);
Menu bankMenu = new Menu(bankService);

bankMenu.Run();