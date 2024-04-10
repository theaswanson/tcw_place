namespace TCWPlace.API
{
    public class PlaceArray
    {
        public static int MAX_X = 320;
        public static int MAX_Y = 180;

        private readonly string[][] _canvas;

        public PlaceArray()
        {
            _canvas = new string[MAX_X][];

            for (var i = 0; i < MAX_X; i++)
            {
                _canvas[i] = new string[MAX_Y];
                Array.Fill(_canvas[i], "#000000");
            }
        }

        public void Change(int x, int y, string color)
        {
            _canvas[x][y] = $"#{color}";
        }

        public string[][] Get() => _canvas;
    }
}
