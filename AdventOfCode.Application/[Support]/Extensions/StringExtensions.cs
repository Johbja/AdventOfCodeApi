using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode.Application.Extensions;
public static class StringExtensions
{
    private const string BrTag = "<br/>";

    public static string RemoveInitialSlashIfAny(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        if (value.StartsWith("/"))
            return value.Substring(1, value.Length - 1);

        return value;
    }

    public static string ReplaceLineBreakWithBrTag(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;

        return str
            .Replace("$", BrTag)
            .Replace("\r\n", BrTag)
            .Replace("\n", BrTag);
    }

    public static string MakeSureItEndsWithSlash(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "/";

        value = value.TrimEnd();
        if (value.EndsWith("/"))
            return value;

        return $"{value}/";
    }

    public static bool StartsWithIgnoreCase(this string value, string compareToValue)
    {
        return value.StartsWith(compareToValue, StringComparison.OrdinalIgnoreCase);
    }

    public static bool HasValue(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static long ToLongOrDefault(this string value, long defaultValue = 0)
    {
        if (long.TryParse(value, out var parsedValue))
        {
            return parsedValue;
        }

        return defaultValue;
    }

    public static int ToIntOrDefault(this string value, int defaultValue = 0)
    {
        if (int.TryParse(value, out var parsedValue))
        {
            return parsedValue;
        }

        return defaultValue;
    }

    public static double ToDoubleOrDefault(this string value, double defaultValue = 0D)
    {
        if (double.TryParse(value, out var parsedValue))
        {
            return parsedValue;
        }

        return defaultValue;
    }

    public static Guid ToGuidOrDefault(this string value, Func<Guid> defaultValueProvider = null)
    {
        defaultValueProvider ??= () => Guid.Empty;

        if (!string.IsNullOrWhiteSpace(value) && Guid.TryParse(value, out var parsedValue))
        {
            return parsedValue;
        }

        return defaultValueProvider();
    }

    public static bool EqualsIgnoreCase(this string left, string right)
    {
        return string.Equals(
            left,
            right,
            StringComparison.OrdinalIgnoreCase);
    }

    public static string ToTitleCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
    }

    public static string ReplaceLineBreakWithNAndR(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return value.Replace("$", "\r\n").Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("<br />", "\r\n");
    }

    public static bool IsEmailAddress(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        try
        {
            var matchTimeoutMilliseconds = 200;

            text = Regex.Replace(text, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(matchTimeoutMilliseconds));

            string DomainMapper(Match match)
            {
                var idn = new IdnMapping();

                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            var matchTimeoutMilliseconds = 250;
            return Regex.IsMatch(text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(matchTimeoutMilliseconds));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsUrl(this string text)
    {
        return Uri.TryCreate(text, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    public static bool StartsWithAny(this string text, params string[] possibleStartValues)
    {
        return possibleStartValues.Select(text.StartsWith).Any(x => x);
    }

}
