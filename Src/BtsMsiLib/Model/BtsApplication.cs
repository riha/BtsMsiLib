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
    }
}
