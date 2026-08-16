using Battle_Ship;

Board board = new Board();
int row = 0;
int col = 0;
bool isHorizontal = false;
int size = 0;
bool isPlaced = false;

void GetRowAndCol()
{
    Console.Write("Please enter start row and column for a ship:");
    while (!Helper.TryParseCoordinate(Console.ReadLine(), out row, out col))
    {
            Console.WriteLine("Invalid coordinate! Try again!");
    }
}

bool GetDirection()
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

void GetSize()
{
    Console.Write("Please enter size of the ship:");
    while (!int.TryParse(Console.ReadLine(), out size) || size <= 0 || size > 4)
    {
            Console.WriteLine("Invalid coordinate! Try again!");
    }
}

void SetupFleetInteractive()
{
    {
        do
        {
                GetRowAndCol();
                var cell = (row, col);  

                if (board.IsCellAroundShip(cell) || board.grid[cell.row, cell.col] != CellState.Empty)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This cell is around another ship or not empty!");
                    Console.ResetColor();
                    continue;   
                }
                isHorizontal = GetDirection();
                GetSize();
                
        
            Ship ship = new Ship(row, col, size, isHorizontal);
        
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

while (true)
{
    SetupFleetInteractive();
}





