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

    //Public
    public float moveSpeed;
    public GameObject mousePos;

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



    }

    private void SetMovePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }

        animator.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }



}
