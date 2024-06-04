using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public static IInputService InputService;

    public Game()
    {
      RegisterInputService();
    }

    private static void RegisterInputService()
    {
      if (Application.isEditor)
        InputService = new DesktopInputService();
      else
        InputService = new MobileInputService();
    }
  }
}