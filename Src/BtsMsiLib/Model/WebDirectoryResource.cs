using BtsMsiLib.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtsMsiLib.Model
{
    //TODO: Hade det kanske varit vettigare att göra om dessa till interface och sedan ha några metoder i en helper istället?
    public class WebDirectoryResource : Resource
    {
        public string ServiceName { get; set; }
        public string PhysicalPath { get; set; }
        public string AspNetVersion { get; set; }
        public string IISProcessMode { get; set; }
        public List<string> FilePaths { get; set; }
        public string DirectoryPath { get; set; }

        public override string TypeName { get { return "System.BizTalk:WebDirectory"; } }
        public override string Luid { get { return ServiceName; } }
        public override string Path { get; set; }

        public WebDirectoryResource(string serviceName, string path)
        {
            Path = path;
            ServiceName = serviceName;
            FilePaths = new List<string>();

            // TODO en massa validering
            if (!Directory.Exists(path))
            {
                throw new InvalidProgramException("Finner inte katalof");
            }

            FindFiles(path);
        }

        private void FindFiles(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                FilePaths.Add(file);
            }

            foreach (var dir in Directory.GetDirectories(path))
            {
                FindFiles(dir);
            }
        }

        internal override string GetResourceFolder(string applicationFolder)
        {
            var resourceTypeFolder = base.GetResourceTypeFolder(applicationFolder);

            string filename = "-" + Luid;
            string luidFilename = FileHelper.GetLuidFilename(filename);

            return System.IO.Path.Combine(resourceTypeFolder, luidFilename);
        }

        internal override void CopyFilesTo(string destinationFolder)
        {
            foreach (var filePath in FilePaths)
            {
                File.Copy(filePath, System.IO.Path.Combine(destinationFolder, System.IO.Path.GetFileName(filePath)));
            }
        }
    }
}
