using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDialogue : MonoBehaviour
{

    public string[] st_message;
    public float fl_distance = 1;
    private GameObject go_PC;
    public GameObject go_panel;
    private Text txt_window;
    public GameObject go_object;
    //public bool speechFinish;
    private int in_message_stage = 0;
    // Start is called before the first frame update
    void Start()
    {
        go_PC = GameObject.FindWithTag("Player");
        txt_window = go_panel.transform.Find("DialogueText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TextSpeech();
    }

    void TextSpeech()
    {
        // Is the PC in trigger distance
        if (Vector3.Distance(go_PC.transform.position, transform.position) < fl_distance)
        {
            go_panel.SetActive(true);
            // Enable the message panel active
            if (!go_panel.activeInHierarchy) ;



            // Step through the messages if there are more than 1
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (st_message.Length > 1 && (in_message_stage < st_message.Length - 1))
                    in_message_stage++;
                else
                {
                    go_panel.SetActive(false);
                    //speechFinish = true;
                    //animBoss.SetTrigger("CombatIdle");
                    Destroy(go_object);
                }

            }

            // update the text box
            txt_window.text = st_message[in_message_stage];
            //animBoss.SetTrigger("CombatIdle");
            //txt_NPC.text = NPC_name;
        }
    }
}
