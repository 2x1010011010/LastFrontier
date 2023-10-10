using UnityEngine;

namespace CodeBase.Generators.MapGenerator
{
  public class MapSpawner : MonoBehaviour
  {
    [SerializeField] private Chunk _mapChunkPrefab;
    [SerializeField] private Chunk _roadChunkPrefab;
    [SerializeField] private GameObject _castlePrefab;
    [SerializeField] private int _mapSizeX;
    [SerializeField] private int _mapSizeZ;
    [SerializeField] private Vector3 _chunkSize;
    private Chunk[,] _spawnedChunks;
    private MapChunk[,] _mapChunks;
      
    public Chunk[,] SpawnedChunks => _spawnedChunks;
    public MapChunk[,] MapChunks => _mapChunks;
      
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
    }

    private void SpawnRoad()
    {
      
    }

    private void SpawnCastle()
    {
      var castle = Instantiate(_castlePrefab, new Vector3((_mapSizeX / 2) * _chunkSize.x, 0, _mapSizeZ * _chunkSize.z + _chunkSize.z), _castlePrefab.transform.rotation);
    }

    private void SpawnPortal()
    {
      
    }
  }
}
