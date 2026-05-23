using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    public static GameObject FindWithTagAcrossAllScenes(string tagName)
    {
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;

            foreach (GameObject root in scene.GetRootGameObjects())
            {
                GameObject found = FindInChildren(root.transform, tagName);
                if (found != null) return found;
            }
        }
        return null;
    }

    private static GameObject FindInChildren(Transform t, string tagName)
    {
        if (t.CompareTag(tagName)) return t.gameObject;

        foreach (Transform child in t)
        {
            GameObject found = FindInChildren(child, tagName);
            if (found != null) return found;
        }
        return null;
    }

    public static Utils Instance;

    void Start()
    {
        
        if (Instance == null)
        {
            
            Instance = this;
        }
        else
        {
            
            Destroy(this);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    [Header("INVENTORY ITEMS")]
    public bool hasIDCardFlint;
    public bool hasIDCardAric;
    public bool hasIDCardAlch;

    [Header("Others")]
    public TMP_Text infoTxt;


    public IEnumerator InfoTextText(string text, Color color, float time)
    {
        infoTxt.text = text;
        infoTxt.color = color;
        infoTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        infoTxt.gameObject.SetActive(false);
    }

}
