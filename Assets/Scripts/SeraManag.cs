using System.ComponentModel;
using UnityEngine;

public class SeraManag : MonoBehaviour
{
    public DialogueSysNew dialog1;

    public GameObject tempText;

    private bool isIn;
    private bool isOut;
    private bool didPress;

    bool canNoMore;

    private bool firstThingDone;

    public AcrossSceneVars vars;

    public BoxCollider boxCollider;

    public Utils utils;

    private bool addedQ;

    public bool buchActive;

    private void Start()
    {
        utils = Utils.Instance;
        vars = AcrossSceneVars.Instance;
        dialog1.enabled = false;
        tempText.SetActive(false);

        if (PlayerPrefs.HasKey("SeraAddedBookQuest"))
        {
            if(PlayerPrefs.GetInt("SeraAddedBookQuest") == 1)
            {
                firstThingDone = true;
                dialog1.enabled = true;
                tempText.SetActive(false);
                addedQ = true;
                boxCollider.enabled = false;
                canNoMore = true;
            }
        }
    }

    private void Update()
    {
        utils = Utils.Instance;
        vars = AcrossSceneVars.Instance;

        if (vars.giveIdBack == true && firstThingDone == false)
        {
            tempText.SetActive(true);
            dialog1.enabled = true;

        }

        if (firstThingDone == true && addedQ == false)
        {
            Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(6);
            //Search kundenbuch quest
            PlayerPrefs.SetInt("SeraAddedBookQuest", 1);

            buchActive = true;

            boxCollider.enabled = false;
            tempText.SetActive(false);
            addedQ = true;
        }

        if (isIn && isOut && didPress)
        {
            if (canNoMore == false)
            {
                firstThingDone = true;
                canNoMore = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player" && dialog1.enabled)
        {
            if (firstThingDone == false)
            {
                isIn = true;

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player" && dialog1.enabled)
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (firstThingDone == false)
                {
                    didPress = true;

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "Player" && dialog1.enabled)
        {
            if (firstThingDone == false)
            {
                isOut = true;

            }
        }
    }

}
