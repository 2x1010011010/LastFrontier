using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Generators.MapGenerator
{
  public sealed class RoadGenerator
  {
    private MapChunk[,] _map;
    private int _mapSizeX;
    private int _mapSizeZ;

    public RoadGenerator(MapChunk[,] map, int mapSizeX, int mapSizeZ)
    {
      _mapSizeX = mapSizeX;
      _mapSizeZ = mapSizeZ;
      _map = new MapChunk[_mapSizeX, _mapSizeZ];
      _map = map;
    }
    
    public List<MapChunk> FindPath(MapChunk start, MapChunk finish)
    {
      var openSet = new Queue<MapChunk>();
      var closedSet = new HashSet<MapChunk>();
      
      openSet.Enqueue(start);

      while (openSet.Count > 0)
      {
        var currentChunk = openSet.Dequeue();

        if (currentChunk == finish)
        {
          return RetracePath(start, finish);
        }

        foreach (var neighbor in FindNeighbors(currentChunk).Where(neighbor => !closedSet.Contains(neighbor)).Where(neighbor => !openSet.Contains(neighbor)))
        {
          neighbor.Parent = currentChunk;
          openSet.Enqueue(neighbor);
        }

        closedSet.Add(currentChunk);
      }
      
      return null;
    }
    
    public List<MapChunk> SetPoints()
    {
      var randomPoints = new List<MapChunk>();

      for (int i = 0; i < _mapSizeZ; i++)
      {
        if (i%4 == 0)
          randomPoints.Add(_map[Random.Range(0, _mapSizeX), i]);
      }

      return randomPoints;
    }

    private List<MapChunk> FindNeighbors(MapChunk currentChunk)
    {
      var neighbors = new List<MapChunk>();
      int startPoint;
      int lastPoint;
      int term;
        
      if(currentChunk.X >= _mapSizeX / 2)
      {
        startPoint = 1;
        lastPoint = -2;
        term = -1;
      }
      else
      {
        startPoint = -1;
        lastPoint = 2;
        term = 1;
      }
      
      for (int x = startPoint; x != lastPoint; x += term)
      {
        for (int y = -1; y <= 1; y++)
        {
          if (Mathf.Abs(x) == Mathf.Abs(y))
            continue;

          var checkX = currentChunk.X + x;
          var checkY = currentChunk.Y + y;

          if (checkX >= 0 && checkX < _mapSizeX && checkY >= 0 && checkY < _mapSizeZ)
          {
            neighbors.Add(_map[checkX, checkY]);
          }
        }
      }

      return neighbors;
    }

    private List<MapChunk> RetracePath(MapChunk start, MapChunk finish)
    {
      var path = new List<MapChunk>();
      var currentChunk = finish;

      while (currentChunk != start)
      {
        path.Add(currentChunk);
        currentChunk = currentChunk.Parent;
      }

      path.Reverse();
      return path;
    }
  }
}
