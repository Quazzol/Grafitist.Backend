namespace Grafitist.Misc;

public class Check
{
    public static void NotNull<T>(T value, string valueName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(valueName);
        }
    }

    public static void NotNull<T>(T value, string valueName, string message)
    {
        if (value == null)
        {
            throw new ArgumentNullException(valueName, message);
        }
    }

    public static void NotNull<T>(T? value, string valueName)
        where T : struct
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(valueName);
        }
    }

    public static void NotNull<T>(T? value, string valueName, string message)
        where T : struct
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(valueName, message);
        }
    }
}
