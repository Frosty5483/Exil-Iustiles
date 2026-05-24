using UnityEngine;
using UnityEngine.UI;

public class InvSystem : MonoBehaviour
{
    public GameObject[] slots = new GameObject[15];

    public Utils utils;
    public AcrossSceneVars vars;

    private void Start()
    {
        utils = Utils.Instance;
        vars = AcrossSceneVars.Instance;

        if (PlayerPrefs.HasKey("SeraAddedBookQuest"))
        {
            if(PlayerPrefs.GetInt("SeraAddedBookQuest") == 1)
            {
                Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(6);
            }
        }
        if (PlayerPrefs.HasKey("AlchDone1"))
        {
            if (PlayerPrefs.GetInt("AlchDone1") == 1)
            {
                Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(3);
            }
        }
        if (PlayerPrefs.HasKey("AlchDone2"))
        {
            if (PlayerPrefs.GetInt("AlchDone2") == 1)
            {
                Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(4);
            }
        }
        if (PlayerPrefs.HasKey("AlchDone3"))
        {
            if (PlayerPrefs.GetInt("AlchDone3") == 1)
            {
                Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(5);
            }
        }
        if (PlayerPrefs.HasKey("FirstQuest"))
        {
            if (PlayerPrefs.GetInt("FirstQuest") == 1)
            {
                Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(0);
            }
        }
        if (PlayerPrefs.HasKey("AddedQuestToAsk"))
        {
            if (PlayerPrefs.GetInt("AddedQuestToAsk") == 1)
            {
                Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(1);
            }
        }
        if (PlayerPrefs.HasKey("AskCityDone"))
        {
            if (PlayerPrefs.GetInt("AskCityDone") == 1)
            {
                Utils.FindWithTagAcrossAllScenes("qSys").GetComponent<QuestSystem>().AddQuest(2);
            }
        }
    }

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
