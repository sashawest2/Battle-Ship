namespace Battle_Ship;


    public class HumanPlayer : Player
    {
        public override (int row, int col) GetShot()
        {
            (int row, int col) = UserInputHelper.ParseCoordinate();
            _moveCounter++;
            return (row, col);
        }

        public override void MakeMove(Board playerBoard, Board enemyBoard)
        {
            PrintBoardBeforeMove(playerBoard, enemyBoard);
        
            do
            {
                var cell = GetShot();
                ShotResult result = enemyBoard.ReceiveShot(cell.row, cell.col);

                if (enemyBoard.IsAllShipsSunk())
                {
                 
                    Console.WriteLine($"You won! You've had {_moveCounter} moves!");
                    _isWon = true;
                    return;
                }

                if (result is ShotResult.Hit or ShotResult.Sunk)
                {
                    
                    Console.Clear();
                    enemyBoard.Print(true);
                    Console.WriteLine("Nice shot! You have another attempt!");
                    _isShot = true;
                }
                else
                {
                    _isShot = false;
                }
    
            } while (_isShot); 
        
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
