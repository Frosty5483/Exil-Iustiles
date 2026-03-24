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

    public List<Cutscenes> cutsceneList;

    int id = 0;

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

        }
    }
}
