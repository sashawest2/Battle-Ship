namespace Battle_Ship;

public class Ship
{
    int Size { get; set; }
    public List<(int Row, int Col)> Cells { get; set; }
    

    public Ship(int startRow, int startCol, int size, bool horizontal)
    {
        Size = size;
        Cells = new List<(int Row, int Col)>();

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
    
    
}