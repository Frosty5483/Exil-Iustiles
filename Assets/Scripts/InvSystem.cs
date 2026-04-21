using UnityEngine;
using UnityEngine.UI;

public class InvSystem : MonoBehaviour
{
    public GameObject[] slots = new GameObject[15];

    public GameObject getNextEmptySlot()
    {
        GameObject returnObj = null;
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].GetComponent<Image>().sprite == slots[i].GetComponent<InvSlot>().slotSprite)
            {
                returnObj = slots[i];
                break;
            }
        }

        return returnObj;
    }
}
