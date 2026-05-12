using System;
using UnityEngine;
using UnityEngine.UI;

public class AbschiedsBrief : MonoBehaviour
{
    public InvSystem invSys;

    public Sprite item;

    public Sprite otherSideItem;

    public string toolTipTxt;

    bool gaveItem = false;

    private void Update()
    {
        if (gameObject.activeSelf && gaveItem == false)
        {
            GameObject slot = invSys.getNextEmptySlot();

            slot.GetComponent<Image>().sprite = item;
            slot.GetComponent<Image>().color = Color.white;
            slot.GetComponent<InvSlot>().toolTipTxt = toolTipTxt;
            slot.GetComponent<InvSlot>().bigItemSprite = otherSideItem;




            gaveItem = true;
        }
    }
}
