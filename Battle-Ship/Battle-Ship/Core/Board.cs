namespace Battle_Ship;

public class Board
{
    private List<Ship> Ships = new List<Ship>();
    public CellState[,] grid = new CellState[10, 10];

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
    


    public void PlaceShipDirectly(Ship ship)
    {
        Ships.Add(ship);

        foreach (var cell in ship.Cells)
        {
            grid[cell.Row, cell.Col] = CellState.Ship;
        }
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

    public void MakeMove()
    {
        while (true)
        {
                var cell = Console.ReadLine().Split(' ');
            
            int row = int.Parse(cell[0]);
            int col = int.Parse(cell[1]);

            var shotResult = ReceiveShot(row, col);
            PrintResult(shotResult);
            PrintGrid();
        }
}
    
    public void PrintGrid()
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
                Console.Write(GetDisplaySymbol(grid[i, j], false));
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