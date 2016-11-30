using System;
using System.IO;
using System.Linq;
using BtsMsiLib.ApplicationDefinitionFile;
using BtsMsiLib.Cab;
using BtsMsiLib.Model;
using BtsMsiLib.Msi;
using Microsoft.Deployment.WindowsInstaller;

namespace BtsMsiLib
{
    public class MsiWriter : IMsiWriter
    {
        public FileStream Write(BtsApplication btsApplication, Resource[] resources)
        {
            BtsApplicationValidator.Validate(btsApplication);
            ResourceValidator.Validate(resources);

            var cabFileWriter = new CabFileWriter();
            var cabFolderPath = cabFileWriter.Write(resources);

            btsApplication.AddDefaultApplicationReference();

            var adfFileWriter = new AdfFileWriter();
            var adfFilePath = adfFileWriter.Write(btsApplication, resources);

            var msiFilePath = MsiFileWriter.Write(btsApplication.MsiVersion);

            var productCode = Guid.NewGuid();
            var upgradeCode = Guid.NewGuid();
            var properties = MsiFileWriter.GetProperties(btsApplication.Name, productCode, upgradeCode, btsApplication.ProductVersion,btsApplication.Manufacturer);
            using (var db = new Database(msiFilePath, DatabaseOpenMode.Direct))
            {
                db.UpdateSummaryInfo(btsApplication);
                db.UpdateUpgradeTable(upgradeCode);
                db.UpdateProperties(properties);
                db.UpdateFileContent(cabFolderPath, adfFilePath, resources.Length);
                db.MakeCustomModifications(productCode, btsApplication.Name);
                db.Commit();
            }

            File.Delete(adfFilePath);
            Directory.Delete(cabFolderPath, true);

            return File.OpenRead(msiFilePath);
        }
    }
}
