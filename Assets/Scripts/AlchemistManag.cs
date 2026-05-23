using UnityEngine;

public class AlchemistManag : MonoBehaviour
{
    public DialogueSysNew dialog1;
    public DialogueSysNew dialog2;

    public bool npcAskingDone;

    private bool isIn;
    private bool isOut;
    private bool didPress;

    private bool canNoMore;

    private bool firstThingDone;


    public AcrossSceneVars vars;

    private bool addedQ;

    private void Start()
    {
        vars = AcrossSceneVars.Instance;
        dialog2.enabled = false;
    }

    private void Update()
    {
        vars = AcrossSceneVars.Instance;

        if (npcAskingDone == true && addedQ == false)
        {
            Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(3);
            vars.askInfos = true;
            addedQ = true;

        }

        if (isIn && isOut && didPress)
        {
            if (canNoMore == false)
            {
                npcAskingDone = true;
                canNoMore = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            if(firstThingDone == false)
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
                if(firstThingDone == false)
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
