namespace Battle_Ship;

public class ComputerPlayer : Player
{
    private readonly HashSet<Cell> _cellsAroundShip = new();
    private readonly Queue<Cell> _targetQueue = new();
    private readonly Queue<Cell> _cellsToAvoid = new();
    private readonly HashSet<Cell> _hitCells = new();
    private readonly HashSet<Cell> _usedCoordinates = new();
    private Cell? _previousCell;
    private bool _isNew;
    private int _hitCounter;

    private Cell GetShot()
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

    private void PickRandomCoordinate(out Cell cell)
    {
            do
            {
                cell = new Cell(Rnd.Next(10), Rnd.Next(10));

                _isNew = !_usedCoordinates.Contains(cell) && !_cellsToAvoid.Contains(cell);
            } while (!_isNew);
    }
    

    public override void MakeMove(Board playerBoard, Board enemyBoard)
    {
        Thread.Sleep(500);
        bool isHorizontal = false;
        
        do
        {
            Cell cell = GetShot();
            var (result, ship) = enemyBoard.ReceiveShot(cell);
            
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
                
                List<Cell> cellsToAvoid = ship.GetCopyOfCellsAroundShip();
                
                foreach (var cellToAvoid in cellsToAvoid)
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

    private bool GetShipDirection(Cell cell, out bool isHorizontal)
    {
           return isHorizontal = _previousCell.Value.Row == cell.Row;
    }

    private void AddCellsAtShipEndsToQueue(bool isHorizontal)
    {
        _targetQueue.Clear();
                    
        if (isHorizontal)
        {
            var min = _hitCells.MinBy(x => x.Col);
            var max = _hitCells.MaxBy(x => x.Col);

            if (min.Col > 0)
            {
                _targetQueue.Enqueue(min with { Col = min.Col - 1 });
            }

            if (max.Col < 9)
            {
                _targetQueue.Enqueue(max with { Col = max.Col + 1 });
            }
        }
        else
        {
            var min = _hitCells.MinBy(x => x.Row);
            var max = _hitCells.MaxBy(x => x.Row);

            if (min.Row > 0)
            {
                _targetQueue.Enqueue(min with { Row = min.Row - 1 });
            }

            if (max.Row < 9)
            {
                _targetQueue.Enqueue(max with { Row = max.Row + 1 });
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

    private void AddCellsToQueue(Cell cell)
    {
        if (cell.Row != 0)
        {
            _targetQueue.Enqueue(cell with { Row = cell.Row - 1 });
        }

        if (cell.Col != 0)
        {
            _targetQueue.Enqueue(cell with { Col = cell.Col - 1 });
        }

        if (cell.Row != 9)
        {
            _targetQueue.Enqueue(cell with {Row = cell.Row + 1});
        }

        if (cell.Col != 9)
        {
            _targetQueue.Enqueue(cell with {Col = cell.Col + 1});
        }
    }

    private void AddDirectionalCellToQueue(Cell cell, Cell previousCell, bool isHorizontal)
    {
        _targetQueue.Clear();

        if (isHorizontal)
        {
            if (previousCell.Col != 0 && cell.Col != 0)
            {
                if (previousCell.Col < cell.Col)
                {
                    Cell horizontalCell = previousCell with { Col = previousCell.Col - 1 };
                    _targetQueue.Enqueue(horizontalCell);
                }
                else
                {
                    var horizontalCell = cell with { Col = cell.Col - 1 };
                    _targetQueue.Enqueue(horizontalCell);
                }
            }

            if (previousCell.Col != 9 && cell.Col != 9)
            {
                if (previousCell.Col > cell.Col)
                {
                    var horizontalCell = previousCell with { Col = previousCell.Col + 1 };
                    _targetQueue.Enqueue(horizontalCell);
                }
                else
                {
                    var horizontalCell = cell with { Col = cell.Col + 1 };
                    _targetQueue.Enqueue(horizontalCell);
                }
            }
        }
        else
        {
            if (previousCell.Row != 0 && cell.Row != 0)
            {
                if (previousCell.Row < cell.Row)
                {
                    var verticalCell = previousCell with { Row = previousCell.Row - 1 };
                    _targetQueue.Enqueue(verticalCell);
                }
                else
                {
                    var verticalCell = cell with { Row = cell.Row - 1 };
                    _targetQueue.Enqueue(verticalCell);
                }
            }

            if (previousCell.Row != 9 && cell.Row != 9)
            {
                if (previousCell.Row > cell.Row)
                {
                    var verticalCell = previousCell with { Row = previousCell.Row + 1 };
                    _targetQueue.Enqueue(verticalCell);
                }
                else
                {
                    Cell verticalCell = cell with { Row = cell.Row + 1 };
                    _targetQueue.Enqueue(verticalCell);
                }
            }
        }
    }
}