using Battle_Ship;

Board board = new Board();
bool isPlaced = false;


void SetupFleetInteractive()
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

                int size = UserInputHelper.GetSize();
                bool orientation = UserInputHelper.GetOrientation();
        
            Ship ship = new Ship(cell.row, cell.col, size, orientation);
        
            if (!board.PlaceShip(ship))
            {
                Console.WriteLine("Can't place ship! Please, try again!");
                isPlaced = false;
            }
            else
            {
                Console.WriteLine("Ship placed!");
                isPlaced = true;
                board.Print();
            } 
        } while (!isPlaced);
        
    }
}

Console.WriteLine("Do you want to place your fleet randomly or by yourself? (r - random, y - yourself)");

string answer = Console.ReadLine().Trim().ToLower();

if (answer == "y")
{
    while (true)
    {
        SetupFleetInteractive();
    }
}

if (answer == "r")
{
    List<Ship> fleet = new List<Ship>();
    
    board.PlaceFleetRandomly(fleet);
    board.Print();
}






