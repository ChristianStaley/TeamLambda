using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;
    public float maxDistance;
    public float minDistance;

    [SerializeField]
    protected GameObject deadBody;

    public enum NPCState 
    {
        IDLE,
        MOVING,
        ATTACK
    }

    public NPCState currentState = NPCState.IDLE;

    [SerializeField]
    protected int soulDropAmount;

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
        if(target == null)
        {
            target = GameObject.Find("Player");
        }
        lastWait = Time.deltaTime;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckHealth();
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
                    MoveToPoint();
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
            transform.LookAt(target.transform.position);
            currentState = NPCState.IDLE;
        }
        else if (foundTarget)
        {
            StartSearch();
        }

        if(Vector3.Distance(target.transform.position, transform.position) <= attackRange)
        {
            currentState = NPCState.ATTACK;
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
    [SerializeField]
    private float attackRange;
    private float attackCooldown = 3f;
    private float lastWait;
    public GameObject projectile;
    private Transform attackTarget;

    public virtual void AttackTarget()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= attackRange && Time.time > lastWait)
        {
            transform.LookAt(target.transform.position);
            Instantiate(projectile, new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z), transform.rotation);
            lastWait = Time.time + attackCooldown;

        }
        Debug.Log("Attack Now");
        
    }

    public virtual void UpdateTarget()
    {


    }



    #endregion


    #region Health

    protected float maxHealth = 100;
    protected float currentHealth = 100;


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
        GM.Souls = soulDropAmount;
        Instantiate(deadBody, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //PrefabUtility.InstantiatePrefab(deadBody);      
        Destroy(this.gameObject, 1f);
        this.enabled = false;
    }

    public void Damage(int damage)
    {
        currentHealth = damage;
    }

    #endregion


    void StartSearch()
    {
        
        foundTarget = false;
    }




}
