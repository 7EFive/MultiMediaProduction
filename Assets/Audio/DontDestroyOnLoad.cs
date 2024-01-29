using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{

    public static DontDestroyOnLoad instance;
    
    
    void Start()
    {
        string name = SceneManager.GetActiveScene().name;
        print(name);
        if (instance != null || name == "Main Menu")
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

   
}
