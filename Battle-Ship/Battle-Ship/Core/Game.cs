namespace Battle_Ship;

public class Game
{
    private bool isWon = false;
    public void Play()
    {
        Player player1 = new HumanPlayer();
        Player player2 = new HumanPlayer();
        
        Board board1 = new Board();
        Board board2 = new Board();
        
        SetupShips(board1);
        SetupShips(board2);

        do
        {
            MakeMove(player1, board1, board2);
            MakeMove(player2, board2, board1);
        } while (!isWon);
        
    }

    private void SetupShips(Board board)
    {
        SetShipsHelper.SetShips(board);
        Console.WriteLine("Press any key to pass the run");
        Console.ReadLine();
        Console.Clear();
    }
    
    private void MakeMove(Player player, Board myBoard, Board enemyBoard)
    {
        bool isShot = false;
        
        myBoard.Print(false);
        Console.WriteLine();
        enemyBoard.Print(true);
        
        do
        {
            var cell = player.GetShot();
            ShotResult result = enemyBoard.ReceiveShot(cell.row, cell.col);

            if (enemyBoard.IsAllShipsSunk())
            {
                Console.WriteLine($"You won! You've had {player.moveCounter} moves!");
                isWon = true;
                return;
            }

            if (result is ShotResult.Hit or ShotResult.Sunk)
            {
                Console.Clear();
                enemyBoard.Print(true);
                Console.WriteLine("Nice shot! You have another attempt!");
                isShot = true;
            }
            else
            {
                isShot = false;
            }
    
        } while (isShot); 
        
        Console.Clear();
        enemyBoard.Print(true);
        Console.WriteLine("Press any key to pass the run");
        Console.ReadLine();
        Console.Clear();
    }
}