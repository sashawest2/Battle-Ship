namespace Battle_Ship;

public abstract class Player
{
    private string Name{get;init;}
    private Board OwnBoard{get;init;}

    public virtual (int row, int col) GetShot()
    {
       Random rnd = new Random();
       return (rnd.Next(10), rnd.Next(10)); 
    }
}

public class HumanPlayer : Player
{
    public override (int row, int col) GetShot()
    {
        (int row, int col) = UserInputHelper.ParseCoordinate();
        return (row, col);
    }
}