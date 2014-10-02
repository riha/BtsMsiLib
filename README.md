Library to create and pack resources in to BizTalk Server specific MSI files.

## Get started ##
Use NuGet to download the [BtsMsiLib package](https://www.nuget.org/packages/BtsMsiLib/).
 
	// Create a BizTalk application object with specific name
	var btsApplication = new BtsApplication("App1");
	
	// Add the resources you'd like to use. Can be both BizTalk reources and more general .NET assemblies
	var btsSchemaResource = new Resource(@"..\..\..\Data\BtsSample.Schemas.dll", ResourceType.BtsResource);
	
	// Create the MSI and get back a file stream
	var msiWriter = new MsiWriter();
	var file = msiWriter.Write(btsApplication, new[] { btsSchemaResource });
	
	// Write the file stream to disk
	using (var destinationFile = File.Create(@"..\..\..\Export\MinimalisticBtsResourceTest.msi"))
	{
		file.CopyTo(destinationFile);
	}
