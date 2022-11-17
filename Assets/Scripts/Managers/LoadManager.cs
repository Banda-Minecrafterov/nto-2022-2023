using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadManager : MonoBehaviour
{
    IEnumerator coroutine;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene("Load Screen");
        coroutine = LoadSceneAsync(scene);
        StartCoroutine(coroutine);
    }


    IEnumerator LoadSceneAsync(string scene)
    {
        yield return null;
        Debug.Log("Start loading: " + scene);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            Debug.Log('\t' + async.progress);

            if (async.progress >= 0.9f)
                async.allowSceneActivation = true;
            yield return null;
        }
        StopCoroutine(coroutine);
        Destroy(this);

        Debug.Log("Stop loading: " + scene);
        yield return null;
    }
}
