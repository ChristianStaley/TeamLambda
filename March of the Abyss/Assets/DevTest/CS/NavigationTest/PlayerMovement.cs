using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //Private
    private Rigidbody rb;
    Animator animator;
    NavMeshAgent agent;

    //Public
    public float moveSpeed;
    public GameObject mousePos;
    private bool previewEnabled = false;

    private SpellAttack spellAttack = new SpellAttack();
    private MeleeAttack meleeAttack = new MeleeAttack();

    private GameObject tempPreview = null;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update()
    {

        if (GM.Health > 0)
        {
            SetMovePosition();
        }
        else
        {
            agent.isStopped = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {

            previewEnabled = true;
        }


        if (previewEnabled)
        {
            
            
            if (tempPreview == null)
            {
                tempPreview = Instantiate(GM.spell.GetComponent<Projectile>().preview);
            }

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
            {
                tempPreview.transform.position = Vector3.MoveTowards(tempPreview.transform.position, hit.point, 2f);
            }

            
            if (Input.GetMouseButtonDown(1))
            {
                

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
                {

                    gameObject.transform.LookAt(hit.point);
                    agent.ResetPath();
                    spellAttack.SpawnProjectile(gameObject, hit.point);
                    Destroy(tempPreview);
                    previewEnabled = false;

                }

            }
        }
        

    }

    private void SetMovePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
            {
                
                agent.destination = hit.point;
            }
        }

        animator.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }



}
