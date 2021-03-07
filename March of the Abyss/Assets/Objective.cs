using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    //Start is called before the first frame update

    public Text objectiveText;
    public string objective;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            objectiveText.text = objective;
        }
    }
}
