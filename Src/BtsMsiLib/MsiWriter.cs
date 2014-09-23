using System;
using System.IO;
using BtsMsiLib.ApplicationDefinitionFile;
using BtsMsiLib.Cab;
using BtsMsiLib.Model;
using BtsMsiLib.Msi;
using Microsoft.Deployment.WindowsInstaller;

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

            var adfFileWriter = new AdfFileWriter();
            var adfFilePath = adfFileWriter.Write(btsApplication, resources);

            var destinationFilePath = Path.GetTempPath();
            MsiFileWriter.Write(destinationFilePath);

            var productCode = Guid.NewGuid();
            var upgradeCode = Guid.NewGuid();
            var properties = MsiFileWriter.GetProperties(btsApplication.Name, productCode, upgradeCode);
            using (var db = new Database(destinationFilePath, DatabaseOpenMode.Direct))
            {
                db.UpdateSummaryInfo();
                db.UpdateUpgradeTable(upgradeCode);
                db.UpdateProperties(properties);
                db.UpdateFileContent(cabFolderPath, adfFilePath, resources.Length);
                db.MakeCustomModifications(productCode, btsApplication.Name);
                db.Commit();
            }

            return File.Open(destinationFilePath, FileMode.Open);
        }
    }


}
