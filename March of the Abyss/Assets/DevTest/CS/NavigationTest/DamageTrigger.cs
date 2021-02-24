using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private int damage = 100;
    private Collider colTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        colTrigger = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("Damage", -damage, SendMessageOptions.DontRequireReceiver);
    }

}
