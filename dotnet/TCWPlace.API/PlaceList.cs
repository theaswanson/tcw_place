namespace TCWPlace.API
{
    public class PlaceList
    {
        public static int MAX_X = 320;
        public static int MAX_Y = 180;

        private readonly List<List<string>> _canvas;

        public PlaceList()
        {
            _canvas = Enumerable.Repeat(Enumerable.Repeat("#000000", MAX_Y).ToList(), MAX_X).ToList();
        }

        public void Change(int x, int y, string color)
        {
            _canvas.ElementAt(x)[y] = $"#{color}";
        }

        public List<List<string>> Get() => _canvas;
    }
}
