using UnityEngine;

public class Manager : MonoBehaviour
{
    static GameObject manager;


    void Awake()
    {
        if (manager != null)
        {
            Debug.LogWarning("Manager already exists");
            Destroy(gameObject);
            return;
        }
        
        manager = gameObject;
        DontDestroyOnLoad(gameObject);
    }


    public static LoadData LoadSaveFile(int id)
    {
        LoadData ret = manager.AddComponent<LoadData>();
        ret.Open(id);
        return ret;
    }

    public static SaveData SaveSaveFile(int id)
    {
        SaveData ret = manager.AddComponent<SaveData>();
        ret.Open(id);
        return ret;
    }


    public static void LoadScene(string scene)
    {
        if (scene == "Menu")
        {
            foreach (SaveData data in manager.GetComponents<SaveData>())
            {
                Destroy(data);
            }
        }

        manager.AddComponent<LoadSceneManager>().LoadScene(scene);
    }

    public static void FinishLoadScreen()
    {
        LoadData comp;
        if (manager.TryGetComponent<LoadData>(out comp))
            comp.Load();
    }
}
