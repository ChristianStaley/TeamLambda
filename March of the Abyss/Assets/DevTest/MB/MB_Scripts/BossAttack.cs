using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject go_projectile;
    public float fl_attack_range = 20;
    public GameObject go_target;
    public float fl_cool_down = 1;
    private float fl_next_shot_time;
    public BossManager bossManager;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // if no target is set find the first tagged as the enemy
        if (!go_target) go_target = GameObject.FindWithTag("Player");
    }//-----

    // Update is called once per frame
    void Update()
    {
        if (bossManager.Dospawn)
        {
            AttackTarget();
        }
    }//-----

    // ----------------------------------------------------------------------
    void AttackTarget()
    {
        if (Time.time > fl_next_shot_time &&
            Vector3.Distance(transform.position, go_target.transform.position) < fl_attack_range)
        {
            // Face the Target
            transform.LookAt(go_target.transform.position);

            // Spawn an arrow     
            Instantiate(go_projectile, transform.position + transform.TransformDirection(new Vector3(0, 0, 2F)), transform.rotation);
            anim.SetTrigger("Attack");
            

            //Reset Cooldown
            fl_next_shot_time = Time.time + fl_cool_down;
        }
        //anim.SetBool("Attack1", false);
    }//------ 

}//========
