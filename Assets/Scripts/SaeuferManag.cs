using UnityEngine;

public class SaeuferManag : MonoBehaviour
{

    private bool isIn;
    private bool isOut;
    private bool didPress;

    private bool canNoMore;

    public TIffanyManager tif;

    private void Start()
    {
        if (PlayerPrefs.HasKey("AskCityDone"))
        {
            if(PlayerPrefs.GetInt("AskCityDone") == 1)
            {
                canNoMore = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            isIn = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                didPress = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            isOut = true;
        }
    }

    private void Update()
    {
        if(isIn && isOut && didPress)
        {
            if(tif.done1 == false && canNoMore == false)
            {
                tif.done1 = true;
                canNoMore = true;
            }
            else if(tif.done2 == false && tif.done1 == true && canNoMore == false)
            {
                tif.done2 = true;
                canNoMore = true;
            }
        }
    }

}
