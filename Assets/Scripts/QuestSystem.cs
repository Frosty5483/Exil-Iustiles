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
    }
}
