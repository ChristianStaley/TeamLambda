using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell1 : MonoBehaviour
{
    public Animator anim;
    private ParticleSystem ps_attached;
    public float fl_damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
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
            anim.SetBool("Attack2", true);

        }

        else
        {
            ps_attached.Stop();
            anim.SetBool("Attack2", false);
            //ps_attached.enableEmission = false;
        }


    }
    private void OnParticleCollision(GameObject other)
    {
        other.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);
    }
}
