using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballAttack : MonoBehaviour
{
    public float fl_damage = 10;
    public float fl_cooldown = 0.1F;
    public float throwForce;

    private float fl_next_attack_time;


    public GameObject go_projectile;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time > fl_next_attack_time )
        {   // Reset the cooldown delay

            GameObject spellGO = Instantiate(go_projectile, spawnPoint.position, spawnPoint.rotation);
            spellGO.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * throwForce, ForceMode.Impulse);
            
        }
    }//-----
}
