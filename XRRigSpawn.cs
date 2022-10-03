using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRRigSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnpoint = transform.position;

        GameObject.Find("XR Rig Advanced").transform.position = new Vector3(0, 0, 0);
        GameObject.Find("PlayerController").transform.position = spawnpoint;
        GameObject.Find("Locomotion").transform.position = new Vector3(0, 0, 0);
        GameObject.Find("HeadCollision").transform.position = new Vector3(0, 0, 0);
    }
}
