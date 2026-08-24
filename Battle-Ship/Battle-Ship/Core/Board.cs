namespace Battle_Ship;

public class Board
{
    private List<Ship> Ships = new List<Ship>();
    public CellState[,] grid = new CellState[10, 10];
    private int moveCounter = 0;
    Random random = new Random();

    public Board()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = CellState.Empty;
            }
        }
    }

    public bool CanPlaceShip(Ship ship)
    {
        foreach (var cell in ship.Cells)
        {
            if (cell.Row < 0 || cell.Row > 9 || cell.Col < 0 || cell.Col > 9)
            {
                return false;
            }

            if (!IsCellEmpty(cell))
            {
                return false;
            }
            
        }
        return true;
    }

    public bool IsCellEmpty((int row, int col) cell)
    {
        foreach (var existingShip in Ships)
        {
            if (existingShip.IsCellAroundShip(cell))
            {
                return false;
            }
        }
        
        if (grid[cell.row, cell.col] != CellState.Empty)
        {
            return false;
        }
        
        return true;
    }

    public void PlaceFleetRandomly(List<Ship> fleet)
    {
        AddRandomShip(1, fleet);
        AddRandomShip(2, fleet);
        AddRandomShip(3, fleet);
        AddRandomShip(4, fleet);
    }

    private void AddRandomShip(int size, List<Ship> fleet)
    {     
        bool isAdded = false;
        
        for (int i = 0; i < 5 - size; i++)
        {
            
            do
            {
                Ship? ship = PlaceShipRandomly(size);
                if (ship != null)
                {
                    fleet.Add(ship);
                    isAdded = true;
                }
                
            } while (!isAdded);
        }
    }

    private Ship? PlaceShipRandomly(int size)
    {
        
        for (int i = 0; i < 100; i++)
        {
            int row = random.Next(10);
            int col = random.Next(10);
            bool horizontal = random.Next(2) == 0;
            
            Ship ship = new Ship(row, col, size, horizontal);
            
            if (PlaceShip(ship))
            {
                return ship;
            }
        }
        return null;
    }


    public bool PlaceShip(Ship ship)
    {
        if (CanPlaceShip(ship))
        {
            foreach (var cell in ship.Cells)
            {
                grid[cell.Row, cell.Col] = CellState.Ship;
            }
            
            Ships.Add(ship);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsAllShipsSunk()
    {
        foreach (var ship in Ships)
        {
            if (!ship.IsSunk())
            {
                return false;
            }
        }
        return true;
    }
    
    public ShotResult ReceiveShot(int row, int col)
    {
        if (grid[row, col] == CellState.Hit)
        {
            return ShotResult.AlreadyShot;
        }
        
        foreach (var ship in Ships)
        {
            if (ship.OccupiesCell(row, col))
            {
                grid[row, col] = CellState.Hit;
                ship.RegisterHit(row, col);
                if (ship.IsSunk())
                {
                    foreach (var cell in ship.Cells)
                    {
                        grid[cell.Row, cell.Col] = CellState.Sunk;
                    }
                    return ShotResult.Sunk;
                }
                return ShotResult.Hit;
            }
        }
        grid[row, col] = CellState.Miss;
        return ShotResult.Miss;
    }

    private char GetDisplaySymbol(CellState state, bool hideShips)
    {
        return state switch
        {
            CellState.Empty => '.',
            CellState.Ship => hideShips ? '.' : 'S',
            CellState.Hit => 'H',
            CellState.Miss => 'M',
            CellState.Sunk => 'X',
            _ => '.'
        };
    }

    private void PrintResult(ShotResult result)
    {
        Console.Clear();

        if (result == ShotResult.Miss)
        {
            Console.WriteLine("Miss");
        }

        if (result == ShotResult.AlreadyShot)
        {
            Console.WriteLine("You've already shot to that cell, please choose another one!");
        }

        if (result == ShotResult.Sunk)
        {
            Console.WriteLine("Congratulations! Ship sank");
        }

        if (result == ShotResult.Hit)
        {
            Console.WriteLine("Hit!");
        }
        
    }
    
    
    public void Print(bool hideShips)
    {
        int counter = 0;

    
        for (int k = -1; k < 10; k++)
        {
            if (k == 8)
            {
                Console.Write(k + 1);
                Console.Write(" |");
                continue;
            }
        
            if (k == -1)
            {
                Console.Write("  ");
                continue;
            }
            Console.Write(k + 1);
            Console.Write(" | ");
        
        }
    
        Console.WriteLine();
    
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            Console.Write(LetterDictionary.RenderLetters[i]);
            Console.Write(" ");
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(GetDisplaySymbol(grid[i, j], hideShips));
                Console.Write(" | ");
                counter++;

                if (counter == 10)
                {
                    Console.WriteLine();
                    counter = 0;
                }
            
            }
        }
    }
}