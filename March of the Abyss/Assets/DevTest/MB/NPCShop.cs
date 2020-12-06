using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCShop : MonoBehaviour
{
    public string[] st_names;
    public GameObject[] go_objects_to_drop;
    public int[] fl_cost;

    public string st_message = "What would you like to buy today?";
    public float fl_distance = 2;
    private GameObject go_PC;
    private GameObject go_panel;
    private Text txt_window;
    private int in_message_stage = 0;
    private bool[] bl_dispensed;


    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        // Find Key Game Objects
        go_PC = GameObject.FindWithTag("Player");
        go_panel = GameObject.Find("GM").transform.Find("MessagePanel").gameObject;
        txt_window = go_panel.transform.Find("MessageText").GetComponent<Text>();

        // Hide Game Objects
        foreach (GameObject _go_item in go_objects_to_drop)
        {
            _go_item.SetActive(false);
        }

        bl_dispensed = new bool[go_objects_to_drop.Length];

    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        DisplayMessage();
        Trade();
    }//-----

     ///----------------------------------------------------------------------
    private void Trade()
    {
        //Is the PC in trigger distance
        if (Vector3.Distance(go_PC.transform.position, transform.position) < fl_distance)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !bl_dispensed[0] &&
            GM.Souls >= fl_cost[0])
            {
                go_objects_to_drop[0].SetActive(true);
                GM.Souls -= fl_cost[0];
                bl_dispensed[0] = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
                if (!bl_dispensed[1] && GM.Souls >= fl_cost[1])
                {
                    go_objects_to_drop[1].SetActive(true);
                    GM.Souls -= fl_cost[1];
                    bl_dispensed[1] = true;
                }

            if (Input.GetKeyDown(KeyCode.Alpha3))
                if (!bl_dispensed[2] && GM.Souls >= fl_cost[2])
                {
                    go_objects_to_drop[2].SetActive(true);
                    GM.Souls -= fl_cost[2];
                    bl_dispensed[2] = true;
                }

            if (Input.GetKeyDown(KeyCode.Alpha4))
                if (!bl_dispensed[3] && GM.Souls >= fl_cost[3])
                {
                    go_objects_to_drop[3].SetActive(true);
                    GM.Souls -= fl_cost[3];
                    bl_dispensed[3] = true;
                }
        }
    }//-----




    // ----------------------------------------------------------------------
    private void DisplayMessage()
    {
        // Is the PC in trigger distance
        if (Vector3.Distance(go_PC.transform.position, transform.position) < fl_distance)
        {
            // Enable the message panel active
            if (!go_panel.activeInHierarchy) go_panel.SetActive(true);

            // update the text box
            txt_window.alignment = TextAnchor.MiddleLeft;
            txt_window.text = st_message + "\n";
            txt_window.text += "\n Press: \n";
            if (!bl_dispensed[0])
                txt_window.text += "\n (1) " + fl_cost[0] + " souls for " + st_names[0];
            if (go_objects_to_drop.Length > 1)
                if (!bl_dispensed[1])
                    txt_window.text += "\n (2) " + fl_cost[1] + " souls for " + st_names[1];
            if (go_objects_to_drop.Length > 2)
                if (!bl_dispensed[2])
                    txt_window.text += "\n (3) " + fl_cost[2] + " souls for " + st_names[2];
            if (go_objects_to_drop.Length > 3)
                if (!bl_dispensed[3])
                    txt_window.text += "\n (4) " + fl_cost[3] + " souls for " + st_names[3];

        }
        else if (go_PC && Vector3.Distance(go_PC.transform.position, transform.position) < fl_distance + 1)
        {
            txt_window.alignment = TextAnchor.MiddleCenter;
            go_panel.SetActive(false);
        }

    }//-----

}//==========
