using BtsMsiLib.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BtsMsiLib.Model
{
    public class BtsAssemblyResource : Resource
    {
        //public string FilePath { get; set; }
        public string FullName { get; private set; }
        public override string TypeName { get { return "System.BizTalk:BizTalkAssembly"; } }
        public override string Luid { get { return FullName; } }
        public override string Path { get; set; }

        public BtsAssemblyResource(string filePath)
        {
            Path = filePath;

            var assembly = Assembly.LoadFrom(filePath);
            FullName = assembly.GetName().FullName;
        }

        internal override string GetResourceFolder(string applicationFolder)
        {
            var resourceTypeFolder = GetResourceTypeFolder(applicationFolder);

            string filename = GetLuidHash(Luid);
            string luidFilename = FileHelper.GetLuidFilename(filename);

            return System.IO.Path.Combine(resourceTypeFolder, luidFilename);
        }

        internal override void CopyFilesTo(string folder)
        {
            File.Copy(Path, System.IO.Path.Combine(folder, System.IO.Path.GetFileName(Path)));
        }
    }
}
