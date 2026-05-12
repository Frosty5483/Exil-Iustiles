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


    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(Inv);
        DontDestroyOnLoad(invSys);
        DontDestroyOnLoad(uI);
        DontDestroyOnLoad(tps);
        DontDestroyOnLoad(inputSys);
        DontDestroyOnLoad(mc);
        DontDestroyOnLoad(pauseMenu);
        DontDestroyOnLoad(eventSys);

        if (Input.GetKeyDown(KeyCode.T))
            SceneManager.LoadScene(1);
    }

}
