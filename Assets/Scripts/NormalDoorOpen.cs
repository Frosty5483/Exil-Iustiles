using UnityEngine;
using System.Collections;

public class NormalDoorOpen : MonoBehaviour
{
    public Animator anim;

    private bool notPressAgain;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F) && notPressAgain == false)
            {
                
                if(anim.GetBool("Open") == false)
                {
                    anim.SetBool("Open", true);
                }
                else if(anim.GetBool("Open") == true)
                {
                    anim.SetBool("Open", false);
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
