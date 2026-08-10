namespace Battle_Ship;

public class Ship
{
    int Size { get; set; }
    public List<(int Row, int Col)> Cells { get; set; }
    public List<(int, int)> HitCells  { get; set; }
    
    

    public Ship(int startRow, int startCol, int size, bool horizontal)
    {
        Size = size;
        Cells = new List<(int Row, int Col)>();
        HitCells = new List<(int Row, int Col)>();

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