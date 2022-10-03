using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    public void Restart()
    {
        Destroy(GameObject.Find("XR Rig Advanced"));
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("Status_UI"));
        Destroy(GameObject.Find("EventSystem"));
        LoadingSceneManager.LoadScene("GameStart");
    }
}
