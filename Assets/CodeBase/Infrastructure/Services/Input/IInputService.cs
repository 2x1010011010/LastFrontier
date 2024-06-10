using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
  public interface IInputService : IService
  {
    Vector3 Axis { get; }

    bool IsButtonPressed();
  }
}