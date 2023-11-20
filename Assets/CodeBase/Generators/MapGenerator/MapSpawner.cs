using System.Collections.Generic;
using CodeBase.Generators.MapGenerator.ScriptableObjects;
using UnityEngine;

namespace CodeBase.Generators.MapGenerator
{
  public sealed class MapSpawner : MonoBehaviour
  {
    private List<Chunk> _mapChunkPrefabs = new List<Chunk>();
    private Chunk _roadChunkPrefab;
    private GameObject _castlePrefab;
    private int _mapSizeX;
    private int _mapSizeZ;
    private Vector3 _chunkSize;
    private Chunk[,] _spawnedChunks;
    private MapChunk[,] _mapChunks;
    private HashSet<Chunk> _path;
    private RoadGenerator _roadGenerator;
      
    public Chunk[,] SpawnedChunks => _spawnedChunks;
    public MapChunk[,] MapChunks => _mapChunks;
    public HashSet<Chunk> Path => _path;

    public void Initialize(MapGeneratorSettings settings)
    {
      _mapChunkPrefabs = settings.MapChunkPrefabs;
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
          var instance = _mapChunkPrefabs[Random.Range(0, _mapChunkPrefabs.Count)];
          Chunk chunk = Instantiate(instance, new Vector3(x * _chunkSize.x, 0, z * _chunkSize.z), instance.transform.rotation).GetComponent<Chunk>();
          chunk.transform.localScale = Vector3.one;
          _spawnedChunks[x, z] = chunk;
        }
      }

      _roadGenerator = new RoadGenerator(_mapChunks, _mapSizeX, _mapSizeZ);
      var finishPoint = SpawnCastle();
      var startPoint = SpawnPortal();
      SpawnRoad(startPoint, finishPoint);
    }


    private void SpawnRoad(MapChunk start, MapChunk finish)
    {
      var points = _roadGenerator.SetPoints();
      MapChunk currentStart;
      var currentFinish = start;
      
      for (int i = 0; i < points.Count+1; i++)
      {
        currentStart = currentFinish;
        currentFinish = i>=points.Count?finish:points[i];
        
        foreach (var chunk in _roadGenerator.FindPath(currentStart, currentFinish))
        {
          SpawnRoadChunk(chunk.X, chunk.Y);
        }
      }
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
  }
}
