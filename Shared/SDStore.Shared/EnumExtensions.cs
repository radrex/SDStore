namespace SDStore.Shared
{
    using System.Reflection;
    using System.ComponentModel.DataAnnotations;
    
    public static class EnumExtensions
    {
        /// <summary>
        /// If <paramref name="Enum"/> has <see cref="DisplayAttribute"/> defined, this will return <see cref="DisplayAttribute.Name"/>. Otherwise, <see langword="null" /> will be returned.
        /// </summary>
        /// <returns><see cref="string"/> containing <see cref="DisplayAttribute.Name"/> if defined. Otherwise, will return <see langword="null" /></returns>
        public static string? GetDisplayName<T>(this T Enum) where T : Enum
            => Enum.GetEnumAttribute<T, DisplayAttribute>()?.Name;
        
        public static TAttribute? GetEnumAttribute<TEnum, TAttribute>(this TEnum Enum)
            where TEnum : Enum
            where TAttribute : Attribute
        {
            MemberInfo[]? MemberInfo = typeof(TEnum).GetMember(Enum.ToString());
            return MemberInfo[0].GetCustomAttribute<TAttribute>();
        }
    }
}