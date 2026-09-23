namespace Battle_Ship;


    public class HumanPlayer : Player
    {
        public int _hitCounter = 0;
        public int MoveCounter = 0;


        public override MoveType MakeMove(Board humanBoard, Board computerBoard, bool isStat)
        {
            if (!isStat)
            {
                PrintBoardBeforeMove(humanBoard, computerBoard);
            }
            
            do
            {
                var input = UserInputHelper.UserInput();
                if (input.Type == MoveType.Stat)
                {
                    return MoveType.Stat;
                }
            
                var cell = input.Cell;
                Shoot(cell, humanBoard, computerBoard);
            } while (_continueShooting);
        
            PrintBoardAfterMove(humanBoard, computerBoard);

            return MoveType.Shoot;
        }

        private bool Shoot(Cell cell, Board humanBoard, Board computerBoard)
        {
                var (result, ship) = computerBoard.ReceiveShot(cell);
                
                if (result is ShotResult.AlreadyShot)
                {
                    return _continueShooting = true;
                }
                
                MoveCounter++;
                
                if (computerBoard.IsAllShipsSunk())
                {
                    _hitCounter++;
                    Console.Clear();
                    Game.NewPrint(humanBoard, computerBoard);
                    Console.WriteLine($"You won! You've had {MoveCounter} moves! ");
                    Game._isWon = true;
                    return _continueShooting = false;
                }

                if (result is ShotResult.Hit or ShotResult.Sunk)
                {
                    Console.Clear();
                    _hitCounter++;
                    
                    if (result == ShotResult.Sunk)
                    {
                        List<Cell> cellsToAvoid = ship.GetCopyOfCellsAroundShip();
                        
                        foreach (var cellToAvoid in cellsToAvoid)
                        {
                            computerBoard.ChangeCellStateAroundShip(cellToAvoid);
                        }
                    }
                    Console.Clear();
                    Game.NewPrint(humanBoard, computerBoard);
                    Console.WriteLine("Nice shot! You have another attempt!");
                    return _continueShooting = true;
                }
                return _continueShooting = false;
            }
        
        private static void PrintBoardBeforeMove(Board humanBoard, Board computerBoard)
        {
            Console.Clear();
            Game.NewPrint(humanBoard, computerBoard);
        }

        private static void PrintBoardAfterMove(Board humanBoard, Board computerBoard)
        {
            if (!computerBoard.IsAllShipsSunk())
            {
                Console.Clear();
                Game.NewPrint(humanBoard, computerBoard);
            }
        }
    }
