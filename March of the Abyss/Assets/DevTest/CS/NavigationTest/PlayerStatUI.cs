using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    public Text txtHealth;
    public Text txtSouls;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtHealth.text = "Health " + GM.Health;
        txtSouls.text = "Souls: " + GM.Souls;
    }
}
