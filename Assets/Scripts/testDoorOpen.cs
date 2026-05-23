using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testDoorOpen : MonoBehaviour
{
    [SerializeField] private Animator blackAnim;

    public GameObject uI;

    public GameObject player;

    private bool now;

    public AcrossSceneVars vars;


    public string nextSceneName;

    private void Start()
    {
        vars = AcrossSceneVars.Instance;
        now = false;
        player = Utils.FindWithTagAcrossAllScenes("Player");
        uI = Utils.FindWithTagAcrossAllScenes("UIcanv");
        blackAnim = uI.GetComponentInChildren<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(nextSceneName != "Bar" && nextSceneName != "Alch")
            {
                if (Input.GetKey(KeyCode.F))
                {
                    blackAnim.SetBool("Black", true);
                    now = true;
                }
            }
            else if(nextSceneName == "Bar" && vars.findFlintBur == true)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    blackAnim.SetBool("Black", true);
                    now = true;
                }
            }
            else if(nextSceneName == "Alch" && vars.askInfos == true)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    blackAnim.SetBool("Black", true);
                    now = true;
                }
            }
            
        }
    }

    private void Update()
    {
        player = Utils.FindWithTagAcrossAllScenes("Player");
        uI = Utils.FindWithTagAcrossAllScenes("UIcanv");
        blackAnim = uI.GetComponentInChildren<Animator>();
        if (blackAnim.GetBool("Black") && now == true)
        {
            if (blackAnim.GetCurrentAnimatorStateInfo(0).IsName("testBlackscreen") && blackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                blackAnim.SetBool("Black", false);
                if(SceneManager.GetActiveScene().name == "Valentina")
                {
                    PlayerPrefs.SetFloat("playerX", player.transform.position.x);
                    PlayerPrefs.SetFloat("playerY", player.transform.position.y);
                    PlayerPrefs.SetFloat("playerZ", player.transform.position.z);


                    PlayerPrefs.SetFloat("playerXR", player.transform.rotation.x);
                    PlayerPrefs.SetFloat("playerYR", player.transform.rotation.y);
                    PlayerPrefs.SetFloat("playerZR", player.transform.rotation.z);
                }
                


                SceneManager.LoadSceneAsync(nextSceneName);
            }
        }
    }

}
