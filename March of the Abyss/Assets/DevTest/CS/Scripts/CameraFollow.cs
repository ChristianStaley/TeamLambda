using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followTarget;

    [Range(0.1f, 0.9f)]
    public float smoothSpeed;
    public Vector3 offset;


    public bool allowRotation;

    Vector3 velocity = Vector3.zero;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = followTarget.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity, smoothSpeed);

        if (allowRotation)
        {
            transform.LookAt(followTarget.transform);
        }
        
    }



    public void UpdateTarget(GameObject target)
    {
        
    }

    
}
