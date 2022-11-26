using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneManager : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene("Load Screen");
        StartCoroutine(LoadSceneAsync(scene));
    }


    IEnumerator LoadSceneAsync(string scene)
    {
        yield return null;
        Debug.Log("Start loading: " + scene);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            Debug.Log("\t" + async.progress);

            if (async.progress >= 0.9f)
                async.allowSceneActivation = true;
            yield return null;
        }
        Manager.FinishLoadScreen();

        Destroy(this);

        Debug.Log("Stop loading: " + scene);
        yield return null;
    }
}
