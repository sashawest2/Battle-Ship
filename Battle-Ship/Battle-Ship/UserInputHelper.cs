namespace Battle_Ship;

public static class UserInputHelper
{
    public static Cell ParseCoordinate()
    {
        do
        {
            int row = -1;
            int col = -1;

            Console.Write("Please enter row and column for a ship:");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Coordinates cannot be empty!");
                continue;
            }

            char letter = char.ToUpper(input[0]);

            if (!LetterDictionary.ParsingLetters.ContainsKey(letter))
            {
                Console.WriteLine("Coordinates should contain a letter!");
                continue;
            }

            if (!int.TryParse(input.Substring(1), out int number))
            {
                Console.WriteLine("Coordinates should contain a number!");
                continue;
            }

            if (number < 1 || number > 10)
            {
                Console.WriteLine("Coordinates should contain number between 1 and 10!");
                continue;
            }

            Cell cell = new(LetterDictionary.ParsingLetters[letter], number - 1);

            return cell;
        } while (true);
    }
    
    public static Cell GetRowAndCol()
    {
        Console.Write("Please enter start row and column for a ship:");

        var cell = ParseCoordinate();

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

}