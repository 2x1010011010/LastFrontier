using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
  public abstract class InputService:IInputService
  {
    public abstract Vector3 Axis { get; }
    public bool IsButtonPressed()
    {
      return true;
    }
  }
}