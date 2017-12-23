using System;
using System.Net;
using System.Threading.Tasks;

namespace Recipes.App.Importing
{
    internal class HtmlLoader : IHtmlLoader
    {
        public Task<string> Load(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadStringTaskAsync(url);
            }
        }
    }
}
