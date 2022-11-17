using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static GameObject managger;

    void Awake()
    {
        if (managger != null)
        {
            Debug.LogWarning("Manager already exists");
            Destroy(gameObject);
            return;
        }
        
        managger = gameObject;
        DontDestroyOnLoad(gameObject);
    }


    public static void LoadScene(string scene)
    {
        managger.AddComponent<LoadManager>().LoadScene(scene);
    }
}
