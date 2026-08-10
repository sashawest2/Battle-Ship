namespace Battle_Ship;

public class Board
{
    private List<Ship> Ships = new List<Ship>();
    char[,] grid = new char[10, 10];

    public Board()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = ' ';
            }
        }
    }
    
    bool OcupiesCell(int row, int col)
    {
        if (grid[row, col] == 'S')
        {
            return true;
        }
        return false;
    }


    public void PlaceShipDirectly(Ship ship)
    {
        Ships.Add(ship);

        foreach (var cell in ship.Cells)
        {
            grid[cell.Row, cell.Col] = 'S';
        }
    }
    
    public ShotResult ReceiveShot(int row, int col)
    {
        if (OcupiesCell(row, col))
        {
            grid[row, col] = 'H';
            Console.WriteLine("Попадание!");
            return ShotResult.Hit;
        }
        else if (grid[row, col] == 'H')
        {
            Console.WriteLine("Клетка была отмеченна раннее");
            return ShotResult.AlreadyShot;
        }
        else
        {
            Console.WriteLine("Промах");
            return ShotResult.Miss;
        }
    }

    private void MarkShot(int row, int col)
    {
        grid[row, col] = 'X';
    }

    private void MarkMiss(int row, int col)
    {
        grid[row, col] = 'O';
    }

    public void MakeMove()
    {
        while (true)
        {
                var cell = Console.ReadLine().Split(' ');
            
            int row = int.Parse(cell[0]);
            int col = int.Parse(cell[1]);

            if (OcupiesCell(row, col))
            {
                MarkShot(row, col);
                PrintGrid();
            }
            else
            {
                MarkMiss(row, col);
                Console.WriteLine("мимо");
                PrintGrid();
            }
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
                Console.Write(grid[i, j]);
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