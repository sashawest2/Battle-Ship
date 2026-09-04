namespace Battle_Ship;

public abstract class Player
{
    protected int MoveCounter = 0;
    protected readonly Random Rnd = new Random();
    public static bool _isWon = false;
    public static bool _isShot = false;

    // protected virtual (int row, int col) GetShot()
    // { 
    //    _rnd = new Random();
    //    _moveCounter++;
    //    return (_rnd.Next(10), _rnd.Next(10)); 
    // }

    // public virtual void MakeMove(Board playerBoard, Board enemyBoard)
    // {
    //     do
    //     {
    //         var cell = GetShot();
    //         ShotResult result = enemyBoard.ReceiveShot(cell.row, cell.col);
    //
    //         if (enemyBoard.IsAllShipsSunk())
    //         {
    //             _isWon = true;
    //             return;
    //         }
    //
    //         if (result is ShotResult.Hit or ShotResult.Sunk)
    //         {
    //             _isShot = true;
    //         }
    //         else
    //         {
    //             _isShot = false;
    //         }
    //
    //     } while (_isShot); 
    // }
    
    public abstract void MakeMove(Board board1, Board board2);
}

