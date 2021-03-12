using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //Might as well be called Player script. Basically everything is in the one script
    
    
    //Private
    private Rigidbody rb;
    Animator animator;
    NavMeshAgent agent;
    private bool previewEnabled = false;

    private SpellAttack spellAttack = new SpellAttack();
    private MeleeAttack meleeAttack = new MeleeAttack();

    private GameObject tempPreview = null;

    private float previewCooldown = 0.2f;
    private float lastCooldown;

    private GameObject target;
    private bool performMeleeAttack = true;

    private int activeSpell = 0;

    //Public
    public float moveSpeed;
    public GameObject mousePos;
    public Collider colliderPlayer;

  

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        
    }

    IEnumerator RespawnDelay()
    {

        agent.enabled = false;
        agent.ResetPath();

        agent.Warp(GM.SpawnLocation);

        yield return new WaitForSeconds(0.1f);
        agent.enabled = true;

    }

    
    void Update()
    {

        

        if (GM.Health > 0)
        {
            
            SetMovePosition();
        }
        else if(GM.Health < 1 && agent.enabled == false)
        {
            Debug.Log("Respawn");

            //StartCoroutine(RespawnDelay());
            
        }

        if (!previewEnabled && Input.GetKeyDown(KeyCode.Alpha1) && !rangeCooldown)
        {

            previewEnabled = true;
            GM.ChangeSpell = 0;
            lastCooldown = Time.deltaTime;
            
        }

        if (!previewEnabled && Input.GetKeyDown(KeyCode.Alpha2) && !rangeCooldown)
        {

            previewEnabled = true;
            GM.ChangeSpell = 1;
            lastCooldown = Time.deltaTime;

        }

        if (!previewEnabled && Input.GetKeyDown(KeyCode.Alpha3) && !rangeCooldown)
        {

            previewEnabled = true;
            GM.ChangeSpell = 2;
            lastCooldown = Time.deltaTime;

        }


        if (previewEnabled)
        {
            

            RaycastHit hit;

            if (tempPreview == null)
            {
                tempPreview = Instantiate(GM.spell.GetComponent<Projectile>().preview, gameObject.transform);
            }

            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))// && Vector3.Distance(gameObject.transform.position, tempPreview.transform.position) <= GM.spell.GetComponent<Projectile>().fl_range)
            {
                tempPreview.transform.position = Vector3.MoveTowards(tempPreview.transform.position, hit.point, 2f);
            }

            
            if (Input.GetMouseButtonDown(1))
            {
                

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
                {

                    gameObject.transform.LookAt(hit.point);
                    agent.ResetPath();
                    SpawnProjectile(gameObject, hit.point);
                    
                    Destroy(tempPreview);
                    previewEnabled = false;

                }

            }

            if (!rangeCooldown && Input.GetKeyDown(KeyCode.S))
            {

                previewEnabled = false;
                Destroy(tempPreview);
                
            }
        }

        

    }
    private bool rangeCooldown = false;
    private Quaternion rotation;
    public GameObject castPoint;
    private Vector3 targetLocation;
    private void SpawnProjectile(GameObject actor, Vector3 castLocation)
    {
        targetLocation = castLocation - castPoint.transform.position;
        rotation = Quaternion.LookRotation(targetLocation, Vector3.up);

        if (!rangeCooldown)
        {
            StartCoroutine(RangedAttackInterval());
        }
        
    }


    GameObject newProjectile;
    GameObject newProjectileSpray;
    IEnumerator RangedAttackInterval()
    {
        if (GM.Mana > 0 && GM.ChangeSpell != 0)
        {
            animator.SetBool("Attack1", true);
            agent.isStopped = true;
            yield return new WaitForSeconds(0.2f);
            agent.isStopped = false;
            newProjectile = Instantiate(GM.spell, castPoint.transform.position + new Vector3(0, 1, 0), rotation);
            Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            GM.Mana = -20;

            rangeCooldown = true;
        }

        else if (GM.Mana > 0 && GM.ChangeSpell == 0)
        {
            animator.SetBool("Attack1", true);
            agent.isStopped = true;
            newProjectileSpray = Instantiate(GM.spell, castPoint.transform.position + new Vector3(0, 1, 0), rotation);
            Physics.IgnoreCollision(newProjectileSpray.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            newProjectileSpray.GetComponent<ParticleSystem>().Play();
            GM.Mana = -20;

            rangeCooldown = true;
        }



        yield return new WaitForSeconds(1f);

        if (GM.ChangeSpell == 0 && newProjectileSpray != null)
        {
            newProjectileSpray.GetComponent<ParticleSystem>().Stop();
           
        }
        agent.isStopped = false;
        rangeCooldown = false;

    }


    private void SetMovePosition()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {

            }

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
            {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Hostile"))
                {
                    target = hit.transform.gameObject;
                    FaceTarget(target);
                }
                else if(hit.transform.gameObject.layer != LayerMask.NameToLayer("UI"))
                {
                    agent.destination = hit.point;
                    target = null;
                }
                
            }
            
        }
        else if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
            {
                if (hit.transform.gameObject.layer != LayerMask.NameToLayer("UI"))
                {
                    agent.destination = hit.point;
                    target = null;
                }
                else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Hostile"))
                {
                    target = hit.transform.gameObject;
                    FaceTarget(target);
                }
                
            }
        }

        if (Input.GetMouseButtonDown(1) && !previewEnabled)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Hostile"))
                {
                    
                    target = hit.transform.gameObject;
                    FaceTarget(target);
                    agent.destination = hit.point;
                    
                }
                
            }

         
        }
        

        if(target != null && !previewEnabled)
        {
            FaceTarget(target);

            if(Vector3.Distance(transform.position, target.transform.position) <= 2)
            {
                if (performMeleeAttack)
                {
                    StartCoroutine(MeleeAttackInterval());
                }
                    
                
                animator.SetBool("isAttack", true);

                if (!Input.GetMouseButton(0))
                {
                    agent.ResetPath();

                }
                else
                {
                    target = null;
                    animator.SetBool("isAttack", false);
                }

                
                meleeAttack.Attack(colliderPlayer, gameObject);
            }
        }
        else
        {
            animator.SetBool("isAttack", false);
        }

        animator.SetFloat("Speed", agent.desiredVelocity.magnitude, 0.1f, Time.deltaTime);
    }

    IEnumerator MeleeAttackInterval()
    {
        animator.SetBool("isAttack", true);
        performMeleeAttack = false;
        
        yield return new WaitForSeconds(1f);

        if(target != null)
        {
            target.SendMessage("Damage", 20, SendMessageOptions.DontRequireReceiver);
        }
        
        performMeleeAttack = true;
        if (target == null)
        {
            animator.SetBool("isAttack", false);
            performMeleeAttack = true;
        }
    }



    void FaceTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
