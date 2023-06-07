using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace learningGate.Extensions;

public static class HtmlExtentions
{
    public static IHtmlContent TruncateWords(this IHtmlHelper htmlHelper, string text, int maxWords)
    {
        var words = text.Split(' ');

        if (words.Length <= maxWords)
        {
            return new HtmlString(text);
        }
        else
        {
            var truncatedWords = words.Take(maxWords);
            var truncatedText = string.Join(" ", truncatedWords) + " ...";
            return new HtmlString(truncatedText);
        }
    }
    public static IHtmlContent TruncateWordsWithoutHtml(this IHtmlHelper htmlHelper, string text, int maxWords)
    {
        // Remove HTML tags
        var plainText = Regex.Replace(text, @"<[^>]+>|&nbsp;", "").Trim();

        var words = plainText.Split(' ');

        if (words.Length <= maxWords)
        {
            return new HtmlString(text);
        }
        else
        {
            var truncatedWords = words.Take(maxWords);
            var truncatedText = string.Join(" ", truncatedWords) + " ...";

            // Replace the image tag at the first line with the truncated text
            var truncatedHtml = Regex.Replace(text, @"<img[^>]+?>", "");
            var truncatedViewHtml = truncatedHtml.Replace(plainText, truncatedText);

            return new HtmlString(truncatedText);
        }
    }
}