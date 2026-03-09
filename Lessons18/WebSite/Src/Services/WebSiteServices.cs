using WebSite.Src.Models;

namespace WebSite.Src.Services
{
    public class WebSiteServices
    {
        public void InputData(WebSiteModel website)
        {
            Console.Write("Enter website name: ");
            website.Name = Console.ReadLine()!;

            Console.Write("Enter website path: ");
            website.Path = Console.ReadLine()!;

            Console.Write("Enter website description: ");
            website.Description = Console.ReadLine()!;

            Console.Write("Enter IP address: ");
            website.IpAddress = Console.ReadLine()!;
        }

        public void ShowData(WebSiteModel website)
        {
            Console.WriteLine("\n--- Website Information ---");
            Console.WriteLine("Name: " + website.Name);
            Console.WriteLine("Path: " + website.Path);
            Console.WriteLine("Description: " + website.Description);
            Console.WriteLine("IP Address: " + website.IpAddress);
        }
    }
}
