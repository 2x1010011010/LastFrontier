using CodeBase.Generators.MapGenerator;
using CodeBase.Generators.MapGenerator.ScriptableObjects;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public sealed class Bootstrap : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private MapSpawner _spawner;
    [SerializeField] private MapGeneratorSettings _mapSettings;
    private Game _game;

    private void Awake()
    {
      _game = new Game(this);
      _game.StateMachine.Enter<BootstrapState>();
      DontDestroyOnLoad(this);
    }

    /*private void Start()
    {
      _spawner.Initialize(_mapSettings);
      _spawner.SpawnMap();
    }*/
  }
}