﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //Private
    private Rigidbody rb;
    Animator animator;
    NavMeshAgent agent;
    private bool previewEnabled = false;

    //Public
    public float moveSpeed;
    public GameObject mousePos;
    public Collider colliderPlayer;

    private SpellAttack spellAttack = new SpellAttack();
    private MeleeAttack meleeAttack = new MeleeAttack();

    private GameObject tempPreview = null;

    private float previewCooldown = 0.2f;
    private float lastCooldown;

    private GameObject target;
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

        if (!previewEnabled && Input.GetKeyDown(KeyCode.S))
        {

            previewEnabled = true;
            lastCooldown = Time.deltaTime;
            
        }


        if (previewEnabled)
        {

            RaycastHit hit;

            if (tempPreview == null)
            {
                tempPreview = Instantiate(GM.spell.GetComponent<Projectile>().preview, gameObject.transform);
            }

            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9) && Vector3.Distance(gameObject.transform.position, tempPreview.transform.position) <= GM.spell.GetComponent<Projectile>().range)
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

            if (Time.deltaTime > lastCooldown + previewCooldown && Input.GetKeyDown(KeyCode.S))
            {

                previewEnabled = false;
                Destroy(tempPreview);
                
            }
        }

        

    }

    private void SetMovePosition()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
            {
                
                agent.destination = hit.point;
            }
            
        }
        else if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 9))
            {

                agent.destination = hit.point;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Hostile"))
                {
                    Debug.Log("Enemy clicked");
                    target = hit.transform.gameObject;
                    FaceTarget(target);
                    agent.destination = hit.point;
                    
                }
                
            }

            Debug.Log("Right Click");
        }
        

        if(target != null)
        {
            FaceTarget(target);

            if(Vector3.Distance(transform.position, target.transform.position) <= 2)
            {
                if (!Input.GetMouseButton(0))
                {
                    agent.ResetPath();
                }
                else
                {
                    target = null;
                }
                
                meleeAttack.Attack(colliderPlayer, gameObject);
            }
        }

        animator.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }

    void FaceTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
