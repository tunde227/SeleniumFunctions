using System;
using System.IO;
using System.Reflection;

namespace Backend.Utility
{
    public static class FileUtils
    {
        public static string ReadFromFile(Assembly assembly, string filename)
        {
            try
            {
                using (var stream = assembly.GetManifestResourceStream(filename))
                {
                    using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                             "Unable To find file:" +
                                                             $" {filename} in {assembly.GetName().Name} Assembly.")))
                    {
                        return reader.ReadToEnd().Trim();
                    }
                }
            }
            catch (Exception e)
            {
                throw new FileLoadException($"Error reading from file: {e.Message}{e.StackTrace}");
            }
        }
    }
}