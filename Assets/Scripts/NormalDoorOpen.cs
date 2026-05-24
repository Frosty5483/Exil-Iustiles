using UnityEngine;
using System.Collections;

public class NormalDoorOpen : MonoBehaviour
{

    private bool notPressAgain;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F) && notPressAgain == false)
            {
                
                if(gameObject.GetComponentInChildren<Animator>().GetBool("Open") == false)
                {
                    gameObject.GetComponentInChildren<Animator>().SetBool("Open", true);
                }
                else if(gameObject.GetComponentInChildren<Animator>().GetBool("Open") == true)
                {
                    gameObject.GetComponentInChildren<Animator>().SetBool("Open", false);
                }

                StartCoroutine(cor());

            }
        }
    }

    private IEnumerator cor()
    {
        notPressAgain = true;
        yield return new WaitForSeconds(0.2f);
        notPressAgain = false;
    }
}
