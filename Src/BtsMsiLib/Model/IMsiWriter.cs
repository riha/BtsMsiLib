using System.IO;

namespace BtsMsiLib.Model
{
    public interface IMsiWriter
    {
        /// <summary>
        /// Main external entry point.
        /// </summary>
        /// <param name="btsApplication">Information on the BizTalk Application the MSI is target for. 
        /// Needs to include a name parameter</param>
        /// <param name="resources">A list of resources that should be added to the Msi.</param>
        /// <returns></returns>
        FileStream Write(BtsApplication btsApplication, Resource[] resources);
    }
}
