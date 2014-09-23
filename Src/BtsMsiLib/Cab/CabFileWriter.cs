using System;
using System.Collections.Generic;
using System.IO;
using BtsMsiLib.Model;
using Microsoft.Deployment.Compression.Cab;

namespace BtsMsiLib.Cab
{
    internal class CabFileWriter
    {
        /// <summary>
        /// Copy each resource from its destination to a temp Cab folder. Each resource then is packed into a separate Cab-file.
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        internal string Write(Resource[] resources)
        {
            var index = 0;

            var tempCabFolderPath = string.Concat(Path.GetTempPath(), Guid.NewGuid());

            if (!Directory.Exists(tempCabFolderPath))
                Directory.CreateDirectory(tempCabFolderPath);

            foreach (var resource in resources)
            {
                var resourceTempFolderPath = Path.GetTempPath() + Guid.NewGuid();
                var resourceFolder = resource.GetResourceFolder(resourceTempFolderPath);

                if (!Directory.Exists(resourceFolder))
                    Directory.CreateDirectory(resourceFolder);

                // We have checked resource.FilePath for possible null values as we received the input parameters
                File.Copy(resource.FilePath, Path.Combine(resourceFolder, Path.GetFileName(resource.FilePath)));

                var cabFileName = string.Format("ITEM~{0}.cab", index);

                resource.ShortCabinetName = cabFileName;

                index = index + 1;

                var cabInfo = new CabInfo(Path.Combine(tempCabFolderPath, cabFileName));
                cabInfo.Pack(resourceTempFolderPath, true, Microsoft.Deployment.Compression.CompressionLevel.Normal, null);
            }

            return tempCabFolderPath;
        }
    }
}
