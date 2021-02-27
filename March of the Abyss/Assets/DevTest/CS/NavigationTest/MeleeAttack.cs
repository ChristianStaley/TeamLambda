using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private bool activeCooldown = false;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Attack(Collider hitbox, GameObject actor)
    {
        if (!activeCooldown)
        {
            activeCooldown = true;
            //StartCoroutine(StartAttack(hitbox, actor));
        }
        
    }

    IEnumerator StartAttack(Collider hitbox, GameObject actor)
    {
        hitbox.enabled = true;
        Physics.IgnoreCollision(hitbox, actor.GetComponent<Collider>());
        

        yield return new WaitForSeconds(1);

        hitbox.enabled = false;
        activeCooldown = false;
    }

}
