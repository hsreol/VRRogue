using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BNG;
using UnityEngine.InputSystem;

public class StatusUI : MonoBehaviour
{ 
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Level_point;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI MeleeDamage;
    public TextMeshProUGUI RangeDamage;
    public TextMeshProUGUI EXP;
    public TextMeshProUGUI Sword_1H;
    public TextMeshProUGUI Sword_2H;
    public TextMeshProUGUI Spear_1H;
    public TextMeshProUGUI Club_1H;
    public TextMeshProUGUI Bow;

    public Image CurrentHP;

    public PlayerStatManager playerStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerStat = GameObject.Find("PlayerStat").GetComponent<PlayerStatManager>();
        Level.text = "Level : "+ playerStat.level.ToString();
        Level_point.text = "Level Point : " + playerStat.levelUpPoint.ToString();
        HP.text = "HP : " + playerStat.playerhealth.currentHealth.ToString() + " / "+ playerStat.playerhealth.maxHealth.ToString();
        MeleeDamage.text = "Melee : " + playerStat.meleeDamage.ToString();
        RangeDamage.text = "Range : " + playerStat.rangeDamage.ToString();
        EXP.text = "EXP : " + playerStat.exp.ToString() +" / 10";
        Sword_1H.text = string.Format("{0:N2}", playerStat.prof.Sword_1H);
        Sword_2H.text = string.Format("{0:N2}", playerStat.prof.Sword_2H);
        Spear_1H.text = string.Format("{0:N2}", playerStat.prof.Spear_1H);
        Club_1H.text = string.Format("{0:N2}", playerStat.prof.Club_1H);
        Bow.text = string.Format("{0:N2}", playerStat.prof.Bow);

        CurrentHP.fillAmount = playerStat.playerhealth.currentHealth / playerStat.playerhealth.maxHealth;
    }
}
