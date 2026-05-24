using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] Transform questTab;

    [SerializeField] List<GameObject> questPrefabs;

    public AcrossSceneVars vars;

    private void Start()
    {
        vars = AcrossSceneVars.Instance;
    }

    public void AddQuest(int questPrefIndex)
    {
        Instantiate(questPrefabs[questPrefIndex], questTab);
    }

    private void Update()
    {
        if(vars.findFlintBur == true)
        {
            Utils.FindWithTagAcrossAllScenes("q1").GetComponentInChildren<Toggle>().isOn = true;
        }

        if(vars.askInfos == true)
        {
            Utils.FindWithTagAcrossAllScenes("q2").GetComponentInChildren<Toggle>().isOn = true;
        }

        if(vars.askAlch == true)
        {
            Utils.FindWithTagAcrossAllScenes("q3").GetComponentInChildren<Toggle>().isOn = true;
        }

        if(vars.searchAlchRoom == true)
        {
            Utils.FindWithTagAcrossAllScenes("q4").GetComponentInChildren<Toggle>().isOn = true;
        }

        if(vars.giveIdBack == true)
        {
            Utils.FindWithTagAcrossAllScenes("q5").GetComponentInChildren<Toggle>().isOn = true;
        }
    }
}
