using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Equipment mEquip;
    public string weaponName1H, weaponName2H, weaponDamage, weaponType, weaponProfType;
    public GameObject GameManager;

    StatManager StatManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        StatManager = GameManager.GetComponent<StatManager>();
        Init();
    }

    public virtual void Init()
    {
        //이름이랑 매칭되는 무기 서칭
        //서칭되면 공격력이랑 타입 가져오기
        try
        {
            for (int i = 0; i < StatManager.Equipments_d.Count; i++)
            {
                if (weaponName1H != null && StatManager.Equipments_d[i].Name == weaponName1H)
                {
                    mEquip = StatManager.Equipments_d[i];
                    break;
                }
                else
                {
                    mEquip = StatManager.Equipments_d[0];
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error");
        }

        weaponDamage = mEquip.ATK;
        weaponType = mEquip.Type_A;
        weaponProfType = mEquip.Type_P;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void HandSwitch()
    {
        //이름이랑 매칭되는 무기 서칭
        //서칭되면 공격력이랑 타입 가져오기
        try
        {
            for (int i = 0; i < StatManager.Equipments_d.Count; i++)
            {
                if (weaponName2H != null && StatManager.Equipments_d[i].Name == weaponName2H)
                {
                    mEquip = StatManager.Equipments_d[i];
                    break;
                }
                else
                {
                    mEquip = StatManager.Equipments_d[0];
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error");
        }
        //이름이랑 매칭되는 무기 서칭
        //서칭되면 공격력이랑 타입 가져오기
        weaponDamage = mEquip.ATK;
        weaponType = mEquip.Type_A;
        weaponProfType = mEquip.Type_P;
    }
}
