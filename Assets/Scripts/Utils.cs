using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    public static GameObject FindWithTagAcrossAllScenes(string tagName)
    {
        // Search normal loaded scenes
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

        // Also search the DontDestroyOnLoad scene via the singleton's scene
        if (Instance != null)
        {
            foreach (GameObject root in Instance.gameObject.scene.GetRootGameObjects())
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
            
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("IDFlint") == 1)
        {
            hasIDCardFlint = true;
        }
        if (hasIDCardFlint)
        {
            PlayerPrefs.SetInt("IDFlint", 1);
        }
        if (PlayerPrefs.GetInt("IDAric") == 1)
        {
            hasIDCardAric = true;
        }
        if (hasIDCardAric)
        {
            PlayerPrefs.SetInt("IDAric", 1);

        }
        if (PlayerPrefs.GetInt("IDAlch") == 1)
        {
            hasIDCardAlch = true;
        }
        if (hasIDCardAlch)
        {
            PlayerPrefs.SetInt("IDAlch", 1);

        }

        if(PlayerPrefs.GetInt("HasWatched") == 1)
        {
            hasWatchedCutScenes = true;
        }

        if (hasWatchedCutScenes)
        {
            PlayerPrefs.SetInt("HasWatched", 1);
        }
    }

    [Header("INVENTORY ITEMS")]
    public bool hasIDCardFlint;
    public bool hasIDCardAric;
    public bool hasIDCardAlch;

    [Header("Others")]
    public TMP_Text infoTxt;

    public bool hasWatchedCutScenes;

    public enum UtilsBoolField
    {
        hasIDCardFlint,
        hasIDCardAric,
        hasIDCardAlch
    }

    public void SetBool(UtilsBoolField field, bool value)
    {
        switch (field)
        {
            case UtilsBoolField.hasIDCardFlint: hasIDCardFlint = value; break;
            case UtilsBoolField.hasIDCardAric: hasIDCardAric = value; break;
            case UtilsBoolField.hasIDCardAlch: hasIDCardAlch = value; break;
        }
    }

    public IEnumerator InfoTextText(string text, Color color, float time)
    {
        infoTxt.text = text;
        infoTxt.color = color;
        infoTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        infoTxt.gameObject.SetActive(false);
    }

}
