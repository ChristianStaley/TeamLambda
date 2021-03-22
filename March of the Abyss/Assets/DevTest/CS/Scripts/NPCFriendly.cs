using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFriendly : NPC
{
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        targetMask = LayerMask.GetMask("Hostile");
        currentState = NPCState.MOVING;
        target = player;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (anim != null)
        {
            //anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        }
    }

    protected override void TargetDistance()
    {
        if (target != null)
        {
            currentDistance = Vector3.Distance(transform.position, target.transform.position);

            if (target != player)
            {
                    
                if (currentDistance <= maxDistance && currentDistance > attackRange) //&& currentDistance <= minDistance)
                {
                   currentState = NPCState.MOVING;
                }
                else if (currentDistance <= attackRange)
                {
                    transform.LookAt(target.transform.position);
                    currentState = NPCState.ATTACK;
                }

                
            }
            else if(target == player)
            {
                StartSearch();
            }

            if (currentDistance <= minDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }

        }
        else
        {
            StartSearch();
        }

        
    }

    protected override void DoKill()
    {
        //Insert death anim
        //Insert death effect
        Destroy(this.gameObject, 1f);
        this.enabled = false;
    }

    protected override void StartSearch()
    {

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, attackRange, transform.forward, out hit))
        {
            if (hit.transform.gameObject.layer == 10)
            {
                target = hit.transform.gameObject;
                currentState = NPCState.ATTACK;
            }
            else if (target == null)
            {
                currentState = NPCState.MOVING;
                target = player;
            }
        }
        else if (Physics.SphereCast(transform.position, attackRange/2, -transform.forward, out hit))
        {
            if (hit.transform.gameObject.layer == 10)
            {
                target = hit.transform.gameObject;
                currentState = NPCState.ATTACK;
            }
            else if (target == null)
            {
                currentState = NPCState.MOVING;
                target = player;
            }
        }
        //else if(Vector3.Distance(transform.position, player.transform.position) > maxDistance)
        //{
        //        currentState = NPCState.MOVING;
        //        target = player;
        //}

        //foundTarget = false;

    }



    public void Damage(int damage)
    {
        currentHealth -= damage;
        currentState = NPCState.ATTACK;
        StartSearch();
    }

}
