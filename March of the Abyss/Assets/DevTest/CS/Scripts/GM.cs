using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    #region Singleton
    static GM mSingleton = null;


    public GM Singleton
    {
        get
        {
            return mSingleton;
        }
    }


    private void Awake()
    {
        if (mSingleton == null)
        {
            mSingleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (mSingleton != this)
        {
            Destroy(gameObject);
        }
    }



    #endregion



    private void Start()
    {
        Application.targetFrameRate = 60;

    }


    #region Spawning

    public enum Prefabs
    {
        WaterParticle,
        GasParticle,
        LastOne
    };

    [SerializeField]
    private GameObject[] SpawnablePrefabs;
    private static GameObject mPlayer;

    public static GameObject SpawnPrefab(Prefabs vPrefab, Vector2 vPosition, Quaternion vRotation) //Spawns the requested prefab at the specified position and rotation
    {
        int tIndex = (int)vPrefab;
        if (mSingleton.SpawnablePrefabs != null && tIndex < mSingleton.SpawnablePrefabs.Length)
        {
            {
                if (mPlayer = null)
                    mPlayer = Instantiate(mSingleton.SpawnablePrefabs[tIndex], vPosition, vRotation);
                else
                    return Instantiate(mSingleton.SpawnablePrefabs[tIndex], vPosition, vRotation);
            }
        }

        return null;
    }

    public Vector3 currentSpawnLocation;
    public static Vector3 SpawnLocation
    {
        get
        {
            Debug.Log("Returning Transform: " + mSingleton.currentSpawnLocation);
            return mSingleton.currentSpawnLocation;

        }
        set 
        {
            Debug.Log("Setting Transform: " + value);
            mSingleton.currentSpawnLocation = value;
        }

    }

    #endregion

    #region Souls

    [SerializeField]
    int currentSouls = 0;
    static public int Souls
    {
        get
        {
            return mSingleton.currentSouls;
        }
        set
        {
            if(value < 0)
            {
                mSingleton.currentSouls += value;
            }
            else
            {
                mSingleton.currentSouls = value;
            }
            
        }

    }

    #endregion

    #region Spells

    public GameObject[] spellList;





    private int currentSpell = 0;
    public static GameObject spell
    {
        get
        {

            return mSingleton.spellList[mSingleton.currentSpell];
        }

    }

    public static int ChangeSpell
    {
        get
        {
            return mSingleton.currentSpell;
        }
        set
        {
            mSingleton.currentSpell = value;
        }
    }

    private bool spell2Active = false;
    public static bool Spell2Active
    {
        get
        {
            return mSingleton.spell2Active;
        }
        set
        {
            mSingleton.spell2Active = value;
        }
    }

    private bool spell3Active = false;
    public static bool Spell3Active
    {
        get
        {
            return mSingleton.spell3Active;
        }
        set
        {
            mSingleton.spell3Active = value;
        }
    }

    #endregion

    #region Health

    private float maxHealth = 100;
    public float currentHealth = 100;
    static public float Health
    {
        get
        {
            return mSingleton.currentHealth;
        }
        set
        {
            if (value <= 0)
            {
                mSingleton.currentHealth += value;
            }
            else
            {
                
                mSingleton.currentHealth = value;
            }
            

        }

    }

    static public float MaxHealth
    {
        get
        {
            return mSingleton.maxHealth;
        }
        set
        {
            mSingleton.maxHealth = value;
        }
    }



    #endregion

    #region Upgrades

    [SerializeField]
    private float currentAttackSpeed;
    public static float AttackSpeed
    {
        get
        {
            return mSingleton.currentAttackSpeed;
        }
        set
        {
            mSingleton.currentAttackSpeed = value;
        }
    }

    [SerializeField]
    private float currentAttackRange;
    public static float AttackRange
    {
        get
        {
            return mSingleton.currentAttackRange;
        }
        set
        {
            mSingleton.currentAttackRange = value;
        }
    }

    [SerializeField]
    private float currentAttackDamage;
    public static float AttackDamage
    {
        get
        {
            return mSingleton.currentAttackDamage;
        }
        set
        {
            mSingleton.currentAttackDamage = value;
        }
    }

    #endregion

    #region Mana

    private float regenManaAmount = 20;
    private float maxMana = 100;
    private bool canRegenMana = true;
    private float regenCooldown = 2f;
    public float currentMana = 100;
    static public float Mana
    {
        get
        {
            return mSingleton.currentMana;
        }
        set
        {
            if (value <= 0)
            {
                mSingleton.currentMana += value;
                mSingleton.RegenMana();
            }
            else
            {
                mSingleton.currentMana += value;
                mSingleton.RegenMana();
            }



        }

    }

    static public float ManaRegen
    {
        get
        {
            return mSingleton.regenManaAmount;
        }
        set
        {
            mSingleton.regenManaAmount = value;
        }
    }

    static public float ManaMax
    {
        get
        {
            return mSingleton.maxMana;
        }
        set
        {
            mSingleton.maxMana = value;
        }
    }


            public void ManaRegenUpgrade(int value)
    {
        regenManaAmount += value;
    }

    public void ManaMaxUpgrade(int value)
    {
        maxMana += value;
    }


    private void RegenMana()
    {
        if(regenCooldown > 0)
        {
            regenCooldown = 2f;
        }
        else
        {
            GM.mSingleton.currentMana += regenManaAmount;
            regenCooldown = 2f;
        }
            
    }

    #endregion

    #region Gold

    [SerializeField]
    private int currentGold = 0;

    public static int Gold
    {
        get
        {
            return mSingleton.currentGold;
        }
        set
        {
            mSingleton.currentGold = value;
        }
    }


    #endregion

    #region UI

    private string currentZone = "Spawn";
    public static string Zone
    {
        get
        {
            return mSingleton.currentZone;
        }
        set
        {
            mSingleton.currentZone = value;
        }

    }

    private bool isUIActive = false;
    public static bool UIActive
    {
        get
        {
            return mSingleton.isUIActive;
        }
        set
        {
            mSingleton.isUIActive = value;
        }
    }


    #endregion

    private int currentKills;
    public static int KillCount
    {
        get
        {
            return mSingleton.currentKills;
        }
        set
        {
            mSingleton.currentKills += value;
        }
    }

    #region GameStates

    int mLevel = 1;
    float waitTime = 10.0f;
    float lastTime = 10.0f;

    public static int Level
    {
        get
        {
            return mSingleton.mLevel;
        }
        set
        {
            mSingleton.mLevel = value;
        }
    }

    private void IncreaseLevel()
    {
        if (Time.time > lastTime)
        {
            mLevel++;
            lastTime += waitTime;
        }
    }

    //[SerializeField]
    //private GameObject GameOverText;    //Set in IDE

    //[SerializeField]
    //private GameObject PressPlayText;    //Set in IDE

    //[SerializeField]
    //private GameObject NextLevelText;    //Set in IDE

    //public enum GameStates
    //{
    //    None,
    //    Init,
    //    Startup,
    //    PressPlay,
    //    Play,
    //    Playing,
    //    NextLevel,
    //    GameOver,
    //}

    //GameStates mCurrentState = GameStates.None;

    //static public GameStates GameState
    //{
    //    private set
    //    {
    //        if (value != mSingleton.mCurrentState)
    //        {
    //            mSingleton.ExitState(mSingleton.mCurrentState);
    //            GameStates tNextState = mSingleton.EnterState(value);
    //            if (value == tNextState)
    //            {
    //                mSingleton.mCurrentState = tNextState;
    //            }
    //            else
    //            {
    //                mSingleton.mCurrentState = value;
    //                GameState = tNextState;
    //            }
    //        }
    //    }
    //    get
    //    {
    //        return mSingleton.mCurrentState;
    //    }
    //}


    //private GameStates EnterState(GameStates vState)
    //{
    //    Debug.LogFormat("Enter State {0}", vState);
    //    switch (vState)
    //    {
    //        case GameStates.Init:

    //            GameClear();
    //            return GameStates.PressPlay;

    //        case GameStates.PressPlay:

    //            break;

    //        case GameStates.Play:

    //            return GameStates.Playing;

    //        case GameStates.NextLevel:
    //            mLevel++;
    //            return GameStates.Playing;

    //        case GameStates.GameOver:

    //            break;

    //        default:
    //            break;
    //    }
    //    return vState;
    //}

    //private void ExitState(GameStates vState)
    //{
    //    Debug.LogFormat("Exit State {0}", vState);
    //    switch (vState)
    //    {
    //        case GameStates.PressPlay:

    //            break;
    //        default:    //No Action
    //            break;
    //    }
    //}

    //IEnumerator GameStateCoRoutine()
    //{
    //    do
    //    {
    //        switch (GameState)
    //        {

    //            case GameStates.PressPlay:
    //                if (Input.GetKey(KeyCode.Space))
    //                {
    //                    GameState = GameStates.Play;    //Go to new state
    //                }
    //                break;

    //            case GameStates.Playing:
    //                {

    //                }
    //                break;

    //            case GameStates.GameOver:
    //                if (Input.GetKey(KeyCode.Space))
    //                {
    //                    GameState = GameStates.Init;    //Go to new state
    //                }
    //                break;

    //            default:    //No Action
    //                break;
    //        }
    //        yield return new WaitForSeconds(0.1f);  //Wait for a 10th of a second before runnign again, lets other stuff process
    //    } while (true); //Never End
    //}


    //static public void InitGame()
    //{
    //    mSingleton.mScore = 0;  //Reset Score
    //    mSingleton.mLevel = 1;  //Start at Level 1
    //    GameState = GameStates.Init;
    //}

    public static void StartGame()
    {


    }

    public static void GameClear()
    {
        //FakePhysicsBase[] tFFObjects = FindObjectsOfType<FakePhysicsBase>();
        //foreach (var tFF in tFFObjects)
        //{
        //    Destroy(tFF.gameObject);
        //}
    }

    #endregion

    #region Time

    float currentTime = 30.0f;
    public static float Timer
    {
        get
        {
            return mSingleton.currentTime;
        }
        set
        {
            mSingleton.currentTime = value;
        }
    }

    public static void AddTime(float time)
    {
        mSingleton.currentTime += time;
    }

    private static void CountTime()
    {
        mSingleton.currentTime -= Time.deltaTime;
    }

    #endregion

    #region Debug


    private void Update()
    {

        IncreaseLevel();
        CountTime();

        if (mSingleton.currentMana < 0)
        {
            mSingleton.currentMana = 0;

        }

        if (mSingleton.currentMana > maxMana)
        {
            mSingleton.currentMana = maxMana;
        }


        if(regenCooldown <= 0 && mSingleton.currentMana < maxMana)
        {

            RegenMana();
        }
        else if(regenCooldown > 0 && mSingleton.currentMana < maxMana)
        {
            regenCooldown -= Time.deltaTime;
        }

        

        if(currentHealth<= 0)
        {
            
            
            //SceneManager.LoadScene("MainScene");
        }

        //Debug.Log("Current Spell: " + mSingleton.currentSpell);
    }

    #endregion
}
