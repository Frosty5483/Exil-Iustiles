using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class KundenBuchManag : MonoBehaviour
{

    public SeraManag sera;

    public UiInputSys inputSys;

    public GameObject bigItem;

    private void Start()
    {
        vars = AcrossSceneVars.Instance;
        bigItem.SetActive(false);
        inputSys = GameObject.FindGameObjectWithTag("InputSys").GetComponent<UiInputSys>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            if(Input.GetKey(KeyCode.F) && sera.buchActive == true)
            {
                ViewItem();
            }
        }
    }

    public void ViewItem()
    {
        inputSys.viewOpen = true;

        inputSys.playerMovNew.animator.enabled = false;
        inputSys.playerMovNew.enabled = false;
        inputSys.tpsCam.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        bigItem.SetActive(true);

    }

    public void ExitViewItem()
    {
        inputSys.viewOpen = false;

        inputSys.playerMovNew.animator.enabled = true;
        inputSys.playerMovNew.enabled = true;
        inputSys.tpsCam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        bigItem.SetActive(false);
    }

    //UI ->

    public GameObject[] seiten;

    private bool gotFlint;

    public AudioSource aS;

    public AudioClip clip;

    public AcrossSceneVars vars;

    public void GotFlint()
    {
        if(gotFlint == false)
        {
            aS.clip = clip;
            aS.Play();
            gotFlint = true;
            vars.searchInSera = true;
        }
        
    }


    public void Forward()
    {
        for (int i = 0; i < seiten.Length; i++)
        {
            if (seiten[i].active == true && i != seiten.Length - 1)
            {
                Debug.Log("geradige seite: " + i);
                Debug.Log("neue seite: " + (i + 1));
                seiten[i + 1].SetActive(true);
                seiten[i].SetActive(false);
                break;
            }
            
        }
    }

    public void Backward()
    {
        for (int i = 0; i < seiten.Length; i++)
        {
            if (seiten[i].active == true && i != 0)
            {
                Debug.Log("geradige seite: " + i);
                Debug.Log("neue seite: " + (i - 1));
                seiten[i - 1].SetActive(true);
                seiten[i].SetActive(false);
                break;
            }
           
        }
    }

}
