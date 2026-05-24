using System.ComponentModel;
using UnityEngine;

public class SeraManag : MonoBehaviour
{
    public DialogueSysNew dialog1;

    private bool isIn;
    private bool isOut;
    private bool didPress;

    bool canNoMore;

    private bool firstThingDone;

    public AcrossSceneVars vars;

    public Utils utils;

    private bool addedQ;

    private void Start()
    {
        utils = Utils.Instance;
        vars = AcrossSceneVars.Instance;

    }

    private void Update()
    {
        utils = Utils.Instance;
        vars = AcrossSceneVars.Instance;

        if (firstThingDone == true && addedQ == false)
        {
            Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(6);
            //Search kundenbuch quest

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
        if (other.gameObject.transform.tag == "Player")
        {
            if (firstThingDone == false)
            {
                isIn = true;

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
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
        if (other.gameObject.transform.tag == "Player")
        {
            if (firstThingDone == false)
            {
                isOut = true;

            }
        }
    }

}
