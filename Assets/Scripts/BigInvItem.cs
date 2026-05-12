using GLTFast.Logging;
using UnityEngine;
using UnityEngine.UI;

public class BigInvItem : MonoBehaviour
{

    public UiInputSys inputSys;

    private GameObject bigItemButton;

    public GameObject bigItem;

    private void Start()
    {
        bigItemButton = gameObject;
    }

    public void DontViewItem()
    {

        inputSys.inventory.SetActive(true);

        inputSys.viewOpen = false;

        gameObject.SetActive(false);
        gameObject.GetComponent<Image>().sprite = null;

        bigItem.SetActive(false);

        bigItemButton.SetActive(false);


    }
}
