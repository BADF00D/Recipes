using System;
using System.Threading.Tasks;

namespace Recipes.App.Importing
{
    internal interface IHtmlLoader
    {
        Task<string> Load(string url);
    }
}
