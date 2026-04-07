namespace SpaceShipLoader.Models;

public class SystemStatus
{
    public bool NavigationOnline { get; set; }
    public bool LifeSupportOnline { get; set; }
    public bool ReactorStable { get; set; }
    public bool CommunicationOnline { get; set; }

    public bool IsAllSystemsNormal()
    {
        return NavigationOnline
            && LifeSupportOnline
            && ReactorStable
            && CommunicationOnline;
    }
}
