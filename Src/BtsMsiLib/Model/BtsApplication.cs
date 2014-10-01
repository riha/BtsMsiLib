using System.Linq;

namespace BtsMsiLib.Model
{
    public class BtsApplication
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public string[] ReferencedApplications { get; set; }

        public BtsApplication(string name)
        {
            Name = name;
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
