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

    public void Load(string sceneName, Action onLoadAction = null) =>
      _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoadAction));

    private IEnumerator LoadScene(string nextSceneName, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextSceneName)
      {
        OnLoadedAction(onLoaded);
        yield break;
      }

      var waitNextScene = SceneManager.LoadSceneAsync(nextSceneName);

      while (!waitNextScene.isDone)
        yield return null;
      
      OnLoadedAction(onLoaded);
    }

    private void OnLoadedAction(Action onLoaded) =>
      onLoaded?.Invoke();
  }
}