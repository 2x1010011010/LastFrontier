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

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
      RegisterServices();
      _sceneLoader.Load(sceneName: Initial, onLoadAction: EnterLoadLevel);
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState, string>("Main");

    public void Exit()
    {
      throw new NotImplementedException();
    }
    
    private void RegisterServices()
    {
      Game.InputService = InputService();

      AllServices.Container.RegisterSingle<IInputService>(InputService());
      AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
    }
    
    private static IInputService InputService()
    {
      if (Application.isEditor)
        return new DesktopInputService();
      else
        return new MobileInputService();
    }
  }
}