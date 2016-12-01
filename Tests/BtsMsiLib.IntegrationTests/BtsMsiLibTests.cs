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

            using (var destinationFile = File.Create(@"..\..\..\Export\MinimalisticBtsResourceTest.msi"))
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

            using (var destinationFile = File.Create(@"..\..\..\Export\MinimalisticResourceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }
        [TestMethod]
        public void MinimalisticBindingsTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Schemas.dll", ResourceType.BtsResource) { SourceLocation = @"c:\test1" };
            var btsBindingsResource = new Resource(@"..\..\..\Data\BtsSample.Dev.BindingInfo.xml", ResourceType.Binding) { SourceLocation = @"c:\test1" }; ;

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource, btsBindingsResource });

            using (var destinationFile = File.Create(@"..\..\..\Export\MinimalisticBindingsTest.msi"))
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

            using (var destinationFile = File.Create(@"..\..\..\Export\MinimalisticMixedResourceTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void DescriptionTest()
        {
            var btsApplication = new BtsApplication("App1") { Description = "Test desc" };
            var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Schemas.dll", ResourceType.BtsResource);

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
            var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Schemas.dll", ResourceType.BtsResource);

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
            var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Schemas.dll", ResourceType.BtsResource) { SourceLocation = @"c:\test1" };
            var resource = new Resource(@"..\..\..\Data\BtsSample.Utilities.dll", ResourceType.Resource) { SourceLocation = @"c:\test2" };

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource, resource });

            using (var destinationFile = File.Create(@"..\..\..\Export\SourceLocationTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }

        [TestMethod]
        public void FileTest()
        {
            var btsApplication = new BtsApplication("App1");
            var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Schemas.dll", ResourceType.BtsResource) { SourceLocation = @"c:\test1" };
            var fileResource = new Resource(@"..\..\..\Data\BtsSample.FileToAdd1.txt", ResourceType.File) { SourceLocation = @"c:\test2" };

            var msiWriter = new MsiWriter();
            var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource, fileResource });

            using (var destinationFile = File.Create(@"..\..\..\Export\FileTest.msi"))
            {
                file.CopyTo(destinationFile);
            }
        }
    }
}
