using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHostile : NPC
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void TargetDistance()
    {
        base.TargetDistance();
        if (currentDistance < minDistance)
        {
            agent.isStopped = true;
            currentState = NPCState.ATTACK;
        }
        else
            agent.isStopped = false;
    }
}
