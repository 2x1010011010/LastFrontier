using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.StateMachine;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
    }
  }
}