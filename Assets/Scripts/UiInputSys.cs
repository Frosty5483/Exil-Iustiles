using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UiInputSys : MonoBehaviour
{
    public bool isInspecting;
    public bool isPaused;
    public bool invOpen;
    public bool questOpen;

    [SerializeField] private PlayerMoveNew playerMovNew;
    [SerializeField] private GameObject tpsCam;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject openQuestMenu;
    [SerializeField] private GameObject closedQuestMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenInventory();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OpenQuestMenu();
        }
        if (Input.GetKeyDown(KeyCode.H) && isInspecting == false)
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

    private IEnumerator waitForNextPause(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        isPaused = !isPaused;
    }
    private IEnumerator waitForNexInv(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        invOpen = !invOpen;
    }
    private IEnumerator waitForNexQuest(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        questOpen = !questOpen;
    }

    public void OpenInventory()
    {
        if (invOpen == false)
        {
            Debug.Log("Open Inv");
            StartCoroutine(waitForNexInv(0.25f));
        }
            
        if (invOpen == true)
        {
            Debug.Log("Close Inv");
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        playerMovNew.enabled = false;
        tpsCam.SetActive(false);
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeTime()
    {
        playerMovNew.enabled = true;
        tpsCam.SetActive(true);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ResumeGame()
    {
        ResumeTime();
        StartCoroutine(waitForNextPause(0.25f));
    }
}
