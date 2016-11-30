using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BtsMsiLib.Msi
{
    public class MsiFileWriter
    {
        public static string Write(BtsMsiLib.Model.MsiVersion version)
        {
            var destinationPath = Path.GetTempPath();
            var destinationFilePath = Path.Combine(destinationPath, Path.GetTempFileName());

            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            string btVersionFolderName = string.Format("Msi.{0}", version);
            var asssembly = Assembly.GetExecutingAssembly();
            const string msiName = "ApplicationTemplate.msi";

            var resourcePath = string.Concat(asssembly.GetName().Name, ".", btVersionFolderName, ".", msiName);

            using (var stream = asssembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                    throw new ApplicationException(string.Format("Could not create MSI template from {0} and resource path {1}", msiName, resourcePath));
            
                using (var reader = new BinaryReader(stream))
                using (var output = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                        output.WriteByte(reader.ReadByte());
                }
            }
            return destinationFilePath;
        }

        public static IDictionary<string, object> GetProperties(string productName, Guid productCode, Guid upgradeCode, string productVersion, string manufacturer)
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            
            dictionary.Add("ProductName", productName);
            dictionary.Add("ProductVersion", productVersion);
            dictionary.Add("ProductCode", productCode.ToString("B").ToUpperInvariant());
            dictionary.Add("UpgradeCode", upgradeCode.ToString("B").ToUpperInvariant());
            dictionary.Add("Manufacturer", manufacturer);
            dictionary.Add("Publisher", manufacturer);
            dictionary.Add("ALLUSERS", 1);
            dictionary.Add("ARPNOREPAIR", 1);
            dictionary.Add("AgreeToLicense", 1);
            dictionary.Add("ApplicationUsers", "AllUsers");
            dictionary.Add("FolderForm_AllUsersVisible", 0);
            dictionary.Add("ARPSYSTEMCOMPONENT", 1);

            return dictionary;
        }
    }
}
