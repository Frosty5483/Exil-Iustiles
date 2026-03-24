using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class pickUpObj : MonoBehaviour
{
    [SerializeField] private TMP_Text popTxt;

    [SerializeField] private Animator playerAnim;

    [SerializeField] CoinsSystem coinSys;

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
                hiddenSafe = false;
                popTxt.text = "";
                gameObject.SetActive(false);
                if(playerAnim.gameObject.GetComponent<PlayerMoveNew>().inFPS == false)
                {
                    playerAnim.SetTrigger("pickUp");
                    coinSys.AddCoins(10);
                }
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

