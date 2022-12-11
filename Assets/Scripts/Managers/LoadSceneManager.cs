using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static void LoadScene(string scene, int id = 0)
    {
        GameObject gameObject = new GameObject();
        DontDestroyOnLoad(gameObject);
        gameObject.AddComponent<LoadSceneManager>().StartCoroutine(LoadScene(scene, id, gameObject));
    }


    static IEnumerator LoadScene(string scene, int id, GameObject gameObject)
    {
        SceneManager.LoadScene("Load Screen");

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
        yield return null;

        if (scene == "Game")
        {
            SaveLoadManager.Load(id);
        }
        AudioManager.SetAudioVolume();

        Destroy(gameObject);

        Debug.Log("Stop loading: " + scene);
    }
}
