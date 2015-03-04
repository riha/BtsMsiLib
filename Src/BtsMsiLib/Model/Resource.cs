using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using BtsMsiLib.Utilities;

namespace BtsMsiLib.Model
{
    public abstract class Resource
    {
        public string ShortCabinetName { get; internal set; }
        public string SourceLocation { get; set; }

        public abstract string TypeName { get; }
        public abstract string Luid { get; }
        public abstract string Path { get; set; }

        internal static string GetLuidHash(string luid)
        {
            var assemblyName = new AssemblyName(luid);
            byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
            var stringBuilder = new StringBuilder();

            foreach (byte num in publicKeyToken)
            {
                stringBuilder.Append(num.ToString("x2", CultureInfo.InvariantCulture));
            }

            string hash = Hasher.HashAssemblyFullyQualifiedName(assemblyName.Name, assemblyName.Version + "-" + stringBuilder).ToString("N");

            return hash;
        }

        internal string GetResourceTypeFolder(string applicationFolder)
        {
            string resourceType = TypeName.Substring(TypeName.IndexOf(':') + 1);
            string validFilename = FileHelper.GetValidFilename(resourceType.Substring(resourceType.IndexOf(':') + 1));

            var resourceTypeFolder = System.IO.Path.Combine(applicationFolder, validFilename);

            return resourceTypeFolder;
        }

        internal abstract void CopyFilesTo(string folder);
        internal abstract string GetResourceFolder(string applicationFolder);
    }
}
