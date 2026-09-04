namespace Battle_Ship;

public class ComputerPlayer : Player
{
    private readonly HashSet<(int, int)> _usedCoordinates = new HashSet<(int, int)>();
    private readonly Queue<(int, int)> _targetQueue = new Queue<(int, int)>();
    private readonly Queue<(int, int)> _cellsToAvoid = new Queue<(int, int)>();
    private readonly HashSet<(int row, int col)> _hitCells = new HashSet<(int, int)>();
    private (int row, int col)? _previousCell;
    private bool _isNew;
    private int _hitCounter;

    private (int row, int col) GetShot()
    {
        if (_targetQueue.Count == 0)
        {
            PickRandomCoordinate(out var cell);
        
            _usedCoordinates.Add(cell);

            return cell;
        }

        var targetCell = _targetQueue.Dequeue();
        _usedCoordinates.Add(targetCell);
        return targetCell;

    }

    private (int row, int col) PickRandomCoordinate(out ( int row, int col) cell)
    {
            do
            {
                cell = (Rnd.Next(10), Rnd.Next(10));

                _isNew = !_usedCoordinates.Contains(cell) && !_cellsToAvoid.Contains(cell);
            } while (!_isNew);
            
            return (cell.row, cell.col);
    }
    

    public override void MakeMove(Board playerBoard, Board enemyBoard)
    {
        Thread.Sleep(500);
        bool isHorizontal = false;
        
        do
        {
            var cell = GetShot();
            var (result, ship) = enemyBoard.ReceiveShot(cell.row, cell.col);
            
            if (enemyBoard.IsAllShipsSunk())
            {
                _isWon = true;
                return;
            }

            if (result is ShotResult.Hit)
            {
                _isShot = true;
                _hitCounter++;
                _hitCells.Add(cell);

                if (_hitCounter == 1)
                {
                    AddCellsToQueue(cell);
                    _previousCell = cell;
                }
                else
                {
                    if (_hitCounter == 2 && _previousCell != null)
                    {
                        GetShipDirection(cell, out isHorizontal);
                    } 
                    AddCellsAtShipEndsToQueue(isHorizontal);
                }
            }

            else if (result is ShotResult.Sunk)
            {
                _hitCounter = 0;
                _hitCells.Clear();
                _previousCell = null;
                _targetQueue.Clear();
                _isShot = true;
                foreach (var cellToAvoid in ship.CellsAroundShip)
                {
                    _cellsToAvoid.Enqueue(cellToAvoid);
                }
            }
            else
            {
                _isShot = false;
            }
            
            
    
        } while (_isShot); 
        
        PrintBoardAfterMove(enemyBoard);
    }

    private bool GetShipDirection((int row, int col) cell, out bool isHorizontal)
    {
           return isHorizontal = _previousCell.Value.row == cell.row;
    }

    private void AddCellsAtShipEndsToQueue(bool isHorizontal)
    {
        _targetQueue.Clear();
                    
        if (isHorizontal)
        {
            var min = _hitCells.MinBy(x => x.col);
            var max = _hitCells.MaxBy(x => x.col);

            if (min.col > 0)
            {
                _targetQueue.Enqueue((min.row, min.col - 1));
            }

            if (max.col < 9)
            {
                _targetQueue.Enqueue((max.row, max.col + 1));
            }
        }
        else
        {
            var min = _hitCells.MinBy(x => x.row);
            var max = _hitCells.MaxBy(x => x.row);

            if (min.row > 0)
            {
                _targetQueue.Enqueue((min.row - 1, min.col));
            }

            if (max.row < 9)
            {
                _targetQueue.Enqueue((max.row + 1, max.col));
            }
            
        }
    }
    private static void PrintBoardAfterMove(Board enemyBoard)
    {
        Console.Clear();
        enemyBoard.Print(true);
        Console.WriteLine("Press any key to pass the run");
        Console.ReadLine();
        Console.Clear();
    }

    private void AddCellsToQueue((int row, int col) cell)
    {
        if (cell.row != 0)
        {
            _targetQueue.Enqueue((cell.row - 1, cell.col));
        }

        if (cell.col != 0)
        {
            _targetQueue.Enqueue((cell.row, cell.col - 1));
        }

        if (cell.row != 9)
        {
            _targetQueue.Enqueue((cell.row + 1, cell.col));
        }

        if (cell.col != 9)
        {
            _targetQueue.Enqueue((cell.row, cell.col + 1));
        }
    }

    private void AddDirectionalCellToQueue((int row, int col) cell, (int row, int col) previousCell, bool isHorizontal)
    {
        _targetQueue.Clear();

        if (isHorizontal)
        {
            if (previousCell.col != 0 && cell.col != 0)
            {
                if (previousCell.col < cell.col)
                {
                    (int row, int col) horizontalCell = (previousCell.row, previousCell.col - 1);
                    _targetQueue.Enqueue(horizontalCell);
                }
                else
                {
                    (int row, int col) horizontalCell = (cell.row, cell.col - 1);
                    _targetQueue.Enqueue(horizontalCell);
                }
            }

            if (previousCell.col != 9 && cell.col != 9)
            {
                if (previousCell.col > cell.col)
                {
                    (int row, int col) horizontalCell = (previousCell.row, previousCell.col + 1);
                    _targetQueue.Enqueue(horizontalCell);
                }
                else
                {
                    (int row, int col) horizontalCell = (cell.row, cell.col + 1);
                    _targetQueue.Enqueue(horizontalCell);
                }
            }
        }
        else
        {
            if (previousCell.row != 0 && cell.row != 0)
            {
                if (previousCell.row < cell.row)
                {
                    (int row, int col) verticalCell = (previousCell.row - 1, previousCell.col);
                    _targetQueue.Enqueue(verticalCell);
                }
                else
                {
                    (int row, int col) verticalCell = (cell.row - 1, cell.col);
                    _targetQueue.Enqueue(verticalCell);
                }
            }

            if (previousCell.row != 9 && cell.row != 9)
            {
                if (previousCell.row > cell.row)
                {
                    (int row, int col) verticalCell = (previousCell.row + 1, previousCell.col);
                    _targetQueue.Enqueue(verticalCell);
                }
                else
                {
                    (int row, int col) verticalCell = (cell.row + 1, cell.col);
                    _targetQueue.Enqueue(verticalCell);
                }
            }
        }
    }

    public static void AddCellsToAvoid()
    {
        
    }
}