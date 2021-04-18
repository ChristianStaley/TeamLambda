using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShop : MonoBehaviour
{
    public GameObject shopUI;
    public int upgrade1Cost;
    public int upgrade2Cost;
    public int upgrade3Cost;


    [Header("Item 1")]
    public GameObject item1Text;
    public GameObject item1Button;

    [Header("Item 2")]
    public GameObject item2Text;
    public GameObject item2Button;

    [Header("Item 3")]
    public GameObject item3Text;
    public GameObject item3Button;


    private Collider cl;


    // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<Collider>();
        shopUI.SetActive(false);
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
            GM.Souls = -upgrade1Cost;
            item1Button.SetActive(false);
            item1Text.SetActive(true);
        }
    }

    public void Upgrade2()
    {
        if (GM.Souls >= upgrade2Cost)
        {
            GM.ManaMax += 20;
            GM.Souls = -upgrade2Cost;
            item2Button.SetActive(false);
            item2Text.SetActive(true);
        }
    }

    public void Upgrade3()
    {
        if (GM.Souls >= upgrade3Cost)
        {
            GM.ManaRegen += 20;
            GM.Souls = -upgrade3Cost;
            item3Button.SetActive(false);
            item3Text.SetActive(true);
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
