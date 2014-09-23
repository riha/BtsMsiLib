using System;
using System.IO;
using System.Linq;
using BtsMsiLib.Model;

namespace BtsMsiLib
{
    public class ResourceValidator
    {
        public static void Validate(Resource[] resources)
        {
            if(resources == null || !resources.Any())
                throw new ArgumentNullException("resources", "Resources cannot be null");

            foreach (var resource in resources.Where(resource => File.Exists(resource.FilePath)))
            {
                throw new ArgumentException(string.Format("Resource with path {0} does not exists.", resource.FilePath));
            }
        }
    }
}
