using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Model;
using System.Text.RegularExpressions;


public record Password(string Word) : ValueObject
{
    static readonly Regex s_Regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$", RegexOptions.Compiled);
    public string Word { get; private init; } = ValidateWord(Word);

    static string ValidateWord(string word)
    {
        if (ReferenceEquals(word, null)) throw new ArgumentNullException(nameof(Word));
        if (!s_Regex.IsMatch(word)) throw new ArgumentException("Requires a string of at least 8 and no more than 16 characters, including one or more types of uppercase and lowercase alphanumeric symbols.", nameof(Word));
        return word;
    }
}
