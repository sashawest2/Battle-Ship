namespace Battle_Ship;

public abstract class Player
{
    private string Name{get;init;}
    private Board OwnBoard{get;init;}
    public int _moveCounter = 0;
    private Random _rnd = new Random();
    public static bool _isWon = false;
    public static bool _isShot = false;

    public virtual (int row, int col) GetShot()
    { 
       _rnd = new Random();
       _moveCounter++;
       return (_rnd.Next(10), _rnd.Next(10)); 
    }

    public virtual void MakeMove(Board playerBoard, Board enemyBoard)
    {
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
    }
}

