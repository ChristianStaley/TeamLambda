using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public string[] st_message;
    public float fl_distance = 1;
    private GameObject go_PC;
    private GameObject go_panel;
    private Text txt_window;
    private int in_message_stage = 0;
    //private Text txt_NPC;
    public string NPC_name;
    

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        go_PC = GameObject.FindWithTag("Player");
        go_panel = GameObject.Find("GM").transform.Find("DialoguePanel").gameObject;
        txt_window = go_panel.transform.Find("DialogueText").GetComponent<Text>();
        //txt_NPC= go_panel.transform.Find("MessageText").GetComponent<Text>();

    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // Is the PC in trigger distance
        if (Vector3.Distance(go_PC.transform.position, transform.position) < fl_distance)
        {
            // Enable the message panel active
            if (!go_panel.activeInHierarchy) go_panel.SetActive(true);
            


            // Step through the messages if there are more than 1
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (st_message.Length > 1 && (in_message_stage < st_message.Length - 1))
                    in_message_stage++;
                else
                {
                    go_panel.SetActive(false);
                }

            }
            
            // update the text box
            txt_window.text = st_message[in_message_stage];
            //txt_NPC.text = NPC_name;
        }
        else if (go_PC && Vector3.Distance(go_PC.transform.position, transform.position) < fl_distance + 1)
        {
            go_panel.SetActive(false);
            
        }

    }

   
    }//-----
