using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;
    protected GameObject player;
    public float maxDistance;
    public float minDistance;

    protected Animator anim;
    

    [SerializeField]
    protected bool ignorePlayer = false;

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
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.speed = moveSpeed;
        agent.angularSpeed = 250;
        agent.acceleration = 100;
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        //if(target == null)
        //{
        //    target = player;
        //}
        lastWait = Time.deltaTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckHealth();
        if(!ignorePlayer)
        TargetDistance();

        switch (currentState)
        {
            case NPCState.IDLE:
                {
                    anim.SetFloat("Speed", 0);
                    anim.SetBool("Attack", false);
                    StartSearch();
                    return;
                }
                                    

            case NPCState.MOVING:
                {
                    anim.SetFloat("Speed", 1);
                    anim.SetBool("Attack", false);
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
        if (target != null)
        {
            currentDistance = Vector3.Distance(transform.position, target.transform.position);
            if (currentDistance <= maxDistance && currentDistance > attackRange) //&& currentDistance <= minDistance)
            {
                currentState = NPCState.MOVING;
            }
            else if(currentDistance > maxDistance)
            {
                currentState = NPCState.IDLE;
            }
            else if (currentDistance <= attackRange)
            {
                transform.LookAt(target.transform.position);
                currentState = NPCState.ATTACK;
            }

        }
        else
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


    #region Movement

    public float moveSpeed;
    private Transform targetLocation;

    public virtual void MoveToPoint()
    {
        if(target != null)
        {
            anim.SetFloat("Speed", 1);
            agent.destination = target.transform.position;
        }
        
    }

    #endregion


    #region Attack

    protected float attackSpeed;
    protected float attackDamage;
    [SerializeField]
    protected float attackRange;
    protected float attackCooldown = 3f;
    [SerializeField]
    protected LayerMask targetMask;
    private float lastWait;
    public GameObject projectile;
    private Transform attackTarget;

    protected virtual void AttackTarget()
    {
        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= attackRange)
            {

                if (Time.time > lastWait)
                {
                    transform.LookAt(target.transform.position);

                    anim.SetBool("Attack", true);
                    GameObject tempProjectile = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    Physics.IgnoreCollision(GetComponent<Collider>(), tempProjectile.GetComponent<Collider>());
                    lastWait = Time.time + attackCooldown;


                }

            }
            else if (Vector3.Distance(target.transform.position, transform.position) >= attackRange + 1)
            {
                anim.SetBool("Attack", false);
                target = null;
                StartSearch();
            }
        }
        else
        {
            anim.SetBool("Attack", false);
            StartSearch();
        }
        
        
        
    }


    #endregion


    #region Health

    [SerializeField]
    protected float maxHealth = 100;
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
        GM.Souls = soulDropAmount;
        Instantiate(deadBody, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);      
        Destroy(this.gameObject, 1f);
        this.enabled = false;
    }

    protected virtual float GetHealth()
    {
        return currentHealth;
    }


    #endregion
    float rayHitDist;

    protected virtual void StartSearch()
    {

        RaycastHit hit;
        if(Physics.SphereCast(transform.position, attackRange, transform.forward, out hit, targetMask))
        {
            rayHitDist = hit.distance;
            if(hit.transform.gameObject.layer == 9 || hit.transform.gameObject.layer == 13)
            {
                target = hit.transform.gameObject;
                currentState = NPCState.ATTACK;
            }
        }
        if(Physics.SphereCast(transform.position, attackRange/2, -transform.forward, out hit, targetMask))
        {
            rayHitDist = hit.distance;
            if (hit.transform.gameObject.layer == 9 || hit.transform.gameObject.layer == 13)
            {
                target = hit.transform.gameObject;
                currentState = NPCState.ATTACK;
            }
        }
    }

}
