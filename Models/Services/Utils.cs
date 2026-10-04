using System.Text.RegularExpressions;
public class Utils
{
    public static string ToSlug(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        return string.Empty;
        string slug = input.ToLowerInvariant();
        //đổi thành chữ thường
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        //xoá các ký tự đặc biệt
        slug = Regex.Replace(slug, @"[\s-]+", " ").Trim();
        //đổi nhiều khoảng trắng
        slug = Regex.Replace(slug, @"\s", "-");
        //đổi khoảng trắng thành “-”
        return slug;
    }
}