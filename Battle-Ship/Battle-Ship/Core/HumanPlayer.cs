namespace Battle_Ship;


    public class HumanPlayer : Player
    {
        private Cell GetShot()
        {
            Cell cell = UserInputHelper.ParseCoordinate();
            MoveCounter++;
            return cell;
        }

        public override void MakeMove(Board playerBoard, Board enemyBoard)
        {
            PrintBoardBeforeMove(playerBoard, enemyBoard);
        
            do
            {
                var cell = GetShot();
                var (result, ship) = enemyBoard.ReceiveShot(cell);

                if (enemyBoard.IsAllShipsSunk())
                {
                 
                    Console.WriteLine($"You won! You've had {MoveCounter} moves!");
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
