using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;

    public Collider[] attackHitboxes;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LaunchMeeleAttack(attackHitboxes[0]);
            animator.SetTrigger("Attack1");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            LaunchMeeleAttack(attackHitboxes[1]);
            animator.SetTrigger("Attack2");
        }
    }

    void LaunchMeeleAttack(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
            Debug.Log(c.name);
    }
}
