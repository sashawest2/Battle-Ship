namespace Battle_Ship;

public class Player
{
    string Name { get; set; }
    Board OwnBoard { get; set; }

    public virtual (int row, int col) GetShot()
    {
        Random rnd = new Random();

        int row = rnd.Next(10);
        int col = rnd.Next(10);
        
        return (row, col);
    }
}

public class HumanPlayer : Player
{
    public override (int row, int col) GetShot()
    {
        return UserInputHelper.InputCoordinate();
    }
}