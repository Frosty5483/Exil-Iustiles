using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour, IPointerExitHandler, IPointerClickHandler
{
    public Image selfImg;

    public bool isFilled;

    [SerializeField] GameObject toolTip;
    public Sprite slotSprite;

    public string toolTipTxt;

    public UiInputSys inputSys;

    public GameObject bigItem;

    public GameObject bigItemButton;

    public Sprite bigItemSprite;

    

    private void Start()
    {
        selfImg = GetComponent<Image>();

        inputSys = GameObject.FindGameObjectWithTag("InputSys").GetComponent<UiInputSys>();
    }

    private void Update()
    {
        

        if (selfImg.sprite == slotSprite)
            isFilled = false;
        else if (selfImg.sprite != slotSprite)
            isFilled = true;

        

    }

    public void ViewItem()
    {
        toolTip.GetComponentInChildren<TMP_Text>().text = "";
        toolTip.SetActive(false);

        inputSys.viewOpen = true;

        inputSys.inventory.SetActive(false);

        inputSys.playerMovNew.animator.enabled = false;
        inputSys.playerMovNew.enabled = false;
        inputSys.tpsCam.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        bigItem.GetComponent<Image>().sprite = bigItemSprite;
        bigItem.SetActive(true);
        bigItemButton.SetActive(true);

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (isFilled == true)
            {
                toolTip.transform.position = Input.mousePosition + new Vector3(10, 15, 0);
                toolTip.SetActive(true);
                toolTip.GetComponentInChildren<TMP_Text>().text = toolTipTxt;
            }
            if (isFilled == false)
            {
                toolTip.transform.position = Input.mousePosition + new Vector3(10, 15, 0);
                toolTip.SetActive(true);
                toolTip.GetComponentInChildren<TMP_Text>().text = "This Slot is Empty";
            }
        }

        if(pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (isFilled == true)
            {
                ViewItem();
            }
            
        }
       
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            toolTip.GetComponentInChildren<TMP_Text>().text = "";
            toolTip.SetActive(false);
        }

    }
}
