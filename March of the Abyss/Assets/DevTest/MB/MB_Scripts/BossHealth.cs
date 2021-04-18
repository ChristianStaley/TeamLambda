using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private bool invincible = false;
    public float fl_HP = 100;
    public float fl_max_HP = 100;
    private HealthBarController healthbar;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GameObject.Find("BossHP").GetComponent<HealthBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float _fl_damage)
    {
        // Subtract the damage sent from HP
        if (!invincible)
        {
            healthbar.ChangeHP(-1);
            fl_HP -= _fl_damage;
            StartCoroutine(DamageDelay());
            //GameObject _GO_hit_text = Instantiate(go_hit_text, transform.position, Quaternion.identity, transform) as GameObject;
            Vector3 dmgPos = Camera.main.WorldToScreenPoint(transform.position);
            //go_hit_text.transform.position = dmgPos;

            // Create text mesh to show hit damage
            //_GO_hit_text.GetComponent<TextMeshPro>().text = _fl_damage.ToString();
            //_GO_hit_text.GetComponent<TextMeshPro>().color = Color.red;
        }






    }//-----

    IEnumerator DamageDelay()
    {
        invincible = true;
        yield return new WaitForSeconds(0.1f);
        invincible = false;

    }

}
