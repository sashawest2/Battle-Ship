namespace Battle_Ship;

public static class UserInputHelper
{
    public static bool TryParseCoordinate(
        string? input,
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
    
    public static (int, int) GetRowAndCol()
    {
        Console.Write("Please enter start row and column for a ship:");

        int row;
        int col;

        while (!TryParseCoordinate(Console.ReadLine(), out row, out col))
        {
            Console.WriteLine("Invalid coordinate! Try again!");
        }

        return (row, col);
    }
    
    public static int GetSize()
    {
        int size = 0;
        
        Console.Write("Please enter size of the ship:");
        while (!int.TryParse(Console.ReadLine(), out size) || size <= 0 || size > 4)
        {
            Console.WriteLine("Invalid coordinate! Try again!");
        }
        return size;
    }
    
    public static bool GetOrientation()
    {
        Console.Write("What direction your ship is? (horizontal or vertical)");
    
        while (true)
        {
            string input = Console.ReadLine().Trim().ToLower();
        
            if (input == "horizontal")
            {
                return true;
            }

            if (input == "vertical")
            {
                return false;
            }
            Console.WriteLine("Unknown direction");
        }
    }

}