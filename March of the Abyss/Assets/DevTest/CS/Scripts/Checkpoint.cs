using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public GameObject player;
    public string zone;
    private Collider pCollider;
    public Text txtLocation;
    public string locationName;

    // Start is called before the first frame update
    void Start()
    {
        pCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == pCollider)
        {
            GM.SpawnLocation = gameObject.transform.position;
            txtLocation.text = locationName;
            Destroy(gameObject);
        }

    }
}
