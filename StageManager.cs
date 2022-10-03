using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public int kill_count;
    public int enemy_count;
    public int this_stage = 0;
    public int next_stage = 0;
    public static int stage_level;

    public List<string> scenes = new List<string>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        stage_level = 1;
        enemy_count = 14;
        init();
    }

    public bool isClear()
    {
        if(kill_count < enemy_count)
        {
            return false;
        }
        else
        {
            kill_count = 0;
            enemy_count = 0;
            return true;
        }
    }

    private void init()
    {
        scenes.Clear();
        scenes.Add("Scene1");
        scenes.Add("Scene1_2");
    }

    public void kill()
    {
        kill_count++;
        if (isClear())
        {
            Clear();
        }
    }

    public void Clear()
    {
        next_stage = Random.Range(0, 2);
        if (this_stage != next_stage)
        {
            stage_level += 1;
            this_stage = next_stage;
            LoadingSceneManager.LoadScene(scenes[next_stage]);
        }
        else
        {
            Clear();
        }
        
    }

    public void GameOver()
    {
        LoadingSceneManager.LoadScene("GameOver");
    }

    public void GameStart()
    {
        LoadingSceneManager.LoadScene(scenes[0]);
    }
}
