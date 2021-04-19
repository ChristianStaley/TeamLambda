using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMinion : MonoBehaviour
{
    
    private GameObject player;
    public int detectDistance = 5;

    public GameObject minion;

    // Start is called before the first frame update
    void Start()
    {
            player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) <= detectDistance && Input.GetKeyDown(KeyCode.R) && GM.Souls >=50)
        {
            GM.Souls = -50;
            GameObject.Instantiate(minion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject, 1f);
            this.enabled = false;
        }
    }
    
}
