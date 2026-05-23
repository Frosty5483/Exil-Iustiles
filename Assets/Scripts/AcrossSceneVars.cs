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
            Destroy(eventSys);
            Destroy(gameObject); // destroy this duplicate last
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            SceneManager.LoadScene(1);
    }
}