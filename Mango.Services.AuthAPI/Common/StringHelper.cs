namespace Mango.Services.AuthAPI.Common
{
    public static class StringHelper
    {
        public static bool EqualsNoCase(this string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        public static bool abc(string name)
        {
            string ab = "asdasdas";

            return ab.EqualsNoCase(name);
        }
    }
}
