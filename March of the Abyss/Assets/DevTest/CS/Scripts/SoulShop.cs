using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShop : MonoBehaviour
{
    public GameObject shopUI;
    public int upgrade1Cost;
    public int upgrade2Cost;
    public int upgrade3Cost;

    private Collider cl;


    // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Upgrade1()
    {
        if (GM.Souls >= upgrade1Cost)
        {
            GM.MaxHealth += 20;
            GM.Souls -= upgrade1Cost;
        }
    }

    public void Upgrade2()
    {
        if (GM.Souls >= upgrade2Cost)
        {
            GM.ManaMax += 20;
            GM.Souls -= upgrade2Cost;
        }
    }

    public void Upgrade3()
    {
        if (GM.Souls >= upgrade3Cost)
        {
            GM.ManaRegen += 20;
            GM.Souls -= upgrade3Cost;
        }
    }

    public void CloseUI()
    {
        GM.UIActive = false;
        shopUI.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        GM.UIActive = true;
        shopUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        GM.UIActive = false;
        shopUI.SetActive(false);
    }
}
