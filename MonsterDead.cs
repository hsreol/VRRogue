using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDead : MonoBehaviour
{
    private List<GameObject> Weapons;

    public void DropEquipment()
    {
        int random = Random.Range(1, 101);

        Weapons = GameObject.Find("GameManager").GetComponent<DropList>().droplist;

        if(random < 61)
        {
            if(random < 11)
            {
                GameObject Drops = Instantiate(Weapons[0], transform.position, transform.rotation);
                Drops.name = Weapons[0].name;
            }
            else if(random < 21)
            {
                GameObject Drops = Instantiate(Weapons[1], transform.position, transform.rotation);
                Drops.name = Weapons[1].name;
            }
            else if (random < 31)
            {
                GameObject Drops = Instantiate(Weapons[2], transform.position, transform.rotation);
                Drops.name = Weapons[2].name;
            }
            else if (random < 41)
            {
                GameObject Drops = Instantiate(Weapons[3], transform.position, transform.rotation);
                Drops.name = Weapons[3].name;
            }
            else if (random < 51)
            {
                GameObject Drops = Instantiate(Weapons[4], transform.position, transform.rotation);
                Drops.name = Weapons[4].name;
            }
            else
            {
                GameObject Drops = Instantiate(Weapons[5], transform.position, transform.rotation);
                Drops.name = Weapons[5].name;
            }
        }
        else
        {
            
        }
    }

    public void DeadEvent()
    {
        GameObject.Find("GameManager").GetComponent<StageManager>().kill();
        GameObject.Find("PlayerStat").GetComponent<PlayerStatManager>().ExpUp();
    }
}
