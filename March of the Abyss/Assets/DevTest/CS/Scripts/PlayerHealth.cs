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
    public int healCost = 50;
    public int healAmount = 20;
    
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

        if (Input.GetKeyDown(KeyCode.H) && GM.Souls >= healCost)
        {
            GM.Souls = -healCost;
            GM.Health += healAmount;
        }
    }


    private void CheckHealth()
    {
        if (GM.Health <= 0)
        {
            KillPlayer();
        }
        else if(GM.Health > GM.MaxHealth)
        {
            GM.Health = GM.MaxHealth;
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
        if (!invincible)
        {
            GM.Health = -damage;
            StartCoroutine(DamageDelay());
        }
    }

    private bool invincible = false;
    IEnumerator DamageDelay()
    {
        invincible = true;
        yield return new WaitForSeconds(0.05f);
        invincible = false;

    }

}
