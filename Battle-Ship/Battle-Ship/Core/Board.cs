namespace Battle_Ship;

public class Board
{
    private List<Ship> Ships = new List<Ship>();
    private CellState[,] grid = new CellState[10, 10];
    private int moveCounter = 0;
    Random random = new Random();
    public int oneDeckShipsCount = 0;
    public int twoDeckShipsCount = 0;
    public int threeDeckShipsCount = 0;
    public int fourDeckShipsCount = 0;

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

    public void ChangeCellStateAroundShip(Cell cell)
    {
        if (grid[cell.Row, cell.Col] != CellState.Miss)
        {
            grid[cell.Row, cell.Col] = CellState.AroundShip;
        }
    }

    public bool IsCellEmpty(Cell cell)
    {
        foreach (var existingShip in Ships)
        {
            if (existingShip.IsCellAroundShip(cell))
            {
                return false;
            }
        }

        if (grid[cell.Row, cell.Col] != CellState.Empty)
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
            Cell cell = new(random.Next(10), random.Next(10));
            bool horizontal = random.Next(2) == 0;

            Ship ship = new Ship(cell, size, horizontal);

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

    public (ShotResult res, Ship? ship) ReceiveShot(Cell cell)
    {
        if (grid[cell.Row, cell.Col] == CellState.Hit || grid[cell.Row, cell.Col] == CellState.Sunk ||
            grid[cell.Row, cell.Col] == CellState.Miss)
        {
            return (ShotResult.AlreadyShot, ship: null);
        }

        foreach (var ship in Ships)
        {
            if (ship.OccupiesCell(cell))
            {
                grid[cell.Row, cell.Col] = CellState.Hit;
                ship.RegisterHit(cell);


                if (ship.IsSunk())
                {
                    foreach (var shipCell in ship.Cells)
                    {
                        grid[shipCell.Row, shipCell.Col] = CellState.Sunk;
                    }
                    return (ShotResult.Sunk, ship);
                }

                return (ShotResult.Hit, null);
            }
        }

        grid[cell.Row, cell.Col] = CellState.Miss;
        return (ShotResult.Miss, null);
    }

    private static char GetDisplaySymbol(CellState state, bool hideShips)
    {
        switch (state)
        {
            case CellState.Hit:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case CellState.Miss:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case CellState.Sunk:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case CellState.Ship:
                Console.ForegroundColor = hideShips
                    ? ConsoleColor.White
                    : ConsoleColor.DarkYellow;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }

        return state switch
        {
            CellState.Empty => ' ',
            CellState.Ship => hideShips ? ' ' : 'S',
            CellState.Hit => 'H',
            CellState.Miss => 'M',
            CellState.AroundShip => '.',
            CellState.Sunk => 'X',
            _ => '.'
        };
    }

    public void PrintHeaders()
    {
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

            if (k == 9)
            {
                Console.Write("   ");
            }
        }
    }

    public void GetAmountOfSunkShips()
    {
        oneDeckShipsCount = 0;
        twoDeckShipsCount = 0;
        threeDeckShipsCount = 0;
        fourDeckShipsCount = 0;
        
        foreach (var ship in Ships)
        {
            if (ship.IsSunk()) continue;
            switch (ship.Size)
            {
                case 1:
                    oneDeckShipsCount++;
                    break;
                case 2:
                    twoDeckShipsCount++;
                    break;
                case 3:
                    threeDeckShipsCount++;
                    break;
                case 4:
                    fourDeckShipsCount++;
                    break;
            }
        }
    }

    public void PrintRow(int row, bool hideShips)
    {
        Console.Write($"{LetterDictionary.RenderLetters[row]} ");

        for (int col = 0; col < grid.GetLength(1); col++)
        {
            char symbol = GetDisplaySymbol(grid[row, col], hideShips);

            
            Console.Write($"{symbol}");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" | ");
         
            Console.ResetColor();
        }
    }
    


    public void Print(bool hidePlayerShips)
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

            if (k == 9)
            {
                Console.Write("   ");
            }
        }

        Console.WriteLine();

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            Console.Write(LetterDictionary.RenderLetters[i]);
            Console.Write(" ");
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(GetDisplaySymbol(grid[i, j], hidePlayerShips));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" | ");
                counter++;

                if (counter == 10)
                {
                    if (i == 9)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        counter = 0;
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
