using TMPro;
using UnityEngine;

public class itemInspect : MonoBehaviour
{
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private GameObject bigItem;
    [SerializeField] private PlayerMoveNew playerMovNew;
    [SerializeField] private bool inRange;

    private bool inspecting;

    private void Start()
    {
        inspecting = GameObject.FindGameObjectWithTag("InputSys").GetComponent<UiInputSys>().isInspecting;
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
                inspecting = true;
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
            if(Input.GetKey(KeyCode.Escape))
            {
                inspecting = false;
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
                inspecting = false;
                inRange = false;
                playerMovNew.ActivateThirdPerson();
                bigItem.SetActive(false);
            }
        }
    }
}
