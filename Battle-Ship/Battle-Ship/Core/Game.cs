namespace Battle_Ship;

public class Game
{
    private readonly bool _isWon = false;

    public void Play()
    {
        var board1 = new Board();
        var board2 = new Board();

        Player player1 = new HumanPlayer();
        SetupShips(board1);
        var player2 = CreateSecondPlayer(board2);
        SetupShipsSecondPlayer(player2, board2);

        do
        {
           player1.MakeMove(board1, board2);
           player2.MakeMove(board2, board1);

        } while (!_isWon);

    }

    private void SetupShips(Board board)
    {
        SetShipsHelper.SetShips(board);
        Console.WriteLine("Press any key to pass the run");
        Console.ReadLine();
        Console.Clear();
    }
    
    private void SetupComputerShips(Board board)
    {
        List<Ship> fleet = new List<Ship>();
        board.PlaceFleetRandomly(fleet);
    }
    
    private void SetupShipsSecondPlayer(Player player, Board board)
    {
        if (player is HumanPlayer)
        {
            SetupShips(board);
        }

        if (player is ComputerPlayer)
        {
            SetupComputerShips(board);
        }
    }
    
    private Player CreateSecondPlayer(Board board)
    {
        Player? player2 = null;

        var isPlayerCreated = false;
        do
        {
            Console.WriteLine("Who do you want to play with? (c - computer, p - another person)");
            var answer = Console.ReadLine().Trim().ToLower();
            if (answer == "c")
            {
                player2 = new ComputerPlayer();
                isPlayerCreated = true;
            }
            else if (answer == "p")
            {
                player2 = new HumanPlayer();
                isPlayerCreated = true;
            }
        } while (!isPlayerCreated);
        
        
        return player2;
    }
}



   