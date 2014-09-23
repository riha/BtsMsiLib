using System.IO;
using BtsMsiLib.Cab;
using BtsMsiLib.Model;

namespace BtsMsiLib
{
    public class MsiWriter : IMsiWriter
    {
        public Stream Write(BtsApplication btsApplication, Resource[] resources)
        {
            BtsApplicationValidator.Validate(btsApplication);

            ResourceValidator.Validate(resources);

            // TODO: Add handling of the referenced BT applications

            var cabFileWriter = new CabFileWriter();
            var cabFolderPath = cabFileWriter.Write(resources);

            return null;
        }
    }


}
