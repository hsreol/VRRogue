using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCount : MonoBehaviour
{
    public GameObject[] enemys;
    public int enemycount;

    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        enemycount = enemys.Length;
        GameObject.Find("GameManager").GetComponent<StageManager>().enemy_count = enemys.Length;
    }
}
