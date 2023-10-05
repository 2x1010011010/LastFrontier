using UnityEngine;

namespace CodeBase.Generators.MapGenerator
{
  public class MapChunk
  {
    private int _coordinateX;
    private int _coordinateY;

    public int X => _coordinateX;
    public int Y => _coordinateY;

    public MapChunk(int x, int y)
    {
      _coordinateX = x;
      _coordinateY = y;
    }
  }
}
