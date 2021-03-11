using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    private float playerHealth;
    private float playerMaxHealth;
    private Animator animPlayer;
    private PlayerMovement pmPlayer;
    private NavMeshAgent agent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponent<Animator>();
        pmPlayer = GetComponent<PlayerMovement>();
        pmPlayer.enabled = true;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }


    private void CheckHealth()
    {
        if (GM.Health <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        
        pmPlayer.enabled = false;
        animPlayer.SetInteger("Die", 1);
        agent.isStopped = true;
        agent.ResetPath();
        agent.Warp(GM.SpawnLocation);
        Debug.Log("New Player Location" + transform.position);
        
        pmPlayer.enabled = true;
        
        animPlayer.SetInteger("Die", 0);
        GM.Health = 100;
        agent.isStopped = false;

    }

    public void Damage(int damage)
    {
        GM.Health = -damage;
    }

}
