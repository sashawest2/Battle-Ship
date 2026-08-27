namespace Battle_Ship;

public class ComputerPlayer : Player
{
    private readonly HashSet<(int, int)> _usedCoordinates = new HashSet<(int, int)>();
    private bool _isNew = false;
    
    public override (int row, int col) GetShot()
    {
        Random rnd = new Random();
        (int row, int col) cell;

        do
        {
            cell = (rnd.Next(10), rnd.Next(10));

            _isNew = !_usedCoordinates.Contains(cell);
        } while (!_isNew);
        
        _usedCoordinates.Add(cell);

        return cell;
    }

    public override void MakeMove(Board playerBoard, Board enemyBoard)
    {
        Thread.Sleep(500);
        
        do
        {
            var cell = GetShot();
            ShotResult result = enemyBoard.ReceiveShot(cell.row, cell.col);

            if (enemyBoard.IsAllShipsSunk())
            {
                _isWon = true;
                return;
            }

            if (result is ShotResult.Hit or ShotResult.Sunk)
            {
                _isShot = true;
            }
            else
            {
                _isShot = false;
            }
    
        } while (_isShot); 
        
        PrintBoardAfterMove(enemyBoard);
    }
    
    private static void PrintBoardAfterMove(Board enemyBoard)
    {
        Console.Clear();
        enemyBoard.Print(true);
        Console.WriteLine("Press any key to pass the run");
        Console.ReadLine();
        Console.Clear();
    }
}