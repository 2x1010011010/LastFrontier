namespace CodeBase.Generators.MapGenerator
{
  public class MapGenerator
  {
    private int _width;
    private int _lenght;

    public MapGenerator(int width, int lenght)
    {
      _width = width;
      _lenght = lenght;
    }

    public MapChunk[,] GenerateMap()
    {
      var map = new MapChunk[_width, _lenght];

      for (int x = 0; x < _width; x++)
      for (int y = 0; y < _lenght; y++)
        map[x, y] = new MapChunk(x, y);

      return map;
    }
  }
}
