using CodeBase.Generators.MapGenerator;
using CodeBase.Generators.MapGenerator.ScriptableObjects;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MapSpawner _spawner;
        [SerializeField] private MapGeneratorSettings _mapSettings;
        void Start()
        {
            _spawner.Initialize(_mapSettings);
            _spawner.SpawnMap();
        }
    }
}
