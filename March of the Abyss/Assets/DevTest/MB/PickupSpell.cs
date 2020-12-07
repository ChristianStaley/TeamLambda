using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpell : MonoBehaviour
{

    public GameObject Spell1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Spell1.SetActive(true);
            Destroy(gameObject);
        }
       
    }
}
