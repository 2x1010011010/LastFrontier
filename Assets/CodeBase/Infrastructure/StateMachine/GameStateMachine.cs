using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.StateMachine.States;


namespace CodeBase.Infrastructure.StateMachine
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _currentState;

    public GameStateMachine(SceneLoader sceneLoader)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
        [typeof(GameLoopState)] = new GameLoopState(this),
        
      };
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }
    
    public void Enter<TState, TPayload>(TPayload payLoad) where TState : class, IPayloadedState<TPayload>
    {
      IPayloadedState<TPayload> state = ChangeState<TState>();
      state.Enter(payLoad);
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _currentState?.Exit();
      TState state = GetState<TState>();
      _currentState = state;
      return state;
    }
  }
}