using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCHealth : MonoBehaviour
{
    // ----------------------------------------------------------------------
    public float fl_HP = 100;
    public float fl_max_HP = 100;
    private Transform tx_HP_bar;
    public GameObject go_hit_text;

    // ----------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        tx_HP_bar = gameObject.transform.Find("HP_Bar");
    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        ResizeBar();
    }//-----

    // ----------------------------------------------------------------------
    void CheckHealth()
    {
        
    }//------ 

    // ----------------------------------------------------------------------
    void ResizeBar()
    {   // is there am HP bar attached
        if (tx_HP_bar)
        {   // Resize and colour the bar based on current HP
            tx_HP_bar.localScale = new Vector3((fl_HP / fl_max_HP), 0.1F, 0.1F);
            if (fl_HP > fl_max_HP / 2) tx_HP_bar.GetComponent<Renderer>().material.color = Color.green;
            if (fl_HP > fl_max_HP / 4 && fl_HP < fl_max_HP / 2) tx_HP_bar.GetComponent<Renderer>().material.color = Color.yellow;
            if (fl_HP < fl_max_HP / 4) tx_HP_bar.GetComponent<Renderer>().material.color = Color.red;
        }
    }//-----    

    // ----------------------------------------------------------------------
    // Damage Receiver
    public void Damage(float _fl_damage)
    {
        // Subtract the damage sent from HP
        fl_HP -= _fl_damage;
        
        // Create text mesh to show hit damage
        //GameObject _GO_hit_text = Instantiate(go_hit_text, transform.position + Vector3.up, transform.rotation) as GameObject;
        GameObject _GO_hit_text = Instantiate(go_hit_text, transform.position, Quaternion.identity, transform) as GameObject;
        Vector3 dmgPos = Camera.main.WorldToScreenPoint(transform.position);
        go_hit_text.transform.position = dmgPos;

        _GO_hit_text.GetComponent<TextMeshPro>().text = _fl_damage.ToString();
        _GO_hit_text.GetComponent<TextMeshPro>().color = Color.red;
    }//-----

}