using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Recipes.App.Tests.Importing
{
    internal class EmbeddedRessources
    {
        public static string LoadString(string name)
        {
            try
            {
                var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(name);
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }catch(Exception exception)
            {
                var resourceNames = Assembly.GetExecutingAssembly()
                    .GetManifestResourceNames();
                Debug.WriteLine("Unable to load ressource. Available resources are:");
                foreach(var resourceName in resourceNames)
                {
                    Debug.WriteLine("   " + resourceName);
                }
                throw exception;
            }
        }
    }
}
