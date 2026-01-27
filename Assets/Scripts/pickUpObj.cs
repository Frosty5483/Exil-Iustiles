using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class pickUpObj : MonoBehaviour
{
    [SerializeField] private TMP_Text popTxt;

    [SerializeField] private Animator playerAnim;

    private bool hiddenSafe;

    private void Start()
    {
        popTxt.text = "";
        hiddenSafe = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popTxt.text = "Press [F] to pick up";
            if (!hiddenSafe && Input.GetKey(KeyCode.F))
            {
                hiddenSafe = true;
                playerAnim.SetTrigger("pickUp");
                hiddenSafe = false;
                popTxt.text = "";
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popTxt.text = "";
        }
    }

}

