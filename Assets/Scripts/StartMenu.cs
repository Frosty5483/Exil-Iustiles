using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject options;

    public GameObject startMenu;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Valentina");
    }

    public void Options()
    {
        options.SetActive(true);
        startMenu.SetActive(false);
    }

    private void Update()
    {
        if(options.active == true)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                options.SetActive (false);
                startMenu.SetActive (true);
            }
        }
    }

    public void Back()
    {
        options.SetActive(false);
        startMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
