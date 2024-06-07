using System;
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
      Game.InputService = RegisterInputService();
    }
    
    private static IInputService RegisterInputService()
    {
      if (Application.isEditor)
        return new DesktopInputService();
      else
        return new MobileInputService();
    }
  }
}