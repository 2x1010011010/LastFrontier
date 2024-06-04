using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
  public interface IInputService
  {
    Vector3 Axis { get; }

    bool IsButtonPressed();
  }
}