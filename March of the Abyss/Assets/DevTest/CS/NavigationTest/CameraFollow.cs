using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followTarget;
    //public GameObject player;

    private Vector3 cameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followTarget.transform.position + new Vector3(-10, 8, 0);
    }


    public void UpdateTarget(GameObject target)
    {
        
    }

    
}
