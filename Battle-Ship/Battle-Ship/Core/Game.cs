namespace Battle_Ship;

public class Game
{
    public void Play()
    {
        Player player1 =  new HumanPlayer();
        Player player2 = new HumanPlayer();
        
        Board boardPlayer1 = new Board();
        Board boardPlayer2 = new Board();
        
      
        UserInputSetup(boardPlayer1);
        UserInputSetup(boardPlayer2);

        do
        {
            MessageBeforeMove(boardPlayer1, boardPlayer2);
            

            boardPlayer1.MakeMove(player1, boardPlayer2);

            if (boardPlayer2.IsAllShipsSunk())
            {
                break;
            }
            
            MessageAfterMove(boardPlayer2);
            
            MessageBeforeMove(boardPlayer2, boardPlayer1);
            (int row, int col) = player2.GetShot();
            boardPlayer1.ReceiveShot(row, col);

            if (boardPlayer1.IsAllShipsSunk())
            {
                break;
            }
            
            MessageAfterMove(boardPlayer1);
            
        } while (!boardPlayer2.IsAllShipsSunk() || !boardPlayer1.IsAllShipsSunk());
    }

    void UserInputSetup(Board board)
    {
        SetupFleetHelper.SetupShips(board);
        Console.WriteLine("Передайте ход другому игроку, нажмите Enter"); 
        Console.ReadLine();
        Console.Clear();
    }

    void MessageBeforeMove(Board board1, Board board2)
    {
        Console.WriteLine("Ваша доска:");
        board1.Print(false);
        Console.WriteLine("Доска противника:");
        board2.Print(true );

        Console.WriteLine("Введите клетку противника, в которую хотите попасть");
    }

    void MessageAfterMove(Board board2)
    {
        Console.Clear();
        Console.WriteLine("Доска противника:");
        board2.Print(true );
        Console.WriteLine("Передайте ход игроку 2, нажмите Enter"); 
        Console.ReadLine();
        Console.Clear();
    }
}