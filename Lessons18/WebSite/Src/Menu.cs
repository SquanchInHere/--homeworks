using WebSite.Src.Models;
using WebSite.Src.Services;

namespace WebSite.Src
{
    public class Menu
    {
        public void Run()
        {
            WebSiteModel website = new WebSiteModel();
            WebSiteServices websiteService = new WebSiteServices();

            websiteService.InputData(website);
            websiteService.ShowData(website);

            Console.WriteLine("\nSeparate field access:");
            Console.WriteLine("Website name: " + website.Name);
            Console.WriteLine("Website path: " + website.Path);
        }
    }
}
