using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : MonoBehaviour
{

    private Vector3 target;
    private Quaternion rotation;

    //  !!Attack!!
    //
    //    > Press Key
    //     > Check Cooldown
    //      > Cast Preview
    //       > Mouse Click
    //        > Get Position
    //         > Face Position
    //          > Spawn Projectile
    //
    //
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Vector3 actorLocation ,Vector3 castLocation)
    {
        target = castLocation - actorLocation;
        
    }

    private void SetRotation()
    {

    }

    public void SpawnProjectile(GameObject actor, Vector3 castLocation)
    {
        Vector3 actorLocation = actor.transform.position;
        target = castLocation - actorLocation;
        rotation = Quaternion.LookRotation(target, Vector3.up);
        GameObject newProjectile = Instantiate(GM.spell, actorLocation + new Vector3(1,0,0), rotation);
        Physics.IgnoreCollision(newProjectile.GetComponent<Collider>(), actor.GetComponent<Collider>());
    }



}
