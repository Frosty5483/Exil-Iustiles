using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utils;

public class pickUpItem : MonoBehaviour
{
    private bool notPressAgain;

    [SerializeField] private Animator playerAnim;

    [SerializeField] private TMP_Text popTxt;

    public InvSystem invSys;

    public Sprite item;

    public Sprite otherSideItem;

    public string toolTipTxt;

    bool gaveItem = false;

    public UtilsBoolField targetBool;

    public Utils utils;

    private void Start()
    {
        utils = Utils.Instance;
        invSys = Utils.FindWithTagAcrossAllScenes("InvSys").GetComponent<InvSystem>();
        popTxt.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            popTxt.text = "Press [F] to pick up";
            if (Input.GetKey(KeyCode.F) && notPressAgain == false && utils.hasIDCardAlch == false)
            {
                popTxt.text = "";

                GameObject slot = invSys.getNextEmptySlot();

                slot.GetComponent<Image>().sprite = item;
                slot.GetComponent<Image>().color = Color.white;
                slot.GetComponent<InvSlot>().toolTipTxt = toolTipTxt;

                slot.GetComponent<InvSlot>().bigItemSprite = otherSideItem;
                gaveItem = true;
                Utils.Instance.SetBool(targetBool, true);

                if (playerAnim.gameObject.GetComponent<PlayerMoveNew>().inFPS == false)
                {
                    playerAnim.SetTrigger("pickUp");
                }

                gameObject.SetActive(false);
                StartCoroutine(cor());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popTxt.text = "";
        }
    }

    private IEnumerator cor()
    {
        notPressAgain = true;
        yield return new WaitForSeconds(0.2f);
        notPressAgain = false;
    }
}
