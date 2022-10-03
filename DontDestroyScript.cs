using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyScript : MonoBehaviour
{
    bool save = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (StageManager.stage_level < 2 && !save)
        {
            DontDestroyOnLoad(gameObject);
            save = true;
        }
        else
        {
            Destroy(gameObject);
        }
            
    }
}
