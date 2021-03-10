using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{

    public GameObject preview;
    private bool gravity;
    

    void Start()
    {
        Destroy(this.gameObject, fl_range / fl_speed);
        GetComponent<Rigidbody>().velocity = fl_speed * transform.TransformDirection(Vector3.forward);

        if (bl_use_Trigger)
            GetComponent<Collider>().isTrigger = true;

        if (!gravity)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        }
    } 

    void OnCollisionEnter(Collision other)
    {
        
        other.collider.gameObject.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }


}