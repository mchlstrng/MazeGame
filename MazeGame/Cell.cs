namespace MazeGame
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsWall { get; set; }

        public Cell(int x, int y, bool isWall)
        {
            X = x;
            Y = y;
            IsWall = isWall;
        }
    }
}