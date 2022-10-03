using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    StageManager stageManager;
    public TextMeshProUGUI MonsterCount;

    void Start()
    {
        stageManager = GameObject.Find("GameManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterCount.text = "Monsters : " + (stageManager.enemy_count - stageManager.kill_count).ToString();
    }
}
