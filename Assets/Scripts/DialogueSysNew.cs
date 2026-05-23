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

    public AudioClip mainTClip;

    [TextArea(2, 5)]
    public string[] extraTexts;

    public AudioClip[] extraTClips;

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

    [Header("Audio")]
    public AudioSource aS;

    [Header("Settings")]
    public float textSpeed = 0.04f;

    private static DialogueSysNew activeDialogue;

    private int currentLineIndex;
    private int currentExtraIndex; // -1 = on main text, 0+ = on that extra text
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
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && activeDialogue == null)
            StartDialogue();

        if (!dialogueCanvas.gameObject.activeSelf) return;

        if (activeDialogue != this) return;

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
        activeDialogue = this;

        playerMoveNew.EnterDialogue();
        mainCamera?.gameObject.SetActive(false);
        firstPersonCamera?.gameObject.SetActive(false);
        if (firstPersonCamera.enabled)
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
        // -1 means we are currently on the main text (no extra text shown yet)
        currentExtraIndex = -1;
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
            aS.Stop();
            aS.clip = line.mainTClip;
            aS.Play();
            return;
        }

        // currentExtraIndex >= 0 ensures we only enter this block after we've
        // properly advanced past the main text
        if (line.extraTexts != null && currentExtraIndex >= 0 && currentExtraIndex < line.extraTexts.Length)
        {
            StartCoroutine(TypeText(line.extraTexts[currentExtraIndex]));
            aS.Stop();
            aS.clip = line.extraTClips[currentExtraIndex];
            aS.Play();
            return;
        }

        ShowOptions(line);
    }

    void AdvanceText()
    {
        DialogueLine line = dialogueLines[currentLineIndex];
        bool hasExtras = line.extraTexts != null && line.extraTexts.Length > 0;

        // Increment first, then check if there is still an extra text to show.
        // Coming from main text: -1 + 1 = 0  → shows extraTexts[0]
        // Coming from extraTexts[i]: i + 1   → shows extraTexts[i+1] or options
        if (hasExtras && currentExtraIndex < line.extraTexts.Length - 1)
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

        // Auto-show options only after the very last text block has finished typing
        bool isLastBlock = (line.extraTexts == null || line.extraTexts.Length == 0)
                        || (currentExtraIndex == line.extraTexts.Length - 1);

        if (!optionsShown && isLastBlock)
        {
            ShowOptions(line);
        }
    }

    void SkipTyping()
    {
        StopAllCoroutines();

        DialogueLine line = dialogueLines[currentLineIndex];

        // currentExtraIndex == -1  →  we are skipping the main text
        // currentExtraIndex >= 0   →  we are skipping one of the extra texts
        if (currentExtraIndex >= 0 && line.extraTexts != null && currentExtraIndex < line.extraTexts.Length)
            dialogueText.text = line.extraTexts[currentExtraIndex];
        else
            dialogueText.text = line.mainText;

        isTyping = false;

        // Same "last block" rule as in TypeText
        bool isLastBlock = (line.extraTexts == null || line.extraTexts.Length == 0)
                        || (currentExtraIndex >= 0 && currentExtraIndex == line.extraTexts.Length - 1);

        if (!optionsShown && isLastBlock)
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
            RectTransform rect = btn.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.localPosition = Vector3.zero;
            StartCoroutine(ForceLayoutRebuild());
            btn.onClick.AddListener(() => SelectOption(option));
        }

        Button leaveBtn = Instantiate(optionButtonPrefab, optionsContainer);
        leaveBtn.GetComponentInChildren<TMP_Text>().text = "Ich habe es mir anders überlegt.";
        RectTransform rect1 = leaveBtn.GetComponent<RectTransform>();
        rect1.localScale = Vector3.one;
        rect1.localPosition = Vector3.zero;
        StartCoroutine(ForceLayoutRebuild());
        leaveBtn.onClick.AddListener(EndDialogue);
    }

    private IEnumerator ForceLayoutRebuild()
    {
        yield return new WaitForEndOfFrame();

        LayoutGroup layoutGroup = optionsContainer.GetComponent<LayoutGroup>();
        if (layoutGroup != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
        }
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
        activeDialogue = null;

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