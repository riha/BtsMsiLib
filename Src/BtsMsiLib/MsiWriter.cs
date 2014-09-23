using System.IO;
using BtsMsiLib.Model;

namespace BtsMsiLib
{
    public class MsiWriter:IMsiWriter
    {
        public Stream Write(BtsApplication btsApplication, Resource[] resources)
        {
            BtsApplicationValidator.Validate(btsApplication);

            ResourceValidator.Validate(resources);

            // TODO: Add handling of the referenced BT applications

     

            return null;
        }
    }

    
}
