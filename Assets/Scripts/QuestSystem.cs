using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] Transform questTab;

    [SerializeField] List<GameObject> questPrefabs;
    
    public void AddQuest(int questPrefIndex)
    {
        Instantiate(questPrefabs[questPrefIndex], questTab);
    }
}
