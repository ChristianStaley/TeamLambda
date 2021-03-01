using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : MonoBehaviour
{

    private Vector3 target;
    private Quaternion rotation;

    private bool onCooldown = true;

    private GameObject actor;
    private Vector3 actorLocation;

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


    


}
