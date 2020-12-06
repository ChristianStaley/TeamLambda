using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //animPlayer.SetInteger("Die", 1);
    }

    public void Damage(int damage)
    {
        GM.Health = damage;
    }

}
