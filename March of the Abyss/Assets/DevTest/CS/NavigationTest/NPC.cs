using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public GameObject target;
    public float maxDistance;
    public float minDistance;

    public enum NPCState 
    {
        IDLE,
        MOVING,
        ATTACK
    }

    public NPCState currentState = NPCState.IDLE;

    public float searchTime;
    protected NavMeshAgent agent;
    private Rigidbody rb;
    private bool foundTarget = false;

    protected float currentDistance;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.speed = moveSpeed;
        agent.angularSpeed = 250;
        agent.acceleration = 100;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        TargetDistance();

        switch (currentState)
        {
            case NPCState.IDLE:
                {

                    return;
                }
                                    

            case NPCState.MOVING:
                {
                    MoveToPoint();
                    return;
                }


            case NPCState.ATTACK:
                {
                    AttackTarget();
                    return;
                }
        }

    }

    protected virtual void TargetDistance()
    {
        currentDistance = Vector3.Distance(transform.position, target.transform.position);
        if (currentDistance <= maxDistance && currentDistance >= minDistance)
        {
            currentState = NPCState.MOVING;
        }
        else if (currentDistance < minDistance)
        {
            currentState = NPCState.IDLE;
        }
        else if (foundTarget)
        {
            StartSearch();
        }
        
    }


    #region Movement

    public float moveSpeed;
    private Transform targetLocation;

    public virtual void MoveToPoint()
    {
           
            agent.destination = target.transform.position;
            foundTarget = true;
    }

    public virtual void SearchForTarget()
    {

    }

    #endregion


    #region Attack

    private float attackSpeed;
    private float attackDamage;
    private Transform attackTarget;

    public virtual void AttackTarget()
    {

    }

    public virtual void UpdateTarget()
    {


    }



    #endregion


    #region Health

    protected float maxHealth;
    protected float currentHealth;


    protected virtual void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            DoKill();
        }
    }

    protected virtual void DoKill()
    {
        //Insert death anim
        //Insert death effect
        //Insert GM souls increase
        Destroy(gameObject);
    }

    protected void DamageRecieved(float value)
    {
        //Insert if for collision
        currentHealth -= value;
    }

    #endregion


    void StartSearch()
    {
        
        foundTarget = false;
    }


}
