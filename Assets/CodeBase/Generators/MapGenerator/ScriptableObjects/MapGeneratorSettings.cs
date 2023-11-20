using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Generators.MapGenerator.ScriptableObjects
{
  [CreateAssetMenu(fileName = "MapGeneratorSettings", menuName = "Scriptable Objects/Map Generator", order = 51)]
  public class MapGeneratorSettings : ScriptableObject
  {
    [field: SerializeField] public List<Chunk> MapChunkPrefabs { get; private set; }
    [field: SerializeField] public Chunk RoadChunkPrefab { get; private set; }
    [field: SerializeField] public GameObject CastlePrefab { get; private set; }
    [field: SerializeField] public int MapSizeX { get; private set; }
    [field: SerializeField] public int MapSizeZ { get; private set; }
    [field: SerializeField] public Vector3 ChunkSize { get; private set; }
  }
}
