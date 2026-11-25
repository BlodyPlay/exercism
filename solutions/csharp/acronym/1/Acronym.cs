using System;
using System.Linq;

public static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        string[] words = phrase.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);

        char[] initials = words
            .Select(word => char.ToUpper(word[0]))
            .ToArray();

        return new string(initials);
    }
}