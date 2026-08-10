namespace Battle_Ship;

public static class Helper
{
    private static bool IsValid(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }
        
        char letter = char.ToUpper(input[0]);
        int number;
        try
        {
           number = Convert.ToInt32(input[1]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        if (LetterDictionary.ParsingLetters.ContainsKey(input[0]) || number < 1 || number > 10)
        {
            return false;
        }
        return true;
    }

    public static bool TryParseCoordinate(string input, out int row, out int col)
    {
        if (IsValid(input))
        {
            row = LetterDictionary.ParsingLetters[input[0]];
            col = input[1] - 1;
            return true;
        }
        row = -1;
        col = -1;
        return false;
    }
}