using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCHealth : MonoBehaviour
{
    // ----------------------------------------------------------------------
    public float fl_HP = 100;
    public float fl_max_HP = 100;
    private Transform tx_HP_bar;
    public GameObject go_hit_text;
    public bool isMinion;

    public Animator anim;

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

        if(fl_HP < 0)
        {
            fl_HP = 0;
        }

        ResizeBar();
        if (fl_HP <= 0)
        {
            if(anim != null)
            {
                anim.SetBool("Dead", true);
            }
                
            //Destroy(gameObject);
        }
    }//-----

    // ----------------------------------------------------------------------
    void CheckHealth()
    {
        
    }//------ 

    // ----------------------------------------------------------------------
    void ResizeBar()
    {   // is there am HP bar attached
        if (fl_HP > 0 && tx_HP_bar != null && tx_HP_bar)
        {   // Resize and colour the bar based on current HP
            tx_HP_bar.localScale = new Vector3((fl_HP / fl_max_HP), 0.1F, 0.1F);
            if (fl_HP > fl_max_HP / 2) tx_HP_bar.GetComponent<Renderer>().material.color = Color.green;
            if (fl_HP > fl_max_HP / 4 && fl_HP < fl_max_HP / 2) tx_HP_bar.GetComponent<Renderer>().material.color = Color.yellow;
            if (fl_HP < fl_max_HP / 4) tx_HP_bar.GetComponent<Renderer>().material.color = Color.red;
        }
    }//-----    

    private bool invincible = false;

    public void MinionDamge(float damage)
    {
        Debug.Log("Method Minion Damage Called");
        if (!invincible && isMinion)
        {
            Debug.Log("Removed health");
            fl_HP -= damage;
            StartCoroutine(DamageDelay());
            GameObject _GO_hit_text = Instantiate(go_hit_text, transform.position, Quaternion.identity, transform) as GameObject;
            Vector3 dmgPos = Camera.main.WorldToScreenPoint(transform.position);
            go_hit_text.transform.position = dmgPos;

            // Create text mesh to show hit damage
            _GO_hit_text.GetComponent<TextMeshPro>().text = damage.ToString();
            _GO_hit_text.GetComponent<TextMeshPro>().color = Color.red;
        }
    }




    // ----------------------------------------------------------------------
    // Damage Receiver
    public void Damage(float _fl_damage)
    {
        // Subtract the damage sent from HP
        if (!invincible && !isMinion)
        {
            fl_HP -= _fl_damage;
            StartCoroutine(DamageDelay());
            GameObject _GO_hit_text = Instantiate(go_hit_text, transform.position, Quaternion.identity, transform) as GameObject;
            Vector3 dmgPos = Camera.main.WorldToScreenPoint(transform.position);
            go_hit_text.transform.position = dmgPos;

            // Create text mesh to show hit damage
            _GO_hit_text.GetComponent<TextMeshPro>().text = _fl_damage.ToString();
            _GO_hit_text.GetComponent<TextMeshPro>().color = Color.red;
        }
        
        
        



    }//-----


    

    IEnumerator DamageDelay()
    {
        invincible = true;
        yield return new WaitForSeconds(0.1f);
        invincible = false;

    }

}