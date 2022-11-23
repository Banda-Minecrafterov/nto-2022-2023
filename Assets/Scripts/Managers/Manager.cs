using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        manager.AddComponent<LoadSceneManager>().LoadScene(scene);
    }

    public static void FinishLoadScreen()
    {
        LoadData data = manager.GetComponent<LoadData>();

        Debug.Log("Manager Load");
        if (data != null)
        {
            data.Load();
        }
    }
}
