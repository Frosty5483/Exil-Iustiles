using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#region DATA_CLASSES

[System.Serializable]
public class DialogueOption
{
    public string optionText;
    [Tooltip("Index of dialogue line to go to. -1 = End Dialogue")]
    public int nextDialogueIndex;
}

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 5)]
    public string mainText;

    [TextArea(2, 5)]
    public string[] extraTexts;

    public List<DialogueOption> options;
}

#endregion

public class DialogueSysNew : MonoBehaviour
{
    [Header("Dialogue Data")]
    public string npcName;
    public List<DialogueLine> dialogueLines;
    public PlayerMoveNew playerMoveNew;

    [Header("UI")]
    public Canvas dialogueCanvas;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Transform optionsContainer;
    public Button optionButtonPrefab;

    [Header("Cameras")]
    public Camera dialogueCamera;
    public Camera mainCamera;
    public Camera firstPersonCamera;

    [Header("Settings")]
    public float textSpeed = 0.04f;

    private int currentLineIndex;
    private int currentExtraIndex;
    private bool mainTextShown;
    private bool isTyping;
    private bool optionsShown;
    private bool playerInRange;

    #region UNITY

    void Start()
    {
        dialogueCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
            StartDialogue(); 

        if (!dialogueCanvas.gameObject.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
                SkipTyping();
            else if (!optionsShown)
                AdvanceText();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    #endregion

    #region DIALOGUE_FLOW

    void StartDialogue()
    {
        playerMoveNew.EnterDialogue();
        mainCamera?.gameObject.SetActive(false);
        firstPersonCamera?.gameObject.SetActive(false);
        if(firstPersonCamera.enabled)
        {
            playerMoveNew.inFPS = true;
        }
        else
        {
            playerMoveNew.inFPS = false;
        }
        firstPersonCamera.enabled = false;
        dialogueCamera?.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dialogueCanvas.gameObject.SetActive(true);
        nameText.text = npcName;

        currentLineIndex = 0;
        ResetLineState();

        ShowCurrentText();
    }

    void ResetLineState()
    {
        currentExtraIndex = 0;
        mainTextShown = false;
        optionsShown = false;
    }

    void ShowCurrentText()
    {
        ClearOptions();

        DialogueLine line = dialogueLines[currentLineIndex];

        if (!mainTextShown)
        {
            mainTextShown = true;
            StartCoroutine(TypeText(line.mainText));
            return;
        }

        if (line.extraTexts != null && currentExtraIndex < line.extraTexts.Length)
        {
            StartCoroutine(TypeText(line.extraTexts[currentExtraIndex]));
            return;
        }

        ShowOptions(line);
    }

    void AdvanceText()
    {
        DialogueLine line = dialogueLines[currentLineIndex];

        if (line.extraTexts != null && currentExtraIndex < line.extraTexts.Length)
        {
            currentExtraIndex++;
            ShowCurrentText();
        }
        else
        {
            ShowOptions(line);
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;

        DialogueLine line = dialogueLines[currentLineIndex];

        if (!optionsShown && (line.extraTexts == null || line.extraTexts.Length == 0))
        {
            ShowOptions(line);
        }
    }

    void SkipTyping()
    {
        StopAllCoroutines();

        DialogueLine line = dialogueLines[currentLineIndex];

        if (!mainTextShown)
            dialogueText.text = line.mainText;
        else if (line.extraTexts != null && currentExtraIndex < line.extraTexts.Length)
            dialogueText.text = line.extraTexts[currentExtraIndex];
        else
            dialogueText.text = line.mainText;

        isTyping = false;

        if (!optionsShown && (line.extraTexts == null || line.extraTexts.Length == 0))
        {
            ShowOptions(line);
        }
    }

    #endregion

    #region OPTIONS

    void ShowOptions(DialogueLine line)
    {
        if (optionsShown) return;

        optionsShown = true;
        ClearOptions();

        foreach (DialogueOption option in line.options)
        {
            Button btn = Instantiate(optionButtonPrefab, optionsContainer);
            btn.GetComponentInChildren<TMP_Text>().text = option.optionText;
            btn.onClick.AddListener(() => SelectOption(option));
        }

        Button leaveBtn = Instantiate(optionButtonPrefab, optionsContainer);
        leaveBtn.GetComponentInChildren<TMP_Text>().text = "Leave";
        leaveBtn.onClick.AddListener(EndDialogue);
    }

    void SelectOption(DialogueOption option)
    {
        if (option.nextDialogueIndex < 0)
        {
            EndDialogue();
            return;
        }

        currentLineIndex = option.nextDialogueIndex;
        ResetLineState();
        ShowCurrentText();
    }

    void ClearOptions()
    {
        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);
    }

    #endregion

    #region END

    void EndDialogue()
    {
        dialogueCanvas.gameObject.SetActive(false);

        dialogueCamera?.gameObject.SetActive(false);
        mainCamera?.gameObject.SetActive(true);
        firstPersonCamera?.gameObject.SetActive(true);
        firstPersonCamera.enabled = true;

        if (playerMoveNew.inFPS)
        {
            firstPersonCamera.enabled = true;
        }
        else if (playerMoveNew.inFPS == false)
        {
            firstPersonCamera.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMoveNew.ExitDialogue();
        ClearOptions();
    }

    #endregion
}
