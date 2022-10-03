using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPos : MonoBehaviour
{
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            cam = GameObject.Find("PlayerController");
            Vector3 dir = cam.transform.localRotation * Vector3.forward;
            //transform.localRotation = Quaternion.LookRotation(dir);
            GetComponent<RectTransform>().rotation = Quaternion.LookRotation(dir);
            GetComponent<RectTransform>().anchoredPosition3D = cam.transform.position + new Vector3(0, 0, 100);
        }
        catch
        {
            Debug.Log("½ÇÇà¾ÈµÊ");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
