using System;
using System.IO;
using System.Linq;

namespace BtsMsiLib.Model
{
    public class AssemblyResourceValidator
    {
        public static void Validate(AssemblyResource[] resources)
        {
            if(resources == null || !resources.Any())
                throw new ArgumentNullException("resources", "Resources cannot be null");

            foreach (var resource in resources.Where(resource => !File.Exists(resource.Path)))
            {
                throw new ArgumentException(string.Format("Resource with path {0} does not exists.", resource.Path));
            }
        }
    }
}
