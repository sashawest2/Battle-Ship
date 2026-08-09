using Battle_Ship;

Board board = new Board();
List<Ship> ships = new List<Ship>()
{
    new Ship(1, 3, 1, false),
    new Ship(2, 9, 1, false),
    new Ship(7, 9, 1, false),
    new Ship(8, 8, 1, false),
    new Ship(3, 1, 2, false),
    new Ship(2, 7, 2, false),
    new Ship(3, 3, 2, true),
    new Ship(9, 4, 3, true),
    new Ship(7, 2, 3, true),
    new Ship(5, 3, 4, true)
    
    
};

foreach (Ship ship in ships)
{
    board.PlaceShipDirectly(ship);
}
board.PrintGrid();

board.MakeMove();



