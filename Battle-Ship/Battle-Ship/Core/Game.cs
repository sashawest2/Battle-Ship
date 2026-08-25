namespace Battle_Ship;

public class Game
{
    private bool isWon = false;

    public void Play()
    {
        Player player1 = new HumanPlayer();
        Player? player2 = null;

        bool isPlayerCreated = false;
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


        Board board1 = new Board();
        Board board2 = new Board();

        SetupShips(board1);
        if (player2 is HumanPlayer)
        {
            SetupShips(board2);
        }

        if (player2 is ComputerPlayer)
        {
            SetupComputerShips(board2);
        }

        do
        {
            MakeMoveHelper.MakeMove(player1, board1, board2);
            MakeMoveHelper.MakeMove(player2, board2, board1);

        } while (!isWon);

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
}

   