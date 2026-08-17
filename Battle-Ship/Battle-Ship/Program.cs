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

while (true)
{
    SetupFleetInteractive();
}





