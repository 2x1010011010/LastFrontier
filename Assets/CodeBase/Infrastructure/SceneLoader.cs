using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) =>
      _coroutineRunner = coroutineRunner;

    public void Load(string name, Action action = null) =>
      _coroutineRunner.StartCoroutine(LoadScene(name, action));

    private  IEnumerator LoadScene(string nextSceneName, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextSceneName)
      {
        onLoaded?.Invoke();
        yield break;
      }

      var waitNextScene = SceneManager.LoadSceneAsync(nextSceneName);

      while (!waitNextScene.isDone)
        yield return null;
      
      onLoaded?.Invoke();
    }
  }
}