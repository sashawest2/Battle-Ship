namespace Battle_Ship;

public static class UserInputHelper
{
    public static (int, int) InputCoordinate()
    {
        int row = -1;
        int col = -1;
        bool isParsed = false;

        do
        {
            Console.Write("Please enter coordinate:");
            string input = Console.ReadLine().Trim();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Your input cannot be empty!");
                continue;
            }

            char letter = char.ToUpper(input[0]);

            if (!LetterDictionary.ParsingLetters.ContainsKey(letter))
            {
                Console.WriteLine("Invalid coordinate! Try again!");
                continue;
            }

            if (!int.TryParse(input.Substring(1), out int number))
            {
                Console.WriteLine("Invalid coordinate! Try again!");
                continue;
            }

            if (number < 1 || number > 10)
            {
                Console.WriteLine("The number must be between 1 and 10!");
                continue;
            }
            
            isParsed = true;
            row = LetterDictionary.ParsingLetters[letter];
            col = number - 1;
        } while (!isParsed);
        
        return (row, col);
    }
    
    public static (int, int) GetRowAndCol()
    {
        Console.Write("Please enter start row and column for a ship:");

        return InputCoordinate();
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