using System;
using System.Collections.Generic;
using System.Text;

namespace WebSite.Src.Models
{
    public class WebSiteModel
    {
        private string name;
        private string path;
        private string description;
        private string ipAddress;

        public WebSiteModel()
        {
            name = string.Empty;
            path = string.Empty;
            description = string.Empty;
            ipAddress = string.Empty;
        }

        public WebSiteModel(string name, string path, string description, string ipAddress)
        {
            this.name = name;
            this.path = path;
            this.description = description;
            this.ipAddress = ipAddress;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
    }
}
