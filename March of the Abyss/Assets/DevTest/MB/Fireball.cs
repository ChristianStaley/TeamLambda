﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // ----------------------------------------------------------------------
    // Variables
    public float fl_range = 20;
    public float fl_speed = 10;
    public float fl_damage = 10;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, fl_range / fl_speed);
        GetComponent<Rigidbody>().velocity = fl_speed * transform.TransformDirection(Vector3.forward);
    } //-----	

    // ----------------------------------------------------------------------

    void OnCollisionEnter(Collision _col_arrow_hit)
    {
        _col_arrow_hit.collider.gameObject.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
