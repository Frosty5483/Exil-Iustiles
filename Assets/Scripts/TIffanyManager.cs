using Unity.VisualScripting;
using UnityEngine;

public class TIffanyManager : MonoBehaviour
{
    public bool npcAskingDone;

    public DialogueSysNew dialog1;
    public DialogueSysNew dialog2;

    public bool done1;
    public bool done2;
    public bool done3;


    private bool isIn;
    private bool isOut;
    private bool didPress;

    private bool canNoMore;

    public AcrossSceneVars vars;

    private bool addedQ;

    private void Start()
    {
        vars = AcrossSceneVars.Instance;
        dialog2.enabled = false;

        if (PlayerPrefs.HasKey("AskCityDone"))
        {
            if(PlayerPrefs.GetInt("AskCityDone") == 1)
            {
                npcAskingDone = true;
                done1 = true;
                done2 = true;
                done3 = true;
                addedQ = true;
                if (dialog1 != null)
                {
                    Destroy(dialog1);
                }
                dialog2.enabled = true;
                canNoMore = true;
            }
        }
    }

    private void Update()
    {
        vars = AcrossSceneVars.Instance;

        if (done1 && done2 && done3)
        {
            npcAskingDone = true;
        }

        if (npcAskingDone == true && addedQ == false)
        {
            Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(2);
            vars.askInfos = true;
            Destroy(dialog1);
            dialog2.enabled = true;
            addedQ = true;

            PlayerPrefs.SetInt("AskCityDone", 1);
            
        }


        if (isIn && isOut && didPress)
        {
            if (canNoMore == false)
            {
                done3 = true;
                canNoMore = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            isIn = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                didPress = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            isOut = true;
        }
    }

}
