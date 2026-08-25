namespace Battle_Ship;

public static class MakeMoveHelper
{
    private static bool _isWon = false;
    
    public static void MakeMove(Player player, Board playerBoard, Board enemyBoard)
    {
        bool isShot = false;

        if (player is HumanPlayer)
        {
            PrintBoardBeforeMove(playerBoard, enemyBoard);
        }

        if (player is ComputerPlayer)
        {
            Thread.Sleep(500);
        }
        
        do
        {
            var cell = player.GetShot();
            ShotResult result = enemyBoard.ReceiveShot(cell.row, cell.col);

            if (enemyBoard.IsAllShipsSunk())
            {
                if (player is HumanPlayer)
                {
                    Console.WriteLine($"You won! You've had {player.moveCounter} moves!");
                }
                _isWon = true;
                return;
            }

            if (result is ShotResult.Hit or ShotResult.Sunk)
            {
                if (player is HumanPlayer)
                {
                    Console.Clear();
                    enemyBoard.Print(true);
                    Console.WriteLine("Nice shot! You have another attempt!");
                }
                isShot = true;
            }
            else
            {
                isShot = false;
            }
    
        } while (isShot); 
        
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
