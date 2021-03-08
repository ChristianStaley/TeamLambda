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
            return mSingleton.currentSpawnLocation;
        }
        set 
        {
            mSingleton.currentSpawnLocation = value;
        }

    }

    #endregion

    #region Souls

    int currentSouls = 150;
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
                mSingleton.currentSouls += value;
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

    public static void ChangeSpell(int n)
    {
        mSingleton.currentSpell += n;
        if (mSingleton.currentSpell < 0)
        {
            //mSingleton.currentSpell = spellMaxRange;
        }
        else if (mSingleton.currentSpell > 1)
        {
            //mSingleton.currentSpell = spellMinRange;
        }
    }

    #endregion

    #region Health

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
                mSingleton.currentHealth += value;
            }
            

        }

    }

    #endregion

    #region Mana

    public float currentMana = 100;
    static public float Mana
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
                mSingleton.currentHealth += value;
            }


        }

    }

    #endregion

    #region Gold

    private int currentGold = 100;

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

    #endregion

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
        if(currentHealth<= 0)
        {
            
            
            //SceneManager.LoadScene("MainScene");
        }
    }

    #endregion
}
