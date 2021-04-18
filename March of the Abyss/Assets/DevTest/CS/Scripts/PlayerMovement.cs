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

    public GameObject pauseMenuPanel;

    public GameObject Spell1UI;

    private Abilities abilitiesUI;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        abilitiesUI = Spell1UI.GetComponent<Abilities>();

    }
    
    IEnumerator RespawnDelay()
    {

        agent.enabled = false;
        agent.ResetPath();

        agent.Warp(GM.SpawnLocation);

        yield return new WaitForSeconds(0.1f);
        agent.enabled = true;

    }

    public GameObject spellUnlocked;
    public GameObject spell2Unlocked;
    void Update()
    {

        if(GM.KillCount >= 10 && spellUnlocked != null)
        {
            GM.Spell2Active = true;
            spellUnlocked.SetActive(true);
            Destroy(spellUnlocked, 5f);
        }

        if(GM.KillCount >= 25 && spell2Unlocked != null)
        {
            GM.Spell3Active = true;
            spell2Unlocked.SetActive(true);
            Destroy(spell2Unlocked, 5f);
        }

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
        
        if (GM.Spell2Active && !previewEnabled && Input.GetKeyDown(KeyCode.Alpha2) && !rangeCooldown)
        {

            previewEnabled = true;
            GM.ChangeSpell = 1;
            lastCooldown = Time.deltaTime;

        }

        if (GM.Spell3Active && !previewEnabled && Input.GetKeyDown(KeyCode.Alpha3) && !rangeCooldown)
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

            
            if (Input.GetMouseButtonDown(0)) // changed
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

        if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            if (GM.UIActive)
            {
                GM.UIActive = false;
            }
            else
            {
                GM.UIActive = true;
            }
            pauseMenuPanel.gameObject.SetActive(!pauseMenuPanel.gameObject.activeSelf);
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
            if (GM.ChangeSpell == 0)
            {
                StartCoroutine(RangedAttackInterval());
            }
            else if (GM.ChangeSpell == 1)
            {
                StartCoroutine(RangedAttack2Interval());
            }
            else if (GM.ChangeSpell == 2)
            {
                StartCoroutine(RangedAttack3Interval());
            }

        }
        
    }


    GameObject newProjectile;
    GameObject newProjectileSpray;
    IEnumerator RangedAttackInterval()
    {
        
        if (GM.Mana > 0 && GM.ChangeSpell == 0)
        {
            animator.SetBool("Attack1", true);
            agent.isStopped = true;
            newProjectileSpray = Instantiate(GM.spell, castPoint.transform.position + new Vector3(0, 1, 0), rotation);
            Physics.IgnoreCollision(newProjectileSpray.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            newProjectileSpray.GetComponent<ParticleSystem>().Play();
            abilitiesUI.Ability1();
            GM.Mana = -40;

            rangeCooldown = true;


            
        }
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
        animator.SetBool("Attack1", false);
        if (GM.ChangeSpell == 0 && newProjectileSpray != null)
        {
            newProjectileSpray.GetComponent<ParticleSystem>().Stop();

        }

        yield return new WaitForSeconds(1f);

        
        rangeCooldown = false;

    }

    IEnumerator RangedAttack2Interval()
    {
        if (GM.Mana > 0 && GM.ChangeSpell == 1)
        {
            animator.SetBool("Attack1", true);
            agent.isStopped = true;
            yield return new WaitForSeconds(0.2f);
            agent.isStopped = false;
            newProjectile = Instantiate(GM.spell, castPoint.transform.position + new Vector3(0, 1, 0), rotation);
            Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            abilitiesUI.Ability2();
            GM.Mana = -30;

            animator.SetBool("Attack1", false);
            rangeCooldown = true;

        }

        agent.isStopped = false;
        yield return new WaitForSeconds(2f);

        
        rangeCooldown = false;

    }

    IEnumerator RangedAttack3Interval()
    {

        if (GM.Mana > 0 && GM.ChangeSpell == 2)
        {
            animator.SetBool("Attack1", true);
            agent.isStopped = true;
            yield return new WaitForSeconds(0.2f);
            agent.isStopped = false;
            newProjectile = Instantiate(GM.spell, castPoint.transform.position + new Vector3(0, 1, 0), rotation);
            Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            abilitiesUI.Ability3();
            GM.Mana = -20;

            rangeCooldown = true;
            agent.isStopped = false;
            animator.SetBool("Attack1", false);
            yield return new WaitForSeconds(1f);
            
            rangeCooldown = false;
            
        }
    }




        private void SetMovePosition()
    {
        RaycastHit hit;

        
        if (Input.GetMouseButtonDown(1) && !GM.UIActive) //Changed
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
        else if (Input.GetMouseButton(1) && !GM.UIActive) //changed
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

        if (Input.GetMouseButtonDown(0) && !previewEnabled && !GM.UIActive) //Changed
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

            if(Vector3.Distance(transform.position, target.transform.position) <= GM.AttackRange)
            {
                if (performMeleeAttack)
                {
                    if(target.GetComponent<NPCHealth>().fl_HP > 0)
                    {
                        StartCoroutine(MeleeAttackInterval());
                    }
                    else
                    {
                        target = null;
                    }
                    
                }
                
                    
                
                animator.SetBool("isAttack", true);

                if (!Input.GetMouseButton(1)) //changed
                {
                    agent.ResetPath();

                }
                else
                {
                    target = null;
                    animator.SetBool("isAttack", false);
                }

                
                //meleeAttack.Attack(colliderPlayer, gameObject);
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
        
        yield return new WaitForSeconds(GM.AttackSpeed);

        if(target != null)
        {
            target.SendMessage("Damage", GM.AttackDamage, SendMessageOptions.DontRequireReceiver);
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
