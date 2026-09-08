namespace Battle_Ship;


    public class HumanPlayer : Player
    {
        public int _hitCounter = 0;
        public int _moveCounter = 0;
        
        private Cell GetShot()
        {
            Cell cell = UserInputHelper.ParseCoordinate();
            _moveCounter++;
            return cell;
        }

        public override void MakeMove(Board playerBoard, Board enemyBoard)
        {
            PrintBoardBeforeMove(playerBoard, enemyBoard);
        
            do
            {
                var cell = GetShot();
                var (result, ship) = enemyBoard.ReceiveShot(cell);
                
                if (result is ShotResult.AlreadyShot)
                {
                    _continueShooting = true;
                    continue;
                }
                
                if (enemyBoard.IsAllShipsSunk())
                {
                    Console.Clear();
                    enemyBoard.Print(true);
                    Console.WriteLine($"You won! You've had {_moveCounter} moves! ");
                    _hitCounter++;
                    Game._isWon = true;
                    return;
                }

                if (result is ShotResult.Hit or ShotResult.Sunk)
                {
                    
                    Console.Clear();

                    if (result == ShotResult.Sunk)
                    {
                        List<Cell> cellsToAvoid = ship.GetCopyOfCellsAroundShip();

                        foreach (var cellToAvoid in cellsToAvoid)
                        {
                            enemyBoard.ChangeCellStateAroundShip(cellToAvoid);
                        }
                    }
                    enemyBoard.Print(true);
                    Console.WriteLine("Nice shot! You have another attempt!");
                    _continueShooting = true;
                    _hitCounter++;
                }
                else
                {
                    _continueShooting = false;
                }
    
            } while (_continueShooting); 
        
            PrintBoardAfterMove(enemyBoard);
        }
        
        private static void PrintBoardBeforeMove(Board myBoard, Board enemyBoard)
        {
            myBoard.Print(false);
            Console.WriteLine();
            enemyBoard.Print(true);
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
