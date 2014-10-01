using System;
using System.IO;
using BtsMsiLib.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BtsMsiLib.IntegrationTests
{
    [TestClass]
    public class BtsMsiLibTests
    {
        [TestMethod]
        public void MinimalisticBtsResourceTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Schemas.dll", ResourceType.BtsResource);

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource });

            using (var destinationFile = File.Create("MinimalisticBtsResourceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void MinimalisticResourceTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Utilities.dll", ResourceType.Resource);

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource });

            using (var destinationFile = File.Create("MinimalisticResourceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void MinimalisticMixedResourceTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Schemas.dll", ResourceType.BtsResource);
            var resource = new Resource(@"..\..\..\Data\BtsSample.Utilities.dll", ResourceType.Resource);

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource, resource });

            using (var destinationFile = File.Create("MinimalisticMixedResourceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }
    }
}
