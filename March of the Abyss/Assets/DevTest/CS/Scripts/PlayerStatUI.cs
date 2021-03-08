using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    public Text txtHealth;
    public Text txtSouls;
    public Text txtGold;
    public Text txtMana;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtHealth.text = + GM.Health + "/ 100";
        txtSouls.text = "Souls: " + GM.Souls;
        txtGold.text = "Gold: " + GM.Gold;
        txtMana.text = GM.Mana + "/100";

    }
}
