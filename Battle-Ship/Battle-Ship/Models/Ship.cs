namespace Battle_Ship;

public class Ship
{
    int Size { get; set; }
    public List<(int Row, int Col)> Cells { get; set; }
    public List<(int, int)> HitCells  { get; set; }
    public List<(int, int)> CellsAroundShip { get; set; }

    private void AddCellAroundShip(int row, int col)
    {
        if (row >= 0 && row < 10 && col >= 0 && col < 10)
        {
            CellsAroundShip.Add((row, col));
        }
    }

    public Ship(int startRow, int startCol, int size, bool horizontal)
    {
        Size = size;
        Cells = new List<(int Row, int Col)>();
        HitCells = new List<(int Row, int Col)>();
        CellsAroundShip = new List<(int, int)>();
        SetCellsAroundShip(startRow, startCol, size, horizontal);

        for (int i = 0; i < size; i++)
        {
            if (horizontal)
            {
                Cells.Add((startRow,  startCol + i));
            }
            else
            {
               Cells.Add((startRow + i, startCol));
            }
        }
        
    }



    public void SetCellsAroundShip(int startRow, int startCol, int size, bool horizontal)
    {
        if (horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                    AddCellAroundShip(startRow - 1, startCol + i);
                    AddCellAroundShip(startRow + 1, startCol + i);
                    
            
                if (i == 0)
                {
                    AddCellAroundShip(startRow, startCol - 1);
                    AddCellAroundShip(startRow - 1, startCol - 1);
                    AddCellAroundShip(startRow + 1, startCol - 1);
                }

                if (i == size - 1)
                {
                    AddCellAroundShip(startRow - 1, startCol + size);
                    AddCellAroundShip(startRow + 1, startCol + size);
                    AddCellAroundShip(startRow, startCol + size);
                }
            }
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                AddCellAroundShip(startRow + i, startCol + 1);
                AddCellAroundShip(startRow + i, startCol - 1);

                if (i == 0)
                {
                    AddCellAroundShip(startRow - 1, startCol + 1);
                    AddCellAroundShip(startRow - 1, startCol - 1);
                    AddCellAroundShip(startRow - 1, startCol);
                }

                if (i == size - 1)
                {
                    AddCellAroundShip(startRow + size, startCol);
                    AddCellAroundShip(startRow + size, startCol + 1);
                    AddCellAroundShip(startRow + size, startCol - 1);
                }
            }
        }
    }
    
    public bool OccupiesCell(int row, int col)
    {
        foreach (var cell in Cells)
        {
            if (cell.Row == row && cell.Col == col)
            {
                return true;
            }
        }
        
        return false;
    }

    public void RegisterHit(int row, int col)
    {
        HitCells.Add((row, col));
    }

    public bool IsSunk()
    {
        if (HitCells.Count != Cells.Count) return false;
        return true;
    }
    
    
}