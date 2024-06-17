using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      
      RegisterServices();
    }

    public void Enter()
    {
      _sceneLoader.Load(sceneName: Initial, onLoadAction: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IInputService>(InputService());
      _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState, string>("Main");

    private static IInputService InputService() =>
      Application.isEditor ? new DesktopInputService() : new MobileInputService();
  }
}