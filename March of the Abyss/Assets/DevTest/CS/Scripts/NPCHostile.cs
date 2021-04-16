using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHostile : NPC
{
    // Start is called before the first frame update
    [SerializeField]
    private int goldDropped;

    [SerializeField]
    protected int soulDropAmount;

    protected override void Start()
    {
        base.Start();
        targetMask = LayerMask.GetMask("Minion", "Player");
        //goldDropped = Random.Range(5,10) * 10;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    protected override void TargetDistance()
    {
        base.TargetDistance();
    }

    protected override void DoKill()
    {
        GM.Souls += soulDropAmount;
        GM.Gold += goldDropped;
        GM.KillCount = 1;
        base.DoKill();
    }

    private bool invicible = false;

    public void Damage(int damage)
    {
        Debug.Log("Invincible: " + invicible);
        if (!invicible)
        {
            
            currentHealth -= damage;
            StartCoroutine(DamageDelay());
            
        }
        currentState = NPCState.ATTACK;
        if (target == null)
        {
            target = player;
        }
    }

    

    IEnumerator DamageDelay()
    {
        invicible = true;
        yield return new WaitForSeconds(0.05f);
        invicible = false;

    }
}
