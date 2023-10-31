using UnityEngine;

namespace CodeBase.Generators.MapGenerator
{
  public class MapChunk
  {
    private int _coordinateX;
    private int _coordinateY;
    public bool Visited { get; private set; }

    public MapChunk Parent { get; set; }

    public int X => _coordinateX;
    public int Y => _coordinateY;

    public MapChunk(int x, int y)
    {
      _coordinateX = x;
      _coordinateY = y;
      Visited = false;
    }

    public void VisitChunk()
    {
      Visited = true;
    }
  }
}
