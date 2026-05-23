using TMPro;
using UnityEngine;

public class itemInspect : MonoBehaviour
{
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private GameObject bigItem;
    [SerializeField] private PlayerMoveNew playerMovNew;
    [SerializeField] private bool inRange;

    public InvSystem invSys;


    private void Start()
    {
        hintText.text = "";
        bigItem.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hintText.text = "Press [F] to inspect";

            if (Input.GetKey(KeyCode.F))
            {
                inRange = true;
                playerMovNew.ActivateFirstPerson();
                bigItem.SetActive(true);
                
            }
        }
    }

    private void Update()
    {
        if (bigItem.active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerMovNew.enabled = false;
            if(Input.GetKey(KeyCode.Space)||Input.GetMouseButton(0))
            {
                inRange = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                playerMovNew.enabled = true;
                bigItem.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hintText.text = "";
            if(inRange == true)
            {
                inRange = false;
                playerMovNew.ActivateThirdPerson();
                bigItem.SetActive(false);
            }
        }
    }
}
