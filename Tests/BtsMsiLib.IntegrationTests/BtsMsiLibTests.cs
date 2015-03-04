using System;
using System.IO;
using BtsMsiLib.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BtsMsiLib.IntegrationTests
{
    [TestClass]
    public class BtsMsiLibTests
    {
        [TestMethod]
        public void MinimalisticBtsResourceTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new BtsAssemblyResource(@"..\..\..\Data\BtsSample.Schemas.dll");

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource });

            using (var destinationFile = File.Create(@"..\..\..\Export\MinimalisticBtsResourceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void MinimalisticResourceTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new BtsAssemblyResource(@"..\..\..\Data\BtsSample.Utilities.dll");

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource });

            using (var destinationFile = File.Create(@"..\..\..\Export\MinimalisticResourceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void MinimalisticMixedResourceTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new BtsAssemblyResource(@"..\..\..\Data\BtsSample.Schemas.dll");
            var resource = new AssemblyResource(@"..\..\..\Data\BtsSample.Utilities.dll");

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new Resource[] { btsSchemaResource, resource });

            using (var destinationFile = File.Create(@"..\..\..\Export\MinimalisticMixedResourceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void DescriptionTest()
        {
            var btsApplication = new BtsApplication("App1") { Description = "Test desc" };
            var btsSchemaResource = new AssemblyResource(@"..\..\..\Data\BtsSample.Schemas.dll");

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource });

            using (var destinationFile = File.Create(@"..\..\..\Export\DescriptionTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void BtsApplicationReferenceTest()
        {
            var btsApplication = new BtsApplication("App1") { ReferencedApplications = new[] { "App2, App3" } };
            var btsSchemaResource = new AssemblyResource(@"..\..\..\Data\BtsSample.Schemas.dll");

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource });

            using (var destinationFile = File.Create(@"..\..\..\Export\BtsApplicationReferenceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void SourceLocationTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new BtsAssemblyResource(@"..\..\..\Data\BtsSample.Schemas.dll") { SourceLocation = @"c:\test1" };
            var resource = new AssemblyResource(@"..\..\..\Data\BtsSample.Utilities.dll") { SourceLocation = @"c:\test2" };

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new Resource[] { btsSchemaResource, resource });

            using (var destinationFile = File.Create(@"..\..\..\Export\SourceLocationTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void WebDirectoryTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new BtsAssemblyResource(@"..\..\..\Data\BtsSample.Schemas.dll");
            var webDirectory = new WebDirectoryResource("SchemaSampleService", @"..\..\..\Data\SchemaSampleService\");
            
            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new Resource[] { btsSchemaResource, webDirectory });

            using (var destinationFile = File.Create(@"..\..\..\Export\WebDirectoryTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }
    }
}
