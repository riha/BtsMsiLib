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
        public ResourceType Type { get; private set; }
        public string ShortCabinetName { get; internal set; }
        public string SourceLocation { get; set; }

        public Resource(string filePath, ResourceType type)
        {
            FilePath = filePath;
            Type = type;
            switch (type)
            {
                case ResourceType.BtsResource:
                    var btsassembly = Assembly.LoadFrom(filePath);
                    FullName = btsassembly.GetName().FullName;
                    break;
                case ResourceType.Resource:
                    var assembly = Assembly.LoadFrom(filePath);
                    FullName = assembly.GetName().FullName;
                    break;
                case ResourceType.File:
                    FullName = System.IO.Path.GetFileName(filePath);
                    break;
                case ResourceType.Binding:
                    FullName = System.IO.Path.GetFileName(filePath);
                    break;
                default:
                    throw new NotImplementedException("Unsupported resource type. Currently only File, BizTalkAssemblies and Assemblies are supported.");
            }
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
                case ResourceType.File:
                    return "System.BizTalk:File";
                case ResourceType.Binding:
                    return "System.BizTalk:BizTalkBinding";
            }

            throw new NotImplementedException("Unsupported resource type. Currently only File, BizTalkAssemblies and Assemblies are supported.");
        }

        internal string GetResourceFolder(string applicationFolder)
        {
            var name = GetTypeName(Type);

            string resourceType = name.Substring(name.IndexOf(':') + 1);
            string resourceTypeFolder = GetResourceTypeFolder(applicationFolder, resourceType);
            string filename = "";
            if (resourceType == "BizTalkAssembly" || resourceType == "Assembly")
            {
                filename = GetLuidHash(FullName);    
            }
            else
            {
                filename = FullName;
            }
            
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
