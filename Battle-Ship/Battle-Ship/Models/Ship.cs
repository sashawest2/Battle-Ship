namespace Battle_Ship;

public class Ship
{
    int Size { get; set; }
    public List<Cell> Cells { get; set; }
    private List<Cell> HitCells  { get; set; }
    private List<Cell> CellsAroundShip { get; set; }

    private void AddCellAroundShip(Cell cell)
    {
        if (cell.Row >= 0 && cell.Row < 10 && cell.Col >= 0 && cell.Col < 10)
        {
            CellsAroundShip.Add(cell);
        }
    }

    public List<Cell> GetCopyOfCellsAroundShip()
    {
        return CellsAroundShip.ToList();
    }
    
    public Ship(Cell startCell, int size, bool horizontal)
    {
        Size = size;
        Cells = new List<Cell>();
        HitCells = new List<Cell>();
        CellsAroundShip = new List<Cell>();
        SetCellsAroundShip(startCell, size, horizontal);

        for (int i = 0; i < size; i++)
        {
            if (horizontal)
            {
                Cells.Add(startCell with { Col = startCell.Col + i });
            }
            else
            {
               CellsAroundShip.Add(startCell with { Row = startCell.Row  + i });
            }
        }
        
    }



    private void SetCellsAroundShip(Cell startCell, int size, bool horizontal)
    {
        if (horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                    AddCellAroundShip(new (startCell.Row - 1, startCell.Col + i));
                    AddCellAroundShip(new(startCell.Row + 1, startCell.Col + i));
                    
            
                if (i == 0)
                {
                    AddCellAroundShip(startCell with { Col = startCell.Col - 1 });
                    AddCellAroundShip(new (startCell.Row - 1, startCell.Col - 1));
                    AddCellAroundShip(new (startCell.Row + 1, startCell.Col - 1));
                }

                if (i == size - 1)
                {
                    AddCellAroundShip(new (startCell.Row - 1, startCell.Col + size));
                    AddCellAroundShip(new (startCell.Row + 1, startCell.Col + size));
                    AddCellAroundShip(startCell with { Col = startCell.Col + size });
                }
            }
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                AddCellAroundShip(new (startCell.Row + i, startCell.Col + 1));
                AddCellAroundShip(new (startCell.Row + i, startCell.Col - 1));

                if (i == 0)
                {
                    AddCellAroundShip(new (startCell.Row - 1, startCell.Col + 1));
                    AddCellAroundShip(new (startCell.Row - 1, startCell.Col - 1));
                    AddCellAroundShip(startCell with { Row = startCell.Row - 1 });
                }

                if (i == size - 1)
                {
                    AddCellAroundShip(startCell with { Row = startCell.Row + size });
                    AddCellAroundShip(new (startCell.Row + size, startCell.Col + 1));
                    AddCellAroundShip(new (startCell.Row + size, startCell.Col - 1));
                }
            }
        }
    }
    
    public bool OccupiesCell(Cell cell)
    {
        foreach (var shipCell in Cells)
        {
            if (shipCell.Row == cell.Row && shipCell.Col == cell.Col)
            {
                return true;
            }
        }
        
        return false;
    }

    public void RegisterHit(Cell cell)
    {
        HitCells.Add(cell);
    }

    public bool IsSunk()
    {
        if (HitCells.Count != Cells.Count) return false;
        return true;
    }

    public bool IsCellAroundShip(Cell cell)
    {
        return CellsAroundShip.Contains(cell);
    }
    
}