using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Invector;


public class WeaponProf{
    public float Sword_1H, Sword_2H, Bow, Spear_1H, Club_1H;            
    public WeaponProf(float Sword_1H, float Sword_2H, float Bow, float Spear_1H, float Club_1H){
        this.Sword_1H = Sword_1H;
        this.Sword_2H = Sword_2H;
        this.Bow = Bow;
        this.Spear_1H = Spear_1H;
        this.Club_1H = Club_1H;
    }
    public float GetProf(string typeName){
        var result = this.GetType().GetField(typeName).GetValue(this);
        return (float)result;
    }
    public void ProfUp(string typeName)
    {
        FieldInfo info = this.GetType().GetField(typeName);
        float profBuffer = (float)info.GetValue(this) + 0.01f;
        info.SetValue(this, profBuffer);
    }
}
public class PlayerStatManager : MonoBehaviour
{
    public int level;
    public int levelUpPoint;
    public int meleeDamage;
    public int rangeDamage;
    public int hp_point;
    public int exp;

    public vPlayerHealthController playerhealth;

    public WeaponProf prof = new WeaponProf(0,0,0,0,0);
    //Sword_1H, Sword_2H, Bow, Spear_1H, Club_1H


    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        levelUpPoint = 0;
        meleeDamage = 10;
        rangeDamage = 10;
        hp_point = 0;
        exp = 0;
        prof.Sword_1H = 0.3f; prof.Sword_2H = 0.3f; prof.Bow = 0.3f; prof.Spear_1H = 0.3f; prof.Club_1H = 0.3f;

        playerhealth = GetComponent<vPlayerHealthController>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void hp_up()
    {
        if(levelUpPoint > 0 && playerhealth.currentHealth > 0)
        {
            hp_point++;
            playerhealth.ChangeMaxHealth(10);
            playerhealth.AddHealth(10);
            levelUpPoint--;
        }
    }

    public void melee_up()
    {
        if (levelUpPoint > 0)
        {
            meleeDamage++;
            levelUpPoint--;
        }
    }

    public void range_up()
    {
        if (levelUpPoint > 0)
        {
            rangeDamage++;
            levelUpPoint--;
        }
    }
    public virtual void ExpUp()
    {
        exp += 1;
        if(exp >= 10){
            LevelUp();
            exp = 0;
        }
    }
    public virtual void LevelUp()
    {
        level++;
        levelUpPoint += 5;
    }
}
