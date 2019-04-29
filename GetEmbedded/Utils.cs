using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GetEmbedded
{
    public static class Utils
    {
        public static void ExtractResources(string targetFolder, string assemblyPath, string resourceName = null)
        {
            var assembly = Assembly.LoadFile(assemblyPath);
            var assemblyName = assembly.GetName();
            var length = assemblyName.Name.Length + 1;
            string[] resourceNames = null;
            if (!string.IsNullOrWhiteSpace(resourceName))
            {
                resourceNames = new string[] { resourceName };
            }
            else
            {
                resourceNames = assembly.GetManifestResourceNames();
            }
            foreach(var resoure in resourceNames)
            {
                using (var resourceStream = assembly.GetManifestResourceStream(resoure))
                {
                    var resourcePath = resoure.Substring(length);
                    var parts = resourcePath.Split('.');
                    var sb = new StringBuilder();
                    if (parts.Length > 1)
                    {
                        for (var i = 0; i < parts.Length - 1; i++)
                        {
                            if (i > 0)
                            {
                                sb.Append(Path.DirectorySeparatorChar);
                            }
                            sb.Append(parts[i]);
                        }
                        sb.Append(".");
                    }
                    sb.Append(parts[parts.Length - 1]);
                    var targetName = Path.Combine(targetFolder, sb.ToString());
                    var endFolder = Path.GetDirectoryName(targetName);
                    if (!Directory.Exists(endFolder))
                    {
                        Directory.CreateDirectory(endFolder);
                    }
                    using (var outputStream = new FileStream(targetName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        resourceStream.CopyTo(outputStream);
                        outputStream.Flush();
                    }
                }

            }
        }
    }
}
