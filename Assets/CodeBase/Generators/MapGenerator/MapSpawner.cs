using CodeBase.Generators.MapGenerator.ScriptableObjects;
using Unity.VisualScripting;
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
      
      SpawnCastle();
      SpawnPortal();
    }

    private void SpawnRoad()
    {
      
    }

    private void SpawnCastle()
    {
      var castle = Instantiate(_castlePrefab, new Vector3(_mapSizeX / 2 * _chunkSize.x, 0, _mapSizeZ * _chunkSize.z + _chunkSize.z), _castlePrefab.transform.rotation);
      SpawnRoadChunk(_mapSizeX/2, _mapSizeZ-1);
    }

    private void SpawnPortal()
    {
      var portalCoordinateX = Random.Range(0, _mapSizeX);
      SpawnRoadChunk(portalCoordinateX, 0);
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
