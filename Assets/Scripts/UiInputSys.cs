using System.Collections;
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
    [SerializeField] private GameObject inventory;
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
        if (Input.GetKeyDown(KeyCode.Escape) && isInspecting == false)
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
            playerMovNew.animator.enabled = true;
            playerMovNew.enabled = true;
            tpsCam.SetActive(true);
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
        playerMovNew.animator.enabled = true;
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
