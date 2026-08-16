namespace Battle_Ship;

public static class Helper
{
    public static bool TryParseCoordinate(
        string input,
        out int row,
        out int col)
    {
        row = -1;
        col = -1;

        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        char letter = char.ToUpper(input[0]);

        if (!LetterDictionary.ParsingLetters.ContainsKey(letter))
        {
            return false;
        }

        if (!int.TryParse(input.Substring(1), out int number))
        {
            return false;
        }

        if (number < 1 || number > 10)
        {
            return false;
        }

        row = LetterDictionary.ParsingLetters[letter];
        col = number - 1;

        return true;
    }
}