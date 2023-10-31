using System.Collections.Generic;
using System.Linq;
using CodeBase.Generators.MapGenerator.ScriptableObjects;
using UnityEngine;

namespace CodeBase.Generators.MapGenerator
{
  public class MapSpawner : MonoBehaviour
  {
    private Chunk _mapChunkPrefab;
    private Chunk _roadChunkPrefab;
    private GameObject _castlePrefab;
    private int _mapSizeX;
    private int _mapSizeZ;
    private Vector3 _chunkSize;
    private Chunk[,] _spawnedChunks;
    private MapChunk[,] _mapChunks;
      
    public Chunk[,] SpawnedChunks => _spawnedChunks;
    public MapChunk[,] MapChunks => _mapChunks;

    public void Initialize(MapGeneratorSettings settings)
    {
      _mapChunkPrefab = settings.MapChunkPrefab;
      _roadChunkPrefab = settings.RoadChunkPrefab;
      _castlePrefab = settings.CastlePrefab;
      _mapSizeX = settings.MapSizeX;
      _mapSizeZ = settings.MapSizeZ;
      _chunkSize = settings.ChunkSize;
    }

    public void SpawnMap()
    {
      var generator = new MapGenerator(_mapSizeX, _mapSizeZ);
      _mapChunks = generator.GenerateMap();
      _spawnedChunks = new Chunk[_mapSizeX, _mapSizeZ];

      for (int x = 0; x < _mapSizeX; x++)
      {
        for (int z = 0; z < _mapSizeZ; z++)
        {
          Chunk chunk = Instantiate(_mapChunkPrefab, new Vector3(x * _chunkSize.x, 0, z * _chunkSize.z), _mapChunkPrefab.transform.rotation).GetComponent<Chunk>();
          chunk.transform.localScale = Vector3.one;
          _spawnedChunks[x, z] = chunk;
        }
      }
      
      var finishPoint = SpawnCastle();
      var startPoint = SpawnPortal();
      SpawnRoad(startPoint, finishPoint);
    }

    private void SpawnRoad(MapChunk start, MapChunk finish)
    {
      foreach (var chunk in FindPath(start, finish))
        SpawnRoadChunk(chunk.X, chunk.Y);
    }

    private MapChunk SpawnCastle()
    {
      var castle = Instantiate(_castlePrefab, new Vector3(_mapSizeX / 2 * _chunkSize.x, 0, _mapSizeZ * _chunkSize.z + _chunkSize.z), _castlePrefab.transform.rotation);
      SpawnRoadChunk(_mapSizeX/2, _mapSizeZ-1);
      return _mapChunks[_mapSizeX / 2, _mapSizeZ - 1];
    }

    private MapChunk SpawnPortal()
    {
      var portalCoordinateX = Random.Range(0, _mapSizeX);
      SpawnRoadChunk(portalCoordinateX, 0);
      return _mapChunks[portalCoordinateX, 0];
    }

    private void SpawnRoadChunk(int x, int z)
    {
      Destroy(_spawnedChunks[x, z].gameObject);
      var roadChunk = Instantiate(_roadChunkPrefab, new Vector3(x * _chunkSize.x,0, z * _chunkSize.z), _roadChunkPrefab.transform.rotation);
      _spawnedChunks[x, z] = roadChunk;
      _mapChunks[x, z].VisitChunk();
    }

    private List<MapChunk> FindPath(MapChunk start, MapChunk finish)
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

    private List<MapChunk> FindNeighbors(MapChunk currentChunk)
    {
      List<MapChunk> neighbors = new List<MapChunk>();

      for (int x = -1; x <= 1; x++)
      {
        for (int y = -1; y <= 1; y++)
        {
          if (Mathf.Abs(x) == Mathf.Abs(y))
            continue;

          var checkX = currentChunk.X + x;
          var checkY = currentChunk.Y + y;

          if (checkX >= 0 && checkX < _mapSizeX && checkY >= 0 && checkY < _mapSizeZ)
          {
            neighbors.Add(_mapChunks[checkX, checkY]);
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
