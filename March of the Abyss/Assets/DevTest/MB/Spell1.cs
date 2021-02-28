using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell1 : MonoBehaviour
{
    private ParticleSystem ps_attached;
    public float fl_damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        ps_attached = GetComponent<ParticleSystem>();
        ps_attached.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            ps_attached.Play();
            //ps_attached.enableEmission = true;
           
        }

        else
        {
            ps_attached.Stop();
            //ps_attached.enableEmission = false;
        }


    }
    private void OnParticleCollision(GameObject other)
    {
        other.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);
    }
}
