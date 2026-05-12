using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cutscenes
{
    [Tooltip("Drag your image gameobject here")]
    public GameObject scene;

    [Tooltip("Duration in seconds for this image")]
    public float duration;
}

public class CutSceneManag : MonoBehaviour
{
    [SerializeField] UiInputSys inputSys;

    [SerializeField] LayerMask layerToShow;

    [SerializeField] QuestSystem qSys;

    [SerializeField] GameObject skipButton;

    public List<Cutscenes> cutsceneList;

    int id = 0;

    private void Start()
    {
        ShowCutScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowCutScene();
        }
    }

    public void ShowCutScene()
    {
        StartCoroutine(waitForNextCutScene(id));
    }

    IEnumerator waitForNextCutScene(int sceneI)
    {
        if(sceneI != cutsceneList.Count)
        {
            inputSys.PauseTime();

            if ((sceneI - 1) >= 0)
            {
                cutsceneList[sceneI - 1].scene.gameObject.SetActive(false);
            }

            cutsceneList[sceneI].scene.gameObject.SetActive(true);

            Debug.Log("this is a new cut scene");

            yield return new WaitForSeconds(cutsceneList[sceneI].duration);

            id++;

            ShowCutScene();
        }
        else if (sceneI == cutsceneList.Count)
        {
            cutsceneList[sceneI - 1].scene.gameObject.SetActive(false);
            skipButton.SetActive(false);
            inputSys.ResumeTime();
            qSys.AddQuest(0);
        }
    }

    public void SkipCutScenes()
    {
        StopAllCoroutines();

        bool pressed = false;
        for (int i = 0; i < cutsceneList.Count; i++)
        {
            cutsceneList[i].scene.gameObject.SetActive(false);
            skipButton.SetActive(false);
            inputSys.ResumeTime();
            if(pressed == false)
            {
                qSys.AddQuest(0);
            }
            pressed = true;
        }
    }
}
