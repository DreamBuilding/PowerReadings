using System;

namespace Converters
{
    internal static class EnumExtension
    {
        public static T GetValueFromCompactedName<T>(string name)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                //compare the name with extra spaces and brackets removed and case insensitive
                if (field.Name.Equals(name.Replace(" ", "").Replace("(", "").Replace(")", ""), StringComparison.InvariantCultureIgnoreCase))
                    return (T)field.GetValue(null);
            }
            throw new ArgumentException("Not found.", nameof(name));
        }
    }
}
