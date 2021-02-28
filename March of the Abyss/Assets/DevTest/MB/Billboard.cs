using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        GameObject camPos = GameObject.Find("Main Camera");
        cam = camPos.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
