using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiInputSys : MonoBehaviour
{
    public bool isInspecting;
    public bool isPaused;
    public bool invOpen;
    public bool questOpen;
    public bool tpsCamEnabled;

    public static Utils utils;

    public PlayerMoveNew playerMovNew;
    public GameObject tpsCam;
    public GameObject pauseMenu;
    public GameObject inventory;
    public GameObject openQuestMenu;
    public GameObject closedQuestMenu;
    public bool viewOpen;

    public GameObject optionsMenu;

    private void Update()
    {
        if(optionsMenu.active == true)
        {
            viewOpen = true;
            if(Input.GetKey(KeyCode.Escape))
            {
                viewOpen = false;
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
        }


        if (playerMovNew == null)
            playerMovNew = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveNew>();

        if (tpsCam == null)
            tpsCam = GameObject.FindGameObjectWithTag("tpsCam");

        if (pauseMenu == null)
            pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu");

        if (inventory == null)
            inventory = GameObject.FindGameObjectWithTag("inventory");

        if (openQuestMenu == null)
            openQuestMenu = GameObject.FindGameObjectWithTag("openQuestMenu");

        if (closedQuestMenu == null)
            closedQuestMenu = GameObject.FindGameObjectWithTag("closedQuestMenu");

        if (playerMovNew.thirdPersonCam == null)
            playerMovNew.thirdPersonCam = tpsCam.GetComponent<CinemachineCamera>();


        if (Input.GetKeyDown(KeyCode.E) && isPaused != true && viewOpen != true)
        {
            OpenInventory();
        }
        if (Input.GetKeyDown(KeyCode.Q) && isPaused != true && viewOpen != true)
        {
            OpenQuestMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isInspecting == false && invOpen != true && questOpen != true && viewOpen != true)
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ShowCursor();
        }
        if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
            DontShowCursor();
        }

    }

    public IEnumerator waitForNextPause(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        isPaused = !isPaused;
    }
    public IEnumerator waitForNexInv(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        invOpen = !invOpen;
    }
    public IEnumerator waitForNexQuest(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        questOpen = !questOpen;
    }

    public void OpenInventory()
    {
        if (invOpen == false)
        {
            playerMovNew.animator.enabled = false;
            playerMovNew.enabled = false;
            tpsCam.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            inventory.SetActive(true);
            StartCoroutine(waitForNexInv(0.25f));
        }
            
        if (invOpen == true)
        {
            
            playerMovNew.enabled = true;
            if(playerMovNew.inFPS == false)
            {
                tpsCam.SetActive(true);
                playerMovNew.animator.enabled = true;
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inventory.SetActive(false);
            StartCoroutine(waitForNexInv(0.25f));
        }
    }

    public void OpenQuestMenu()
    {
        if (questOpen == false)
        {
            openQuestMenu.SetActive(true);
            closedQuestMenu.SetActive(false);
            StartCoroutine(waitForNexQuest(0.25f));
        }

        if (questOpen == true)
        {
            openQuestMenu.SetActive(false);
            closedQuestMenu.SetActive(true);
            StartCoroutine(waitForNexQuest(0.25f));
        }
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void DontShowCursor()
    {
        if(invOpen == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void PauseGame()
    {
        if (isPaused == false)
        {
            PauseTime();
            StartCoroutine(waitForNextPause(0.25f));
        }
        if (isPaused == true)
        {
            ResumeGame();
        }
    }

    public void PauseTime()
    {
        playerMovNew.animator.enabled = false;
        playerMovNew.enabled = false;
        tpsCam.SetActive(false);
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeTime()
    {
        
        playerMovNew.enabled = true;
        if(playerMovNew.inFPS == false)
        {
            tpsCam.SetActive(true);
            playerMovNew.animator.enabled = true;
        }
        
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ResumeGame()
    {
        ResumeTime();
        StartCoroutine(waitForNextPause(0.25f));
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
