using CodeBase.Generators.MapGenerator;
using CodeBase.Generators.MapGenerator.ScriptableObjects;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public sealed class Bootstrap : MonoBehaviour
  {
    [SerializeField] private MapSpawner _spawner;
    [SerializeField] private MapGeneratorSettings _mapSettings;
    private Game _game;

    private void Awake()
    {
      _game = new Game();
      DontDestroyOnLoad(this);
    }

    private void Start()
    {
      _spawner.Initialize(_mapSettings);
      _spawner.SpawnMap();
    }
  }
}