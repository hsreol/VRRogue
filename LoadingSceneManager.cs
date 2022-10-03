using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField]
    Image progressBar;
    GameObject Cam;

    private void Start()
    {
        progressBar.fillAmount = 0;
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        try
        {
            Cam = GameObject.Find("XR Rig Advanced");
            Cam.SetActive(false);
        }
        catch
        {
            
        }

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime / 5f;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if(progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if(progressBar.fillAmount == 1.0f)
                {
                    try
                    {
                        Cam.SetActive(true);
                    }
                    catch
                    {
                        
                    }

                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
