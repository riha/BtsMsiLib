﻿using System.Linq;

namespace BtsMsiLib.Model
{
    public class BtsApplication
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public string[] ReferencedApplications { get; set; }

        public string Subject { get; set; }
        public string Authors { get; set; }
        public string Keywords{ get; set; }
        public string ProductVersion { get; set; }
        public string Manufacturer { get; set; }
        public MsiVersion MsiVersion { get; set; }
              
        public BtsApplication(string name)
        {
            Name = name;
            Keywords = "BizTalk, deployment, application";
            Subject = "";
            Authors = "";
            Description = "";
            ProductVersion = "1.0.0.0";
            Manufacturer = "Generated by BizTalk";
            MsiVersion = MsiVersion.BT2013;
        }

        internal void AddDefaultApplicationReference()
        {
            if (ReferencedApplications == null)
            {
                ReferencedApplications = new[] {"BizTalk.System"};
            }
            else
            {
                if (!ReferencedApplications.Contains("BizTalk.System"))
                    ReferencedApplications.ToList().Add("BizTalk.System");
            }
        }
    }
}
