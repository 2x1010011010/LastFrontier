using System;
using CodeBase.Generators.MapGenerator;

namespace CodeBase.Data
{
  [Serializable]
  public class LevelData
  {
    public string Name;
    public int Width;
    public int Length;
    public MapChunk[,] Map;
    
    public LevelData(string name, int width, int length, MapChunk[,] map)
    {
      Name = name;
      Width = width;
      Length = length;
      Map = new MapChunk[Width, Length];
      Map = map;
    }
  }
}