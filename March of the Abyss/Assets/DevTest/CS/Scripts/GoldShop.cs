using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldShop : MonoBehaviour
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
        if(GM.Gold >= upgrade1Cost)
        {
            GM.Gold -= upgrade1Cost;
        }
    }

    public void Upgrade2()
    {
        if (GM.Gold >= upgrade2Cost)
        {
            GM.Gold -= upgrade2Cost;
        }
    }

    public void Upgrade3()
    {
        if (GM.Gold >= upgrade3Cost)
        {
            GM.Gold -= upgrade3Cost;
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
