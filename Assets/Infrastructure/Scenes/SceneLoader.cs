using System;
using System.Collections;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Scenes
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string initial, Action onLoad)
        {
            _coroutineRunner.StartCoroutine(LoadScene(initial, onLoad));
        }
        
        private IEnumerator LoadScene(string sceneName, Action onLoad)
        {
            if (AlreadyLoaded(sceneName))
            {
                onLoad?.Invoke();
                yield break;
            }

            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            
            while (!loadSceneAsync.isDone)
                yield return null;
            
            onLoad?.Invoke();
        }

        private static bool AlreadyLoaded(string sceneName) => 
            SceneManager.GetActiveScene().name == sceneName;
    }
}