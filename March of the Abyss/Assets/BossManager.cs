using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public GameObject Phase2;
    public GameObject Phase2Text;
    public GameObject Phase2End;


    public Animator animBoss;

    private HealthBarController healthbar;
    //public bool tickOn;

    static BossManager mSingleton = null; // created a share static singleton

    public BossManager singleton
    {
        get
        {
            return mSingleton;
        }
    }
    public void Awake()//this will called just after obehct is instantiated
    {
        StartCoroutine(GuardSpawn());
        go_PC = GameObject.FindWithTag("Player");
        healthbar = GameObject.Find("BossHP").GetComponent<HealthBarController>();
        txt_window = go_panel.transform.Find("DialogueText").GetComponent<Text>();
        //go_Player = GameObject.Find("Player"); // find the game object with name player
        Cursor.visible = false; // turn off the current cursor

        if (mSingleton == null)
        {
            mSingleton = this;
            DontDestroyOnLoad(gameObject);


            GameState = GameStates.Speech; //Init the game with the state machine
            StartCoroutine(GameStateCoRoutine());
            

        }
        else if (mSingleton != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Speech();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Spawn();
            healthbar.ChangeHP(-25);
        }
    }

    public enum GameStates
    {   //State the game is in
        None        //Pre start
, Speech
, Playing
, Phase1
, Phase2
, Phase3
, GameOver
    }
    GameStates mCurrentState = GameStates.None;


    public GameStates EnterState(GameStates vState)
    {
        //Debug.LogFormat("Enter State {0}", vState);
        switch (vState)
        {
              //Also trigger new state on exit

            case GameStates.Speech:


                
                return GameStates.Playing;

            case GameStates.Phase1:

                Dospawn = true;
                //Spawn();
                return GameStates.Playing;

            case GameStates.Phase2:

                Phase2Text.SetActive(true);
                Phase2.SetActive(true);
                return GameStates.Playing;
            case GameStates.Phase3:

                //Spawn();
                return GameStates.Playing;


            case GameStates.GameOver:
                // GameOver and Restart boss fight
                break;

            default:    //No Action
                break;
        }
        return vState;  //Default just return state we entered
    }

    IEnumerator GameStateCoRoutine()
    {
        do
        {
            switch (GameState)
            {


                case GameStates.Playing:
                    {
                        if (HealthBarController.currenthp <= 100)
                        {
                            //Spawn();
                            //Speech();
                            GameState = GameStates.Speech;
                        }

                        if (HealthBarController.currenthp <= 100 && speechFinish)
                        {
                            //Spawn();
                            //Dospawn = true;
                            //StartCoroutine(GuardSpawn());
                            GameState = GameStates.Phase1;

                        }
                        if (HealthBarController.currenthp <= 50)
                        {
                            //Spawn();
                            //Spawn Guards
                            GameState = GameStates.Phase2;

                        }
                        if (!Phase2End)
                        {
                            ////back to Phase 1
                            GameState = GameStates.Phase1;
                        }
                  

                        if (HealthBarController.currenthp <= 0)
                        {
                            GameState = GameStates.GameOver;
                        }
                    }
                    break;

                case GameStates.Speech:
                    {
                        Speech();
                    }
                    break;

                case GameStates.Phase1:
                    {
                        
                        StartCoroutine(GuardSpawn());
                    }
                    break;
                case GameStates.Phase2:
                    {
                        //Spawn();
                    }
                    break;
                case GameStates.GameOver:
                    {
                       
                        if (Input.GetKey(KeyCode.Escape))
                        {
                            //Application.OpenURL(url);    //Quit the app is the player hit ESC
                        }
                        if (Input.GetKey(KeyCode.Space))
                        {
                            //DoSpawn = true; // turn on the CoRoutine of the spawn
                            GameState = GameStates.Speech;    //Go to new state
                        }
                    }
                    break;


                default:    //No Action
                    break;
            }
            yield return new WaitForSeconds(1f);  //Wait for a 10th of a second before runnign again, lets other stuff process
        } while (true); //Never End
    }


    static public GameStates GameState
    {   //This may call itself recursivly
        private set
        {
            if (value != mSingleton.mCurrentState)
            {  //Only change state if different from last one, or its first time its used
                //mSingleton.ExitState(mSingleton.mCurrentState);  //Exit last state
                GameStates tNextState = mSingleton.EnterState(value); //Enter new state
                if (value == tNextState)
                { //If return state is final state, set it
                    mSingleton.mCurrentState = tNextState;
                }
                else
                {
                    mSingleton.mCurrentState = value;  //State we are in now
                    GameState = tNextState; //If not we need to change state again, until we reach the final one
                }
            }
        }
        get
        {
            return mSingleton.mCurrentState;   //Get Current State
        }
    }
    //public Transform plane;
    public GameObject spawnablePrefab;
    public Transform[] spawnPoints;
    public bool Dospawn;
    public bool Spawning;
    int randomSpawn;
    //public Vector3 size;

    public IEnumerator GuardSpawn()
    {

        while(Spawning)
        {
            Debug.Log("SpawningOn");
            while (Dospawn)
            {

                Debug.Log("Spawning");
                randomSpawn = Random.Range(0, spawnPoints.Length);
                Instantiate(spawnablePrefab, spawnPoints[randomSpawn].position, Quaternion.identity);

                yield return new WaitForSeconds(5f);
            }
            yield return new WaitForSeconds(1f);
        } 

    }  
    //public int spawn = 2;
    //void Spawn()
    //{
    //    for (int i = 0; i < spawn; i++)
    //    {
            

    //        Instantiate(spawnablePrefab, Spawnpoints[i].position, Spawnpoints[i].rotation);
    //    }


    //}

    public string[] st_message;
    public float fl_distance = 1;
    private GameObject go_PC;
    public GameObject go_panel;
    private Text txt_window;
    public bool speechFinish;
    private int in_message_stage = 0;
    //NPC Speech
    
    void Speech()
    {
        // Is the PC in trigger distance
        if (Vector3.Distance(go_PC.transform.position, transform.position) < fl_distance)
        {
            // Enable the message panel active
            if (!go_panel.activeInHierarchy);



            // Step through the messages if there are more than 1
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (st_message.Length > 1 && (in_message_stage < st_message.Length - 1))
                    in_message_stage++;
                else
                {
                    go_panel.SetActive(false);
                    speechFinish = true;
                    animBoss.SetTrigger("CombatIdle");
                }

            }

            // update the text box
            txt_window.text = st_message[in_message_stage];
            //animBoss.SetTrigger("CombatIdle");
            //txt_NPC.text = NPC_name;
        }
    }
}
