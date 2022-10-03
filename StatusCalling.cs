using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BNG;
using UnityEngine.InputSystem;

public class StatusCalling : MonoBehaviour
{
    public GameObject Status;
    public GameObject HP;
    public GameObject cam;
    private OptionUI ui;

    private bool isClicked;

    private void Start()
    {
        ui = GameObject.Find("Option").GetComponent<OptionUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputBridge.Instance.AButtonDown && !isClicked)
        {
            Vector3 dir = cam.transform.localRotation * Vector3.forward;
            Status.transform.localRotation = Quaternion.LookRotation(dir);
            isClicked = true;
        }
        else if(InputBridge.Instance.AButtonDown && isClicked)
        {
            ui.Init();
            isClicked = false;
        }

        Status.GetComponent<RectTransform>().anchoredPosition3D = cam.transform.position + ui.addPos_Stat;
        HP.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-160f + ui.addPos_HP.x, -40f + ui.addPos_HP.y, 550f + ui.addPos_HP.z);
        Status.SetActive(isClicked);
        //HP.SetActive(!isClicked);
    }
}
