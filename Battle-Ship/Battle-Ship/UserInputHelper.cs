namespace Battle_Ship;

public static class UserInputHelper
{
    private static bool TryParseCoordinate(string input, out Cell cell)
    {
            int row = -1;
            int col = -1;
            cell = new Cell();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Coordinates cannot be empty!");
                return false;
            }

            char letter = char.ToUpper(input[0]);

            if (!LetterDictionary.ParsingLetters.ContainsKey(letter))
            {
                Console.WriteLine("Coordinates should contain a letter!");
                return false;
            }

            if (!int.TryParse(input.Substring(1), out int number))
            {
                Console.WriteLine("Coordinates should contain a number!");
                return false;
            }

            if (number < 1 || number > 10)
            {
                Console.WriteLine("Coordinates should contain number between 1 and 10!");
                return false;
            }

            cell = new(LetterDictionary.ParsingLetters[letter], number - 1);

            return true;
    }

    public static string? ReadFromConsole()
    {
        Console.Write("Please enter row and column for a ship:"); 
        string? input = Console.ReadLine();
        
        return input;
    }
    
    
    public static Cell GetRowAndCol()
    {
        Console.Write("Please enter start row and column for a ship:");
        var input = ReadFromConsole();
        TryParseCoordinate(input, out var cell);
        
        return cell;
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

    public static (MoveType Type, Cell Cell) UserInput()
    {
        bool isCorrectCommandInput = false;
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Type the command or desired cell:");
            var inputRaw = Console.ReadLine();
            switch (inputRaw)
            {
                case "/stat":
                    return (MoveType.Stat, new Cell());
                default:
                    if (!TryParseCoordinate(inputRaw, out Cell cell))
                    {
                        isCorrectCommandInput = false;
                        break;
                    }
                    return (MoveType.Shoot, cell);
            }
        }
        while (!isCorrectCommandInput);
        throw new Exception("Invalid command!");
    }
}