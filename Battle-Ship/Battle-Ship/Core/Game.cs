namespace Battle_Ship;

public class Game
{
    public static bool _isWon = false;
    private bool _statisticsCalculated = false;
    HumanPlayer player1;
    Player player2;
    Board board1 = new Board();
    Board board2 = new Board();
    
    

    public void Play()
    {
        player1 = new HumanPlayer();
        SetupShips(board1);
        player2 = CreateSecondPlayer(board2);
        SetupShipsSecondPlayer(player2, board2);

        NewPrint(board1, board2);
        
        do
        {
            bool isStat = false;
            do
            {
                MoveType moveResult = player1.MakeMove(board1, board2, isStat);

                if (moveResult == MoveType.Stat)
                {
                    isStat = true;
                    PrintStatistics();
                }
                else
                {
                    isStat = false;
                }
            }
            while (isStat);
           
           
           if (_isWon)
           {
               break;
           }
           
           player2.MakeMove(board1, board2, false);

        } while (!_isWon);
        
        Console.WriteLine($"Hit percentage: {Math.Round(GetHitPercentage(player1), 2)}%");
        GetStatistics();
    }

    public static void ComputerWinMessage()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You lost :(");
    }
    private double GetHitPercentage(HumanPlayer humanPlayer)
    {
        double hitPercentage = (double)humanPlayer._hitCounter / humanPlayer.MoveCounter * 100;
        return hitPercentage;
    }
    private void SetupShips(Board board)
    {
        SetShipsHelper.SetShips(board);
        Console.WriteLine();
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
    
    
    public static void NewPrint(Board humanBoard, Board computerBoard)
    {
        humanBoard.PrintHeaders();
            
        Console.Write("     ");
            
        computerBoard.PrintHeaders();
        
        Console.WriteLine();
        
        for (int i = 0; i < 10; i++)
        {
            
            humanBoard.PrintRow(i, false);
            
            Console.Write("        ");
            
            computerBoard.PrintRow(i, true);
            
            Console.WriteLine();
        }
    }

    private void PrintStatistics()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        
        Console.Write("Left:");
        Console.Write("                                             ");
        Console.WriteLine("Left:");
        
       GetStatistics();
    }  
    
    public void GetStatistics()
    {
        board1.GetAmountOfSunkShips();
        board2.GetAmountOfSunkShips();
    

        Console.Write($"{board1.oneDeckShipsCount} *");
        Console.Write("                                               ");
        Console.WriteLine($"{board2.oneDeckShipsCount} *");
        
        Console.Write($"{board1.twoDeckShipsCount} **");
        Console.Write("                                              ");
        Console.WriteLine($"{board2.twoDeckShipsCount} **");
        
        Console.Write($"{board1.threeDeckShipsCount} ***");
        Console.Write("                                             ");
        Console.WriteLine($"{board2.threeDeckShipsCount} ***");
        
        Console.Write($"{board1.fourDeckShipsCount} ****");
        Console.Write("                                            ");
        Console.WriteLine($"{board2.fourDeckShipsCount} ****");
    }
}



   