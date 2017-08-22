using System;
using System.Linq;

namespace WikiImages.Infrastructure
{
    public static class StringExtensions
    {
        private const string Prefix = "File:";
        private static readonly string[] Extensions = { ".jpg", ".jpeg", ".png", ".svg", ".gif" };

        public static string ClearTitle(string value)
        {
            if (value.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                value = value.Substring(Prefix.Length);
            }
            var ending = Extensions.FirstOrDefault(o => value.EndsWith(o, StringComparison.InvariantCultureIgnoreCase));

            if (ending != null)
            {
                value = value.Remove(value.Length - ending.Length, ending.Length);
            }

            return value;
        }
    }
}
