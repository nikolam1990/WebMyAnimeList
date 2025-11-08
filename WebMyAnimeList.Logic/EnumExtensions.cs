using System.ComponentModel;

namespace WebMyAnimeList.Logic;

public static class EnumExtensions
{
    public static string Description<T>(this T value)
    {
        var fi = value!.GetType().GetField(value.ToString()!);
        var attributes = (DescriptionAttribute[])fi!.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;

        return value.ToString()!;
    }
}
