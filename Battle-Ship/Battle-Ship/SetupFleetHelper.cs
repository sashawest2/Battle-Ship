namespace Battle_Ship;

public static class SetupFleetHelper
{
    private static bool _isPlaced;


    private static void SetupFleetInteractive(Board board)
    {
        {
            do
            {
                (int row, int col) cell = UserInputHelper.GetRowAndCol(); 

                if (!board.IsCellEmpty(cell))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This cell is around another ship or not empty!");
                    Console.ResetColor();
                    continue;   
                }
                
                bool orientation = UserInputHelper.GetOrientation();
                int size = UserInputHelper.GetSize();

                if (board.CanPlaceSizeShip(size, board))
                {
                    Ship ship = new Ship(cell.row, cell.col, size, orientation);
        
                    if (!board.PlaceShip(ship))
                    {
                        Console.WriteLine("Can't place ship! Please, try again!");
                        _isPlaced = false;
                    }
                    else
                    {
                        Console.WriteLine("Ship placed!");
                        _isPlaced = true;
                        board.Print(false);
                    }  
                }
                else
                {
                    Console.WriteLine($"Can't place ship! You have enough {size}-sized ships");
                }

                
            } while (!_isPlaced);
            
            _isPlaced = false;
        }
    }


    public static void SetupShips(Board board)
    {
        while (!_isPlaced)
        {
            Console.WriteLine("Do you want to place your fleet randomly or by yourself? (r - random, y - yourself)");

            string answer = Console.ReadLine().Trim().ToLower();

            if (answer == "y")
            {
                while (true)
                {
                    SetupFleetInteractive(board);
                }
            }

            if (answer == "r")
            {
                List<Ship> fleet = new List<Ship>();
    
                board.PlaceFleetRandomly(fleet);
                board.Print(false);
                _isPlaced = true;
            }
        }
        _isPlaced = false;

    }

}