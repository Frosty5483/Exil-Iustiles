using System.Collections;
using UnityEngine;

public class DoorNeedItem : MonoBehaviour
{

    public Utils utils;

    public AcrossSceneVars vars;

    public QuestSystem qSys;

    private bool notPressAgain;

    public Color infoTxtColor;

    private bool noNewQuest;



    private void Start()
    {
        utils = Utils.Instance;

        vars = AcrossSceneVars.Instance;

        qSys = Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>();

    }

    private void Update()
    {
        utils = Utils.Instance;

        vars = AcrossSceneVars.Instance;

        qSys = Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>();
    }
    private void OnTriggerStay(Collider collision)
    {

        if(collision.transform.tag == "Player")
        {
            if(Input.GetKey(KeyCode.F) && notPressAgain == false)
            {
                vars.findFlintBur = true;
                
                if(noNewQuest == false)
                {
                    qSys.AddQuest(1);
                    noNewQuest = true;
                }
                

                StartCoroutine(cor());

                if (utils.hasIDCardFlint && utils.hasIDCardAric)
                {
                    gameObject.GetComponentInParent<Animator>().SetBool("Open", true);
                }
                if ((utils.hasIDCardFlint && utils.hasIDCardAric == false) || (utils.hasIDCardAric && utils.hasIDCardFlint == false))
                {
                    //nicht öffnen -> sagen es fehlt eine karte
                    StartCoroutine(utils.InfoTextText("Dir fehlt eine ID-Karte", infoTxtColor, 1f));
                }
                if(utils.hasIDCardFlint == false && utils.hasIDCardAric == false)
                {
                    // nicht öffnen -> sagen es fehlen beide karten
                    StartCoroutine(utils.InfoTextText("Dir fehlen zwei ID-Karten", infoTxtColor, 1f));
                }
            }
        }
    }

    private IEnumerator cor()
    {
        notPressAgain = true;
        yield return new WaitForSeconds(0.2f);
        notPressAgain = false;
    }
}
