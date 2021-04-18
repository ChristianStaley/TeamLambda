using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    private Rigidbody rb;
    public GameObject preview;
    private bool gravity;
    public bool constantDamage = false;
    public float constantAttackCooldown = 0.1f;
    private float lastTime;
    public float duration = 1.5f;
    public bool damageMinion;

    private Collider cl;


    void Start()
    {

        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(12, 12);

        if (!constantDamage)
        {
            Destroy(this.gameObject, fl_range / fl_speed);
        }

        cl = GetComponent<Collider>();

        rb.velocity = fl_speed * transform.TransformDirection(Vector3.forward);

        lastTime = constantAttackCooldown;

        if (bl_use_Trigger)
            GetComponent<Collider>().isTrigger = true;

        if (!gravity)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        }

        if (duration == 0)
        {
            duration = 1f;
        }

        if (constantDamage)
        {
            Destroy(gameObject, duration);
        }
    }

    private void Update()
    {
        lastTime -= Time.deltaTime;
    }


    void OnCollisionEnter(Collision other)
    {

        if (!constantDamage)
        {
            other.collider.gameObject.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);

            if (damageMinion)
            {
                other.collider.gameObject.SendMessage("MinionDamage", fl_damage, SendMessageOptions.DontRequireReceiver);
            }
            Destroy(this.gameObject);
        }

    }


    private void OnTriggerStay(Collider other)
    {

        other.gameObject.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);

    }

}
    