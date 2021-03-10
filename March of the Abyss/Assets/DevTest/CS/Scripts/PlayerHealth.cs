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
    
    
    // Start is called before the first frame update
    void Start()
    {
        animPlayer = GetComponent<Animator>();
        pmPlayer = GetComponent<PlayerMovement>();
        pmPlayer.enabled = true;
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
        transform.position = GM.SpawnLocation;
        Debug.Log("New Player Location" + transform.position);
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        pmPlayer.enabled = true;
        
        animPlayer.SetInteger("Die", 0);
        GM.Health = 100;
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;

    }

    public void Damage(int damage)
    {
        GM.Health = -damage;
    }

}
