using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testDoorOpen : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Animator blackAnim;


    public string nextSceneName;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                blackAnim.SetBool("Black", true);
                doorAnim.SetTrigger("Open");
            }
        }
    }

    private void Update()
    {
        if (blackAnim.GetBool("Black"))
        {
            if (blackAnim.GetCurrentAnimatorStateInfo(0).IsName("testBlackscreen") && blackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                blackAnim.SetBool("Black", false);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

}
