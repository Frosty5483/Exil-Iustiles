using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour, IPointerExitHandler, IPointerClickHandler
{
    public Image selfImg;

    public bool isFilled;

    [SerializeField] GameObject toolTip;
    [SerializeField] Sprite slotSprite;

    private void Start()
    {
        selfImg = GetComponent<Image>();
    }

    private void Update()
    {
        if(selfImg.sprite == slotSprite)
            isFilled = false;
        else if(selfImg.sprite != slotSprite)
            isFilled = true;

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (isFilled == true)
        {
            toolTip.transform.position = Input.mousePosition + new Vector3(10, 10, 0);
            toolTip.SetActive(true);
            toolTip.GetComponent<TMP_Text>().text = "";
        }
        if (isFilled == false)
        {
            toolTip.transform.position = Input.mousePosition + new Vector3(10, 10, 0);
            toolTip.SetActive(true);
            toolTip.GetComponentInChildren<TMP_Text>().text = "This Slot is Empty";
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        toolTip.GetComponentInChildren<TMP_Text>().text = "";
        toolTip.SetActive(false);
    }
}
