using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Generators.MapGenerator.ScriptableObjects
{
  [CreateAssetMenu(fileName = "MapGeneratorSettings", menuName = "Scriptable Objects/Map Generator", order = 51)]
  public class MapGeneratorSettings : ScriptableObject
  {
    [field: SerializeField] public List<Chunk> Chunks { get; private set; }
  }
}
