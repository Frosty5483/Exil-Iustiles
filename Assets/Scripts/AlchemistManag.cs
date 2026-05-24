using UnityEngine;
using UnityEngine.UI;

public class AlchemistManag : MonoBehaviour
{
    public DialogueSysNew dialog1;
    public DialogueSysNew dialog2;
    public DialogueSysNew dialog3;

    public bool npcAskingDone;

    private bool isIn;
    private bool isIn1;
    private bool isOut;
    private bool isOut1;
    private bool didPress;
    private bool didPress1;

    private bool canNoMore;

    private bool firstThingDone;
    private bool secondThingDone;


    public AcrossSceneVars vars;

    public Utils utils;

    private bool addedQ;
    private bool addedQ1;

    private void Awake()
    {
        dialog2.enabled = false;
        dialog3.enabled = false;
    }

    private void Start()
    {
        utils = Utils.Instance;
        vars = AcrossSceneVars.Instance;
        
    }

    private void Update()
    {
        utils = Utils.Instance;
        vars = AcrossSceneVars.Instance;

        if (npcAskingDone == true && addedQ == false)
        {
            Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(3);
            vars.askAlch = true;
            addedQ = true;

        }

        if(utils.hasIDCardAlch == true)
        {
            vars.searchAlchRoom = true;
        }

        if (vars.searchAlchRoom == true && addedQ1 == false)
        {
            Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(4);
            //zimmer durchforsten quest
            Destroy(dialog1);
            firstThingDone = true;
            dialog2.enabled = true;
            addedQ1 = true;
        }

        if (isIn && isOut && didPress)
        {
            if (canNoMore == false)
            {
                npcAskingDone = true;
                canNoMore = true;
            }
        }
        if(isIn1 && isOut1 && didPress1 && secondThingDone == false)
        {
            Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(5);
            vars.giveIdBack = true;
            secondThingDone = true;
            //250 münzen auftrag quest
            //sobald fertig muss dialog3 enabled werden und dialog2 destroyed
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
            if(firstThingDone == true && secondThingDone == false)
            {
                isIn1 = true;
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
                if (firstThingDone == true && secondThingDone == false)
                {
                    didPress1 = true;
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
            if (firstThingDone == true && secondThingDone == false)
            {
                isOut1 = true;
            }
        }
    }
}
