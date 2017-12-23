using System.Text;

namespace Recipes.App.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveMoreThenOneSpace(this string value)
        {
            var result = new StringBuilder(value.Length);
            var wasLastCharacterASpace = false;
            foreach (var character in value)
            {
                if (character != ' ')
                {
                    wasLastCharacterASpace = false;
                }
                else
                {
                    if(wasLastCharacterASpace)
                    {
                        continue;
                    }

                    wasLastCharacterASpace = true;
                }
                result.Append(character);
            }

            return result.ToString();
        }
    }
}