namespace Battle_Ship;

public abstract class Player
{
    private string Name{get;init;}
    private Board OwnBoard{get;init;}
    public int moveCounter = 0;

    public virtual (int row, int col) GetShot()
    {
       Random rnd = new Random();
       moveCounter++;
       return (rnd.Next(10), rnd.Next(10)); 
    }
}

public class HumanPlayer : Player
{
    public override (int row, int col) GetShot()
    {
        (int row, int col) = UserInputHelper.ParseCoordinate();
        moveCounter++;
        return (row, col);
    }
}