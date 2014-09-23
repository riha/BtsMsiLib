using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using BtsMsiLib.Utilities;

namespace BtsMsiLib.Model
{
    public class Resource
    {
        public string FilePath { get; set; }
        public string FullName { get; private set; }
        public ResourceType Type { get; set; }
        public string ShortCabinetName { get; set; }
        public string SourceLocation { get; set; }

        public Resource(string filePath)
        {
            FilePath = filePath;
            var assembly = Assembly.LoadFrom(filePath);

            FullName = assembly.GetName().FullName;
        }

        internal static string GetLuidHash(string luid)
        {
            var assemblyName = new AssemblyName(luid);
            byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
            var stringBuilder = new StringBuilder();

            foreach (byte num in publicKeyToken)
                stringBuilder.Append(num.ToString("x2", CultureInfo.InvariantCulture));

            string hash = Hasher.HashAssemblyFullyQualifiedName(assemblyName.Name, assemblyName.Version + "-" + stringBuilder).ToString("N");

            return hash;
        }


        public static string GetTypeName(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.BtsResource:
                    return "System.BizTalk:BizTalkAssembly";
                case ResourceType.Resource:
                    return "System.BizTalk:Assembly";
            }

            throw new NotImplementedException("Unsupported resource type. Currently only BizTalkAssemblies and Assemblies are supported.");
        }

        internal string GetResourceFolder(string applicationFolder)
        {
            var name = GetTypeName(Type);

            string resourceType = name.Substring(name.IndexOf(':') + 1);
            string resourceTypeFolder = GetResourceTypeFolder(applicationFolder, resourceType);
            string filename = GetLuidHash(FullName);
            string luidFilename = FileHelper.GetLuidFilename(filename);

            return Path.Combine(resourceTypeFolder, luidFilename);
        }

        internal string GetResourceTypeFolder(string applicationFolder, string resourceType)
        {
            string validFilename = FileHelper.GetValidFilename(resourceType.Substring(resourceType.IndexOf(':') + 1));

            return Path.Combine(applicationFolder, validFilename);
        }
    }
}
