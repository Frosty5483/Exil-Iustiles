using UnityEngine;
using UnityEngine.SceneManagement;

public class AcrossSceneVars : MonoBehaviour
{
    public GameObject Inv;
    public GameObject invSys;
    public GameObject uI;
    public GameObject inputSys;
    public GameObject tps;
    public GameObject mc;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject eventSys;

    [Header("Quests")]
    public bool findFlintBur;
    public bool askInfos;
    public bool askAlch;
    public bool searchAlchRoom;
    public bool giveIdBack;
    public bool earn250Coins;
    public bool searchInSera;
    public bool earn250Coins2;
    public bool searchMoreFamily;
    public bool findLostID;
    public bool tryDoor;
    public bool searchWorkLab;
    public bool findChainOwner;
    public bool askAlch2;
    public bool costume;
    public bool earn400Coins;
    public bool askUnc;

    public static AcrossSceneVars Instance;

    void Awake() // <-- Awake not Start, runs before anything else
    {
        if (Instance == null)
        {
            Instance = this;

            // Only call DontDestroyOnLoad once, on the first instance
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(Inv);
            DontDestroyOnLoad(invSys);
            DontDestroyOnLoad(uI);
            DontDestroyOnLoad(tps);
            DontDestroyOnLoad(inputSys);
            DontDestroyOnLoad(mc);
            DontDestroyOnLoad(pauseMenu);
            DontDestroyOnLoad(optionsMenu);
            DontDestroyOnLoad(eventSys);
        }
        else
        {
            // Destroy the duplicate scene versions of ALL the objects
            Destroy(Inv);
            Destroy(invSys);
            Destroy(uI);
            Destroy(tps);
            Destroy(inputSys);
            Destroy(mc);
            Destroy(pauseMenu);
            Destroy(optionsMenu);
            Destroy(eventSys);
            Destroy(gameObject); // destroy this duplicate last
        }
    }

    void Update()
    {

        if (findFlintBur)
        {
            PlayerPrefs.SetInt("findFlintBur", 1);
        }
        if(PlayerPrefs.GetInt("findFlintBur") == 1)
        {
            findFlintBur = true;
        }

        if (askInfos)
        {
            PlayerPrefs.SetInt("askInfos", 1);
        }
        if (PlayerPrefs.GetInt("askInfos") == 1)
        {
            askInfos = true;
        }

        if (askAlch)
        {
            PlayerPrefs.SetInt("askAlch", 1);
        }
        if (PlayerPrefs.GetInt("askAlch") == 1)
        {
            askAlch = true;
        }

        if (searchAlchRoom)
        {
            PlayerPrefs.SetInt("searchAlchRoom", 1);
        }
        if (PlayerPrefs.GetInt("searchAlchRoom") == 1)
        {
            searchAlchRoom = true;
        }

        if (giveIdBack)
        {
            PlayerPrefs.SetInt("giveIdBack", 1);
        }
        if (PlayerPrefs.GetInt("giveIdBack") == 1)
        {
            giveIdBack = true;
        }

        if (earn250Coins)
        {
            PlayerPrefs.SetInt("earn250Coins", 1);
        }
        if (PlayerPrefs.GetInt("earn250Coins") == 1)
        {
            earn250Coins = true;
        }

        if (searchInSera)
        {
            PlayerPrefs.SetInt("searchInSera", 1);
        }
        if (PlayerPrefs.GetInt("searchInSera") == 1)
        {
            searchInSera = true;
        }

        if (earn250Coins2)
        {
            PlayerPrefs.SetInt("earn250Coins2", 1);
        }
        if (PlayerPrefs.GetInt("earn250Coins2") == 1)
        {
            earn250Coins2 = true;
        }

        if (searchMoreFamily)
        {
            PlayerPrefs.SetInt("searchMoreFamily", 1);
        }
        if (PlayerPrefs.GetInt("searchMoreFamily") == 1)
        {
            searchMoreFamily = true;
        }

        if (findLostID)
        {
            PlayerPrefs.SetInt("findLostID", 1);
        }
        if (PlayerPrefs.GetInt("findLostID") == 1)
        {
            findLostID = true;
        }

        if (tryDoor)
        {
            PlayerPrefs.SetInt("tryDoor", 1);
        }
        if (PlayerPrefs.GetInt("tryDoor") == 1)
        {
            tryDoor = true;
        }

        if (searchWorkLab)
        {
            PlayerPrefs.SetInt("searchWorkLab", 1);
        }
        if (PlayerPrefs.GetInt("searchWorkLab") == 1)
        {
            searchWorkLab = true;
        }

        if (findChainOwner)
        {
            PlayerPrefs.SetInt("findChainOwner", 1);
        }
        if (PlayerPrefs.GetInt("findChainOwner") == 1)
        {
            findChainOwner = true;
        }

        if (askAlch2)
        {
            PlayerPrefs.SetInt("askAlch2", 1);
        }
        if (PlayerPrefs.GetInt("askAlch2") == 1)
        {
            askAlch2 = true;
        }

        if (costume)
        {
            PlayerPrefs.SetInt("costume", 1);
        }
        if (PlayerPrefs.GetInt("costume") == 1)
        {
            costume = true;
        }

        if (earn400Coins)
        {
            PlayerPrefs.SetInt("earn400Coins", 1);
        }
        if (PlayerPrefs.GetInt("earn400Coins") == 1)
        {
            earn400Coins = true;
        }

        if (askUnc)
        {
            PlayerPrefs.SetInt("askUnc", 1);
        }
        if (PlayerPrefs.GetInt("askUnc") == 1)
        {
            askUnc = true;
        }



    }
}